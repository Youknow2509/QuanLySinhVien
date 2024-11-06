using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace QLDT_WPF.Views.Shared
{
    public partial class AdminLeftNavbar : UserControl
    {
        // Event to notify the main window about the sidebar toggle
        public event EventHandler SidebarToggled;
        private bool isSidebarExpanded = true; // Initial state of the sidebar (expanded)

        public AdminLeftNavbar()
        {
            InitializeComponent();
            btn_MenuAction.Click += Btn_MenuAction_Click; // Attach click event handler
        }

        // Toggle button click handler
        private void Btn_MenuAction_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the sidebar's expanded state
            isSidebarExpanded = !isSidebarExpanded;

            // Notify the main window that the sidebar's state has changed
            SidebarToggled?.Invoke(this, EventArgs.Empty);

            // Update the visibility of items within the sidebar
            UpdateSidebarItems();

            // Update the icon of the toggle button based on the expanded state
            img_MenuAction.Source = isSidebarExpanded
                ? new BitmapImage(new Uri("pack://application:,,,/Images/LeftNavBar/left-arrow.png"))
                : new BitmapImage(new Uri("pack://application:,,,/Images/LeftNavBar/arrow-right.png"));
        }

        // Updates visibility of sidebar items based on whether it is expanded or collapsed
        private void UpdateSidebarItems()
        {
            // Access the StackPanel inside the sidebar Border
            if (sideBar.Child is StackPanel stackPanel)
            {
                // Iterate through each button in the sidebar
                foreach (Button btn in stackPanel.Children.OfType<Button>())
                {
                    // Find the TextBlock inside each button and toggle its visibility
                    var textBlock = (btn.Content as StackPanel)?.Children.OfType<TextBlock>().FirstOrDefault();
                    if (textBlock != null)
                    {
                        textBlock.Visibility = isSidebarExpanded ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            }

            // Toggle visibility for the role TextBlock based on sidebar state
            txtRole.Visibility = isSidebarExpanded ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
