using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using System;
using System.Collections.Generic;
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

namespace QLDT_WPF.Views.Shared.Components.Admin.Help
{
    /// <summary>
    /// Interaction logic for AddLopHocPhan.xaml
    /// </summary>
    public partial class AddLopHocPhan : Window
    {
        // Variables
        private LopHocPhanRepository lopHocPhanRepository;
        private MonHocRepository monHocRepository;
        private GiaoVienRepository giaoVienRepository;

        private List<MonHocDto> list_mon_hoc;
        private List<GiaoVienDto> list_giao_vien;

        // Constructor
        public AddLopHocPhan()
        {
            InitializeComponent();

            lopHocPhanRepository = new LopHocPhanRepository();
            monHocRepository = new MonHocRepository();
            giaoVienRepository = new GiaoVienRepository();

            list_mon_hoc = new List<MonHocDto>();
            list_giao_vien = new List<GiaoVienDto>();

            // init select box khoa
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            // Set date now for dpThoiGianBatDau and dpThoiGianKetThuc
            dpThoiGianBatDau.SelectedDate = DateTime.Now;
            dpThoiGianKetThuc.SelectedDate = DateTime.Now;

            // Load list mon hoc
            var request_lis_mon_hoc = await monHocRepository.GetAll();
            if (request_lis_mon_hoc.Status == false)
            {
                MessageBox.Show(request_lis_mon_hoc.Message);
                return;
            }
            list_mon_hoc = request_lis_mon_hoc.Data;
            foreach (var item in list_mon_hoc)
            {
                cbMonHoc.Items.Add(new ComboBoxItem
                {
                    Content = item.TenMonHoc,
                    Tag = item.IdMonHoc
                });
            }
            // Load list giao vien
            var request_list_giao_vien = await giaoVienRepository.GetAll();
            if (request_list_giao_vien.Status == false)
            {
                MessageBox.Show(request_list_giao_vien.Message);
                return;
            }
            list_giao_vien = request_list_giao_vien.Data;
            foreach (var item in list_giao_vien)
            {
                cbGiaoVien.Items.Add(new ComboBoxItem
                {
                    Content = item.TenGiaoVien,
                    Tag = item.IdGiaoVien
                });
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO 
        }

    }
}
