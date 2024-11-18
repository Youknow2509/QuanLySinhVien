using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.Models;
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
    /// Interaction logic for AddChuongTrinhHoc.xaml
    /// </summary>
    public partial class AddChuongTrinhHoc : Window
    {
        // Variables
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        // Constructor
        public AddChuongTrinhHoc()
        {
            InitializeComponent();
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string tenChuongTrinhHoc = txtTenChuongTrinhHoc.Text.Trim();
            if (string.IsNullOrEmpty(tenChuongTrinhH)){
                MessageBox.Show("Tên chương trình học không được để trống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Create new chuong trinh hoc
            ChuongTrinhHocDto newChuongTrinhHoc = new ChuongTrinhHocDto{
                TenChuongTrinhHoc = tenChuongTrinhHoc,
                IdChuongTrinhHoc = Guid.NewGuid().ToString()
            };

            // Add new chuong trinh hoc to database
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () => await chuongTrinhHocRepository.Add(newChuongTrinhHoc)).Result;

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
