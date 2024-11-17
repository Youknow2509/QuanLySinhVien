using System.Threading.Tasks;
using System.Windows;
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private readonly IdentityRepository _identityRepository;
        public UserDto User { get; private set; }

        // Constructor nhận dữ liệu UserDto
        public UserProfileWindow(UserDto _user)
        {
            InitializeComponent();
            User = _user;
            _identityRepository = new IdentityRepository();

            // Khởi tạo dữ liệu lên giao diện
            LoadData();
        }

        // Hàm khởi tạo dữ liệu lên giao diện
        private void LoadData()
        {
            if (User != null)
            {
                txtFullName.Text = User.FullName;
                txtEmail.Text = User.Email;
                txtPhoneNumber.Text = User.Phone;
                txtAddress.Text = User.Address;
            }
        }

        // Sự kiện khi bấm nút "Save changes"
        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật dữ liệu từ giao diện
            User.FullName = txtFullName.Text;
            User.Email = txtEmail.Text;
            User.Phone = txtPhoneNumber.Text;
            User.Address = txtAddress.Text;

            // Gửi dữ liệu cập nhật lên server hoặc lưu vào database
            bool success = await _identityRepository.UpdateUserAsync(User);

            if (success)
            {
                MessageBox.Show("Thông tin đã được cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            // Lấy dữ liệu từ giao diện
            string currentPassword = txtCurrentPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            // Kiểm tra mật khẩu nhập vào
            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Gửi yêu cầu đổi mật khẩu
            bool success = _identityRepository.ChangePassword(currentPassword, newPassword);

            if (success)
            {
                MessageBox.Show("Mật khẩu đã được thay đổi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi đổi mật khẩu. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
