using QLDT_WPF.Dto;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;
using System.Windows.Media;


namespace QLDT_WPF.Views.Components
{
    public partial class SinhVienDetails : UserControl
    {
        private string idSinhVien;

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectsTableView), new PropertyMetadata(null));


        public SinhVienDetails(string id)
        {
            InitializeComponent();

            idSinhVien = id;

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
            // Giả lập dữ liệu
            DataGrid.ItemsSource = new[]
             {
                new { LopHocPhan = "Lớp 1", GiangVien = "Nguyễn Văn A", MonHoc = "Toán" },
                new { LopHocPhan = "Lớp 2", GiangVien = "Trần Thị B", MonHoc = "Lý" },
                new { LopHocPhan = "Lớp 3", GiangVien = "Phạm Văn C", MonHoc = "Hóa" }
            };
        }


        // private void BackButton_Click(object sender, RoutedEventArgs e)
        // {
        //     if (TargetContentArea != null)
        //     {
        //         TargetContentArea.Content = new SinhVienTableView();
        //     }
        //     else
        //     {
        //         MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //     }
        // }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the SinhVienEditWindow
            var editWindow = new SinhVienEditWindow();
            editWindow.ShowDialog();
        }

    }
}
