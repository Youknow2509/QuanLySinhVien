using System.Windows;
using System.Windows.Controls;
using QLDT_WPF.Dto;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;
using System.Windows.Media;
using QLDT_WPF.Repositories;


namespace QLDT_WPF.Views.Components
{
    public partial class LopHocPhanDetails : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(LopHocPhanDetails), new PropertyMetadata(null));

        // Variables
        private string idLopHocPhan;
        private LopHocPhanDto lopHocPhanDto;
        private LopHocPhanRepository lopHocPhanRepository;

        // Constructor
        public LopHocPhanDetails(string id)
        {
            InitializeComponent();

            // inir repository
            lopHocPhanRepository = new LopHocPhanRepository();

            // set variables in constructor
            idLopHocPhan = id;

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

                await InitAysnc();
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

        private async InitAysnc(){
            // Set title - title_lop_hoc_phan

            // Set - description_lop_hoc_phan

            // Load Calendar - calendar_lop_hoc_phan

            // Load sinh vien thuoc danh sach lop hoc phan - StudentDataGrid

            // Load diem sinh vien lop hoc phan - ScoreDataGrid
        }

        // Show detail of sinh vien click - tag : id sinh vien
        private void ChiTietSinhVien_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Show detail of chuong trinh hoc click - tag : id chuong trinh hoc
        private void ChiTietChuongTrinhHoc_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // btn lick sua diem - tag : binding diemDto
        private void SuaDiem_Click(object sender, RoutedEventArgs e)
        {
            //TODO
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
