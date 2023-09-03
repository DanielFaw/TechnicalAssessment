using PenskeTechnicalAssessment.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PenskeTechnicalAssessment.Views
{
    /// <summary>
    /// Interaction logic for NASCARDisplay.xaml
    /// </summary>
    public partial class NASCARView : UserControl
    {
        public NASCARView()
        {
            InitializeComponent();
        }

        private void YearSelected(object sender, RoutedEventArgs e)
        {
            //Convert the item's content to an int and tell the data context to refresh
            var item = sender as ComboBoxItem;
            var year = int.Parse(item.Content.ToString());
            (DataContext as DisplayVM)?.RefreshData(year);
        }
    }
}
