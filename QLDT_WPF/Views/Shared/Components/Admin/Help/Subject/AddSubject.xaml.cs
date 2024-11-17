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
    /// Interaction logic for AddSubject.xaml
    /// </summary>
    public partial class AddSubject : Window
    {
        // Variables
        private KhoaRepository khoaRepository;
        private List<KhoaDto> khoaDtoList;
        private MonHocRepository monHocRepository;

        // Constructor
        public AddSubject()
        {
            InitializeComponent();

            khoaRepository = new KhoaRepository();
            monHocRepository = new MonHocRepository();

            khoaDtoList = new List<KhoaDto>();
            // init select box khoa
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var rep = await khoaRepository.GetAll();
            if (rep.Status == false)
            {
                MessageBox.Show(rep.Message);
                return;
            }
            khoaDtoList = rep.Data;
            foreach (var khoa in khoaDtoList)
            {
                cbbKhoa.Items.Add(
                    new ComboBoxItem { 
                        Content = khoa.TenKhoa ,
                        Tag = khoa.IdKhoa
                    });
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from input fields
            string tenMonHoc = txtTenMonHoc.Text;
            int.TryParse(txtSoTinChi.Text, out int soTinChi);
            int.TryParse(txtSoTiet.Text, out int soTiet);
            string khoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Content.ToString();
            string idKhoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (tenMonHoc == "" || soTinChi == 0 || soTiet == 0 || idKhoa == "-1")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Create new subject
            MonHocDto newMonHoc = new MonHocDto
            {
                IdMonHoc = Guid.NewGuid().ToString(),
                TenMonHoc = tenMonHoc,
                SoTinChi = soTinChi,
                SoTietHoc = soTiet,
                TenKhoa = khoa,
                IdKhoa = idKhoa
            };

            // Add new subject to database
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(() => monHocRepository.Add(newMonHoc)).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == false)
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
