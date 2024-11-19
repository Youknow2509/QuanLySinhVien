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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using QLDT_WPF.Views.Components;

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for SubjectDetails.xaml
    /// </summary>
    public partial class SubjectDetails : UserControl
    {
        private MonHocRepository monHocRepository;
        private LopHocPhanRepository lopHocPhanRepository;
        private string idMonHoc;
        private ObservableCollection<LopHocPhanDto> collection_lop_hoc_phan;

        public ObservableCollection<MonHocDto> ObservableMonHoc { get; private set; }

        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
            DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(SubjectDetails), new PropertyMetadata(null));


        public SubjectDetails(string idMonHoc)
        {   
            InitializeComponent();
            monHocRepository = new MonHocRepository();
            lopHocPhanRepository = new LopHocPhanRepository();
            this.idMonHoc = idMonHoc;
            collection_lop_hoc_phan = new ObservableCollection<LopHocPhanDto>();

            // Loaded asynchrnously
            Loaded += async (s, e) => {
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

        private async Task InitAsync()
        {
            var req_lhp = await lopHocPhanRepository.GetLopHocPhansFromMonHoc(idMonHoc);
            if (req_lhp.Status == false)
            {
                MessageBox.Show(req_lhp.Message);
                return;
            }

            var lopHocPhans = req_lhp.Data;
            foreach (var lopHocPhan in lopHocPhans)
            {
                collection_lop_hoc_phan.Add(lopHocPhan);
            }
            sfDataGrid.ItemsSource = collection_lop_hoc_phan;

            var monHoc = await monHocRepository.GetById(idMonHoc);
            if (monHoc.Status == false)
            {
                MessageBox.Show(monHoc.Message);
                return;
            }
            titleDataTable.Text = $"Danh sách lớp học phần của môn học {monHoc.Data.TenMonHoc}";
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO

        }

        private void detail_LopHocPhan(object sender, RoutedEventArgs e)
        {
            // TODO

        }

        private void detail_GiaoVien(object sender, RoutedEventArgs e)
        {
            // TODO

        }

    }
}
