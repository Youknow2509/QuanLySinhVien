using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Collections.ObjectModel;


namespace QLDT_WPF.Views.Components
{
    public partial class CalendarComponent : UserControl
    {
        // ObservableCollection để chứa dữ liệu của các cuộc hẹn
        public ObservableCollection<ScheduleAppointment> Appointments
        {
            get => (ObservableCollection<ScheduleAppointment>)GetValue(AppointmentsProperty);
            set => SetValue(AppointmentsProperty, value);
        }

        // DependencyProperty để cho phép binding từ bên ngoài
        public static readonly DependencyProperty AppointmentsProperty =
            DependencyProperty.Register("Appointments", typeof(ObservableCollection<ScheduleAppointment>), typeof(CalendarComponent),
                new PropertyMetadata(new ObservableCollection<ScheduleAppointment>(), OnAppointmentsChanged));

        // Constructor
        public CalendarComponent()
        {
            InitializeComponent();

            // Đặt chế độ xem mặc định là Tuần
            scheduler.ViewType = SchedulerViewType.Week;

            // Gán danh sách cuộc hẹn cho Scheduler
            scheduler.ItemsSource = Appointments;

            // Đặt ComboBox mặc định là chế độ xem Tuần
            viewTypeComboBox.SelectedIndex = 1;
        }

        // Cập nhật dữ liệu Scheduler khi Appointments thay đổi
        private static void OnAppointmentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CalendarComponent calendarComponent)
            {
                calendarComponent.scheduler.ItemsSource = e.NewValue as ObservableCollection<ScheduleAppointment>;
            }
        }

        // Xử lý sự kiện thay đổi chế độ xem
        private void viewTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (viewTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string viewType = selectedItem.Tag.ToString();

                // Chuyển đổi chế độ xem dựa trên lựa chọn của người dùng
                switch (viewType)
                {
                    case "Month":
                        scheduler.ViewType = SchedulerViewType.Month;
                        break;
                    case "Week":
                        scheduler.ViewType = SchedulerViewType.Week;
                        break;
                    case "Day":
                        scheduler.ViewType = SchedulerViewType.Day;
                        break;
                }
            }
        }

        // Xử lý hiển thị chi tiết khi người dùng double-click vào cuộc hẹn
        private void Scheduler_AppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            // Hủy bỏ editor mặc định nếu cần
            e.Cancel = true;

            if (e.Appointment != null)
            {
                var appointment = e.Appointment;

                // Định dạng thông điệp để hiển thị chi tiết cuộc hẹn
                string message = $"Tên Lớp Học Phần: {appointment.Subject}\n" +
                                 $"Thời Gian Bắt Đầu: {appointment.StartTime}\n" +
                                 $"Thời Gian Kết Thúc: {appointment.EndTime}\n" +
                                 $"Địa Điểm: {appointment.Location}";

                // Hiển thị thông điệp đã định dạng trong hộp thoại
                MessageBox.Show(message, "Chi Tiết Sự Kiện");
            }
        }
    }

}
