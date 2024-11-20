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

        // Constructor
        public LopHocPhanDetails(string id)
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
                calendar_collections.Add(dto);

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
                diem_collections.Add(dto);
            }
            ScoreDataGrid.ItemsSource = diem_collections;
        }

        // Show detail of sinh vien click - tag : id sinh vien
        private void ChiTietSinhVien_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Show detail of chuong trinh hoc click - tag : id chuong trinh hoc
        private void ChiTietChuongTrinhHoc_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // btn lick sua diem - tag : binding diemDto
        private void SuaDiem_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // handle click ExportToExcel_Point_SinhVien_LopHocPhan
        private void ExportToExcel_Point_SinhVien_LopHocPhan(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // handle text change txtTimKiem_TextChanged_Point_SinhVien_LopHocPhan
        private void txtTimKiem_TextChanged_Point_SinhVien_LopHocPhan(object sender, TextChangedEventArgs e)
        {
            //TODO
        }

        // handle click Edit_ThoiGian_LopHocPhan
        private void Edit_ThoiGian_LopHocPhan(object sender, RoutedEventArgs e)
        {
            //TODO
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


        // private void BackButton_Click(object sender, RoutedEventArgs e)
        // {
        //     if (TargetContentArea != null)
        //     {
        //         // Navigate back to LopHocPhanTableView or the parent view
        //         TargetContentArea.Content = new LopHocPhanTableView();
        //     }
        //     else
        //     {
        //         MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //     }
        // }
    }
}
