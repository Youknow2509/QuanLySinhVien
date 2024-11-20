using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using QLDT_WPF.Dto;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for EditDiem.xaml
    /// </summary>
    public partial class EditDiem : Window
    {
        // variables
        private DiemDto diem;

        private DiemRepository diemRepository;

        // constructor
        public EditDiem(DiemDto d)
        {
            InitializeComponent();

            // Khởi tạo repository
            diemRepository = new DiemRepository();

            //
            diem = d;

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        private async Task InitAsync()
        {
            // Load dữ liệu từ database
            if (diem != null)
            {
                txtIdDiem.Text = diem.IdDiem.ToString();
                txtDiemQuaTrinh.Text = diem.DiemQuaTrinh.ToString();
                txtDiemKetThuc.Text = diem.DiemKetThuc.ToString();
                txtDiemTongKet.Text = diem.DiemTongKet.ToString();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            string diemQuaTrinh = txtDiemQuaTrinh.Text;
            string diemKetThuc = txtDiemKetThuc.Text;
            string diemTongKet = txtDiemTongKet.Text;
            string idDiem = txtIdDiem.Text;

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrWhiteSpace(diemQuaTrinh) || string.IsNullOrWhiteSpace(diemKetThuc) || string.IsNullOrWhiteSpace(diemTongKet))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // handle api
            try
            {
                DiemDto diemEdit = new DiemDto
                {
                    IdDiem = idDiem,
                    DiemQuaTrinh = decimal.Parse(diemQuaTrinh),
                    DiemKetThuc = decimal.Parse(diemKetThuc),
                    DiemTongKet = decimal.Parse(diemTongKet)
                };

                Task.Run(
                    async () =>
                    {
                        var result = await diemRepository.Edit(diemEdit);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (result.Status == true)
                            {
                                MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại sau!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        });
                    }
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại sau!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void txtInputScore_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Kiểm tra ký tự nhập vào (số hoặc dấu '.')
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9.]$");

            // Đảm bảo chỉ một dấu '.' được nhập
            if (e.Text == "." && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtInputScore_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Kiểm tra giá trị nhập vào có phải số thập phân hợp lệ
                if (decimal.TryParse(textBox.Text, out decimal value))
                {
                    // Kiểm tra nếu nằm ngoài khoảng 0 - 10
                    if (value < 0 || value > 10)
                    {
                        MessageBox.Show("Vui lòng nhập giá trị từ 0 đến 10!", "Lỗi giá trị", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBox.Focus();
                        textBox.Text = ""; // Xóa nội dung nếu không hợp lệ
                    }
                }
                else
                {
                    // Nếu không phải số hợp lệ
                    MessageBox.Show("Giá trị không hợp lệ. Vui lòng nhập lại!", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBox.Focus();
                    textBox.Text = ""; // Xóa nội dung nếu không hợp lệ
                }
            }
        }
    }
}
