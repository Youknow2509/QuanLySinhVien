using System.Windows;
using System.Windows.Controls;
using QLDT_WPF.Dto;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;
using System.Windows.Media;
using QLDT_WPF.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Markup;
using QLDT_WPF.Views.Shared.Components.Admin.Help;
using Syncfusion.XlsIO;

namespace QLDT_WPF.Views.Components
{
    public partial class LopHocPhanDetails : UserControl
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
        public LopHocPhanDetails(string id, string par, string idparent)
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
            parent = par;
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
            this.idparent = idparent;
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        private async Task InitAysnc(){
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

                calendar_collections.Add(new CalendarDto{
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

                Appointments.Add(new ScheduleAppointment{
                    Subject = dto.Title,
                    StartTime = dto.Start ?? DateTime.MinValue,
                    EndTime = dto.End ?? DateTime.MinValue,
                    Location = dto.Location,
                    Notes = dto.Description
                });
            }
            calendar_lop_hoc_phan.Appointments = Appointments;
            DataGrid_ThoiGian_LopHocPhan.ItemsSource = calendar_collections;
        }

        private async Task Load_ScoreDataGrid()
        {
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
                diem_collections.Add(new DiemDto{
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

        // Show detail of sinh vien click - tag : id sinh vien
        private void ChiTietSinhVien_Click(object sender, RoutedEventArgs e)
        {
            var id = (sender as TextBlock)?.Tag.ToString() ?? "";
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Không tìm thấy id sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var detail = new SinhVienDetails(id,constLHPD);
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = detail;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            var detail = new QLDT_WPF.Views.Shared.Components.Admin.View.ChuongTrinhHocDetails(id,constLHPD);
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

            using (ExcelEngine excelEngine = new ExcelEngine()){
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

                foreach(var sv in diem_collections){
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
            //TODO
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
            var window = new QLDT_WPF.Views.Shared.Components.Admin.View.EditThoiGianLopHocPhan(tg, idLopHocPhan);
            window.ShowDialog();

            // refresh data
            Load_Calendar();
        }

        // handle click ExportToExcel_ThoiGian_LopHocPhan
        private void ExportToExcel_ThoiGian_LopHocPhan(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // handle text change txtTimKiem_TextChanged_ThoiGian_LopHocPhan
        private void txtTimKiem_TextChanged_ThoiGian_LopHocPhan(object sender, TextChangedEventArgs e)
        {
            //TODO
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
             if (TargetContentArea != null)
             {
                Object _parent = Parent_Find.Get_Template(constLHPD,parent,idparent);
                TargetContentArea.Content = _parent;
            }
             else
             {
                 MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
             }
         }
    }
}
