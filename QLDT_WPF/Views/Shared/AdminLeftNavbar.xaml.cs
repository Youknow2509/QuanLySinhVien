using QLDT_WPF.Views.Components;
using System.Windows;
using System.Windows.Controls;

namespace QLDT_WPF.Views.Shared
{
    public partial class AdminLeftNavbar : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(AdminLeftNavbar), new PropertyMetadata(null));

        public AdminLeftNavbar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea == null) return;

            Button button = sender as Button;
            switch (button.Name)
            {
                case "btnQLChuongTrinhHoc":
                    TargetContentArea.Content = new ChuongTrinhHocTableView();
                    break;
                case "btnQLMonHoc":
                    TargetContentArea.Content = new SubjectsTableView();
                    break;
                case "btnQLSinhVien":
                    TargetContentArea.Content = new SinhVienTableView();
                    break;
                case "btnLichHoc":
                    TargetContentArea.Content = new LopHocPhanTableView();
                    break;
                case "btnQLGiaoVien":
                    TargetContentArea.Content = new TeacherTableView();
                    break;
                case "btnQLNguyenVong":
                    TargetContentArea.Content = new NguyenVongTableView();
                    break;
                default:
                    break;
            }
        }
    }
}
