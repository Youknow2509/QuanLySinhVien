using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.XlsIO;
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using QLDT_WPF.Models;
using Microsoft.Win32;
using System.IO;
using System;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for ChuongTrinhHocTableView.xaml
    /// </summary>
    public partial class ChuongTrinhHocTableView : UserControl
    {
        // Variables
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        public ObservableCollection<ChuongTrinhHocDto> ObservableChuongTrinhHoc { get; private set; }

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(ChuongTrinhHocTableView), new PropertyMetadata(null));

        // Constructor
        public ChuongTrinhHocTableView()
        {
            InitializeComponent();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();
            ObservableChuongTrinhHoc = new ObservableCollection<ChuongTrinhHocDto>();

            // Hook up Loaded event to call async initialization after control loads
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

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list_chuongTrinhHoc = await chuongTrinhHocRepository.GetAll();

            // Handle unsuccessful response
            if (list_chuongTrinhHoc.Status == false)
            {
                MessageBox.Show(list_chuongTrinhHoc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add items to ObservableCollection
            foreach (var item in list_chuongTrinhHoc.Data)
            {
                ObservableChuongTrinhHoc.Add(item);
            }

            // Bind to DataGrid or other UI components as needed
            sfDataGridPrograms.ItemsSource = ObservableChuongTrinhHoc;
        }

        // Export ChuongTrinhHoc to Excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu ObservableChuongTrinhHoc không có dữ liệu
            if (ObservableChuongTrinhHoc == null || ObservableChuongTrinhHoc.Count == 0)
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
                worksheet[1, 1].Text = "ID Chương Trình Học";
                worksheet[1, 2].Text = "Tên Chương Trình Học";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var cch in ObservableChuongTrinhHoc)
                {
                    worksheet[row, 1].Text = cch.IdChuongTrinhHoc ?? "N/A";
                    worksheet[row, 2].Text = cch.TenChuongTrinhHoc ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachChuongTrinhHoc.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle Search
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text.ToLower();
            if (txt_search == "")
            {
                sfDataGridPrograms.ItemsSource = ObservableChuongTrinhHoc;
            }
            else
            {
                sfDataGridPrograms.ItemsSource = ObservableChuongTrinhHoc.Where(x =>
                    x.IdChuongTrinhHoc.ToLower().Contains(txt_search) ||
                    x.TenChuongTrinhHoc.ToLower().Contains(txt_search)
                );
            }
        }


        // Add new ChuongTrinhHoc
        private void AddChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            var addSubjectWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddChuongTrinhHoc();
            addSubjectWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddChuongTrinhHocWithFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv"; // Chỉ cho phép chọn file CSV

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Đọc file CSV và xử lý từng dòng
                    string[] lines = File.ReadAllLines(filePath);
                    List<ChuongTrinhHocDto> list_chuong_trinh_hoc = new List<ChuongTrinhHocDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một môn học với định dạng "Mã Môn Học, Tên Môn Học, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 2)
                        {
                            list_chuong_trinh_hoc.Add(new ChuongTrinhHocDto
                            {
                                IdChuongTrinhHoc = data[0],
                                TenChuongTrinhHoc = data[1],
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách môn học từ file CSV trong repository
                        var response = await chuongTrinhHocRepository
                            .AddListChuongTrinhHocFromCSV(list_chuong_trinh_hoc);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi môn học bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(monh => monh.TenChuongTrinhHoc));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách chương trình học từ file CSV: " + string.Join(", ", list_chuong_trinh_hoc.Select(x => x.TenChuongTrinhHoc)) + " thành công!");

                                await InitAsync();
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi đọc file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file CSV để thêm môn học!");
            }
        }

        // Edit ChuongTrinhHoc
        private void Click_Edit_ChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ChuongTrinhHocDto chuongTrinhHoc)
            {
                var editSubjectWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.EditChuongTrinhHoc(chuongTrinhHoc);
                editSubjectWindow.ShowDialog();
                InitAsync();
            }
        }

        // Delete ChuongTrinhHoc
        private void Click_Delete_ChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng ChuongTrinhHocDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is ChuongTrinhHocDto chuongTrinhHoc)
            {
                string idchuongTrinhHoc = chuongTrinhHoc.IdChuongTrinhHoc;
                string tenchuongTrinhHoc = chuongTrinhHoc.TenChuongTrinhHoc;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa chương trình học '{tenchuongTrinhHoc}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await chuongTrinhHocRepository.Delete(idchuongTrinhHoc);

                            // Kiểm tra nếu việc xóa không thành công
                            if (response.Status == false)
                            {
                                // Nếu thất bại, hiển thị thông báo lỗi trên luồng UI
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                });
                                return;
                            }

                            // Nếu xóa thành công, tải lại dữ liệu trên luồng UI
                            Application.Current.Dispatcher.Invoke(async () =>
                            {
                                await InitAsync();
                            });
                        }
                        catch (Exception ex)
                        {
                            // Xử lý bất kỳ ngoại lệ nào xảy ra trong quá trình xóa
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show($"Có lỗi xảy ra khi xóa chương trình học '{tenchuongTrinhHoc}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }

        // handle show detail chuong trinh hoc
        private void ChiTietChuongTrinhHoc_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
