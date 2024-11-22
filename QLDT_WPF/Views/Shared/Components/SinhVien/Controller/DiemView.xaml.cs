using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
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
    /// Interaction logic for DiemView.xaml
    /// </summary>
    public partial class DiemView : UserControl
    {

        private UserInformation userInformation;

        private string idSinhVien;
        private SinhVienDto sinhVienDto;

        private SinhVienRepository sinhVienRepository;
        private DiemRepository diemRepository;

        public ObservableCollection<DiemDto> diem_collection { get; set; }

        public DiemView()
        {
            InitializeComponent();
        }

        public DiemView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;
            this.idSinhVien = userInformation.UserName;

            sinhVienRepository = new SinhVienRepository();
            diemRepository = new DiemRepository();

            diem_collection = new ObservableCollection<DiemDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            diemRepository = new DiemRepository();
            sinhVienRepository = new SinhVienRepository();
            diem_collection = new ObservableCollection<DiemDto>();
            await Load_Point();
        }



        private async Task Load_Point()
        {
            var req_point = await diemRepository.GetByIdSinhVien(idSinhVien);
            if (req_point.Status == false)
            {
                MessageBox.Show("Không tìm thấy điểm của sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            diem_collection.Clear();
            foreach (var it in req_point.Data)
            {
                diem_collection.Add(new DiemDto
                {
                    IdDiem = it.IdDiem,
                    IdSinhVien = it.IdSinhVien,
                    IdMon = it.IdMon,
                    IdLopHocPhan = it.IdLopHocPhan,
                    DiemKetThuc = it.DiemKetThuc,
                    DiemQuaTrinh = it.DiemQuaTrinh,
                    DiemTongKet = it.DiemTongKet,
                    TenSinhVien = it.TenSinhVien,
                    TenMonHoc = it.TenMonHoc,
                    TenLopHocPhan = it.TenLopHocPhan,
                    LanHoc = it.LanHoc,

                    TrangThai = it.DiemTongKet >= 4 ? "Qua Môn" : "Học Lại",
                });
            }

            ChuongTrinhHocDataGrid.ItemsSource = diem_collection;
        }



    }
}
