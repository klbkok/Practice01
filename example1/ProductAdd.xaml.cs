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

namespace example1
{
    /// <summary>
    /// Логика взаимодействия для ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        public ProductAdd()
        {
            InitializeComponent();
        }

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            if (NameProduct.Text == "" || PriceProduct.Text == "" || AmountProduct.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым");
                return;
            }
            Close();
        }
    }
}
