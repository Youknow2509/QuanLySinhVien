using Microsoft.VisualBasic.ApplicationServices;
using QLDT_WPF.Models;
using QLDT_WPF.Repositories;
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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class TeacherEditWindow : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(TeacherEditWindow), new PropertyMetadata(null));

        public TeacherEditWindow()
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

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add your code here
        }

        // Sự kiện khi bấm nút "Cancel"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // todo
        }

        // Sự kiện thay đổi mật khẩu
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add your code here
        }


        // Set value
        private void SetInfomationUser()
        {
            
        }

        // Set avatar 
        private async Task GetAvatar_Set()
        {
            
        }

        // handle click Click_Choose_File - upload temp and show in avarta
        private void Click_Choose_File(object sender, RoutedEventArgs e)
        {
                    }

        // handle click SaveImage - save image to database
        private async void SaveImage(object sender, RoutedEventArgs e)
        {
            
        }

        // hadnle click Save_In4 - save information
        private async void Save_In4(object sender, RoutedEventArgs e)
        {
            
        }

        // handle click Save_Password - save password with root
        private async void Save_Password(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
