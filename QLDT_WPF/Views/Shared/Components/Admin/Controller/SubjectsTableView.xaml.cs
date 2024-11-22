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
using QLDT_WPF.Views.Shared;


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

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectsTableView), new PropertyMetadata(null));


        private string constMH = "SubjectsTableView";
        // Constructor
        public SubjectsTableView()
        {
            InitializeComponent();
            monHocRepository = new MonHocRepository();
            ObservableMonHoc = new ObservableCollection<MonHocDto>();
            // Handle loading asynchronously
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
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
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

        // Handle export to excel button click
        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu ObservableMonHoc không có dữ liệu
            if (ObservableMonHoc == null || ObservableMonHoc.Count == 0)
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
                worksheet[1, 1].Text = "ID Môn Học";
                worksheet[1, 2].Text = "Tên Môn Học";
                worksheet[1, 3].Text = "Số Tín Chỉ";
                worksheet[1, 4].Text = "Số Tiết Học";
                worksheet[1, 5].Text = "ID Khoa";
                worksheet[1, 6].Text = "Tên Khoa";

                // Bắt đầu từ dòng thứ 2 để ghi dữ liệu
                int row = 2;

                foreach (var monHoc in ObservableMonHoc)
                {
                    worksheet[row, 1].Text = monHoc.IdMonHoc ?? "N/A";
                    worksheet[row, 2].Text = monHoc.TenMonHoc ?? "N/A";
                    worksheet[row, 3].Text = monHoc.SoTinChi.ToString() ?? "N/A";
                    worksheet[row, 4].Text = monHoc.SoTietHoc.ToString() ?? "N/A";
                    worksheet[row, 5].Text = monHoc.IdKhoa ?? "N/A";
                    worksheet[row, 6].Text = monHoc.TenKhoa ?? "N/A";
                    row++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.UsedRange.AutofitColumns();

                // Lưu file Excel
                workbook.SaveAs("DanhSachMonHoc.xlsx");
            }

            MessageBox.Show("Xuất file Excel thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // hande add subject with file 
        private void AddSubjectWithFile(object sender, RoutedEventArgs e)
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
                    List<MonHocDto> list_monHoc = new List<MonHocDto>();

                    foreach (string line in lines)
                    {
                        // Giả sử mỗi dòng là một môn học với định dạng "Mã Môn Học, Tên Môn Học, So Tin Chi, So Tiet Hoc, Id Khoa"
                        string[] data = line.Split(',');
                        if (data.Count() >= 5)
                        {
                            list_monHoc.Add(new MonHocDto
                            {
                                IdMonHoc = data[0],
                                TenMonHoc = data[1],
                                SoTinChi = int.Parse(data[2]),
                                SoTietHoc = int.Parse(data[3]),
                                IdKhoa = data[4]
                            });
                        }
                    }

                    Task.Run(async () =>
                    {
                        // Gọi hàm thêm danh sách môn học từ file CSV trong repository
                        var response = await monHocRepository.AddListMonHocFromCSV(list_monHoc);

                        // Hiển thị thông báo kết quả trên luồng UI
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            if (response.Status == false)
                            {
                                // Tạo chuỗi lỗi chi tiết cho mỗi môn học bị lỗi
                                string errorDetails = string.Join(Environment.NewLine,
                                    response.Data.Select(monh => monh.TenMonHoc));

                                // Hiển thị thông báo lỗi
                                MessageBox.Show($"{response.Message}\n\nChi tiết lỗi:\n{errorDetails}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                // message box show list mon hoc dto
                                MessageBox.Show("Thêm danh sách môn học từ file CSV: " + string.Join(", ", list_monHoc.Select(x => x.TenMonHoc)) + " thành công!");

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
                MessageBox.Show("Vui lòng chọn file CSV để thêm môn học!");
            }
        }

        // Show detail mon hoc
        private void ChiTietMonHoc_Click(object sender, MouseButtonEventArgs e)
        {
            // Lấy ID môn học từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string subjectId = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string subjectName = textBlock.Text; // Lấy tên môn học từ thuộc tính Text của TextBlock

                // Mo cua so chi tiet mon hoc thay cho cua so hien tai
                var detail_subject = new QLDT_WPF.Views.Shared.Components.Admin.View.SubjectDetails(subjectId,constMH);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail_subject;
            }
        }

        // Show detail khoa
        private void ChiTietKhoa_Click(object sender, MouseButtonEventArgs e)
        {
            // Lấy ID khoa từ Tag của TextBlock
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null && textBlock.Tag != null)
            {
                string facultyId = (string)textBlock.Tag; // Hoặc nếu ID là kiểu string, bạn có thể chuyển thành (string)textBlock.Tag
                string facultyName = textBlock.Text; // Lấy tên khoa từ thuộc tính Text của TextBlock

                // Gọi hàm hoặc mở cửa sổ chi tiết khoa, truyền vào ID và tên khoa
                // MessageBox.Show($"Hiển thị chi tiết khoa với ID: {facultyId}, Tên Khoa: {facultyName}");
                var detail = new QLDT_WPF.Views.Components.KhoaDetails(facultyId,constMH);
                if (TargetContentArea == null) return;
                TargetContentArea.Content = detail;
            }
        }
    }
}