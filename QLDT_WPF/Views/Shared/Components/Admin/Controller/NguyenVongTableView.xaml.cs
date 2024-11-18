using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;
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
using System.Windows.Shapes;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for NguyenVongTableView.xaml
    /// </summary>
    public partial class NguyenVongTableView : UserControl
    {
        // Variables
        private NguyenVongSinhVienRepository nguyenVongSinhVienRepository;
        private NguyenVongGiaoVienRepository nguyenVongGiaoVienRepository;

        private ObservableCollection<NguyenVongSinhVienDto> observable_sinhvien;
        private ObservableCollection<NguyenVongThayDoiLichDto> observable_giaovien;

        // Constructor
        public NguyenVongTableView()
        {
            InitializeComponent();
            nguyenVongSinhVienRepository = new NguyenVongSinhVienRepository();
            nguyenVongGiaoVienRepository = new NguyenVongGiaoVienRepository();

            observable_sinhvien = new ObservableCollection<NguyenVongSinhVienDto>();
            observable_giaovien = new ObservableCollection<NguyenVongThayDoiLichDto>();

            //  Loaded asyn data
            Loaded += async (sender, e) =>
            {
                await InitAsyncData();
            };
        }

        // Init async data
        private async Task InitAsyncData()
        {
            observable_sinhvien.Clear();
            observable_giaovien.Clear();
            await load_nguyen_vong_sinh_vien();
            await load_nguyen_vong_giao_vien();
        }

        private async Task load_nguyen_vong_sinh_vien()
        {
            var list_nv_sv = await nguyenVongSinhVienRepository.GetAll();
            if (list_nv_sv.Status == false)
            {
                MessageBox.Show(list_nv_sv.Message);
                return;
            }
            for (int i = 0; i < list_nv_sv.Data.Count; i++)
            {
                observable_sinhvien.Add(list_nv_sv.Data[i]);
            }
            sfDataGridSinhVien.ItemsSource = observable_sinhvien;
        }

        private async Task load_nguyen_vong_giao_vien()
        {
            var list_nv_gv = await nguyenVongGiaoVienRepository.GetAll();
            if (list_nv_gv.Status == false)
            {
                MessageBox.Show(list_nv_gv.Message);
                return;
            }
            for (int i = 0; i < list_nv_gv.Data.Count; i++)
            {
                observable_giaovien.Add(list_nv_gv.Data[i]);
            }
            sfDataGridGiaoVien.ItemsSource = observable_giaovien;
        }

        // Xu li chap nhan yeu cau tu sinh vien
        private void ApproveRequest_Student(object sender, RoutedEventArgs e)
        {
            // Get data in tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongSinhVienDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }

            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongSinhVienRepository.Accpet(nv.IdNguyenVong)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    load_nguyen_vong_sinh_vien();
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

        // Xu li khong chap nhan yeu cau tu sinh vien
        private void RejectRequest_Student(object sender, RoutedEventArgs e)
        {
            // Get data in tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongSinhVienDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }

            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongSinhVienRepository.Reject(nv.IdNguyenVong)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reload data table
                    load_nguyen_vong_sinh_vien();
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

        // Xu li chap nhan yeu cau tu giao vien
        private void ApproveRequest_Teacher(object sender, RoutedEventArgs e)
        {
            // Get data in tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongThayDoiLichDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }

            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongGiaoVienRepository.Accpet(nv.IdDangKyDoiLich)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reload data table
                    load_nguyen_vong_giao_vien();
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

        // Xu li khong chap nhan yeu cau tu giao vien
        private void RejectRequest_Teacher(object sender, RoutedEventArgs e)
        {
            // Get data in tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongThayDoiLichDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }

            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongGiaoVienRepository.Reject(nv.IdDangKyDoiLich)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reload data table
                    load_nguyen_vong_giao_vien();
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

        // Xoa yeu cau tu sinh vien
        private void DeleteRequest_Student(object sender, RoutedEventArgs e)
        {
            // get data from tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongSinhVienDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }
            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongSinhVienRepository.Delete(nv.IdNguyenVong)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reload data table
                    load_nguyen_vong_sinh_vien();
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

        // Xoa yeu cau tu giao vien
        private void DeleteRequest_Teacher(object sender, RoutedEventArgs e)
        {
            // get data from tag
            var btn = sender as Button;
            var nv = btn.Tag as NguyenVongSinhVienDto;
            if (nv == null)
            {
                MessageBox.Show("Không thể lấy dữ liệu từ phần tử");
                return;
            }
            // Hanndle data
            try
            {
                // Sử dụng Task.Run để chạy hàm bất đồng bộ và đợi kết quả
                var response = Task.Run(async () =>
                    await nguyenVongGiaoVienRepository.Delete(nv.IdNguyenVong)
                ).Result;

                // Kiểm tra kết quả trả về
                if (response.Status == true)
                {
                    MessageBox.Show(response.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Reload data table
                    load_nguyen_vong_giao_vien();
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
