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

namespace QLDT_WPF.Views.Shared.Components.SinhVien.View
{
    /// <summary>
    /// Interaction logic for LopHocPhanComponent.xaml
    /// </summary>
    public partial class LopHocPhanComponent : UserControl
    {
        private UserInformation userInformation;

        private string idSinhVien;
        private SinhVienDto sinhVienDto;

        private SinhVienRepository sinhVienRepository;
        private LopHocPhanRepository lopHocPhanRepository;

        public ObservableCollection<LopHocPhanDto> lhp_collection { get; set; }

        public LopHocPhanComponent()
        {
            InitializeComponent();
        }

        public LopHocPhanComponent(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;
            this.idSinhVien = userInformation.UserName;

            sinhVienRepository = new SinhVienRepository();
            lopHocPhanRepository = new LopHocPhanRepository();

            lhp_collection = new ObservableCollection<LopHocPhanDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            sinhVienRepository = new SinhVienRepository();
            lhp_collection = new ObservableCollection<LopHocPhanDto>();
            await Load_Class();
        }



            private async Task Load_Class()
        {
            var req_point = await lopHocPhanRepository.GetLopHocPhansFromSinhVien(idSinhVien);
            if (req_point.Status == false)
            {
                MessageBox.Show("Không tìm thấy lớp của sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            lhp_collection.Clear();
            foreach (var it in req_point.Data)
            {
                lhp_collection.Add(new LopHocPhanDto
                {
                    TenLopHocPhan = it.TenLopHocPhan,
                    TenMonHoc = it.TenMonHoc,
                    TenGiaoVien = it.TenGiaoVien,

                });
            }

            LopHocPhanDataGrid.ItemsSource = lhp_collection;
        }

    }
}
