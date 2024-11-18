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
        private IdentityRepository identityRepository;
        public ObservableCollection<SinhVienDto> ObservableSinhVien { get; private set; }

        // Constructor
        public SinhVienTableView()
        {
            InitializeComponent();
            sinhVienRepository = new SinhVienRepository();
            identityRepository = new IdentityRepository();
            ObservableSinhVien = new ObservableCollection<SinhVienDto>();

            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };

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
                    worksheet[row, 8].Text = sinhVien.NgaySinh?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
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
                    x.NgaySinh.ToString().ToLower().Contains(txt_search) ||
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv"; // Chỉ cho phép chọn file CSV

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Đọc file CSV và xử lý từng dòng
                    string[] lines = File.ReadAllLines(filePath);
                    List<SinhVienDto> list_sinh_vien = new List<SinhVienDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một môn học với định dạng "Mã Môn Học, Tên Môn Học, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 8)
                        {
                            list_sinh_vien.Add(new SinhVienDto
                            {
                                IdSinhVien = data[0],
                                HoTen = data[1],
                                IdKhoa = data[2],
                                IdChuongTrinhHoc = data[3],
                                Lop = data[4],
                                NgaySinh = DateTime.Parse(data[5]),
                                SoDienThoai = data[6],
                                Email = data[7]
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách môn học từ file CSV trong repository
                        var response = await identityRepository
                            .CreateListSinhVienFromCSV(list_sinh_vien);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi môn học bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(sv => sv.HoTen));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách sinh viên từ file CSV: " + string.Join(", ", list_sinh_vien.Select(x => x.HoTen)) + " thành công!");

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

    }
}
