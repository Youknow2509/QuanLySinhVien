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
    /// Interaction logic for EditChuongTrinhHoc.xaml
    /// </summary>
    public partial class EditChuongTrinhHoc : Window
    {
        // Variable
        private ChuongTrinhHocDto chuongTrinhHoc;
        private ChuongTrinhHocRepository chuongTrinhHocRepository;

        // Constructor
        public EditChuongTrinhHoc(ChuongTrinhHocDto chuongTrinhHocDto)
        {
            InitializeComponent();

            // Set value
            chuongTrinhHoc = chuongTrinhHocDto;
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();

            // Set giá trị hiện tại 
            txtEditTenChuongTrinhHoc.Text = chuongTrinhHoc.TenChuongTrinhHoc;
            txtEditIdChuongTrinhHoc.Text = chuongTrinhHoc.IdChuongTrinhHoc;
        }

        // Handle close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Handle save button click
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from input fields
            string id = txtEditIdChuongTrinhHoc.Text.Trim();
            string tenChuongTrinhHoc = txtEditTenChuongTrinhHoc.Text.Trim();
            if (id == "" || tenChuongTrinhHoc == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            ChuongTrinhHocDto editChuongTrinhHoc = new ChuongTrinhHocDto
            {
                IdChuongTrinhHoc = id,
                TenChuongTrinhHoc = tenChuongTrinhHoc
            };

            // Update chuong trinh hoc
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await chuongTrinhHocRepository.Update(editChuongTrinhHoc)
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
