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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for LopHocPhanTableView.xaml
    /// </summary>
    public partial class LopHocPhanTableView : UserControl
    {
        // Variables
        private LopHocPhanRepository lopHocPhanRepository;
        public ObservableCollection<LopHocPhanDto> ObservableKhoa { get; private set; }

        // Constructor
        public LopHocPhanTableView()
        {
            InitializeComponent();
            lopHocPhanRepository = new LopHocPhanRepository();
            ObservableKhoa = new ObservableCollection<LopHocPhanDto>();
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
                ObservableKhoa.Add(item);
            }

            // Bind to DataGrid or other UI components as needed
            dataGridLopHocPhan.ItemsSource = ObservableKhoa;
        }
    }
}
