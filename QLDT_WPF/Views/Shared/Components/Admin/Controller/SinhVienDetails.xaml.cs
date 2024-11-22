using QLDT_WPF.Dto;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;
using System.Windows.Media;
using QLDT_WPF.Repositories;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using QLDT_WPF.Views.Shared.Components.Admin.Help;
using Syncfusion.XlsIO;


namespace QLDT_WPF.Views.Components
{
    public partial class SinhVienDetails : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SinhVienDetails), new PropertyMetadata(null));

        private string idSinhVien;
        private SinhVienDto sinhVienDto;

        private SinhVienRepository sinhVienRepository;
        private DiemRepository diemRepository;
        private CalendarRepository calendarRepository;
        private IdentityRepository identityRepository;

        public ObservableCollection<ScheduleAppointment> Appointments { get; set; }
        public ObservableCollection<DiemDto> diem_collection { get; set; }

        private string parent;

        private string constSVD = "SinhVienDetails";

        public SinhVienDetails(string id, string parent)
        {
            InitializeComponent();

            idSinhVien = id;

            sinhVienRepository = new SinhVienRepository();
            diemRepository = new DiemRepository();
            calendarRepository = new CalendarRepository();
            identityRepository = new IdentityRepository();

            Appointments = new ObservableCollection<ScheduleAppointment>();
            diem_collection = new ObservableCollection<DiemDto>();

            this.parent = parent;

            // Load data asynchron
            Loaded += async (s, e) =>
            {
                if (TargetContentArea == null)
                {
                    var parentWindow = FindParent<Window>(this); // Tìm parent window
                    if (parentWindow != null)
                    {
                        var contentArea = parentWindow.FindName("ContentArea") as ContentControl; // Tìm ContentArea
                        if (contentArea != null)
                        {
                            TargetContentArea = contentArea;
                        }
                        else
                        {
                            TargetContentArea = new ContentControl();
                        }
                    }
                    else
                    {
                        TargetContentArea = new ContentControl();
                    }
                }
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            var req_sv = await sinhVienRepository.GetById(idSinhVien);
            if (req_sv.Status == false)
            {
                MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            sinhVienDto = req_sv.Data;

            // Set avatar
            await Set_Avatar();

            // Set data in4 sinh vien
            await Set_Data_In4_SV();

            // Load Calendar
            await Load_Calendar();

            // Load Point
            await Load_Point();
        }

        // Set avatar
        private async Task Set_Avatar()
        {
            var req_avt = await identityRepository.GetAvatar(idSinhVien);
            if (req_avt.Status == false)
            {
                MessageBox.Show("Không tìm thấy ảnh đại diện của sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] imageBytes = req_avt.Data;
            if (imageBytes == null || imageBytes.Length == 0)
            {
                MessageBox.Show("Dữ liệu ảnh không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Convert byte array to an image
            using (var stream = new System.IO.MemoryStream(imageBytes))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Load image into memory
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                // Assign the bitmap to the ImageBrush
                if (AvatarImageControl.Fill is ImageBrush imageBrush)
                {
                    imageBrush.ImageSource = bitmap;
                }
                else
                {
                    // If the Fill is not already an ImageBrush, create one
                    AvatarImageControl.Fill = new ImageBrush(bitmap) { Stretch = Stretch.UniformToFill };
                }
            }
        }

        // Set data in4 sinh vien
        private async Task Set_Data_In4_SV()
        {
            txtStudentID.Text = sinhVienDto.IdSinhVien;
            txtFullName.Text = sinhVienDto.HoTen;
            txtClass.Text = sinhVienDto.Lop;
            txtBirthDate.Text = sinhVienDto.NgaySinh?.ToString("dd/MM/yyyy") ?? string.Empty;
            txtAddress.Text = sinhVienDto.DiaChi;
            txtChuongTrinhHoc.Text = sinhVienDto.TenChuongTrinhHoc;
            txtKhoa.Text = sinhVienDto.TenKhoa;
        }

        // Load Calendar
        private async Task Load_Calendar()
        {
            var req_calendar = await calendarRepository.GetCalendarSinhVien(idSinhVien);
            if (req_calendar.Status == false)
            {
                MessageBox.Show("Không tìm thấy lịch học của sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Appointments.Clear();
            foreach (var it in req_calendar.Data)
            {
                Appointments.Add(new ScheduleAppointment
                {
                    Subject = it.Title,
                    StartTime = it.Start ?? DateTime.MinValue,
                    EndTime = it.End ?? DateTime.MinValue,
                    Location = it.Location,
                    Notes = it.Description
                });
            }

            // Set the Appointments property of the CalendarComponent
            calendar.Appointments = Appointments;
        }

        // Load Point
        private async Task Load_Point()
        {
            var req_point = await diemRepository.GetByIdSinhVien(idSinhVien);
            if (req_point.Status == false)
            {
                MessageBox.Show("Không tìm thấy điểm của sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            diem_collection.Clear();
            foreach (var it in req_point.Data)
            {
                diem_collection.Add(new DiemDto
                {
                    IdDiem = it.IdDiem,
                    IdSinhVien = it.IdSinhVien,
                    IdMon = it.IdMon,
                    IdLopHocPhan = it.IdLopHocPhan,

                    DiemKetThuc = it.DiemKetThuc,
                    DiemQuaTrinh = it.DiemQuaTrinh,
                    DiemTongKet = it.DiemTongKet,
                    TenSinhVien = it.TenSinhVien,
                    TenMonHoc = it.TenMonHoc,
                    TenLopHocPhan = it.TenLopHocPhan,
                    LanHoc = it.LanHoc,

                    TrangThai = it.DiemTongKet >= 4 ? "Qua Môn" : "Học Lại",
                });
            }

            DataGrid.ItemsSource = diem_collection;
        }

        // Find parent window
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                Object _parent = Parent_Find.Get_Template(parent,idSinhVien,parent);
                TargetContentArea.Content = _parent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // handle edit profile button
        private void Edit_SV_Button_Click(object sender, RoutedEventArgs e)
        {
            // Redirect 
            // Redirect to MonHocDetails
            if (TargetContentArea != null)
            {
                TargetContentArea.Content =
                    new QLDT_WPF.Views.Components.SinhVienEditWindow(sinhVienDto);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // handle edit point button - Button tag binding DiemDto
        private void Edit_Point_Student(object sender, RoutedEventArgs e)
        {
            // get data in tag
            var diem = (sender as Button)?.Tag as DiemDto;
            if (diem == null)
            {
                MessageBox.Show("Không tìm thấy điểm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Open the DiemEditWindow
            var editWindow = new QLDT_WPF.Views.Shared.Components.Admin.View.EditDiem(diem);
            editWindow.ShowDialog();

            // Refresh data
            Load_Point();
        }

        // export point sinh vien 
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            if (diem_collection == null || diem_collection.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất ra Excel", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                // Tạo workbook và worksheet
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                // Đặt tiêu đề cho các cột trong worksheet
                worksheet[1, 1].Text = "ID Điểm";
                worksheet[1, 2].Text = "ID Sinh Viên";
                worksheet[1, 3].Text = "ID Môn";
                worksheet[1, 4].Text = "ID Lớp Học Phần";
                worksheet[1, 5].Text = "Tên Sinh Viên";
                worksheet[1, 6].Text = "Tên Môn Học";
                worksheet[1, 7].Text = "Tên Lớp Học Phần";
                worksheet[1, 8].Text = "Lần Học";
                worksheet[1, 9].Text = "Điểm Quá Trình";
                worksheet[1, 10].Text = "Điểm Kết Thúc";
                worksheet[1, 11].Text = "Điểm Tổng Kết";
                worksheet[1, 12].Text = "Trạng Thái";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var diem in diem_collection)
                {
                    worksheet[row, 1].Text = diem.IdDiem ?? "N/A";
                    worksheet[row, 2].Text = diem.IdSinhVien ?? "N/A";
                    worksheet[row, 3].Text = diem.IdMon ?? "N/A";
                    worksheet[row, 4].Text = diem.IdLopHocPhan ?? "N/A";
                    worksheet[row, 5].Text = diem.TenSinhVien ?? "N/A";
                    worksheet[row, 6].Text = diem.TenMonHoc ?? "N/A";
                    worksheet[row, 7].Text = diem.TenLopHocPhan ?? "N/A";
                    worksheet[row, 8].Text = diem.LanHoc == null ? "N/A" : diem.LanHoc.ToString();
                    worksheet[row, 9].Text = diem.DiemQuaTrinh == null ? "N/A" : diem.DiemQuaTrinh.ToString();
                    worksheet[row, 10].Text = diem.DiemKetThuc == null ? "N/A" : diem.DiemKetThuc.ToString();
                    worksheet[row, 11].Text = diem.DiemTongKet == null ? "N/A" : diem.DiemTongKet.ToString();
                    worksheet[row, 12].Text = diem.TrangThai ?? "N/A";

                    row++;
                }

                 // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachLopHocPhan.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // handle search ponint 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = txtTimKiem.Text;
            if (keyword == null || keyword.Length == 0)
            {
                DataGrid.ItemsSource = diem_collection;
            }
            else
            {
                DataGrid.ItemsSource = diem_collection.Where(x =>
                    x.TenMonHoc.ToLower().Contains(keyword.ToLower()) ||
                    x.TenLopHocPhan.ToLower().Contains(keyword.ToLower()) ||
                    x.TrangThai.ToLower().Contains(keyword.ToLower())
                );
            }
        }

        // show detail mon hoc - TextBlock tag binding IdMon
        private void ChiTietMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // get data id mon hoc in tag
            var idMon = (sender as TextBlock)?.Tag as string;
            if (idMon == null)
            {
                MessageBox.Show("Không tìm thấy mã môn học!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Redirect to MonHocDetails
            if (TargetContentArea != null)
            {
                TargetContentArea.Content =
                    new QLDT_WPF.Views.Shared.Components.Admin.View.SubjectDetails(idMon,constSVD);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // show detail LopHocPhan - TextBlock tag binding IdLopHocPhan
        private void ChiTietLopHocPhan_Click(object sender, RoutedEventArgs e)
        {
            // get data id lop hoc phan in tag
            var idLopHocPhan = (sender as TextBlock)?.Tag as string;
            if (idLopHocPhan == null)
            {
                MessageBox.Show("Không tìm thấy mã lớp học phần!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Redirect to LopHocPhanDetails
            if (TargetContentArea != null)
            {
                TargetContentArea.Content =
                    new QLDT_WPF.Views.Components.LopHocPhanDetails(idLopHocPhan,constSVD,null);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
