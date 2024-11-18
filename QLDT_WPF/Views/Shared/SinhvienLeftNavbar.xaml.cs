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
                    TargetContentArea.Content = new DiemView();
                    break;
                case "btnDangKyNguyenVong":
                    TargetContentArea.Content = new DangKyNguyenVongView();
                    break;
                case "btnDanhSachLopHocPhan":
                    TargetContentArea.Content = new LopHocPhanComponent();
                    break;
                case "btnLichHoc":
                    TargetContentArea.Content = new LichhocView();
                    break;
                default:
                    break;

            }
        }
    }
}
