using QLDT_WPF.Dto;
using QLDT_WPF.Models;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Components;
using Syncfusion.UI.Xaml.Grid;
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

namespace QLDT_WPF.Views.Shared.Components.GiaoVien.View
{
    /// <summary>
    /// Interaction logic for LopHocPhanTableView.xaml
    /// </summary>
    public partial class LopHocPhanTableView : UserControl
    {
        public ContentControl TargetContentArea
        {
            get { return (ContentControl)GetValue(TargetContentAreaProperty); }
            set { SetValue(TargetContentAreaProperty, value); }
        }

        public static readonly DependencyProperty TargetContentAreaProperty =
             DependencyProperty.Register(nameof(TargetContentArea), typeof(ContentControl), typeof(LopHocPhanTableView), new PropertyMetadata(null));

        private UserInformation userInformation;

        private string idGiaoVien;

        private LopHocPhanRepository lopHocPhanRepository;

        public ObservableCollection<LopHocPhanDto> lhp_collection { get; set; }


        public LopHocPhanTableView(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            idGiaoVien = userInformation.UserName;

            lopHocPhanRepository = new LopHocPhanRepository();

            lhp_collection = new ObservableCollection<LopHocPhanDto>();

            Loaded += async (s, e) =>
            {
                await InitAsync();
            };
        }

        public LopHocPhanTableView()
        {
            InitializeComponent();
        }

        private async Task InitAsync()
        {

            lopHocPhanRepository = new LopHocPhanRepository();

            lhp_collection = new ObservableCollection<LopHocPhanDto>();

            // Load Calendar
            await Load_LopHocPhan();
        }

        private async Task Load_LopHocPhan()
        {
            var lhp = await lopHocPhanRepository.GetLopHocPhansFromGiaoVien(idGiaoVien);
            lhp_collection.Clear();
            foreach (var item in lhp.Data)
            {
                lhp_collection.Add(
                    new LopHocPhanDto
                    {
                        IdLopHocPhan = item.IdLopHocPhan,
                        TenMonHoc = item.TenMonHoc,
                        TenLopHocPhan = item.TenLopHocPhan,
                        TenGiaoVien = item.TenGiaoVien,
                        ThoiGianBatDau = item.ThoiGianBatDau,
                        ThoiGianKetThuc = item.ThoiGianKetThuc,
                    }
                );
            }

            LopHocPhanDataGrid.ItemsSource = lhp_collection;
        }
    }
}
