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
            dataGridStudents.ItemsSource = ObservableSinhVien;
        }
    }
}
