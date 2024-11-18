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
    /// Interaction logic for AddSinhVien.xaml
    /// </summary>
    public partial class AddSinhVien : Window
    {
        // Variables

        // Constructor
        public AddSinhVien()
        {
            InitializeComponent();

            // init select box khoa
            Loaded += async (sender, e) =>
            {
                await InitAsync();
            };
        }

        // Init window asynchronously
        private async Task InitAsync()
        {
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO 
        }

        // Only allow integer input in text box
        private void handle_input_key_press_number(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
