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
    /// Interaction logic for TeacherTableView.xaml
    /// </summary>
    public partial class TeacherTableView : UserControl
    {
        // Variables
        private GiaoVienRepository giaoVienRepository;
        private IdentityRepository identityRepository;
        public ObservableCollection<GiaoVienDto> ObservableGiaoVien { get; private set; }

        // Constructor
        public TeacherTableView()
        {
            InitializeComponent();
            giaoVienRepository = new GiaoVienRepository();
            identityRepository = new IdentityRepository();
            ObservableGiaoVien = new ObservableCollection<GiaoVienDto>();
            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list = await giaoVienRepository.GetAll();
            if (list.Status == false)
            {
                MessageBox.Show(list.Message);
                return;
            }
            ObservableGiaoVien.Clear();
            foreach (var item in list.Data)
            {
                ObservableGiaoVien.Add(item);
            }
            dataGridGiaoVien.ItemsSource = ObservableGiaoVien;
        }

        // Export GiaoVien to Excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu ObservableGiaoVien không có dữ liệu
            if (ObservableGiaoVien == null || ObservableGiaoVien.Count == 0)
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
                worksheet[1, 1].Text = "ID Giảng Viên";
                worksheet[1, 2].Text = "Họ Tên";
                worksheet[1, 3].Text = "ID Khoa";
                worksheet[1, 4].Text = "Tên Khoa";
                worksheet[1, 5].Text = "Email";
                worksheet[1, 6].Text = "Số Điện Thoại";


                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var sinhVien in ObservableGiaoVien)
                {
                    worksheet[row, 1].Text = sinhVien.IdGiaoVien;
                    worksheet[row, 2].Text = sinhVien.TenGiaoVien;
                    worksheet[row, 3].Text = sinhVien.IdKhoa;
                    worksheet[row, 4].Text = sinhVien.TenKhoa;
                    worksheet[row, 5].Text = sinhVien.Email;
                    worksheet[row, 6].Text = sinhVien.SoDienThoai;

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhGiaoVien.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        // Handle Search
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text.ToLower();
            if (txt_search == "")
            {
                dataGridGiaoVien.ItemsSource = ObservableGiaoVien;
            }
            else
            {
                dataGridGiaoVien.ItemsSource = ObservableGiaoVien.Where(x =>
                    x.IdGiaoVien.ToLower().Contains(txt_search) ||
                    x.TenGiaoVien.ToLower().Contains(txt_search) ||
                    x.Email.ToLower().Contains(txt_search) ||
                    x.SoDienThoai.ToLower().Contains(txt_search) ||
                    x.IdKhoa.ToLower().Contains(txt_search) ||
                    x.TenKhoa.ToLower().Contains(txt_search)
                );
            }
        }


        // Add new GiaoVien
        private void AddGiaoVien(object sender, RoutedEventArgs e)
        {
            var addGiaoVienWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddGiaoVien();
            addGiaoVienWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddGiaoVienWithFile(object sender, RoutedEventArgs e)
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
                    List<GiaoVienDto> list_giao_vien = new List<GiaoVienDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một môn học với định dạng "Mã Môn Học, Tên Môn Học, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 6)
                        {
                            list_giao_vien.Add(new GiaoVienDto
                            {
                                IdGiaoVien = data[0],
                                TenGiaoVien = data[1],
                                Email = data[2],
                                SoDienThoai = data[3],
                                IdKhoa = data[4],
                                TenKhoa = data[5]
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách môn học từ file CSV trong repository
                        var response = await identityRepository
                            .CreateListGiaoVienFromCSV(list_giao_vien);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi môn học bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(gv => gv.TenGiaoVien));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách giáo viên từ file CSV: " + string.Join(", ", list_giao_vien.Select(x => x.TenGiaoVien)) + " thành công!");

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
