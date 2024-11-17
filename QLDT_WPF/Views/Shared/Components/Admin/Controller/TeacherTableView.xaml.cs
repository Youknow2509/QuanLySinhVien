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

        }


        // Add new GiaoVien
        private void AddGiaoVien(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Add new Lop Hoc Phan With File
        private void AddGiaoVienWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Delete GiaoVien
        private void Click_Delete_GiaoVien(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
