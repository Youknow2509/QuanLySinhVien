using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using Syncfusion.UI.Xaml.Scheduler;
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
    /// Interaction logic for SinhVienTableView.xaml
    /// </summary>
    public partial class SinhVienTableView : UserControl
    {
        private UserInformation userInformation;

        private string idGiaoVien;

        private GiaoVienRepository giaoVienRepository;

        public ObservableCollection<SinhVienDto> sinhvien_collection { get; set; }


        public SinhVienTableView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            idGiaoVien = userInformation.UserName;

            giaoVienRepository = new GiaoVienRepository();

            sinhvien_collection = new ObservableCollection<SinhVienDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        public SinhVienTableView()
        {
            InitializeComponent();
        }

        private async Task InitAsync()
        {

            giaoVienRepository = new GiaoVienRepository();

            sinhvien_collection = new ObservableCollection<SinhVienDto>();

            // Load Calendar
            await Load_SinhVien();
        }

        private async Task Load_SinhVien()
        {
            var req = await giaoVienRepository.GetSinhVienByGiaoVienId(idGiaoVien);
            if (req.Status == false)
            {
                MessageBox.Show("Không tìm thấy sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            sinhvien_collection.Clear();
            foreach (var item in req.Data)
            {
                sinhvien_collection.Add(
                    new SinhVienDto
                    {
                        IdSinhVien = item.IdSinhVien,
                        HoTen = item.HoTen,
                        NgaySinh = item.NgaySinh,
                        DiaChi = item.DiaChi,
                        Lop = item.Lop,
                        TenKhoa = item.TenKhoa,
                        TenChuongTrinhHoc = item.TenChuongTrinhHoc
                    }
                );
            }

            SinhVienDataGrid.ItemsSource = sinhvien_collection;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBox = sender as TextBox;
            var search = searchBox.Text;
            if (search == "")
            {
                SinhVienDataGrid.ItemsSource = sinhvien_collection;
            }
            else
            {
                SinhVienDataGrid.ItemsSource = sinhvien_collection.Where(x => x.HoTen.ToLower().Contains(search.ToLower()));
            }
        }
    }
}
