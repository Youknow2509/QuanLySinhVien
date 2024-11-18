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
    /// Interaction logic for EditLopHocPhan.xaml
    /// </summary>
    public partial class EditLopHocPhan : Window
    {
        // Variable
        private LopHocPhanDto lopHocPhan;

        private LopHocPhanRepository lopHocPhanRepository;
        private MonHocRepository monHocRepository;
        private GiaoVienRepository giaoVienRepository;

        private List<MonHocDto> list_mon_hoc;
        private List<GiaoVienDto> list_giao_vien;

        // Constructor
        public EditLopHocPhan(LopHocPhanDto l)
        {
            InitializeComponent();

            lopHocPhan = l;

            lopHocPhanRepository = new LopHocPhanRepository();
            monHocRepository = new MonHocRepository();
            giaoVienRepository = new GiaoVienRepository();

            list_mon_hoc = new List<MonHocDto>();
            list_giao_vien = new List<GiaoVienDto>();

            // Handle loading asynchronously init
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Async init view
        private async Task InitAsync()
        {
            // Set values
            txtIdLopHocPhan.Text = lopHocPhan.IdLopHocPhan;
            txtTenLopHocPhan.Text = lopHocPhan.TenLopHocPhan;
            dpThoiGianBatDau.SelectedDate = lopHocPhan.ThoiGianBatDau;
            dpThoiGianKetThuc.SelectedDate = lopHocPhan.ThoiGianKetThuc;

            // Load list mon hoc and set value for cbbMonHoc
            var request_lis_mon_hoc = await monHocRepository.GetAll();
            if (request_lis_mon_hoc.Status == false)
            {
                MessageBox.Show(request_lis_mon_hoc.Message);
                return;
            }
            list_mon_hoc = request_lis_mon_hoc.Data;
            foreach (var item in list_mon_hoc)
            {
                cbbMonHoc.Items.Add(new ComboBoxItem
                {
                    Content = item.TenMonHoc,
                    Tag = item.IdMonHoc
                });
            }
            cbbMonHoc.SelectedValue = lopHocPhan.IdMonHoc;

            // Load list giao vien and set value for cbbGiangVien
            var request_list_giao_vien = await giaoVienRepository.GetAll();
            if (request_list_giao_vien.Status == false)
            {
                MessageBox.Show(request_list_giao_vien.Message);
                return;
            }
            list_giao_vien = request_list_giao_vien.Data;
            foreach (var item in list_giao_vien)
            {
                cbbGiangVien.Items.Add(new ComboBoxItem
                {
                    Content = item.TenGiaoVien,
                    Tag = item.IdGiaoVien
                });
            }
            cbbGiangVien.SelectedValue = lopHocPhan.IdGiaoVien;
        }

        // Handle close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Handle save button click
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string tenLopHocPhan = txtTenLopHocPhan.Text.Trim();
            string idMonHoc = (cbbMonHoc.SelectedValue as ComboBoxItem).Tag.ToString();
            string idGiaoVien = (cbbGiangVien.SelectedValue as ComboBoxItem).Tag.ToString();
            DateTime? thoiGianBatDau = dpThoiGianBatDau.SelectedDate;
            DateTime? thoiGianKetThuc = dpThoiGianKetThuc.SelectedDate;
            string idLopHocPhan = txtIdLopHocPhan.Text.Trim();
            
            if (tenLopHocPhan == "" || idMonHoc == "-1" || idGiaoVien == "-1"
                || thoiGianBatDau == null || thoiGianKetThuc == null || idLopHocPhan == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
          

            LopHocPhanDto editLopHocPhan = new LopHocPhanDto
            {
                IdLopHocPhan = idLopHocPhan,
                TenLopHocPhan = tenLopHocPhan,
                IdMonHoc = idMonHoc,
                IdGiaoVien = idGiaoVien,
                ThoiGianBatDau = thoiGianBatDau,
                ThoiGianKetThuc = thoiGianKetThuc
            };

            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () => 
                    await lopHocPhanRepository.Edit(editLopHocPhan)
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
