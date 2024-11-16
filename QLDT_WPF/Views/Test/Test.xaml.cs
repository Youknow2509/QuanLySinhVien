using QLDT_WPF.Views.Components;
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
using System.Windows.Shapes;

namespace QLDT_WPF.Views.Test
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public ObservableCollection<ScheduleAppointment> Appointments { get; set; }

        public Test()
        {
            InitializeComponent();

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
            calendar_comonent.Appointments = Appointments;

        }
    }
}
