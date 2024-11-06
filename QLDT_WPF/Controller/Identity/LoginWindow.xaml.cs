using System.Windows;

using QLDT_WPF.Views.Admin;
using QLDT_WPF.Views.GiaoVien;
using QLDT_WPF.Views.SinhVien;

namespace QLDT_WPF.Views.Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string role = "";

            // Kiểm tra tài khoản và phân quyền
            if (username == "admin" && password == "admin")
            {
                role = "admin";
            }
            else if (username == "giaovien" && password == "giaovien")
            {
                role = "giaovien";
            }
            else if (username == "sinhvien" && password == "sinhvien")
            {
                role = "sinhvien";
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                return;
            }

            // Chuyển đến cửa sổ tương ứng dựa trên quyền
            OpenMainWindow(role);
            this.Close();
        }

        private void OpenMainWindow(string role)
        {
            Window dashboard;

            switch (role)
            {
                case "admin":
                    dashboard = new AdminDashboard();  // Giao diện chính cho Admin
                    break;
                case "giaovien":
                    dashboard = new GiaoVienWindow();  // Giao diện chính cho Giáo viên
                    break;
                case "sinhvien":
                    dashboard = new SinhVienWindow();  // Giao diện chính cho Sinh viên
                    break;
                default:
                    return;
            }

            dashboard.Show();
        }
    }
}
