using System.Windows;
using System.Windows.Controls;
using QLDT_WPF.Dto;
using Syncfusion.UI.Xaml.Grid;

namespace QLDT_WPF.Views.Components
{
    public partial class LopHocPhanDetails : UserControl
    {
        public ContentControl TargetContentArea { get; set; }

        public LopHocPhanDetails(ContentControl targetContentArea, LopHocPhanDto? lophocphan)
        {
            InitializeComponent();
            TargetContentArea = targetContentArea;
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            // Sample data for Student Grid
            StudentDataGrid.ItemsSource = new[]
            {
                new { HoVaTen = "Nguyễn Văn A", Lop = "Lớp 1", NgaySinh = "01/01/2000", DiaChi = "Hà Nội", ChuongTrinhHoc = "Công Nghệ Thông Tin" },
                new { HoVaTen = "Trần Thị B", Lop = "Lớp 2", NgaySinh = "02/02/2001", DiaChi = "Hồ Chí Minh", ChuongTrinhHoc = "Kinh Tế" },
            };

            // Sample data for Score Grid
            ScoreDataGrid.ItemsSource = new[]
            {
                new { SinhVien = "Nguyễn Văn A", DiemQuaTrinh = 7, DiemKetThuc = 8, DiemTongKet = 7.5, TrangThai = "Đạt" },
                new { SinhVien = "Trần Thị B", DiemQuaTrinh = 6, DiemKetThuc = 7, DiemTongKet = 6.5, TrangThai = "Học Lại" },
            };
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                // Navigate back to LopHocPhanTableView or the parent view
                TargetContentArea.Content = new LopHocPhanTableView();
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
