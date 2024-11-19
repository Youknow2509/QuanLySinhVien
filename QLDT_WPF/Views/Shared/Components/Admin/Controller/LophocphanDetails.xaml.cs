using System.Windows;
using System.Windows.Controls;
using QLDT_WPF.Dto;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;


namespace QLDT_WPF.Views.Components
{
    public partial class LopHocPhanDetails : UserControl
    {
        private string idLopHocPhan;

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectsTableView), new PropertyMetadata(null));

        public LopHocPhanDetails(string id)
        {
            InitializeComponent();

            idLopHocPhan = id;

            LoadSampleData();

            if (TargetContentArea == null)
            {
                var parentWindow = FindParent<Window>(this); // Tìm parent window
                if (parentWindow != null)
                {
                    var contentArea = parentWindow.FindName("ContentArea") as ContentControl; // Tìm ContentArea
                    if (contentArea != null)
                    {
                        TargetContentArea = contentArea;
                    }
                }
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
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

        // private void BackButton_Click(object sender, RoutedEventArgs e)
        // {
        //     if (TargetContentArea != null)
        //     {
        //         // Navigate back to LopHocPhanTableView or the parent view
        //         TargetContentArea.Content = new LopHocPhanTableView();
        //     }
        //     else
        //     {
        //         MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //     }
        // }
    }
}
