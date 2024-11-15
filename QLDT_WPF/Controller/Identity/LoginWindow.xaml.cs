using System.Windows;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Admin;
using QLDT_WPF.Views.GiaoVien;
using QLDT_WPF.Views.SinhVien;

namespace QLDT_WPF.Views.Login
{
    public partial class LoginWindow : Window
    {
        private IdentityRepository identityRepository;
        public LoginWindow()
        {
            InitializeComponent();
            identityRepository = new IdentityRepository();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Kiểm tra tài khoản và phân quyền
            (bool status, string message) = await identityRepository.Login(username, password);
            if (!status)
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UserInformation userInformation = await identityRepository.GetUserInformation(username);
            if (userInformation == null)
            {
                MessageBox.Show("Không thể lấy thông tin người dùng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Chuyển đến cửa sổ tương ứng dựa trên quyền
            OpenMainWindow(userInformation.RoleName, userInformation);
            this.Close();
        }

        private void OpenMainWindow(string role,UserInformation userInformation)
        {
            Window dashboard;

            switch (role.ToUpper())
            {
                case "ADMIN":
                    dashboard = new AdminDashboard(userInformation);  // Giao diện chính cho Admin
                    break;
                case "GIAOVIEN":
                    dashboard = new GiaoVienWindow();  // Giao diện chính cho Giáo viên
                    break;
                case "SINHVIEN":
                    dashboard = new SinhVienWindow();  // Giao diện chính cho Sinh viên
                    break;
                default:
                    return;
            }

            dashboard.Show();
        }
    }
}
