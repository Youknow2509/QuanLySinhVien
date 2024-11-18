using QLDT_WPF.Views.Components;
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


namespace QLDT_WPF.Views.Shared.Components.GiaoVien.View
{
    /// <summary>
    /// Interaction logic for LopHocPhanDetailsView.xaml
    /// </summary>
    public partial class LopHocPhanDetailsView : UserControl
    {
        public ContentControl TargetContentArea { get; set; }
        public LopHocPhanDetailsView(ContentControl targetContentArea)
        {
            InitializeComponent();
            TargetContentArea = targetContentArea;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = new TeacherTableView
                {
                    TargetContentArea = TargetContentArea
                };
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
