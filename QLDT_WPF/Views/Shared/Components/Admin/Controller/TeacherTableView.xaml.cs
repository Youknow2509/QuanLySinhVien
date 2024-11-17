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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for TeacherTableView.xaml
    /// </summary>
    public partial class TeacherTableView : UserControl
    {
        // Variables
        private GiaoVienRepository giaoVienRepository;
        public ObservableCollection<GiaoVienDto> ObservableGiaoVien { get; private set; }

        // Constructor
        public TeacherTableView()
        {
            InitializeComponent();
            giaoVienRepository = new GiaoVienRepository();
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
            // TODO
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

        // Edit GiaoVien
        private void Click_Edit_GiaoVien(object sender, RoutedEventArgs e)
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
