using QLDT_WPF.Dto;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using QLDT_WPF.Views.Shared;

namespace QLDT_WPF.Views.Components
{
    public partial class TeacherDetails : UserControl
    {

        public ContentControl TargetContentArea { get; set; }


        public TeacherDetails(ContentControl targetContentArea)
        {
            InitializeComponent();
            LoadSampleData();
            TargetContentArea = targetContentArea;
        }

        private void LoadSampleData()
        {
            // Giả lập dữ liệu
            DataGrid.ItemsSource = new[]
            {
                new { LopHocPhan = "Lớp 1", GiangVien = "Nguyễn Văn A", MonHoc = "Toán" },
                new { LopHocPhan = "Lớp 2", GiangVien = "Trần Thị B", MonHoc = "Lý" },
                new { LopHocPhan = "Lớp 3", GiangVien = "Phạm Văn C", MonHoc = "Hóa" }
            };
        }

        private void DataGrid_CellTapped(object sender, GridCellTappedEventArgs e)
        {
            // Kiểm tra nếu cột được click là "Giảng Viên"
            if (e.Record != null && e.Column.MappingName == "GiangVien")
            {
                // Lấy dữ liệu của hàng được chọn
                dynamic selectedRow = e.Record;

                // Hiển thị thông tin chi tiết trong các TextBox
                txtFullName.Text = selectedRow.GiangVien;
                txtEmail.Text = $"{selectedRow.GiangVien.ToLower().Replace(" ", "")}@school.edu.vn";
                txtPhoneNumber.Text = "0123456789"; // Dữ liệu giả lập
                txtAddress.Text = "Khoa Công Nghệ Thông Tin"; // Dữ liệu giả lập
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (TargetContentArea != null)
            {
                TargetContentArea.Content = new TeacherTableView
                {
                    TargetContentArea = TargetContentArea
                };
            }
            else
            {
                MessageBox.Show("Không tìm thấy khu vực hiển thị nội dung!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ TeacherEditWindow
            var userProfileWindow = new TeacherEditWindow();
            userProfileWindow.ShowDialog();
        }
    }
}
