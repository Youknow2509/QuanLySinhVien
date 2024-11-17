using System.Windows;
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        private readonly SinhVienRepository _sinhVienRepository;
        public SinhVienDto SinhVien { get; private set; }

        // Constructor nhận dữ liệu SinhVienDto
        public UserProfileWindow(SinhVienDto sinhVien)
        {
            InitializeComponent();
            SinhVien = sinhVien;
            _sinhVienRepository = new SinhVienRepository();

            // Khởi tạo dữ liệu lên giao diện
            LoadData();
        }

        // Hàm khởi tạo dữ liệu lên giao diện
        private void LoadData()
        {
            if (SinhVien != null)
            {
                txtFullName.Text = SinhVien.HoTen;
                txtEmail.Text = SinhVien.Email;
                txtPhoneNumber.Text = SinhVien.SoDienThoai;
                txtAddress.Text = SinhVien.DiaChi;
            }
        }

        // Sự kiện khi bấm nút "Save changes"
        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (SinhVien != null)
            {
                // Cập nhật thông tin từ giao diện vào đối tượng SinhVien
                SinhVien.HoTen = txtFullName.Text;
                SinhVien.Email = txtEmail.Text;
                SinhVien.SoDienThoai = txtPhoneNumber.Text;
                SinhVien.DiaChi = txtAddress.Text;

                // Lưu thông tin vào cơ sở dữ liệu
                var response = await _sinhVienRepository.Edit(SinhVien);

                if (response.Status == true)
                {
                    MessageBox.Show("Changes saved successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Sự kiện khi bấm nút "Cancel"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        // ChangePassword_Click
        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // Todo change password
        }
    }
}
