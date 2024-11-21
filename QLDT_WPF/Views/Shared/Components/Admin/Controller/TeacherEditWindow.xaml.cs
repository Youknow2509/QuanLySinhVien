using System;
using System.Collections.Generic;
using System.IO;
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
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class TeacherEditWindow : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(TeacherEditWindow), new PropertyMetadata(null));

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }
        // Variables
        private GiaoVienDto giaoVien;
        private byte[] avatar_save_temp;

        private GiaoVienRepository giaoVienRepository;
        private IdentityRepository identityRepository;
        private KhoaRepository khoaRepository;
        private LopHocPhanRepository lopHocPhanRepository;

        // Constructor
        public TeacherEditWindow(GiaoVienDto gv)
        {
            InitializeComponent();

            // Set var constructor
            giaoVien = gv;

            // Init repository
            giaoVienRepository = new GiaoVienRepository();
            identityRepository = new IdentityRepository();
            khoaRepository = new KhoaRepository();
            lopHocPhanRepository = new LopHocPhanRepository();

            Loaded += async (s, e) =>
            {
                if (TargetContentArea == null)
                {
                    var parentWindow = FindParent<Window>(this); // Tìm parent window
                    if (parentWindow != null)
                    {
                        var contentArea = parentWindow.FindName("ContentArea") as ContentControl; // Tìm ContentArea
                        if (contentArea != null)
                        {
                            TargetContentArea = contentArea;
                        }
                        else
                        {
                            TargetContentArea = new ContentControl();
                        }
                    }
                    else
                    {
                        TargetContentArea = new ContentControl();
                    }
                }

                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            // Set value
            await SetInfomationUser();

            // Get avatar
            await GetAvatar_Set();

        }

        // Set value
        private async Task SetInfomationUser()
        {
            txtFullName.Text = giaoVien.TenGiaoVien;
            txtEmail.Text = giaoVien.Email;
            txtPhoneNumber.Text = giaoVien.SoDienThoai;

            // handle cbb khoa
            var req_kh = await khoaRepository.GetAll();
            if (req_kh.Status == false)
            {
                MessageBox.Show("Lỗi: " + req_kh.Message);
                return;
            }

            foreach (var khoa in req_kh.Data)
            {
                cbbKhoa.Items.Add(
                    new ComboBoxItem
                    {
                        Content = khoa.TenKhoa,
                        Tag = khoa.IdKhoa
                    }
                );
            }
            cbbKhoa.DisplayMemberPath = "TenKhoa";
            cbbKhoa.SelectedValuePath = "IdKhoa";
            cbbKhoa.SelectedValue = giaoVien.IdKhoa;

            // Neu Giao Vien Dang Ton Tai Lop Hoc Phan Dang Dien Ra, Hoac Dien Ra Trong Tuong Lai
            var req_check_thay_doi_khoa =
                await lopHocPhanRepository.CheckLopHocPhanGiaoVien(giaoVien.IdGiaoVien);
            if (req_check_thay_doi_khoa.Status == false)
            {
                MessageBox.Show("Lỗi: " + req_check_thay_doi_khoa.Message);
                return;
            }

            if (req_check_thay_doi_khoa.Data == true)
            {
                cbbKhoa.IsEnabled = false;
            }
        }

        // Set avatar 
        private async Task GetAvatar_Set()
        {
            var req_avt = await identityRepository.GetAvatar(giaoVien.IdGiaoVien);
            if (req_avt.Status == false)
            {
                MessageBox.Show("Không tìm thấy ảnh đại diện của giáo viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] imageBytes = req_avt.Data;
            if (imageBytes == null || imageBytes.Length == 0)
            {
                MessageBox.Show("Dữ liệu ảnh không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Convert byte array to an image
            using (var stream = new System.IO.MemoryStream(imageBytes))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Load image into memory
                bitmap.StreamSource = stream;
                bitmap.EndInit();

                // Assign the bitmap to the ImageBrush
                if (AvatarImageControl.Fill is ImageBrush imageBrush)
                {
                    imageBrush.ImageSource = bitmap;
                }
                else
                {
                    // If the Fill is not already an ImageBrush, create one
                    AvatarImageControl.Fill = new ImageBrush(bitmap) { Stretch = Stretch.UniformToFill };
                }
            }
        }

        // handle click Click_Choose_File - upload temp and show in avarta
        private void Click_Choose_File(object sender, RoutedEventArgs e)
        {
            // Open a file dialog for selecting an image file
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn Một Hình Ảnh"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Read the selected file as a byte array
                    byte[] imageBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                    // Check if the file size exceeds 2MB (2 * 1024 * 1024 bytes)
                    const long maxFileSize = 2 * 1024 * 1024; // 2MB in bytes
                    if (imageBytes.Length > maxFileSize)
                    {
                        MessageBox.Show("File Được Chọn Phải Có Kích Thước Nhỏ Hơn 2 MB.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Validate the file (you can add additional checks if necessary)
                    if (imageBytes == null || imageBytes.Length == 0)
                    {
                        MessageBox.Show("File Được Chọn Không Phải Là Một Ảnh", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    this.avatar_save_temp = imageBytes;

                    // Load the image into a BitmapImage
                    using (var stream = new System.IO.MemoryStream(imageBytes))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // Load image into memory
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();

                        // Assign the image to the Avatar control
                        if (AvatarImageControl.Fill is ImageBrush imageBrush)
                        {
                            imageBrush.ImageSource = bitmap;
                        }
                        else
                        {
                            AvatarImageControl.Fill = new ImageBrush(bitmap) { Stretch = Stretch.UniformToFill };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while processing the image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // handle click SaveImage - save image to database
        private async void SaveImage(object sender, RoutedEventArgs e)
        {
            if (avatar_save_temp == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh trước khi lưu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var req = await identityRepository.SetAvatar(giaoVien.IdGiaoVien, avatar_save_temp);
            if (req.Status == false)
            {
                MessageBox.Show("Lưu ảnh thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Lưu ảnh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            // Reload avatar
            await GetAvatar_Set();
        }

        // hadnle click Save_In4 - save information
        private async void Save_In4(object sender, RoutedEventArgs e)
        {
            string tenGiaoVien = txtFullName.Text;
            string email = txtEmail.Text;
            string soDienThoai = txtPhoneNumber.Text;
            string idKhoa = (cbbKhoa.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (string.IsNullOrEmpty(tenGiaoVien) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(idKhoa))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check regex email
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Check regex phone number
            if (!System.Text.RegularExpressions.Regex.IsMatch(soDienThoai, @"^0[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            // update
            GiaoVienDto giaoVienEdit = new GiaoVienDto{
                IdGiaoVien = giaoVien.IdGiaoVien,
                TenGiaoVien = tenGiaoVien,
                Email = email,
                SoDienThoai = soDienThoai,
                IdKhoa = idKhoa
            };

            var req = await identityRepository.UpgradeGiaoVien(giaoVienEdit);
            if (req.Status == false)
            {
                MessageBox.Show("Lưu thông tin thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Lưu thông tin thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            
            // reload
            giaoVien = giaoVienEdit;
            SetInfomationUser();
        }

        // handle click Save_Password - save password with root
        private async void Save_Password(object sender, RoutedEventArgs e)
        {
            string password = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var req = await identityRepository.AdminEditPasswordAdmin(giaoVien.IdGiaoVien, password);
            if (req.Status == false)
            {
                MessageBox.Show("Lưu mật khẩu thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Lưu mật khẩu thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear password
            txtNewPassword.Password = "";
            txtConfirmPassword.Password = "";
        }
    }
}
