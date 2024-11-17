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

        }


        // Add new Khoa
        private void AddKhoa(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Add new Lop Hoc Phan With File
        private void AddKhoaWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
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
