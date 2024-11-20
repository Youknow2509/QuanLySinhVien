using QLDT_WPF.Dto;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;
using System.Windows.Media;
using QLDT_WPF.Repositories;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;


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

        private SinhVienRepository sinhVienRepository;
        private string idSinhVien;
        private SinhVienDto sinhVienDto;
        public ObservableCollection<ScheduleAppointment> Appointments { get; set; }

        public SinhVienDetails(string id)
        {
            InitializeComponent();

            idSinhVien = id;
            sinhVienRepository = new SinhVienRepository();
            Appointments = new ObservableCollection<ScheduleAppointment>();

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
                }
            }

            // Load data asynchron
            Loaded += async (s, e) =>
            {
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
            Set_Avatar();
            // Set data in4 sinh vien
            Set_Data_In4_SV();
            // Load Calendar
            Load_Calendar();
            // Load Point
            Load_Point();
        }

        // Set avatar
        private void Set_Avatar()
        {
            // TODO
        }

        // Set data in4 sinh vien
        private void Set_Data_In4_SV()
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
        private void Load_Calendar()
        {
            // TODO
            // Create sample data
            Appointments = new ObservableCollection<ScheduleAppointment>
            {
                new ScheduleAppointment
                {
                    Subject = "Team Meeting 1",
                    StartTime = DateTime.Now.AddHours(2),
                    EndTime = DateTime.Now.AddHours(3),
                    Location = "Room A",
                    AppointmentBackground = new SolidColorBrush(Colors.LightBlue)
                },
                new ScheduleAppointment
                {
                    Subject = "Team Meeting 2",
                    StartTime = DateTime.Now.AddHours(3),
                    EndTime = DateTime.Now.AddHours(4),
                    Location = "Room B",
                    AppointmentBackground = new SolidColorBrush(Colors.LightGreen)
                }
            };

            // Set the Appointments property of the CalendarComponent
            calendar.Appointments = Appointments;
        }

        // Load Point
        private void Load_Point()
        {
            // TODO
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

        // private void BackButton_Click(object sender, RoutedEventArgs e)
        // {
        //     if (TargetContentArea != null)
        //     {
        //         TargetContentArea.Content = new SinhVienTableView();
        //     }
        //     else
        //     {
        //         MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //     }
        // }

        // handle edit profile button
        private void Edit_SV_Button_Click(object sender, RoutedEventArgs e)
        {
            // Open the SinhVienEditWindow
            var editWindow = new SinhVienEditWindow();
            editWindow.ShowDialog();
        }

        // handle edit point button
        private void Edit_Point_Student(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // export point sinh vien 
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // handle search ponint 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO
        }

    }
}
