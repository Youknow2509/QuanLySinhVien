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
using QLDT_WPF.Migrations;
using QLDT_WPF.Repositories;
using QLDT_WPF.Views.Shared.Components.Admin.Help;
using Syncfusion.XlsIO;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for KhoaDetails.xaml
    /// </summary>
    public partial class KhoaDetails : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(KhoaDetails), new PropertyMetadata(null));

        // Variables
        private string idKhoa;
        private KhoaDto khoa;

        private KhoaRepository khoaRepository;
        private SinhVienRepository sinhVienRepository;
        private GiaoVienRepository giaoVienRepository;

        private ObservableCollection<SinhVienDto> sinhviens_collection;
        private ObservableCollection<GiaoVienDto> giaoviens_collection;


        private string constK = "KhoaDetails";
        private string parent;

        public KhoaDetails(string id, string parent)
        {
            InitializeComponent();

            idKhoa = id;

            khoaRepository = new KhoaRepository();
            sinhVienRepository = new SinhVienRepository();
            giaoVienRepository = new GiaoVienRepository();

            sinhviens_collection = new ObservableCollection<SinhVienDto>();
            giaoviens_collection = new ObservableCollection<GiaoVienDto>();
            
            this.parent = parent;

            // Loaded asyn data
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
                await InintAsync();
            };
        }

        private async Task InintAsync()
        {
            var req_khoa = await khoaRepository.GetById(idKhoa);
            if (req_khoa.Status == false)
            {
                MessageBox.Show(req_khoa.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            khoa = req_khoa.Data;

            txtTitle.Text = $"Chi tiết khoa {khoa.TenKhoa}";

            await load_data_giao_vien();
            await load_data_sinh_vien();

        }

        private async Task load_data_sinh_vien()
        {
            sinhviens_collection.Clear();
            var req_sinhviens = await khoaRepository.GetSinhVien(idKhoa);
            if (req_sinhviens.Status == false)
            {
                MessageBox.Show(req_sinhviens.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var sv in req_sinhviens.Data)
            {
                sinhviens_collection.Add(sv);
            }
            StudentDataGrid.ItemsSource = sinhviens_collection;
        }

        private async Task load_data_giao_vien()
        {
            giaoviens_collection.Clear();
            var req_giaoviens = await khoaRepository.GetGiaoVien(idKhoa);
            if (req_giaoviens.Status == false)
            {
                MessageBox.Show(req_giaoviens.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var gv in req_giaoviens.Data)
            {
                giaoviens_collection.Add(gv);
            }
            TeacherDataGrid.ItemsSource = giaoviens_collection;
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                Object _parent = Parent_Find.Get_Template(parent,idKhoa,parent);
                TargetContentArea.Content = _parent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Show details giaovien
        private void ChiTietGiaoVien_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Components.TeacherDetails(Id, constK);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show details SinhVien
        private void ChiTietSinhVien_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Components.SinhVienDetails(Id,constK);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Export data giao  vien
        private void ExportToExcel_gv(object sender, RoutedEventArgs e)
        {
            if(giaoviens_collection.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (ExcelEngine excelEngine = new ExcelEngine()){
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet[1,1].Text = "STT";
                worksheet[1,2].Text = "Họ tên";
                worksheet[1,3].Text = "Số điện thoại";
                worksheet[1,4].Text = "Email";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var mh in giaoviens_collection)
                {
                    worksheet[row, 1].Text = row - 1 + "";
                    worksheet[row, 2].Text = mh.TenGiaoVien.ToString() ?? "N/A";
                    worksheet[row, 3].Text = mh.SoDienThoai.ToString() ?? "N/A";
                    worksheet[row,4].Text = mh.Email.ToString() ?? "N/A";

                    row++;
                }
                 // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachMonHoc.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);




            }
        }

        // Export data sinh vien
        private void ExportToExcel_sv(object sender, RoutedEventArgs e)
        {
            if(sinhviens_collection.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            using (ExcelEngine excelEngine = new ExcelEngine()){
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2016;

                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet[1,1].Text = "STT";
                worksheet[1,2].Text = "Họ tên";
                worksheet[1,3].Text = "Ngày sinh";
                worksheet[1,4].Text = "Lớp";
                worksheet[1,5].Text = "Địa chỉ";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var mh in sinhviens_collection)
                {
                    worksheet[row, 1].Text = row - 1 + "";
                    worksheet[row, 2].Text = mh.HoTen.ToString() ?? "N/A";
                    worksheet[row, 3].Text = mh.NgaySinh.ToString() ?? "N/A";
                    worksheet[row,4].Text = mh.Lop.ToString() ?? "N/A";
                    worksheet[row,5].Text = mh.DiaChi.ToString() ?? "N/A";
                    row++;
                }
                 // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachMonHoc.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // handle search in data giao vien
        private void txtTimKiem_TextChanged_gv(object sender, TextChangedEventArgs e)
        {
            // TODO
        }

        // handle search in data sinh vien
        private void txtTimKiem_TextChanged_sv(object sender, TextChangedEventArgs e)
        {
            // TODO
        }
    }
}
