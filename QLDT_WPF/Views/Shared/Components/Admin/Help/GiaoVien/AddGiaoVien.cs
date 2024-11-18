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
    /// Interaction logic for AddGiaoVien.xaml
    /// </summary>
    public partial class AddGiaoVien : Window
    {
        // Variables
        private KhoaRepository khoaRepository;
        private IdentityRepository identityRepository;

        private List<KhoaDto> khoaDtos;

        // Constructor
        public AddGiaoVien()
        {
            InitializeComponent();

            khoaRepository = new KhoaRepository();
            identityRepository = new IdentityRepository();

            // init select box khoa
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var req = await khoaRepository.GetAll();
            if (req.Status == false)
            {
                MessageBox.Show(req.Message);
                return;
            }
            khoaDtos = req.Data;
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
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from input fields
            string maGiaoVien = txtMaGiaoVien.Text.Trim();
            string tenGiaoVien = txtTenGiaoVien.Text.Trim();
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string khoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Content.ToString();
            string idKhoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (idKhoa == "-1" || maGiaoVien == "" || tenGiaoVien == "" 
                || email == "" || soDienThoai == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Create new Giao Vien 
            GiaoVienDto newGiaoVien = new GiaoVienDto
            {
                IdGiaoVien = maGiaoVien,
                TenGiaoVien = tenGiaoVien,
                Email = email,
                SoDienThoai = soDienThoai,
                IdKhoa = idKhoa,
                TenKhoa = khoa
            };
            // Add new giao vien to database
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () => 
                    await identityRepository.CreateGiaoVienUser(newGiaoVien, "123456789")
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
