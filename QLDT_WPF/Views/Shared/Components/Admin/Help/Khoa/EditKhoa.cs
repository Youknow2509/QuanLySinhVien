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
    /// Interaction logic for EditKhoa.xaml
    /// </summary>
    public partial class EditKhoa : Window
    {
        // Variable
        private KhoaRepository khoaRepository;
        private KhoaDto khoa;

        // Constructor
        public EditKhoa(KhoaDto k)
        {
            InitializeComponent();
            khoa = k;
            khoaRepository = new KhoaRepository();

            // Handle loading asynchronously init
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Async init view
        private async Task InitAsync()
        {
            txtEditTenKhoa.Text = khoa.TenKhoa;
            txtEditIdKhoa.Text = khoa.IdKhoa;
        }

        // Handle close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Handle save button click
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string tenKhoa = txtEditTenKhoa.Text.Trim();
            string idKhoa = txtEditIdKhoa.Text.Trim();

            if (tenKhoa == "" || idKhoa == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            KhoaDto khoaEdit = new KhoaDto
            {
                IdKhoa = idKhoa,
                TenKhoa = tenKhoa
            };

            // Update khoa
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await khoaRepository.Update(khoaEdit)
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
