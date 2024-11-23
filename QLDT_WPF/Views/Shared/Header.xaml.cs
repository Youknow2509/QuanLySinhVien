using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QLDT_WPF.Dto;
using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Components;
using QLDT_WPF.Views.Login;

namespace QLDT_WPF.Views.Shared
{
    public partial class Header : UserControl
    {

        private UserInformation _user;



        public string FullName
        {
            get => (string)GetValue(FullNameProperty);
            set => SetValue(FullNameProperty, value);
        }

        public string Avatar
        {
            get => (string)GetValue(AvatarProperty);
            set => SetValue(AvatarProperty, value);
        }

        // DependencyProperty
        public static readonly DependencyProperty FullNameProperty =
             DependencyProperty.Register("FullName",
                 typeof(string),
                 typeof(Header),
                 new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty AvatarProperty =
            DependencyProperty.Register("Avatar",
                 typeof(string),
                 typeof(Header),
                 new PropertyMetadata(string.Empty));

        public Header() { }

        public Header(UserInformation user)
        {
            InitializeComponent();

            _user = user;

            // Gán dữ liệu vào giao diện Header
            FullName = _user.FullName;
        }

        private void Avatar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Hiển thị menu popup
            UserMenuPopup.IsOpen = !UserMenuPopup.IsOpen;
        }

        private void ProfileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ UserProfileWindow
            UserDto userDto = new UserDto
            {
                IdClaim = _user.IdUser,
                UserName = _user.UserName,
                FullName = _user.FullName,
                ProfilePicture = _user.Image,
                RoleName = _user.RoleName

            };
            var userProfileWindow = new UserProfileWindow(userDto);
            userProfileWindow.ShowDialog();
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Xác nhận đăng xuất
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Chuyển sang màn hình đăng nhập
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Window.GetWindow(this).Close();
            }
        }
    }
}
