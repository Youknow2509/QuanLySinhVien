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


namespace QLDT_WPF.Views.Shared.Components.SinhVien.View
{
    /// <summary>
    /// Interaction logic for LichhocView.xaml
    /// </summary>
    public partial class LichhocView : UserControl
    {
        private UserInformation userInformation;

        private string idSinhVien;
        private SinhVienDto sinhVienDto;

        private CalendarRepository calendarRepository;
        private SinhVienRepository sinhVienRepository;

        private ObservableCollection<ScheduleAppointment> Appointments = new ObservableCollection<ScheduleAppointment>();


        public LichhocView()
        {
            InitializeComponent();
        }
        public LichhocView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;
            this.idSinhVien = userInformation.UserName;

            calendarRepository = new CalendarRepository();
            sinhVienRepository = new SinhVienRepository();

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

            // Load Calendar
            await Load_Calendar();
        }

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

            // Truyền dữ liệu vào CalendarComponent
            CalendarView.Appointments = Appointments;
        }


    }
}
