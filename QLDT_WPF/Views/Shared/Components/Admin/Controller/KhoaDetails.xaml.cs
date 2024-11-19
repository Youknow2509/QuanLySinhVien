using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for KhoaDetails.xaml
    /// </summary>
    public partial class KhoaDetails : UserControl
    {
        private string idKhoa;

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectsTableView), new PropertyMetadata(null));

        public KhoaDetails(string id)
        {
            InitializeComponent();
            idKhoa = id;
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
            // Sample data for teachers
            TeacherDataGrid.ItemsSource = new ObservableCollection<object>
            {
                new { TenGiaoVien = "Bùi Ngọc Dũng", Email = "dungbn@utc.edu.vn", SoDienThoai = "0915473821" },
                new { TenGiaoVien = "Cao Thị Luyến", Email = "caoluyen@utc.edu.vn", SoDienThoai = "0123456789" },
                new { TenGiaoVien = "Lại Mạnh Dũng", Email = "dunglm@utc.edu.vn", SoDienThoai = "0987654321" }
                // Add more sample data
            };

            // Sample data for students
            StudentDataGrid.ItemsSource = new ObservableCollection<object>
            {
                new { HoVaTen = "Nguyễn Văn A", Lop = "CNTT1", NgaySinh = "01/01/2000", DiaChi = "Hà Nội" },
                new { HoVaTen = "Trần Thị B", Lop = "CNTT2", NgaySinh = "05/02/2001", DiaChi = "Hải Phòng" },
                new { HoVaTen = "Phạm Văn C", Lop = "CNTT3", NgaySinh = "10/03/2002", DiaChi = "Đà Nẵng" }
                // Add more sample data
            };
        }

        // private void BackButton_Click(object sender, RoutedEventArgs e)
        // {
        //     if (TargetContentArea != null)
        //     {
        //         TargetContentArea.Content = new DepartmentTableView();
        //     }
        //     else
        //     {
        //         MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //     }
        // }
    }
}
