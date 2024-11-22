using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLDT_WPF.Views.Shared.Components.GiaoVien.View
{
    /// <summary>
    /// Interaction logic for NguyenVongTableView.xaml
    /// </summary>
    public partial class NguyenVongTableView : UserControl
    {
        private UserInformation userInformation;

        private string idGiaoVien;

        private NguyenVongGiaoVienRepository nguyenVongGiaoVienRepository;

        public ObservableCollection<NguyenVongThayDoiLichDto> nguynv_collection { get; set; }


        public NguyenVongTableView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            idGiaoVien = userInformation.UserName;

            nguyenVongGiaoVienRepository = new NguyenVongGiaoVienRepository();

            nguynv_collection = new ObservableCollection<NguyenVongThayDoiLichDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        public NguyenVongTableView()
        {
            InitializeComponent();
        }

        private async Task InitAsync()
        {

            nguyenVongGiaoVienRepository = new NguyenVongGiaoVienRepository();

            nguynv_collection = new ObservableCollection<NguyenVongThayDoiLichDto>();

            await load_nguyen_vong_giao_vien();
        }

        private async Task load_nguyen_vong_giao_vien()
        {
            nguynv_collection.Clear();
            var list_nv_gv = await nguyenVongGiaoVienRepository.GetByGiaoVienId(idGiaoVien);
            if (list_nv_gv.Status == false)
            {
                MessageBox.Show(list_nv_gv.Message);
                return;
            }
            for (int i = 0; i < list_nv_gv.Data.Count; i++)
            {
                nguynv_collection.Add(list_nv_gv.Data[i]);
            }
            NguyenVongDataGrid.ItemsSource = nguynv_collection;
        }
    }
}
