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
    /// Interaction logic for AddSinhVien.xaml
    /// </summary>
    public partial class AddSinhVien : Window
    {
        // Variables
        private KhoaRepository khoaRepository;
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        private IdentityRepository identityRepository;

        private List<KhoaDto> khoaDtos;
        private List<ChuongTrinhHocDto> chuongTrinhHocDtos;

        // Constructor
        public AddSinhVien()
        {
            InitializeComponent();

            khoaRepository = new KhoaRepository();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();
            identityRepository = new IdentityRepository();

            khoaDtos = new List<KhoaDto>();
            chuongTrinhHocDtos = new List<ChuongTrinhHocDto>();

            // init 
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            // Load cbbKhoa
            var req_khoa = await khoaRepository.GetAll();
            if (req_khoa.Status == false)
            {
                MessageBox.Show(req_khoa.Message);
                return;
            }
            khoaDtos = req_khoa.Data;
            foreach (var khoa in khoaDtos)
            {
                cbbKhoa.Items.Add(
                    new ComboBoxItem
                    {
                        Content = khoa.TenKhoa,
                        Tag = khoa.IdKhoa
                    }
                );
            }

            // Load cbbChuongTrinhHoc
            var req_chuongTrinhHoc = await chuongTrinhHocRepository.GetAll();
            if (req_chuongTrinhHoc.Status == false)
            {
                MessageBox.Show(req_chuongTrinhHoc.Message);
                return;
            }
            chuongTrinhHocDtos = req_chuongTrinhHoc.Data;
            foreach (var chuongTrinhHoc in chuongTrinhHocDtos)
            {
                cbbChuongTrinhHoc.Items.Add(
                    new ComboBoxItem
                    {
                        Content = chuongTrinhHoc.TenChuongTrinhHoc,
                        Tag = chuongTrinhHoc.IdChuongTrinhHoc
                    }
                );
            }

            // load time now
            dpNgaySinh.SelectedDate = DateTime.Now;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string tenSinhVien = txtTenSinhVien.Text.Trim();
            string maSinhVien = txtMaSinhVien.Text.Trim();
            string lop = txtLop.Text.Trim();
            DateTime? ngaySinh = dpNgaySinh.SelectedDate;
            string idKhoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Tag.ToString();
            string idChuongTrinhHoc = (cbbChuongTrinhHoc.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (idKhoa == "-1" || idChuongTrinhHoc == "-1" || tenSinhVien == "" || maSinhVien == "" || lop == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }

            // Create new SinhVien
            SinhVienDto newSinhVien = new SinhVienDto
            {
                IdSinhVien = maSinhVien,
                HoTen = tenSinhVien,
                Lop = lop,
                NgaySinh = ngaySinh,
                IdKhoa = idKhoa,
                IdChuongTrinhHoc = idChuongTrinhHoc
            };

            // Call API to create new SinhVien
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () => 
                    await identityRepository.CreateSinhVienUser(newSinhVien, "123456789")
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Đóng cửa sổ nếu thêm thành công
                }
                else
                {
                    MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (AggregateException ex)
            {
                // Xử lý ngoại lệ bất đồng bộ
                foreach (var innerEx in ex.InnerExceptions)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {innerEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
