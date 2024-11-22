using Microsoft.Win32;
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.Views.Components;
using QLDT_WPF.Views.Shared.Components.GiaoVien.Helper;
using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace QLDT_WPF.Views.Shared.Components.GiaoVien.View
{
    /// <summary>
    /// Interaction logic for LopHocPhanDetailsView.xaml
    /// </summary>
    public partial class LopHocPhanDetailsView : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(LopHocPhanDetails), new PropertyMetadata(null));

        // Variables
        private string idLopHocPhan;
        private LopHocPhanDto lopHocPhanDto;
        private MonHocDto monHocDto;

        private LopHocPhanRepository lopHocPhanRepository;
        private MonHocRepository monHocRepository;
        private CalendarRepository calendarRepository;
        private DiemRepository diemRepository;

        public ObservableCollection<ScheduleAppointment> Appointments { get; set; }
        public ObservableCollection<CalendarDto> calendar_collections;
        public ObservableCollection<DiemDto> diem_collections;

        private string parent;
        private string constLHPD = "LophocphanDetails";
        private string idparent;

        // Constructor
        public LopHocPhanDetailsView(string id)
        {
            InitializeComponent();

            // inir repository
            lopHocPhanRepository = new LopHocPhanRepository();
            monHocRepository = new MonHocRepository();
            calendarRepository = new CalendarRepository();
            diemRepository = new DiemRepository();


            //
            Appointments = new ObservableCollection<ScheduleAppointment>();
            calendar_collections = new ObservableCollection<CalendarDto>();
            diem_collections = new ObservableCollection<DiemDto>();

            // set variables in constructor
            idLopHocPhan = id;
            this.idparent = idparent;

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

                await InitAysnc();
            };
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        private async Task InitAysnc()
        {
            var req_lhp = await lopHocPhanRepository.GetById(idLopHocPhan);
            if (req_lhp.Status == false)
            {
                MessageBox.Show(req_lhp.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            lopHocPhanDto = req_lhp.Data;

            var req_mh = await monHocRepository.GetById(lopHocPhanDto.IdMonHoc);
            if (req_mh.Status == false)
            {
                MessageBox.Show(req_mh.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            monHocDto = req_mh.Data;

            // Set title - title_lop_hoc_phan
            title_lop_hoc_phan.Text = $"Chi tiết lớp học phần {lopHocPhanDto.TenLopHocPhan}";

            // Set - description_lop_hoc_phan
            description_lop_hoc_phan.Text = $"Môn học: {monHocDto.TenMonHoc} - Số tín chỉ: {monHocDto.SoTinChi} - Số tiết: {monHocDto.SoTietHoc} - Bắt Đầu: {lopHocPhanDto.ThoiGianBatDau} - Kết thúc: {lopHocPhanDto.ThoiGianKetThuc}";

            // Load Calendar - calendar_lop_hoc_phan
            await Load_Calendar();

            // Load diem sinh vien lop hoc phan - ScoreDataGrid
            await Load_ScoreDataGrid();
        }

        private async Task Load_Calendar()
        {
            var req_calendar =
                await calendarRepository.GetCalendarLopHocPhan(idLopHocPhan);
            if (req_calendar.Status == false)
            {
                MessageBox.Show(req_calendar.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<CalendarDto> dtoList = req_calendar.Data;
            Appointments.Clear();
            calendar_collections.Clear();
            // Convert to ScheduleAppointment
            foreach (var dto in dtoList)
            {
                // Đã Kết Thúc, Đang Diễn Ra, Sắp Diễn Ra - So Với Thời Gian Hiện Tại
                string StatusMessage = "";
                if (dto.End < DateTime.Now)
                {
                    StatusMessage = "Đã Kết Thúc";
                }
                else if (dto.Start < DateTime.Now && dto.End > DateTime.Now)
                {
                    StatusMessage = "Đang Diễn Ra";
                }
                else if (dto.Start > DateTime.Now)
                {
                    StatusMessage = "Sắp Diễn Ra";
                }

                calendar_collections.Add(new CalendarDto
                {
                    Id = dto.Id,
                    Title = dto.Title,
                    Description = dto.Description,
                    GroupId = dto.GroupId,
                    Start = dto.Start,
                    End = dto.End,
                    DisplayEventTime = dto.DisplayEventTime,
                    Location = dto.Location,
                    StatusMessage = StatusMessage,
                });

                Appointments.Add(new ScheduleAppointment
                {
                    Subject = dto.Title,
                    StartTime = dto.Start ?? DateTime.MinValue,
                    EndTime = dto.End ?? DateTime.MinValue,
                    Location = dto.Location,
                    Notes = dto.Description
                });
            }

            DataGrid_ThoiGian_LopHocPhan.ItemsSource = calendar_collections;
        }

        private async Task Load_ScoreDataGrid()
        {
            if(lopHocPhanDto.TrangThaiNhapDiem == false)
            {
                NhapDiemBangFile.IsEnabled = false;
                TrangThaiNhapDiem.Text = "Nhập điểm không khả dụng";
            }
            else
            {
                NhapDiemBangFile.IsEnabled = true;
                TrangThaiNhapDiem.Text = "Nhập điểm khả dụng";
            }
            var req_diem = await diemRepository
                .GetDiemByIdLopHocPhan(idLopHocPhan);
            if (req_diem.Status == false)
            {
                MessageBox.Show(req_diem.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            diem_collections.Clear();
            foreach (var dto in req_diem.Data)
            {
                diem_collections.Add(new DiemDto
                {
                    IdDiem = dto.IdDiem,
                    IdLopHocPhan = dto.IdLopHocPhan,
                    IdSinhVien = dto.IdSinhVien,
                    IdMon = dto.IdMon,
                    DiemQuaTrinh = dto.DiemQuaTrinh,
                    DiemKetThuc = dto.DiemKetThuc,
                    DiemTongKet = dto.DiemTongKet,
                    LanHoc = dto.LanHoc,
                    TenMonHoc = dto.TenMonHoc,
                    TenLopHocPhan = dto.TenLopHocPhan,
                    TenSinhVien = dto.TenSinhVien,
                    TrangThai = dto.DiemTongKet >= 4 ? "Qua Môn" : "Học Lại"
                });
            }
            ScoreDataGrid.ItemsSource = diem_collections;
        }

        // Show detail of chuong trinh hoc click - tag : id chuong trinh hoc
        private void ChiTietChuongTrinhHoc_Click(object sender, RoutedEventArgs e)
        {
            var id = (sender as Button)?.Tag.ToString() ?? "";
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Không tìm thấy id chương trình học!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var detail = new QLDT_WPF.Views.Shared.Components.Admin.View.ChuongTrinhHocDetails(id, constLHPD);
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = detail;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // btn lick sua diem - tag : binding diemDto
        private void SuaDiem_Click(object sender, RoutedEventArgs e)
        {
            var diem = (sender as Button)?.Tag as DiemDto;
            if (diem == null)
            {
                MessageBox.Show("Không tìm thấy thông tin điểm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Open window SuaDiem
            var window = new QLDT_WPF.Views.Shared.Components.Admin.View.EditDiem(diem);
            window.ShowDialog();
            // refresh data
            Load_ScoreDataGrid();
        }

        // handle click ExportToExcel_Point_SinhVien_LopHocPhan
        private void ExportToExcel_Point_SinhVien_LopHocPhan(object sender, RoutedEventArgs e)
        {
            if (diem_collections.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet[1, 1].Text = "STT";
                worksheet[1, 2].Text = "Tên Sinh Viên";
                worksheet[1, 3].Text = "Điểm Quá Trình";
                worksheet[1, 4].Text = "Điểm Kết Thúc";
                worksheet[1, 5].Text = "Điểm Tổng Kết";
                worksheet[1, 6].Text = "Lần Học";
                worksheet[1, 7].Text = "Trạng Thái";

                int row = 2;

                foreach (var sv in diem_collections)
                {
                    worksheet[row, 1].Number = row - 1;
                    worksheet[row, 2].Text = sv.TenSinhVien;
                    worksheet[row, 3].Text = sv.DiemQuaTrinh.ToString() ?? "0";
                    worksheet[row, 4].Text = sv.DiemKetThuc.ToString() ?? "0";
                    worksheet[row, 5].Text = sv.DiemTongKet.ToString() ?? "0";
                    worksheet[row, 6].Text = sv.LanHoc.ToString() ?? "0";
                    worksheet[row, 7].Text = sv.TrangThai ?? "";

                    row++;
                }

                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachMonHoc.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }

        // handle text change txtTimKiem_TextChanged_Point_SinhVien_LopHocPhan
        private void txtTimKiem_TextChanged_Point_SinhVien_LopHocPhan(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem_Point == null)
            {
                ScoreDataGrid.ItemsSource = diem_collections;
            }
            else
            {
                var filter = txtTimKiem_Point.Text.ToLower();
                var result = diem_collections.Where(x => x.TenSinhVien.ToLower().Contains(filter)).ToList();
                ScoreDataGrid.ItemsSource = result;
            }
        }

        // handle click Edit_ThoiGian_LopHocPhan
        private void Edit_ThoiGian_LopHocPhan(object sender, RoutedEventArgs e)
        {
            var tg = (sender as Button)?.Tag as CalendarDto;
            if (tg == null)
            {
                MessageBox.Show("Không tìm thấy thông tin thời gian!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Open window SuaThoiGian
            var window = new EditThoiGianLopHocPhan(tg, idLopHocPhan);
            window.ShowDialog();

            // refresh data
            Load_Calendar();
        }

        // handle click ExportToExcel_ThoiGian_LopHocPhan
        private void ExportToExcel_ThoiGian_LopHocPhan(object sender, RoutedEventArgs e)
        {
            if (calendar_collections.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet[1, 1].Text = "STT";
                worksheet[1, 2].Text = "Tiêu Đề";
                worksheet[1, 3].Text = "Mô Tả";
                worksheet[1, 4].Text = "Thời Gian Bắt Đầu";
                worksheet[1, 5].Text = "Thời Gian Kết Thúc";
                worksheet[1, 6].Text = "Địa Điểm";
                worksheet[1, 7].Text = "Trạng Thái";

                int row = 2;

                foreach (var tg in calendar_collections)
                {
                    worksheet[row, 1].Number = row - 1;
                    worksheet[row, 2].Text = tg.Title;
                    worksheet[row, 3].Text = tg.Description;
                    worksheet[row, 4].DateTime = tg.Start ?? DateTime.MinValue;
                    worksheet[row, 5].DateTime = tg.End ?? DateTime.MinValue;
                    worksheet[row, 6].Text = tg.Location;
                    worksheet[row, 7].Text = tg.StatusMessage;

                    row++;
                }

                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachThoiGian.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        // handle text change txtTimKiem_TextChanged_ThoiGian_LopHocPhan
        private void txtTimKiem_TextChanged_ThoiGian_LopHocPhan(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem_ThoiGian_LopHocPhan == null)
            {
                DataGrid_ThoiGian_LopHocPhan.ItemsSource = calendar_collections;
            }
            else
            {
                var filter = txtTimKiem_ThoiGian_LopHocPhan.Text.ToLower();
                var result = calendar_collections.Where(x => x.StatusMessage.ToLower().Contains(filter)).ToList();
                DataGrid_ThoiGian_LopHocPhan.ItemsSource = result;
            }
        }

        // handle click UploadDiemBangFile
        private void UploadDiemBangFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv"; // Chỉ cho phép chọn file CSV

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    // Đọc file CSV và xử lý từng dòng
                    string[] lines = File.ReadAllLines(filePath);
                    List<NhapDiemDto> listDiemDto = new List<NhapDiemDto>();

                    foreach (string line in lines)
                    {
                        string[] data = line.Split(',');

                        if (data.Count() >= 4)
                        {
                            listDiemDto.Add(new NhapDiemDto
                            {
                                IdSinhVien = data[0],
                                DiemQuaTrinh = Convert.ToDecimal(double.Parse(data[1])),
                                DiemKetThuc = Convert.ToDecimal(double.Parse(data[2])),
                                DiemTongKet = Convert.ToDecimal(double.Parse(data[3])),
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm nhập điểm sinh viên từ file CSV trong repository
                        var response = await diemRepository
                            .NhapDiemSinhVienList(idLopHocPhan, listDiemDto);
                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi lớp học phần bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(d => d.IdDiem));

                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("Thêm điểm sinh viên từ file CSV thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                // Refresh data
                                Load_Calendar();
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi đọc file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file CSV để thêm điểm cho sinh viên!");
            }
        }
        private void LuuListDiem(object sender, RoutedEventArgs e)
        {




            // Convert ObservableCollection<DiemDto> to List<NhapDiemDto>
            List<NhapDiemDto> listDiemDto = new List<NhapDiemDto>();
            foreach (var diem in diem_collections)
            {
                listDiemDto.Add(new NhapDiemDto
                {
                    IdSinhVien = diem.IdSinhVien,
                    DiemQuaTrinh = diem.DiemQuaTrinh.Value,
                    DiemKetThuc = diem.DiemKetThuc.Value,
                    DiemTongKet = diem.DiemTongKet.Value,
                });
            }

            try
            {
                Task.Run(async () =>
                {
                    // Gọi hàm nhập điểm sinh viên từ file CSV trong repository
                    var response = await diemRepository
                        .NhapDiemSinhVienList(lopHocPhanDto.IdLopHocPhan, listDiemDto);
                    // Hiển thị thông báo kết quả trên luồng UI
                    Application.Current.Dispatcher.Invoke(async () =>
                    {
                        if (response.Status == false)
                        {
                            // Tạo chuỗi lỗi chi tiết cho mỗi lớp học phần bị lỗi
                            string errorDetails = string.Join(Environment.NewLine,
                                response.Data.Select(d => d.IdDiem));

                            MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Thêm điểm sinh viên thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Refresh data
                            Load_Calendar();
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi đọc file: " + ex.Message);
            }

        }

        private void txtInputScore_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Kiểm tra giá trị nhập vào có phải số thập phân hợp lệ
                if (decimal.TryParse(textBox.Text, out decimal value))
                {
                    // Kiểm tra nếu nằm ngoài khoảng 0 - 10
                    if (value < 0 || value > 10)
                    {
                        MessageBox.Show("Vui lòng nhập giá trị từ 0 đến 10!", "Lỗi giá trị", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBox.Focus();
                        textBox.Text = ""; // Xóa nội dung nếu không hợp lệ
                    }
                }
                else
                {
                    // Nếu không phải số hợp lệ
                    MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập lại!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBox.Focus();
                    textBox.Text = ""; // Xóa nội dung nếu không hợp lệ
                }
            }
        }
    }
}
