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
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for EditThoiGianLopHocPhan.xaml
    /// </summary>
    public partial class EditThoiGianLopHocPhan : Window
    {
        private CalendarDto calendarDto;
        private string idLopHocPhan;
        private LopHocPhanRepository lopHocPhanRepository;

        public EditThoiGianLopHocPhan(CalendarDto calendar, string id)
        {
            InitializeComponent();

            calendarDto = calendar;
            idLopHocPhan = id;

            lopHocPhanRepository = new LopHocPhanRepository();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            // Set 
            idThoiGian.Text = calendarDto.Id;
            datePicker.SelectedDate = DateTime.Parse(calendarDto.Start);
            diaDiemTextBox.Text = calendarDto.Location;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var thoi_gian = datePicker.SelectedDate.Value;
            var dia_diem = diaDiemTextBox.Text;
            var ca = (cbbCaHoc.SelectedItem as ComboBoxItem)?.Tag;

            if (thoi_gian == null || string.IsNullOrWhiteSpace(dia_diem) || ca == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Call api handle async
            try
            {
                var (ThoiGianBatDau, ThoiGianKetThuc) = helpConvertTime(thoi_gian, ca.ToString());
                var thayDoiThoiGianLopHocPhanDto = new ThayDoiThoiGianLopHocPhanDto
                {
                    IdThoiGian = calendarDto.Id,
                    IdLopHocPhan = "",
                    ThoiGianBatDau = ThoiGianBatDau,
                    ThoiGianKetThuc = ThoiGianKetThuc,
                    DiaDiem = dia_diem,
                };

                Task.Run(
                    async () =>
                    {
                        var response = await lopHocPhanRepository.ChangeTime(thayDoiThoiGianLopHocPhanDto);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (response.Status == true)
                            {
                                MessageBox.Show("Thay đổi thời gian lớp học phần thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: {response.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        });
                    }
                );

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        }

        private (DateTime ThoiGianBatDau, DateTime ThoiGianKetThuc)
            helpConvertTime(DateTime thoiGian, string ca)
        {
            // Ca 1: 7:00 AM -> 9:30 AM
            // Ca 2: 9:35 AM -> 12:00 PM
            // Ca 3: 1:00 PM -> 3:30 PM
            // Ca 4: 3:35 PM -> 6:00 PM

            DateTime ThoiGianBatDau;
            DateTime ThoiGianKetThuc;

            switch (ca)
            {
                case "1":
                    ThoiGianBatDau = thoiGian.Date.AddHours(7).AddMinutes(0); // 7:00 AM
                    ThoiGianKetThuc = thoiGian.Date.AddHours(9).AddMinutes(30); // 9:30 AM
                    break;

                case "2":
                    ThoiGianBatDau = thoiGian.Date.AddHours(9).AddMinutes(35); // 9:35 AM
                    ThoiGianKetThuc = thoiGian.Date.AddHours(12).AddMinutes(0); // 12:00 PM
                    break;

                case "3":
                    ThoiGianBatDau = thoiGian.Date.AddHours(13).AddMinutes(0); // 1:00 PM
                    ThoiGianKetThuc = thoiGian.Date.AddHours(15).AddMinutes(30); // 3:30 PM
                    break;

                case "4":
                    ThoiGianBatDau = thoiGian.Date.AddHours(15).AddMinutes(35); // 3:35 PM
                    ThoiGianKetThuc = thoiGian.Date.AddHours(18).AddMinutes(0); // 6:00 PM
                    break;

                default:
                    throw new ArgumentException("Invalid shift (ca).");
            }
        }
    }
}
