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

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for SubjectsTableView.xaml
    /// </summary>
    public partial class SubjectsTableView : UserControl
    {
        // Variables
        private MonHocRepository monHocRepository;
        public ObservableCollection<MonHocDto> ObservableMonHoc { get; private set; }

        // Constructor
        public SubjectsTableView()
        {
            InitializeComponent();
            monHocRepository = new MonHocRepository();
            ObservableMonHoc = new ObservableCollection<MonHocDto>();
            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list = await monHocRepository.GetAll();
            if (list.Status == false)
            {
                MessageBox.Show(list.Message);
                return;
            }
            ObservableMonHoc.Clear();
            foreach (var item in list.Data)
            {
                ObservableMonHoc.Add(item);
            }
            dataGridMonHoc.ItemsSource = ObservableMonHoc;
        }

        // TextBox TextChanged Event Handler - Handle search when changle value 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text;
            if (txt_search == "")
            {
                dataGridMonHoc.ItemsSource = ObservableMonHoc;
            }
            else
            {
                dataGridMonHoc.ItemsSource = ObservableMonHoc.Where(x => 
                    x.TenMonHoc.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenKhoa.ToLower().Contains(txt_search.ToLower()) ||
                    x.SoTinChi.ToString().Contains(txt_search) ||
                    x.SoTietHoc.ToString().Contains(txt_search)
                );
            }
        }


    }
}
