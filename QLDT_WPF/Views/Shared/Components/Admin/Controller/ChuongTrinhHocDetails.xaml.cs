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
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for ChuongTrinhHocDetails.xaml
    /// </summary>
    public partial class ChuongTrinhHocDetails : UserControl
    {

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(ChuongTrinhHocDetails), new PropertyMetadata(null));

        // Variables
        private string idChuongTrinhHoc;
        private KhoaRepository monHocRepository;
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        private ChuongTrinhHocDto chuongTrinhHoc;
        private ObservableCollection<MonHocDto> monHoc_collection;

        // Constructor
        public ChuongTrinhHocDetails(string id)
        {
            InitializeComponent();

            // init repository
            monHocRepository = new MonHocRepository();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();

            // 
            monHoc_collection = new ObservableCollection<MonHocDto>();

            // set var
            idChuongTrinhHoc = id;

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
            // Set title
            var req_cth = await chuongTrinhHocRepository.GetById(idChuongTrinhHoc);
            if (req_cth.Status == false)
            {
                MessageBox.Show(req_cth.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            chuongTrinhHoc = req_cth.Data;
            tile_datagrid.Text = $"Danh Sách Các Môn Học Của Chương Trình - {chuongTrinhHoc.TenChuongTrinhHoc}";

            // init sfDataGridMonHoc
            await InitMonHoc();
        }

        // init mh in sfDataGridMonHoc
        private async Task InitMonHoc()
        {
            var req_mh = await chuongTrinhHocRepository.GetMonHocByIdChuongTrinhHoc(idChuongTrinhHoc);
            if (req_mh.Status == false)
            {
                MessageBox.Show(req_mh.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            (List<MonHocDto> mh_t, List<MonHocDto> mh_kt) = req_mh.Data;
            monHoc_collection.Clear();

            foreach (var item in mh_t)
            {
                monHoc_collection.Add(item);
            }
            sfDataGridMonHoc.ItemSource = monHoc_collection;
        }


        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        // handle search
        private void txtTimKiem_TextChanged(object s, TextChangedEventArgs e)
        {
            // TODO
        }

        // Exprot to ex
        private void ExportToExcel(object s, RoutedEventArgs e)
        {
            // TODO
        }

        // Handlee them mon hoc 
        private void EditCth(object s, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Shared.Components.Admin.Controller.ChuongTrinhHocEdit(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show detail mon hoc
        private void ChiTietMonHoc_Click(object s, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = 
                    new QLDT_WPF.Views.Shared.Components.Admin.View.SubjectDetails(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show detail khoa
        private void ChiTietKhoa_Click(object s, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = 
                    new QLDT_WPF.Views.Components.KhoaDetails(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }
    }
}
