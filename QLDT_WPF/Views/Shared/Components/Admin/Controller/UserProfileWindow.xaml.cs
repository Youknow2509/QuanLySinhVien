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
        private readonly IdentityRepository  _identityRepository;
        public UserDto User { get; private set; }

        // Constructor nhận dữ liệu SinhVienDto
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
            // todo save changes
        }

        // Sự kiện khi bấm nút "Cancel"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        // ChangePassword_Click
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // Todo change password
        }
    }
}
