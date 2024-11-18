using QLDT_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Windows;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;

namespace QLDT_WPF.Views.SinhVien
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SinhVienDashboard : Window, INotifyPropertyChanged
    {

        private readonly UserInformation userInformation;

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

        // kích hoạt bất cứ khi nào một thuộc tính trong lớp thay đổi ~ INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public SinhVienDashboard(UserInformation userInformation)
        {
            InitializeComponent();
        }
    }
}
