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
            foreach(var item in list_nv_gv.Data)
            {
                nguynv_collection.Add(new NguyenVongThayDoiLichDto
                {
                    IdDangKyDoiLich = item.IdDangKyDoiLich,
                    IdThoiGian = item.IdThoiGian,
                    IdLopHocPhan = item.IdLopHocPhan,
                    ThoiGianBatDauHienTai = item.ThoiGianBatDauHienTai,
                    ThoiGianKetThucHienTai = item.ThoiGianKetThucHienTai,
                    ThoiGianBatDauMoi = item.ThoiGianBatDauMoi,
                    ThoiGianKetThucMoi = item.ThoiGianKetThucMoi,
                    TrangThai = item.TrangThai,
                    StatusMessage = (item.TrangThai == 0 ) ? "Từ chối" : (item.TrangThai == 1) ? "Đã xác nhận" : "Chờ xác nhận",
                    TenLopHocPhan = item.TenLopHocPhan
                });
            }
            NguyenVongDataGrid.ItemsSource = nguynv_collection;
        }
    }
}
