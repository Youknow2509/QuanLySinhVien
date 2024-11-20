using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.XlsIO;
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
using Microsoft.Win32;
using System.IO;
using System;

namespace QLDT_WPF.Views.Components
{
    /// <summary>
    /// Interaction logic for LopHocPhanTableView.xaml
    /// </summary>
    public partial class LopHocPhanTableView : UserControl
    {
        // Variables
        private LopHocPhanRepository lopHocPhanRepository;
        public ObservableCollection<LopHocPhanDto> ObservableLopHocPhan { get; private set; }

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
             DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(LopHocPhanTableView), new PropertyMetadata(null));

        // Constructor
        public LopHocPhanTableView()
        {
            InitializeComponent();
            lopHocPhanRepository = new LopHocPhanRepository();
            ObservableLopHocPhan = new ObservableCollection<LopHocPhanDto>();
            // Load asynchronously
            Loaded += async (sender, e) =>
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
                        } else
                        {
                            TargetContentArea = new ContentControl();
                        }
                    } else
                    {
                        TargetContentArea = new ContentControl();
                    }
                }
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            var list_lopHocPhan = await lopHocPhanRepository.GetAll();

            // Handle unsuccessful response
            if (list_lopHocPhan.Status == false)
            {
                MessageBox.Show(list_lopHocPhan.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Add items to ObservableCollection
            foreach (var item in list_lopHocPhan.Data)
            {
                // Status message: Đang Diễn Ra, Đã Kết Thúc, Sắp Diễn Ra
                if (item.ThoiGianBatDau <= DateTime.Now && item.ThoiGianKetThuc >= DateTime.Now)
                {
                    item.StatusMessage = "Đang Diễn Ra";
                }
                else if (item.ThoiGianKetThuc < DateTime.Now)
                {
                    item.StatusMessage = "Đã Kết Thúc";
                }
                else
                {
                    item.StatusMessage = "Sắp Diễn Ra";
                }
                ObservableLopHocPhan.Add(new LopHocPhanDto{
                    IdLopHocPhan = item.IdLopHocPhan,
                    TenLopHocPhan = item.TenLopHocPhan,
                    IdGiaoVien = item.IdGiaoVien,
                    TenGiaoVien = item.TenGiaoVien,
                    IdMonHoc = item.IdMonHoc,
                    TenMonHoc = item.TenMonHoc,
                    ThoiGianBatDau = item.ThoiGianBatDau,
                    ThoiGianKetThuc = item.ThoiGianKetThuc,
                    StatusMessage = item.StatusMessage
                });
            }

            // Bind to DataGrid or other UI components as needed
            dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan;
        }

        // TextBox TextChanged Event Handler - Handle search when changle value 
        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt_search = ((TextBox)sender).Text;
            if (txt_search == "")
            {
                dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan;
            }
            else
            {
                dataGridLopHocPhan.ItemsSource = ObservableLopHocPhan.Where(x =>
                    x.TenLopHocPhan.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenGiaoVien.ToLower().Contains(txt_search.ToLower()) ||
                    x.TenMonHoc.ToLower().Contains(txt_search.ToLower()) ||
                    x.ThoiGianBatDau.ToString().Contains(txt_search) ||
                    x.ThoiGianKetThuc.ToString().Contains(txt_search)
                );
            }
        }

        // Export data to excel
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            if (ObservableLopHocPhan == null || ObservableLopHocPhan.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất ra Excel", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                // Tạo workbook và worksheet
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                // Đặt tiêu đề cho các cột trong worksheet
                worksheet[1, 1].Text = "ID Lớp Học Phần";
                worksheet[1, 2].Text = "Tên Lớp Học Phần";
                worksheet[1, 3].Text = "ID Giáo Viên";
                worksheet[1, 4].Text = "Tên Giáo Viên";
                worksheet[1, 5].Text = "ID lớp học phần";
                worksheet[1, 6].Text = "Tên lớp học phần";
                worksheet[1, 7].Text = "Thời Gian Bắt Đầu";
                worksheet[1, 8].Text = "Thời Gian Kết Thúc";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var lhp in ObservableLopHocPhan)
                {
                    worksheet[row, 1].Text = lhp.IdLopHocPhan ?? "N/A";
                    worksheet[row, 2].Text = lhp.TenLopHocPhan ?? "N/A";
                    worksheet[row, 3].Text = lhp.IdGiaoVien ?? "N/A";
                    worksheet[row, 4].Text = lhp.TenGiaoVien ?? "N/A";
                    worksheet[row, 5].Text = lhp.IdMonHoc ?? "N/A";
                    worksheet[row, 6].Text = lhp.TenMonHoc ?? "N/A";
                    worksheet[row, 7].Text = lhp.ThoiGianBatDau?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                    worksheet[row, 8].Text = lhp.ThoiGianKetThuc?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachLopHocPhan.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Add new LopHocPhan
        private void AddLopHocPhan(object sender, RoutedEventArgs e)
        {
            var addLopHocPhanWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.AddLopHocPhan();
            addLopHocPhanWindow.ShowDialog();
            InitAsync();
        }

        // Add new Lop Hoc Phan With File
        private void AddLopHocPhanWithFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv"; // Chỉ cho phép chọn file CSV

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Đọc file CSV và xử lý từng dòng
                    string[] lines = File.ReadAllLines(filePath);
                    List<LopHocPhanDto> list_lop_hoc_phan = new List<LopHocPhanDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một lớp học phần với định dạng "Mã lớp học phần, Tên lớp học phần, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 8)
                        {
                            list_lop_hoc_phan.Add(new LopHocPhanDto
                            {
                                IdLopHocPhan = data[0],
                                TenLopHocPhan = data[1],
                                IdGiaoVien = data[2],
                                TenGiaoVien = data[3],
                                IdMonHoc = data[4],
                                TenMonHoc = data[5],
                                ThoiGianBatDau = DateTime.Parse(data[6]),
                                ThoiGianKetThuc = DateTime.Parse(data[7])
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách lớp học phần từ file CSV trong repository
                        var response = await lopHocPhanRepository
                            .AddListLopHocPhanFromCSV(list_lop_hoc_phan);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi lớp học phần bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(monh => monh.TenLopHocPhan));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách lớp học phần từ file CSV: " + string.Join(", ", list_lop_hoc_phan.Select(x => x.TenMonHoc)) + " thành công!");

                                await InitAsync();
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi đọc file: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file CSV để thêm lớp học phần!");
            }
        }

        // Edit LopHocPhan
        private void Click_Edit_LopHocPhan(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is LopHocPhanDto lopHocPhan)
            {
                var editLopHocPhanWindow = new QLDT_WPF.Views.Shared.Components.Admin.Help.EditLopHocPhan(lopHocPhan);
                editLopHocPhanWindow.ShowDialog();
                InitAsync();
            }
        }

        // Delete LopHocPhan
        private void Click_Delete_LopHocPhan(object sender, RoutedEventArgs e)
        {
            // Lấy đối tượng LopHocPhanDto từ thuộc tính Tag của nút
            if (sender is Button button && button.Tag is LopHocPhanDto lopHocPhan)
            {
                string idlopHocPhan = lopHocPhan.IdLopHocPhan;
                string tenlopHocPhan = lopHocPhan.TenLopHocPhan;

                // Hiển thị hộp thoại xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học phần '{tenlopHocPhan}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Thực hiện thao tác xóa bất đồng bộ
                    Task.Run(async () =>
                    {
                        try
                        {
                            // Gọi hàm xóa trong repository và lấy phản hồi
                            var response = await lopHocPhanRepository.Delete(idlopHocPhan);

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
                                MessageBox.Show($"Có lỗi xảy ra khi xóa lớp học phần '{tenlopHocPhan}': {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            });
                        }
                    });
                }
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }


        // private void dataGridLHP_CellTapped(object sender, GridCellTappedEventArgs e)
        // {
        //     var teacherDetails = new LopHocPhanDetails(TargetContentArea, null);

        //     TargetContentArea.Content = teacherDetails;
        // }

        // Show details of LopHocPhan
        private void detail_LopHocPhan(object sender, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Components.LopHocPhanDetails(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show details of GiaoVien
        private void detail_GiaoVien(object sender, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Components.TeacherDetails(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }

        // Show details of MonHoc
        private void detail_MonHoc(object sender, RoutedEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string Id = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string Name = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail = new QLDT_WPF.Views.Shared.Components.Admin.View.SubjectDetails(Id);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }
    }
}
