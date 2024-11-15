using System;
using System.ComponentModel;
using System.Windows;
using QLDT_WPF.Repositories;
using QLDT_WPF.ViewModels;

namespace QLDT_WPF.Views.Admin
{
    public partial class AdminDashboard : Window, INotifyPropertyChanged
    {
        // Variables
        private readonly UserInformation userInformation;

        private ChuongTrinhHocRepository chuongTrinhHocRepository;
        private DiemRepository diemRepository;
        private GiaoVienRepository giaoVienRepository;
        private IdentityRepository identityRepository;
        private KhoaRepository khoaRepository;
        private LopHocPhanRepository lopHocPhanRepository;
        private MonHocRepository monHocRepository;
        private NguyenVongGiaoVienRepository nguyenVongGiaoVienRepository;
        private NguyenVongSinhVienRepository NguyenVongSinhVienRepository;
        private SinhVienRepository sinhVienRepository;

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

        // Constructor
        public AdminDashboard(UserInformation userInformation)
        {
            InitializeComponent();
            this.userInformation = userInformation;
            chuongTrinhHocRepository = new ChuongTrinhHocRepository();
            diemRepository = new DiemRepository();
            giaoVienRepository = new GiaoVienRepository();
            identityRepository = new IdentityRepository();
            khoaRepository = new KhoaRepository();
            lopHocPhanRepository = new LopHocPhanRepository();
            monHocRepository = new MonHocRepository();
            nguyenVongGiaoVienRepository = new NguyenVongGiaoVienRepository();
            NguyenVongSinhVienRepository = new NguyenVongSinhVienRepository();
            sinhVienRepository = new SinhVienRepository();
            //
            DataContext = this;
            // init
            this.initWindow();
        }

        // Handle event when window loaded
        private void initWindow()
        {
            this.FullName = userInformation.FullName;
        }
    }
}
