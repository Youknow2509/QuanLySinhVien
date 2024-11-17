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
    /// Interaction logic for NguyenVongTableView.xaml
    /// </summary>
    public partial class NguyenVongTableView : UserControl
    {
        // Variables
        private NguyenVongSinhVienRepository nguyenVongSinhVienRepository;
        private NguyenVongGiaoVienRepository nguyenVongGiaoVienRepository;

        private ObservableCollection<NguyenVongSinhVienDto> observable_sinhvien;
        private ObservableCollection<NguyenVongThayDoiLichDto> observable_giaovien;

        // Constructor
        public NguyenVongTableView()
        {
            InitializeComponent();
            nguyenVongSinhVienRepository = new NguyenVongSinhVienRepository();
            nguyenVongGiaoVienRepository = new NguyenVongGiaoVienRepository();

            observable_sinhvien = new ObservableCollection<NguyenVongSinhVienDto>();
            observable_giaovien = new ObservableCollection<NguyenVongThayDoiLichDto>();

            //  Loaded asyn data
            Loaded += async (sender, e) =>
            {

            };
        }

        // Init async data
        private async Task InitAsyncData()
        {
            // Load data
            var list_nv_sv = await nguyenVongSinhVienRepository.GetAll();
            if (list_nv_sv.Status == false)
            {
                MessageBox.Show(list_nv_sv.Message);
                return;
            }

            var list_nv_gv = await nguyenVongGiaoVienRepository.GetAll();
            if (list_nv_gv.Status == false)
            {
                MessageBox.Show(list_nv_gv.Message);
                return;
            }

            // Add data to observable collection
            for (int i = 0; i < list_nv_sv.Data.Count; i++)
            {
                observable_sinhvien.Add(list_nv_sv.Data[i]);
            }
            for (int i = 0; i < list_nv_gv.Data.Count; i++)
            {
                observable_giaovien.Add(list_nv_gv.Data[i]);
            }

            // Set data to table
            sfDataGridSinhVien.ItemsSource = observable_sinhvien;
            sfDataGridGiaoVien.ItemsSource = observable_giaovien;
        }
    }
}
