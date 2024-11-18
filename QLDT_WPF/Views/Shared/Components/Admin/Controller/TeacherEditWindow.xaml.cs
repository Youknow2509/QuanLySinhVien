using Microsoft.VisualBasic.ApplicationServices;
using QLDT_WPF.Repositories;
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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class TeacherEditWindow : Window
    {
        public TeacherEditWindow()
        {
            InitializeComponent();
        }
        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add your code here
        }

        // Sự kiện khi bấm nút "Cancel"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        // Sự kiện thay đổi mật khẩu
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add your code here
        }

    }
}
