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
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using Syncfusion.XlsIO;

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

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {

        }
    }
}
