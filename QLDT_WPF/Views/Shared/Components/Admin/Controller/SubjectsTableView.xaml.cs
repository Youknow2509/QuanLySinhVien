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
using System.Collections.ObjectModel;
using System.ComponentModel;
using QLDT_WPF.Models;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for SubjectsTableView.xaml
    /// </summary>
    public partial class SubjectsTableView : UserControl
    {
        // Variables
        private MonHocRepository monHocRepository;
        public ObservableCollection<MonHocDto> ObservableMonHoc { get; private set; }

        // Constructor
        public SubjectsTableView()
        {
            InitializeComponent();
            monHocRepository = new MonHocRepository();
            ObservableMonHoc = new ObservableCollection<MonHocDto>();
            // Handle loading asynchronously
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list = await monHocRepository.GetAll();
            if (list.Status == false)
            {
                MessageBox.Show(list.Message);
                return;
            }
            ObservableMonHoc.Clear();
            foreach (var item in list.Data)
            {
                ObservableMonHoc.Add(item);
            }
            dataGridMonHoc.ItemsSource = ObservableMonHoc;
        }

        // TextBox TextChanged Event Handler - Handle search when changle value 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text;
            if (txt_search == "")
            {
                dataGridMonHoc.ItemsSource = ObservableMonHoc;
            }
            else
            {
                dataGridMonHoc.ItemsSource = ObservableMonHoc.Where(x => 
                    x.TenMonHoc.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenKhoa.ToLower().Contains(txt_search.ToLower()) ||
                    x.SoTinChi.ToString().Contains(txt_search) ||
                    x.SoTietHoc.ToString().Contains(txt_search)
                );
            }
        }

        // Handle Add Subject Button Click
        private void AddSubject(object sender, RoutedEventArgs e)
        {
            var addSubjectWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddSubject();
            addSubjectWindow.ShowDialog();
            InitAsync();
        }

        // Hanle Edit Subject Button Click
        private void Click_Edit_Subject(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MonHocDto monHoc)
            {
                var editSubjectWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.EditSubject(monHoc);
                editSubjectWindow.ShowDialog();
                InitAsync();
            }
        }

        // Xử lý sự kiện khi nhấn nút Xóa Môn Học
        private void Click_Delete_Subject(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng MonHocDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is MonHocDto monHoc)
            {
                string idMonHoc = monHoc.IdMonHoc;
                string tenMonHoc = monHoc.TenMonHoc;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa môn học '{tenMonHoc}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await monHocRepository.Delete(idMonHoc);

                            // Kiểm tra nếu việc xóa không thành công
                            if (response.Status == false)
                            {
                                // Nếu thất bại, hiển thị thông báo lỗi trên luồng UI
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    MessageBox.Show(response.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                });
                                return;
                            }

                            // Nếu xóa thành công, tải lại dữ liệu trên luồng UI
                            Application.Current.Dispatcher.Invoke(async () =>
                            {
                                await InitAsync();
                            });
                        }
                        catch (Exception ex)
                        {
                            // Xử lý bất kỳ ngoại lệ nào xảy ra trong quá trình xóa
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show($"Có lỗi xảy ra khi xóa môn học '{tenMonHoc}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }
    }
}
