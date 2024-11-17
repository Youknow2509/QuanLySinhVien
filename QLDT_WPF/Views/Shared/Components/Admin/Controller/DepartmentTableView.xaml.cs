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
    /// Interaction logic for DepartmentTableView.xaml
    /// </summary>
    public partial class DepartmentTableView : UserControl
    {
        // Variables
        private KhoaRepository khoaRepository;
        public ObservableCollection<KhoaDto> ObservableKhoa { get; private set; }


        // Constructor
        public DepartmentTableView()
        {
            InitializeComponent();
            khoaRepository = new KhoaRepository();
            ObservableKhoa = new ObservableCollection<KhoaDto>();
            // 

            // Hook up Loaded event to call async initialization after control loads
            Loaded += async (s, e) => await InitAsync();
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list_khoa = await khoaRepository.GetAll();

            // Handle unsuccessful response
            if (list_khoa.Status == false)
            {
                MessageBox.Show(list_khoa.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add items to ObservableCollection
            foreach (var item in list_khoa.Data)
            {
                ObservableKhoa.Add(item);
            }

            // Bind to DataGrid or other UI components as needed
            sfDataGridDepartments.ItemsSource = ObservableKhoa;
        }


        // Export Khoa to Excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu ObservableKhoa không có dữ liệu
            if (ObservableKhoa == null || ObservableKhoa.Count == 0)
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
                worksheet[1, 1].Text = "ID Khoa";
                worksheet[1, 2].Text = "Tên Khoa";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var kh in ObservableKhoa)
                {
                    worksheet[row, 1].Text = kh.IdKhoa ?? "N/A";
                    worksheet[row, 2].Text = kh.TenKhoa ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachKhoa.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle Search
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text.ToLower();
            if (txt_search == "")
            {
                sfDataGridDepartments.ItemsSource = ObservableKhoa;
            }
            else
            {
                sfDataGridDepartments.ItemsSource = ObservableKhoa.Where(x => 
                    x.TenKhoa.ToLower().Contains(txt_search)
                );
            }
        }


        // Add new Khoa
        private void AddKhoa(object sender, RoutedEventArgs e)
        {
            var addKhoaWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddKhoa();
            addKhoaWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddKhoaWithFile(object sender, RoutedEventArgs e)
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
                    List<KhoaDto> list_khoa = new List<KhoaDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một môn học với định dạng "Mã Môn Học, Tên Môn Học, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 5)
                        {
                            list_khoa.Add(new KhoaDto
                            {
                                IdKhoa = data[0],
                                TenKhoa = data[1],
                            });
                        }  
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách môn học từ file CSV trong repository
                        var response = await khoaRepository
                            .AddListKhoaFromCSV(list_khoa);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi môn học bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(monh => monh.TenKhoa));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách khoa từ file CSV: " + string.Join(", ", list_khoa.Select(x => x.TenKhoa)) + " thành công!");

                                await InitAsync();
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi đọc file: " + ex.Message);
                }
            } else
            {
                MessageBox.Show("Vui lòng chọn file CSV để thêm môn học!");
            }
        }

        // Edit Khoa
        private void Click_Edit_Khoa(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Delete Khoa
        private void Click_Delete_Khoa(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
