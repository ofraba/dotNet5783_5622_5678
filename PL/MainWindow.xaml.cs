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
using BLApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = BLApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToThe_List_Click(object sender, RoutedEventArgs e)
        {
            ProductListView p = new ProductListView(bl);
            p.Show();
        }
    }
}
