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
    /// Interaction logic for LopHocPhanTableView.xaml
    /// </summary>
    public partial class LopHocPhanTableView : UserControl
    {
        // Variables
        private LopHocPhanRepository lopHocPhanRepository;
        public ObservableCollection<LopHocPhanDto> ObservableLopHocPhan { get; private set; }

        // Constructor
        public LopHocPhanTableView()
        {
            InitializeComponent();
            lopHocPhanRepository = new LopHocPhanRepository();
            ObservableLopHocPhan = new ObservableCollection<LopHocPhanDto>();
            // Load asynchronously
            Loaded += async (s, e) => await InitAsync();
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list_lopHocPhan = await lopHocPhanRepository.GetAll();

            // Handle unsuccessful response
            if (list_lopHocPhan.Status == false)
            {
                MessageBox.Show(list_lopHocPhan.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add items to ObservableCollection
            foreach (var item in list_lopHocPhan.Data)
            {
                ObservableLopHocPhan.Add(item);
            }

            // Bind to DataGrid or other UI components as needed
            dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan;
        }

        // TextBox TextChanged Event Handler - Handle search when changle value 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text;
            if (txt_search == "")
            {
                dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan;
            }
            else
            {
                dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan.Where(x =>
                    x.TenLopHocPhan.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenGiaoVien.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenMonHoc.ToLower().Contains(txt_search.ToLower()) ||
                    x.ThoiGianBatDau.ToString().Contains(txt_search) ||
                    x.ThoiGianKetThuc.ToString().Contains(txt_search)
                );
            }
        }

        // Export data to excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            if (ObservableLopHocPhan == null || ObservableLopHocPhan.Count == 0)
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
                worksheet[1, 3].Text = "ID Giáo Viên";
                worksheet[1, 4].Text = "Tên Giáo Viên";
                worksheet[1, 5].Text = "ID Môn Học";
                worksheet[1, 6].Text = "Tên Môn Học";
                worksheet[1, 7].Text = "Thời Gian Bắt Đầu";
                worksheet[1, 8].Text = "Thời Gian Kết Thúc";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var lhp in ObservableLopHocPhan)
                {
                    worksheet[row, 1].Text = lhp.IdLopHocPhan ?? "N/A";
                    worksheet[row, 2].Text = lhp.TenLopHocPhan ?? "N/A";
                    worksheet[row, 3].Text = lhp.IdGiaoVien ?? "N/A";
                    worksheet[row, 4].Text = lhp.TenGiaoVien ?? "N/A";
                    worksheet[row, 5].Text = lhp.IdMonHoc ?? "N/A";
                    worksheet[row, 6].Text = lhp.TenMonHoc ?? "N/A";
                    worksheet[row, 7].Text = lhp.ThoiGianBatDau?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                    worksheet[row, 8].Text = lhp.ThoiGianKetThuc?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachLopHocPhan.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Add new LopHocPhan
        private void AddLopHocPhan(object sender, RoutedEventArgs e)
        {
            var addLopHocPhanWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddLopHocPhan();
            addLopHocPhanWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddLopHocPhanWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Edit LopHocPhan
        private void Click_Edit_LopHocPhan(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is LopHocPhanDto lopHocPhan)
            {
                var editLopHocPhanWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.EditLopHocPhan(lopHocPhan);
                editLopHocPhanWindow.ShowDialog();
                InitAsync();
            }
        }

        // Delete LopHocPhan
        private void Click_Delete_LopHocPhan(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng LopHocPhanDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is LopHocPhanDto lopHocPhan)
            {
                string idlopHocPhan = lopHocPhan.IdLopHocPhan;
                string tenlopHocPhan = lopHocPhan.TenLopHocPhan;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học phần '{tenlopHocPhan}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await lopHocPhanRepository.Delete(idlopHocPhan);

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
                                MessageBox.Show($"Có lỗi xảy ra khi xóa môn học '{tenlopHocPhan}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }

    }
}
