using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Shared.Components.GiaoVien.View;
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

namespace QLDT_WPF.Views.Shared
{
    /// <summary>
    /// Interaction logic for GiaoVienLeftNavbar.xaml
    /// </summary>
    public partial class GiaoVienLeftNavbar : UserControl
    {
        public UserInformation UserInformation { get; }
        public GiaoVienLeftNavbar()
        {
            InitializeComponent();
        }

        public GiaoVienLeftNavbar(UserInformation userInformation)
        {
            InitializeComponent();
            UserInformation = userInformation;
        }

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(GiaoVienLeftNavbar), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea == null) return;

            Button button = sender as Button;
            switch (button.Name)
            {
                case "btnLichHoc":
                    TargetContentArea.Content = new LichDayView(UserInformation);
                    break;
                case "btnDangKyNguyenVong":
                    TargetContentArea.Content = new NguyenVongTableView();
                    break;
                case "btnDanhSachLopHocPhan":
                    TargetContentArea.Content = new LopHocPhanTableView(UserInformation);
                    break;
                case "btnQuanLySinhVien":
                    TargetContentArea.Content = new SinhVienTableView(UserInformation);
                    break;
                default:
                    break;

            }
        }
    }
}
