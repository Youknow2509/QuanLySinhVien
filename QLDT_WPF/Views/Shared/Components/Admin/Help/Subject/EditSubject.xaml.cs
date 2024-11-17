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
    /// Interaction logic for EditSubject.xaml
    /// </summary>
    public partial class EditSubject : Window
    {
        // Variable
        private MonHocDto monHoc;
        private MonHocRepository monHocRepository;
        private KhoaRepository khoaRepository;

        // Constructor
        public EditSubject(MonHocDto monHocDto)
        {
            InitializeComponent();

            monHocRepository = new MonHocRepository();
            khoaRepository = new KhoaRepository();
            monHoc = monHocDto;

            // Handle loading asynchronously init
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Async init view
        private async Task InitAsync()
        {
            txtEditTenMonHoc.Text = monHoc.TenMonHoc;
            txtEditSoTinChi.Text = monHoc.SoTinChi.ToString();
            txtEditSoTiet.Text = monHoc.SoTietHoc.ToString();
            txtEditIdMonHoc.Text = monHoc.IdMonHoc;

            var listKhoa = await khoaRepository.GetAll();
            if (listKhoa.Status == false)
            {
                MessageBox.Show(listKhoa.Message);
                return;
            }
            foreach (var item in listKhoa.Data)
            {
                cbbEditKhoa.Items.Add(item);
            }
            cbbEditKhoa.DisplayMemberPath = "TenKhoa";
            cbbEditKhoa.SelectedValuePath = "IdKhoa";
            cbbEditKhoa.SelectedValue = monHoc.IdKhoa;
        }

        // Handle close button click
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Handle save button click
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from input fields
            string idMonHoc = txtEditIdMonHoc.Text;
            string tenMonHoc = txtEditTenMonHoc.Text;
            int.TryParse(txtEditSoTinChi.Text, out int soTinChi);
            int.TryParse(txtEditSoTiet.Text, out int soTiet);
            string khoa = cbbEditKhoa.SelectedItem.ToString();

            // TODO: Add logic to save the edited subject (e.g., update in database)

            MessageBox.Show("Thông tin môn học đã được lưu thành công!");
            this.Close();
        }
    }
}
