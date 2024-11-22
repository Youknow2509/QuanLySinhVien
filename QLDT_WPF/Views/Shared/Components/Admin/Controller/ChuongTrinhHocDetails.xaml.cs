using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
using QLDT_WPF.Views.Shared.Components.Admin.Help;
using Syncfusion.XlsIO;

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
        private MonHocRepository monHocRepository;
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        private ChuongTrinhHocDto chuongTrinhHoc;
        private ObservableCollection<MonHocDto> monHoc_collection;

        private string parent;
        private string constCTH = "ChuongTrinhHocDetails";

        // Constructor
        public ChuongTrinhHocDetails(string id, string parent)
        {
            InitializeComponent();

            // init repository
            monHocRepository = new MonHocRepository();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();

            // 
            monHoc_collection = new ObservableCollection<MonHocDto>();

            // set var
            idChuongTrinhHoc = id;
            this.parent = parent;

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
            sfDataGridMonHoc.ItemsSource = monHoc_collection;
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
            var txt = txtTimKiem.Text;
            if (txt == "")
            {
                sfDataGridMonHoc.ItemsSource = monHoc_collection;
            }
            else
            {
                sfDataGridMonHoc.ItemsSource = monHoc_collection.Where(x => x.TenMonHoc.ToLower().Contains(txt.ToLower()));
            }

        }

        // Exprot to ex
        private void ExportToExcel(object s, RoutedEventArgs e)
        {
            if (monHoc_collection == null || monHoc_collection.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất ra Excel", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                // Tạo workbook và worksheet
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                // Đặt tiêu đề cho các cột trong worksheet
                worksheet[1, 1].Text = "ID Môn Học";
                worksheet[1, 2].Text = "Tên Môn Học";
                worksheet[1, 3].Text = "Số Tín Chỉ";
                worksheet[1, 4].Text = "Học Kỳ";
                worksheet[1, 5].Text = "Số Tiết Lý Thuyết";
                worksheet[1, 6].Text = "Số Tiết Thực Hành";
                worksheet[1, 7].Text = "Số Tiết Tự Học";
                worksheet[1, 8].Text = "Mô Tả";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var mh in monHoc_collection)
                {
                    worksheet[row, 1].Text = mh.IdMonHoc ?? "N/A";
                    worksheet[row, 2].Text = mh.TenMonHoc ?? "N/A";
                    worksheet[row, 3].Text = mh.SoTinChi.ToString() ?? "N/A";
                    worksheet[row,4].Text = mh.TenKhoa.ToString() ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachMonHoc.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            
        }

        // Handlee them mon hoc 
        private void EditCth(object s, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = s as TextBlock;
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
            TextBlock textBlock = s as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = 
                    new QLDT_WPF.Views.Shared.Components.Admin.View.SubjectDetails(Id,constCTH);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show detail khoa
        private void ChiTietKhoa_Click(object s, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = s as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = 
                    new QLDT_WPF.Views.Components.KhoaDetails(Id,constCTH);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }
    }
}
