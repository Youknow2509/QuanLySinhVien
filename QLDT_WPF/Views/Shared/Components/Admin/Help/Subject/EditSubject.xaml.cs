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
    /// Interaction logic for EditSubject.xaml
    /// </summary>
    public partial class EditSubject : Window
    {
        // Variable
        private MonHocDto monHoc;
        private MonHocRepository monHocRepository;
        private KhoaRepository khoaRepository;

        // Constructor
        public EditSubject(MonHocDto monHocDto)
        {
            InitializeComponent();

            monHocRepository = new MonHocRepository();
            khoaRepository = new KhoaRepository();
            monHoc = monHocDto;

            // Handle loading asynchronously init
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Async init view
        private async Task InitAsync()
        {
            txtEditTenMonHoc.Text = monHoc.TenMonHoc;
            txtEditSoTinChi.Text = monHoc.SoTinChi.ToString();
            txtEditSoTiet.Text = monHoc.SoTietHoc.ToString();
            txtEditIdMonHoc.Text = monHoc.IdMonHoc;

            var listKhoa = await khoaRepository.GetAll();
            if (listKhoa.Status == false)
            {
                MessageBox.Show(listKhoa.Message);
                return;
            }
            foreach (var item in listKhoa.Data)
            {
                cbbEditKhoa.Items.Add(item);
            }
            cbbEditKhoa.DisplayMemberPath = "TenKhoa";
            cbbEditKhoa.SelectedValuePath = "IdKhoa";
            cbbEditKhoa.SelectedValue = monHoc.IdKhoa;
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
            string idMonHoc = txtEditIdMonHoc.Text;
            string tenMonHoc = txtEditTenMonHoc.Text;
            int.TryParse(txtEditSoTinChi.Text, out int soTinChi);
            int.TryParse(txtEditSoTiet.Text, out int soTiet);
            string idKhoa = cbbEditKhoa.SelectedValue.ToString() ?? string.Empty;

            if (idMonHoc == "" || tenMonHoc == "" || soTinChi == 0 || soTiet == 0 || idKhoa == "" || idKhoa == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            MonHocDto monHocDto = new MonHocDto
            {
                IdMonHoc = idMonHoc,
                TenMonHoc = tenMonHoc,
                SoTinChi = soTinChi,
                SoTietHoc = soTiet,
                IdKhoa = idKhoa
            };

            // Update mon hoc
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () => await monHocRepository.Update(monHocDto)).Result;

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

        // Only allow integer input in text box
        private void handle_input_key_press_number(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
