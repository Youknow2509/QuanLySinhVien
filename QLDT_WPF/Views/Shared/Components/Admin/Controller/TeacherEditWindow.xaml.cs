using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

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

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }
        // Variables
        private GiaoVienDto giaoVien;
        private byte[] avatar_save_temp;

        private GiaoVienRepository giaoVienRepository;
        private IdentityRepository identityRepository;

        // Constructor
        public TeacherEditWindow(GiaoVienDto gv)
        {
            InitializeComponent();

            // Set var constructor
            giaoVien = gv;

            // Init repository
            giaoVienRepository = new GiaoVienRepository();
            identityRepository = new IdentityRepository();

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

                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            // Set value
            SetInfomationUser();

            // Get avatar
            await GetAvatar_Set();

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
