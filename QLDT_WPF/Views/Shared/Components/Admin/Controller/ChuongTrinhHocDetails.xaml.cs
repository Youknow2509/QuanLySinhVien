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

namespace QLDT_WPF.Views.Shared.Components.Admin.View
{
    /// <summary>
    /// Interaction logic for ChuongTrinhHocDetails.xaml
    /// </summary>
    public partial class ChuongTrinhHocDetails : UserControl
    {

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        public ChuongTrinhHocDetails()
        {
            InitializeComponent();
        }

        // handle search
        private void txtTimKiem_TextChanged(object s, TextChangedEventArgs e)
        {
            // TODO
        }

        // Exprot to ex
        private void ExportToExcel(object s, RoutedEventArgs e)
        {
            // TODO
        }

        // Handlee them mon hoc 
        private void EditCth(object s, RoutedEventArgs e)
        {
            // TODO
        }
    }
}
