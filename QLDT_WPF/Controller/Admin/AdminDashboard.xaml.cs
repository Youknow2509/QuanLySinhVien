using System;
using System.Windows;
using System.Windows.Controls;

namespace QLDT_WPF.Views.Admin
{
    public partial class AdminDashboard : Window
    {
        private bool isSidebarExpanded = true; // Matches the initial state in AdminLeftNavbar

        public AdminDashboard()
        {
            InitializeComponent();
            sideBar.SidebarToggled += OnSidebarToggled; // Subscribe to the SidebarToggled event
        }

        private void OnSidebarToggled(object sender, EventArgs e)
        {
            // Define target width and margin based on sidebar state
            double targetWidth = isSidebarExpanded ? 100 : 300;
            double targetMargin = isSidebarExpanded ? 110 : 310;

            // Directly set the Width and Margin properties without animation
            sideBar.Width = targetWidth;
            mainContentGrid.Margin = new Thickness(targetMargin + 10, 100, 20, 20);

            // Toggle the sidebar state
            isSidebarExpanded = !isSidebarExpanded;
        }
    }
}
