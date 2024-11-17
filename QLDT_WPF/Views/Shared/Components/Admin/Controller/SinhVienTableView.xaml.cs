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
    /// Interaction logic for SinhVienTableView.xaml
    /// </summary>
    public partial class SinhVienTableView : UserControl
    {
        // Variables
        private SinhVienRepository sinhVienRepository;
        public ObservableCollection<SinhVienDto> ObservableSinhVien { get; private set; }

        // Constructor
        public SinhVienTableView()
        {
            InitializeComponent();
            sinhVienRepository = new SinhVienRepository();
            ObservableSinhVien = new ObservableCollection<SinhVienDto>();

            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };

            // Initialize events
            cbbPageSize.SelectionChanged += PageSizeChanged;
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list = await sinhVienRepository.GetAll();
            if (list.Status == false)
            {
                MessageBox.Show(list.Message);
                return;
            }

            ObservableSinhVien.Clear();
            foreach (var item in list.Data)
            {
                ObservableSinhVien.Add(item);
            }

            sfDataGrid.ItemsSource = ObservableSinhVien;
        }

        // Handle page size change
        private void PageSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbPageSize.SelectedItem is ComboBoxItem selectedItem)
            {
                sfDataGrid.PageSize = int.Parse(selectedItem.Content.ToString());
            }
        }

        // Export SinhVien to Excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
                        // Kiểm tra nếu ObservableSinhVien không có dữ liệu
            if (ObservableSinhVien == null || ObservableSinhVien.Count == 0)
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
                worksheet[1, 1].Text = "ID Sinh Viên";
                worksheet[1, 2].Text = "Tên Sinh Viên";
                worksheet[1, 3].Text = "ID Khoa";
                worksheet[1, 4].Text = "Tên Khoa";
                worksheet[1, 5].Text = "ID Chương Trình Học";
                worksheet[1, 6].Text = "Tên Chương Trình Học";
                worksheet[1, 7].Text = "Lớp";
                worksheet[1, 8].Text = "Ngày Sinh";
                worksheet[1, 9].Text = "Số Điện Thoại";
                worksheet[1, 10].Text = "Email";                

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var sinhVien in ObservableSinhVien)
                {
                    worksheet[row, 1].Text = sinhVien.IdSinhVien;
                    worksheet[row, 2].Text = sinhVien.HoTen;
                    worksheet[row, 3].Text = sinhVien.IdKhoa;
                    worksheet[row, 4].Text = sinhVien.TenKhoa;
                    worksheet[row, 5].Text = sinhVien.IdChuongTrinhHoc;
                    worksheet[row, 6].Text = sinhVien.TenChuongTrinhHoc;
                    worksheet[row, 7].Text = sinhVien.Lop;
                    worksheet[row, 8].Text = sinhVien.NgaySinh;
                    worksheet[row, 9].Text = sinhVien.SoDienThoai;
                    worksheet[row, 10].Text = sinhVien.Email;

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSinhVien.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle Search
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text.ToLower();
            if (txt_search == "")
            {
                sfDataGrid.ItemsSource = ObservableSinhVien;
            }
            else
            {
                sfDataGrid.ItemsSource = ObservableSinhVien.Where(x =>
                    x.IdSinhVien.ToLower().Contains(txt_search) ||
                    x.HoTen.ToLower().Contains(txt_search) ||
                    x.IdKhoa.ToLower().Contains(txt_search) ||
                    x.TenKhoa.ToLower().Contains(txt_search) ||
                    x.IdChuongTrinhHoc.ToLower().Contains(txt_search) ||
                    x.TenChuongTrinhHoc.ToLower().Contains(txt_search) ||
                    x.Lop.ToLower().Contains(txt_search) ||
                    x.NgaySinh.ToLower().Contains(txt_search) ||
                    x.SoDienThoai.ToLower().Contains(txt_search) ||
                    x.Email.ToLower().Contains(txt_search)
                );
            }
        }


        // Add new SinhVien
        private void AddSinhVien(object sender, RoutedEventArgs e)
        {
            var addSinhVienWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddSinhVien();
            addSinhVienWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddSinhVienWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Delete SinhVien
        private void Click_Delete_SinhVien(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng MonHocDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is SinhVienDto sinhVien)
            {
                string idSinhVien = sinhVien.IdSinhVien;
                string tenSinhVien = sinhVien.HoTen;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên '{tenSinhVien}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await sinhVienRepository.Delete(idSinhVien);

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
                                MessageBox.Show($"Có lỗi xảy ra khi xóa sinh viên '{tenSinhVien}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }
    }
}
