using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
// Your namespaces
using QLDT_WPF.Repositories;
using QLDT_WPF.Dto;

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
            // TODO
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
