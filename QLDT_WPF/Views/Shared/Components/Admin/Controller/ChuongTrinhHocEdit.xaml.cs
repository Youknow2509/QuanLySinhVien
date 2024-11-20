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
using QLDT_WPF.Views.Components;

namespace QLDT_WPF.Views.Shared.Components.Admin.Controller
{
    /// <summary>
    /// Interaction logic for ChuongTrinhHocEdit.xaml
    /// </summary>
    public partial class ChuongTrinhHocEdit : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(ChuongTrinhHocEdit), new PropertyMetadata(null));


        public ChuongTrinhHocEdit(string id)
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
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
                        else
                        {
                            TargetContentArea = new ContentControl();
                        }
                    }
                    else
                    {
                        TargetContentArea = new ContentControl();
                    }
                }
            };
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }


        // Event Handler for Adding a MonHoc
        private void AddMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // Get the MonHoc object from the button's Tag property
            var monHoc = (sender as Button)?.Tag;
            if (monHoc != null)
            {
                MessageBox.Show($"Thêm môn học: {monHoc}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                // Add your logic here to handle adding the MonHoc to the Chương Trình Học
                // TODO

            }
        }

        // Event Handler for Removing a MonHoc
        private void RemoveMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // Get the MonHoc object from the button's Tag property
            var monHoc = (sender as Button)?.Tag;
            if (monHoc != null)
            {
                MessageBox.Show($"Xóa môn học: {monHoc}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Add your logic here to handle removing the MonHoc from the Chương Trình Học
                // TODO

            }
        }

        // Search filtering for the first grid
        private void SearchBox_TrongChuongTrinh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO
        }

        // Search filtering for the second grid
        private void SearchBox_NgoaiChuongTrinh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO
        }
    }
}
