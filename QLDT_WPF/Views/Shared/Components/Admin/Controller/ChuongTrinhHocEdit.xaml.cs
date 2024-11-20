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
using QLDT_WPF.Dto;
using QLDT_WPF.Models;
using QLDT_WPF.Repositories;
using QLDT_WPF.Views.Components;

namespace QLDT_WPF.Views.Shared.Components.Admin.Controller
{
    /// <summary>
    /// Interaction logic for ChuongTrinhHocEdit.xaml
    /// </summary>
    public partial class ChuongTrinhHocEdit : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(ChuongTrinhHocEdit), new PropertyMetadata(null));
        
        // Variable
        private string IdChuongTrinhHoc;
        private ChuongTrinhHocDto chuongTrinhHocDto;
        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        private ObservableCollection<MonHocDto> MonHocTrongChuongTrinh_Collection;
        private ObservableCollection<MonHocDto> MonHocNgoaiChuongTrinh_Collection;

        // Constructor  
        public ChuongTrinhHocEdit(string id)
        {
            InitializeComponent();

            // Set var constructor
            IdChuongTrinhHoc = id;

            // Init repository
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();

            //
            MonHocNgoaiChuongTrinh_Collection = new ObservableCollection<MonHocDto>();
            MonHocTrongChuongTrinh_Collection = new ObservableCollection<MonHocDto>();

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

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        private async Task InitAsync()
        {
            // init title_edit_cth
            var req_cth = await chuongTrinhHocRepository.GetById(IdChuongTrinhHoc);
            if(req_cth.Status == false){
                MessageBox.Show("Không tìm thấy chương trình học", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            chuongTrinhHocDto = req_cth.Data;
            title_edit_cth.Text = $"Chỉnh sửa chương trình học: {chuongTrinhHocDto.TenChuongTrinhHoc}";

            // init sfDataGrid_MonHocTrongChuongTrinh
            await Init_Data_sfDataGrid_MonHocTrongChuongTrinh();

            // init sfDataGrid_MonHocNgoaiChuongTrinh
            await Init_Data_sfDataGrid_MonHocNgoaiChuongTrinh();

        }

        // init data sfDataGrid_MonHocTrongChuongTrinh
        private async Task Init_Data_sfDataGrid_MonHocTrongChuongTrinh(){
            var req_mh = await chuongTrinhHocRepository
                .GetMonHocByIdChuongTrinhHoc(IdChuongTrinhHoc);
            if(req_mh.Status == false){
                MessageBox.Show("Không tìm thấy môn học trong chương trình học", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MonHocTrongChuongTrinh_Collection.Clear();
            (List<MonHocDto> mh_t, List<MonHocDto> mh_kt) = req_mh.Data;
            foreach(MonHocDto mh in mh_t){
                MonHocTrongChuongTrinh_Collection.Add(mh);
            }
            sfDataGrid_MonHocTrongChuongTrinh.ItemsSource = MonHocTrongChuongTrinh_Collection;
        }

        // init data sfDataGrid_MonHocNgoaiChuongTrinh
        private async Task Init_Data_sfDataGrid_MonHocNgoaiChuongTrinh(){
            var req_mh = await chuongTrinhHocRepository
                .GetMonHocNotInChuongTrinhHoc(IdChuongTrinhHoc);
            if(req_mh.Status == false){
                MessageBox.Show("Không tìm thấy môn học ngoài chương trình học", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MonHocNgoaiChuongTrinh_Collection.Clear();
            (List<MonHocDto> mh_t, List<MonHocDto> mh_kt) = req_mh.Data;
            foreach(MonHocDto mh in mh_kt){
                MonHocNgoaiChuongTrinh_Collection.Add(mh);
            }
            sfDataGrid_MonHocNgoaiChuongTrinh.ItemsSource = MonHocNgoaiChuongTrinh_Collection;
        }

        // Event Handler for Adding a MonHoc
        private void AddMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // Get the MonHoc object from the button's Tag property
            var monHoc = (sender as Button)?.Tag;
            if (monHoc != null)
            {
                MessageBox.Show($"Thêm môn học: {monHoc}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                // Add your logic here to handle adding the MonHoc to the Chương Trình Học
                // TODO

            }
        }

        // Event Handler for Removing a MonHoc
        private void RemoveMonHoc_Click(object sender, RoutedEventArgs e)
        {
            // Get the MonHoc object from the button's Tag property
            var monHoc = (sender as Button)?.Tag;
            if (monHoc != null)
            {
                MessageBox.Show($"Xóa môn học: {monHoc}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Add your logic here to handle removing the MonHoc from the Chương Trình Học
                // TODO

            }
        }

        // Search filtering for the first grid
        private void SearchBox_TrongChuongTrinh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO
        }

        // Search filtering for the second grid
        private void SearchBox_NgoaiChuongTrinh_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO
        }
    }
}
