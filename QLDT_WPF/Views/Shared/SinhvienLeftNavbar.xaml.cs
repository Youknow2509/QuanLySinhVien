using System;
using System.Windows;
using System.Windows.Controls;
using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Shared.Components.SinhVien.View;

namespace QLDT_WPF.Views.Shared
{
    /// <summary>
    /// Interaction logic for SinhvienLeftNavbar.xaml
    /// </summary>
    public partial class SinhvienLeftNavbar : UserControl
    {
        public SinhvienLeftNavbar()
        {
            InitializeComponent();
        }
        public SinhvienLeftNavbar(UserInformation userInformation)
        {
            InitializeComponent();
            UserInformation = userInformation;
        }

        public UserInformation UserInformation { get; }

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SinhvienLeftNavbar), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea == null) return;

            Button button = sender as Button;
            switch (button.Name)
            {
                case "btnQuanLyDiem":
                    var Target = new DiemView(UserInformation); // Truyền UserInformation vào DiemView
                    TargetContentArea.Content = Target; // Truyền UserInformation vào DiemView
                    break;
                case "btnDangKyNguyenVong":
                    var Target2 = new DangKyNguyenVongView(UserInformation); // Truyền UserInformation vào DangKyNguyenVongView
                    TargetContentArea.Content = Target2; // Truyền UserInformation vào DangKyNguyenVongView
                    break;
                case "btnDanhSachLopHocPhan":
                    var Target3 = new LopHocPhanComponent(UserInformation); // Truyền UserInformation vào LopHocPhanComponent
                    TargetContentArea.Content = Target3; // Truyền UserInformation vào LopHocPhanComponent
                    break;
                case "btnLichHoc":
                    var Target1 = new LichhocView(UserInformation); // Truyền UserInformation vào LichhocView
                    TargetContentArea.Content = Target1; // Truyền UserInformation vào LichhocView
                    break;
                default:
                    break;
            }
        }
    }
}
