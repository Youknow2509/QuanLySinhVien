using QLDT_WPF.Dto;
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
using System.Collections.ObjectModel;
using QLDT_WPF.Views.Components;
using QLDT_WPF.Views.Shared.Components.Admin.Help;
using Syncfusion.XlsIO;

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for SubjectDetails.xaml
    /// </summary>
    public partial class SubjectDetails : UserControl
    {
        private MonHocRepository monHocRepository;
        private LopHocPhanRepository lopHocPhanRepository;
        private string idMonHoc;
        private ObservableCollection<LopHocPhanDto> collection_lop_hoc_phan;

        public ObservableCollection<MonHocDto> ObservableMonHoc { get; private set; }

        private string constMH = "SubjectDetails";

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectDetails), new PropertyMetadata(null));

        private string parent;

        public SubjectDetails(string idMonHoc, string parent)
        {   
            InitializeComponent();
            monHocRepository = new MonHocRepository();
            lopHocPhanRepository = new LopHocPhanRepository();
            this.idMonHoc = idMonHoc;
            collection_lop_hoc_phan = new ObservableCollection<LopHocPhanDto>();

            this.parent = parent;   

            // Loaded asynchrnously
            Loaded += async (s, e) => {
                if (TargetContentArea == null)
                {
                    var parentWindow = FindParent<Window>(this); // Tìm parent window
                    if (parentWindow != null)
                    {
                        var contentArea = parentWindow.FindName("ContentArea") as ContentControl; // Tìm ContentArea
                        if (contentArea != null)
                        {
                            TargetContentArea = contentArea;
                        } else
                        {
                            TargetContentArea = new ContentControl();
                        }
                    } else
                    {
                        TargetContentArea = new ContentControl();
                    }
                }

                await InitAsync(); 
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

        private async Task InitAsync()
        {
            var req_lhp = await lopHocPhanRepository.GetLopHocPhansFromMonHoc(idMonHoc);
            if (req_lhp.Status == false)
            {
                MessageBox.Show(req_lhp.Message);
                return;
            }

            var lopHocPhans = req_lhp.Data;
            foreach (var lopHocPhan in lopHocPhans)
            {
                collection_lop_hoc_phan.Add(lopHocPhan);
            }
            sfDataGrid.ItemsSource = collection_lop_hoc_phan;

            var monHoc = await monHocRepository.GetById(idMonHoc);
            if (monHoc.Status == false)
            {
                MessageBox.Show(monHoc.Message);
                return;
            }
            titleDataTable.Text = $"Danh sách lớp học phần của môn học {monHoc.Data.TenMonHoc}";
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            //if (ObservableChuongTrinhHoc == null || ObservableChuongTrinhHoc.Count == 0)
            // {
            //     MessageBox.Show("Không có dữ liệu để xuất ra Excel", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //     return;
            // }

            // using (ExcelEngine excelEngine = new ExcelEngine())
            // {
            //     IApplication application = excelEngine.Excel;
            //     application.DefaultVersion = ExcelVersion.Excel2013;

            //     // Tạo workbook và worksheet
            //     IWorkbook workbook = application.Workbooks.Create(1);
            //     IWorksheet worksheet = workbook.Worksheets[0];

            //     // Đặt tiêu đề cho các cột trong worksheet
            //     worksheet[1, 1].Text = "ID Chương Trình Học";
            //     worksheet[1, 2].Text = "Tên Chương Trình Học";

            //     // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
            //     int row = 2;

            //     foreach (var cch in ObservableChuongTrinhHoc)
            //     {
            //         worksheet[row, 1].Text = cch.IdChuongTrinhHoc ?? "N/A";
            //         worksheet[row, 2].Text = cch.TenChuongTrinhHoc ?? "N/A";

            //         row++;
            //     }

            //     // Tự động điều chỉnh kích thước các cột
            //     worksheet.UsedRange.AutofitColumns();

            //     // Lưu file Excel
            //     workbook.SaveAs("DanhSachChuongTrinhHoc.xlsx");
            // }

            // MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            if (collection_lop_hoc_phan == null || collection_lop_hoc_phan.Count == 0)
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
                worksheet[1, 1].Text = "ID Lớp Học Phần";
                worksheet[1, 2].Text = "Tên Lớp Học Phần";
                worksheet[1, 3].Text = "Tên Giáo Viên";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var lhp in collection_lop_hoc_phan)
                {
                    worksheet[row, 1].Text = lhp.IdLopHocPhan ?? "N/A";
                    worksheet[row, 2].Text = lhp.TenLopHocPhan ?? "N/A";
                    worksheet[row, 3].Text = lhp.TenGiaoVien ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachLopHocPhan.xlsx");

                MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = txtTimKiem.Text;
            if (txt == "")
            {
                sfDataGrid.ItemsSource = collection_lop_hoc_phan;
            }
            else
            {
                sfDataGrid.ItemsSource = collection_lop_hoc_phan.Where(lhp => lhp.TenLopHocPhan.ToLower().Contains(txt.ToLower()) || lhp.TenGiaoVien.ToLower().Contains(txt.ToLower()));
            }

        }

        private void detail_LopHocPhan(object sender, RoutedEventArgs e)
        {
            // get data text block in tag
            var idLopHocPhan = (sender as TextBlock)?.Tag.ToString();
            if (idLopHocPhan == null)
            {
                MessageBox.Show("Không thể xem chi tiết lớp học phần");
                return;
            }
            // Redirect to LopHocPhanDetails
            var lopHocPhanDetails = new LopHocPhanDetails(idLopHocPhan, constMH,idMonHoc);
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = lopHocPhanDetails;
            }

        }

        private void detail_GiaoVien(object sender, RoutedEventArgs e)
        {
            // get data text block in tag
            var idGiaoVien = (sender as TextBlock)?.Tag.ToString();
            if (idGiaoVien == null)
            {
                MessageBox.Show("Không thể xem chi tiết lớp học phần");
                return;
            }
            // Redirect to LopHocPhanDetails
            var lopHocPhanDetails = new LopHocPhanDetails(idGiaoVien, constMH,idMonHoc);
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = lopHocPhanDetails;
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                Object _parent = Parent_Find.Get_Template(parent,idMonHoc,parent);
                TargetContentArea.Content = _parent;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
