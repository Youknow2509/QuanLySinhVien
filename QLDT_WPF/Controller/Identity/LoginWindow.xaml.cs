using System.Windows;
using System.Windows.Input;
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

        // Xử lý sự kiện KeyDown trên toàn bộ cửa sổ
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                helpper_login(); // Gọi phương thức đăng nhập
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            helpper_login();
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

        // Helpper login
        private async void helpper_login()
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Kiểm tra tài khoản và phân quyền
            (bool status, string message) = await identityRepository.Login(username, password);
            if (!status)
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                UsernameTextBox.Focus();
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
    }
}
