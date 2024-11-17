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

        // Constructor
        public ChuongTrinhHocTableView()
        {
            InitializeComponent();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();
            ObservableChuongTrinhHoc = new ObservableCollection<ChuongTrinhHocDto>();

            // Hook up Loaded event to call async initialization after control loads
            Loaded += async (s, e) => await InitAsync();
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

        }


        // Add new ChuongTrinhHoc
        private void AddChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Add new Lop Hoc Phan With File
        private void AddChuongTrinhHocWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Edit ChuongTrinhHoc
        private void Click_Edit_ChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Delete ChuongTrinhHoc
        private void Click_Delete_ChuongTrinhHoc(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
