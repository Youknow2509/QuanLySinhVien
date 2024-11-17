using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        public ObservableCollection<SinhVienDto> FilteredSinhVien { get; private set; }

        // Constructor
        public SinhVienTableView()
        {
            InitializeComponent();
            sinhVienRepository = new SinhVienRepository();
            ObservableSinhVien = new ObservableCollection<SinhVienDto>();
            FilteredSinhVien = new ObservableCollection<SinhVienDto>();

            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };

            // Initialize events
            txtTimKiem.TextChanged += SearchTextChanged;
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

            // Initialize filtered collection
            FilteredSinhVien = new ObservableCollection<SinhVienDto>(ObservableSinhVien);

            // Bind data to SfDataPager
            sfDataPager.Source = FilteredSinhVien;
            sfDataGrid.ItemsSource = sfDataPager.PagedSource;
        }

        // Handle search functionality
        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = txtTimKiem.Text.ToLower();

            // Filter data based on search text
            var filteredData = ObservableSinhVien.Where(s =>
                s.HoTen.ToLower().Contains(searchText) ||
                s.DiaChi.ToLower().Contains(searchText) ||
                s.TenSinhVien.ToLower().Contains(searchText)).ToList();

            FilteredSinhVien = new ObservableCollection<SinhVienDto>(filteredData);

            // Refresh pager source
            sfDataPager.Source = FilteredSinhVien;
        }

        // Handle page size change
        private void PageSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbPageSize.SelectedItem is ComboBoxItem selectedItem)
            {
                sfDataPager.PageSize = int.Parse(selectedItem.Content.ToString());
            }
        }

        // Edit
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sfDataGrid.SelectedItem is SinhVienDto sinhVien)
            {
                var window = new UserProfileWindow(sinhVien);
                window.ShowDialog();
            }
        }

        // Export SinhVien to Excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Handle Search
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        // Add new SinhVien
        private void AddSinhVien(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Add new Lop Hoc Phan With File
        private void AddSinhVienWithFile(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Edit SinhVien
        private void Click_Edit_SinhVien(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        // Delete SinhVien
        private void Click_Delete_SinhVien(object sender, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
