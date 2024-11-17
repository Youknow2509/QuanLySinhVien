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

        // Constructor
        public SinhVienTableView()
        {
            InitializeComponent();
            sinhVienRepository = new SinhVienRepository();
            ObservableSinhVien = new ObservableCollection<SinhVienDto>();

            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };

            // Initialize events
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

        // Handle page size change
        private void PageSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbPageSize.SelectedItem is ComboBoxItem selectedItem)
            {
                sfDataPager.PageSize = int.Parse(selectedItem.Content.ToString());
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

        // Delete SinhVien
        private void Click_Delete_SinhVien(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng MonHocDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is SinhVienDto sinhVien)
            {
                string idSinhVien = sinhVien.IdSinhVien;
                string tenSinhVien = sinhVien.HoTen;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên '{tenSinhVien}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await sinhVienRepository.Delete(idSinhVien);

                            // Kiểm tra nếu việc xóa không thành công
                            if (response.Status == false)
                            {
                                // Nếu thất bại, hiển thị thông báo lỗi trên luồng UI
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                });
                                return;
                            }

                            // Nếu xóa thành công, tải lại dữ liệu trên luồng UI
                            Application.Current.Dispatcher.Invoke(async () =>
                            {
                                await InitAsync();
                            });
                        }
                        catch (Exception ex)
                        {
                            // Xử lý bất kỳ ngoại lệ nào xảy ra trong quá trình xóa
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show($"Có lỗi xảy ra khi xóa sinh viên '{tenSinhVien}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }
    }
}
