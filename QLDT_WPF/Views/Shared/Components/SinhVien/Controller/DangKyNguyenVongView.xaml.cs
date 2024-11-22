using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
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

namespace QLDT_WPF.Views.Shared.Components.SinhVien.View
{
    /// <summary>
    /// Interaction logic for DangKyNguyenVongView.xaml
    /// </summary>
    public partial class DangKyNguyenVongView : UserControl
    {
        private UserInformation userInformation;

        private string idSinhVien;

        private NguyenVongSinhVienRepository nguyenVongsinhVienRepository;
        private DiemRepository diemRepository;

        public ObservableCollection<DiemDto> diem_collection { get; set; }

        public ObservableCollection<NguyenVongSinhVienDto> nguynv_collection { get; set; }


        public DangKyNguyenVongView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            idSinhVien = userInformation.UserName;

            nguyenVongsinhVienRepository = new NguyenVongSinhVienRepository();

            diemRepository = new DiemRepository();

            nguynv_collection = new ObservableCollection<NguyenVongSinhVienDto>();

            diem_collection = new ObservableCollection<DiemDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        public DangKyNguyenVongView()
        {
            InitializeComponent();
        }

        private async Task InitAsync()
        {

            nguyenVongsinhVienRepository = new NguyenVongSinhVienRepository();

            nguynv_collection = new ObservableCollection<NguyenVongSinhVienDto>();



            diemRepository = new DiemRepository();

            await load_nguyen_vong_sinh_vien();
            await load_nguyen_vong_sinh_vien_co_the_dang_ky();
        }

        private async Task load_nguyen_vong_sinh_vien()
        {
            nguynv_collection.Clear();
            var list_nv_gv = await nguyenVongsinhVienRepository.GetByIdSinhVien(idSinhVien);
            if (list_nv_gv.Status == false)
            {
                MessageBox.Show(list_nv_gv.Message);
                return;
            }
            foreach(var item in list_nv_gv.Data)
            {
                nguynv_collection.Add(new NguyenVongSinhVienDto
                {
                    IdSinhVien = item.IdSinhVien,
                    IdMonHoc = item.IdMonHoc,
                    TenMonHoc = item.TenMonHoc,
                    IdNguyenVong = item.IdNguyenVong,
                    TenSinhVien = item.TenSinhVien,
                    TrangThai = item.TrangThai,
                });
            }
            NguyenVongDataGrid.ItemsSource = nguynv_collection;
        }

        private async Task load_nguyen_vong_sinh_vien_co_the_dang_ky()
        {
            var list_nv = await diemRepository.GetDiemDangKyNguyenVongFromSinhVien(idSinhVien);
            if (list_nv.Status == false)
            {
                MessageBox.Show(list_nv.Message);
                return;
            }
            diem_collection.Clear();
            foreach (var item in list_nv.Data)
            {
                diem_collection.Add(new DiemDto
                {
                    IdDiem = item.IdDiem,
                    IdSinhVien = item.IdSinhVien,
                    IdMon = item.IdMon,
                    TenMonHoc = item.TenMonHoc,
                    DiemQuaTrinh = item.DiemQuaTrinh,
                    DiemKetThuc = item.DiemKetThuc,
                    DiemTongKet = item.DiemTongKet,
                    TenLopHocPhan = item.TenLopHocPhan,
                    LanHoc = item.LanHoc,
                });
            }
            HocPhanDataGrid.ItemsSource = diem_collection;
        }

        private async void DangKy_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ hàng hiện tại
                var button = sender as Button;
                if (button?.DataContext is DiemDto selectedHocPhan)
                {
                    // Tạo DTO để gửi API
                    var nguyenVongDto = new NguyenVongSinhVienDto
                    {
                        IdSinhVien = selectedHocPhan.IdSinhVien, // Thay thế bằng mã sinh viên hiện tại (lấy từ Binding hoặc Context)
                        IdMonHoc = selectedHocPhan.IdMon // Lấy từ dữ liệu hàng hiện tại
                    };

                    // Gọi API
                    var response = await nguyenVongsinhVienRepository.Add(nguyenVongDto);

                    // Xử lý phản hồi
                    if (response.Status == true)
                    {
                        MessageBox.Show(response.Message, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Cập nhật lại danh sách nguyện vọng sau khi đăng ký thành công
                        await load_nguyen_vong_sinh_vien();
                        await load_nguyen_vong_sinh_vien_co_the_dang_ky();
                    }
                    else
                    {
                        MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Huy_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ hàng hiện tại
                var button = sender as Button;
                if (button?.DataContext is NguyenVongSinhVienDto selectedNguyenVong)
                {
                    // Kiểm tra trạng thái trước khi xử lý
                    if (selectedNguyenVong.TrangThai == -1)
                    {
                        string id = selectedNguyenVong.IdNguyenVong;
                        var response = await nguyenVongsinhVienRepository.Delete(id);

                        // Xử lý phản hồi
                        if (response.Status == true)
                        {
                            MessageBox.Show(response.Message, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Cập nhật lại danh sách nguyện vọng sau khi hủy thành công
                            load_nguyen_vong_sinh_vien();
                            load_nguyen_vong_sinh_vien_co_the_dang_ky();
                        }
                        else
                        {
                            MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        // Không thực hiện hành động nếu trạng thái khác -1
                        Button buttonHuy = sender as Button;
                        buttonHuy.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


}
