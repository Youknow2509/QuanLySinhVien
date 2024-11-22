using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QLDT_WPF.Views.Shared.Components.GiaoVien.View
{
    /// <summary>
    /// Interaction logic for LichDayView.xaml
    /// </summary>
    public partial class LichDayView : UserControl
    {


        private UserInformation userInformation;

        private string idGiaoVien;
        private GiaoVienDto giaoVienDto;

        private CalendarRepository calendarRepository;
        private GiaoVienRepository giaoVienRepository;

        private ObservableCollection<ScheduleAppointment> Appointments = new ObservableCollection<ScheduleAppointment>();


        public LichDayView()
        {
            InitializeComponent();
        }
        public LichDayView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;
            this.idGiaoVien = userInformation.UserName;

            calendarRepository = new CalendarRepository();
            giaoVienRepository = new GiaoVienRepository();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            var req_sv = await giaoVienRepository.GetById(idGiaoVien);
            if (req_sv.Status == false)
            {
                MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            giaoVienDto = req_sv.Data;

            // Load Calendar
            await Load_Calendar();
        }

        private async Task Load_Calendar()
        {
            var req_calendar = await calendarRepository.GetCalendarFromGiaoVien(idGiaoVien);
            if (req_calendar.Status == false)
            {
                MessageBox.Show("Không tìm thấy lịch dạy của giáo viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // Truyền dữ liệu vào CalendarComponent
            CalendarView.Appointments = Appointments;
        }
    }
}
