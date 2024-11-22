using QLDT_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;
using QLDT_WPF.Views.Shared.Components.SinhVien.View;
using QLDT_WPF.Views.Shared;

namespace QLDT_WPF.Views.SinhVien
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SinhVienDashboard : Window, INotifyPropertyChanged
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

        public SinhVienDashboard(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;

            DataContext = this;
            this.initWindow();

            // Truyền UserInformation vào LichhocView
            var lichhocView = new LichhocView(userInformation);
            // truyen UserInformation vào SinhvienLeftNavbar
            var sinhvienLeftNavbar = new SinhvienLeftNavbar(userInformation);
            sideBar.Content = sinhvienLeftNavbar;
            sinhvienLeftNavbar.TargetContentArea = ContentArea;
            // Gắn LichhocView vào một placeholder hoặc container trong giao diện
            ContentArea.Content = lichhocView; // MainContent là tên của ContentControl trong XAML
        }

        private void initWindow()
        {
            this.FullName = userInformation.FullName;
        }
    }

}
