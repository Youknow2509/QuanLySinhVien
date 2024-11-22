using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Shared.Components.SinhVien.View;
using QLDT_WPF.Views.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using QLDT_WPF.Views.Shared.Components.GiaoVien.View;
using QLDT_WPF.Repositories;

namespace QLDT_WPF.Views.GiaoVien
{
    /// <summary>
    /// Interaction logic for GiaoVienWindow.xaml
    /// </summary>
    public partial class GiaoVienWindow : Window
    {
        public readonly UserInformation userInformation;

        // Data Context
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GiaoVienWindow(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            DataContext = this;
            this.initWindow();

            // Truyền UserInformation vào LichhocView
            var lichdayView = new LichDayView(userInformation);
            // truyen UserInformation vào SinhvienLeftNavbar
            var sinhvienLeftNavbar = new GiaoVienLeftNavbar(userInformation);
            sideBar.Content = sinhvienLeftNavbar;
            sinhvienLeftNavbar.TargetContentArea = ContentArea;
            // Gắn LichhocView vào một placeholder hoặc container trong giao diện
            ContentArea.Content = lichdayView; // MainContent là tên của ContentControl trong XAML
        }

        private void initWindow()
        {
            this.FullName = userInformation.FullName;
        }
    }
}
