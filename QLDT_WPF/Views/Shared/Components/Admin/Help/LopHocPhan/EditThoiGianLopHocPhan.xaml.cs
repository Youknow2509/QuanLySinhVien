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

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for EditThoiGianLopHocPhan.xaml
    /// </summary>
    public partial class EditThoiGianLopHocPhan : Window
    {
        public EditThoiGianLopHocPhan()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string thoiGianBatDau = datePicker.SelectedDate?.ToString("yyyy-MM-dd");
            string ca_hoc = (cbbCaHoc.SelectedItem as ComboBoxItem)?.Tag.ToString();
            string diaDiem = diaDiemTextBox.Text;
            string id = idThoiGian.Text;
            // Xử lý lưu dữ liệu
        }
    }
}
