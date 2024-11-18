using QLDT_WPF.Dto;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;

namespace QLDT_WPF.Views.Components
{
    public partial class SinhVienDetails : UserControl
    {
        public ContentControl TargetContentArea { get; set; }

        public SinhVienDetails(ContentControl targetContentArea)
        {
            InitializeComponent();
            LoadSampleData();
            TargetContentArea = targetContentArea;
        }

        private void LoadSampleData()
        {
            // Giả lập dữ liệu
            DataGrid.ItemsSource = new[]
             {
                new { LopHocPhan = "Lớp 1", GiangVien = "Nguyễn Văn A", MonHoc = "Toán" },
                new { LopHocPhan = "Lớp 2", GiangVien = "Trần Thị B", MonHoc = "Lý" },
                new { LopHocPhan = "Lớp 3", GiangVien = "Phạm Văn C", MonHoc = "Hóa" }
            };
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = new SinhVienTableView();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the SinhVienEditWindow
            var editWindow = new SinhVienEditWindow();
            editWindow.ShowDialog();
        }

    }
}
