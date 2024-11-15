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
using System.Windows.Shapes;
using QLDT_WPF.Views.Admin;
using QLDT_WPF.Views.Shared;
using QLDT_WPF.Views.Login;

namespace QLDT_WPF.Views.Shared
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        // variables

        public string FullName
        {
            get => (string)GetValue(FullNameProperty);
            set => SetValue(FullNameProperty, value);
        }

        // DependencyProperty
        public static readonly DependencyProperty FullNameProperty =
             DependencyProperty.Register("FullName", 
                 typeof(string), 
                 typeof(Header),
                 new PropertyMetadata(string.Empty));
     

        // Constructor
        public Header()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Changle to windown login
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Window.GetWindow(this).Close();

            }
        }
    }
}
