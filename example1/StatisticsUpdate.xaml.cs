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
    /// Логика взаимодействия для StatisticsUpdate.xaml
    /// </summary>
    public partial class StatisticsUpdate : Window
    {
        public StatisticsUpdate()
        {
            InitializeComponent();
        }

        private void Stat_Update(object sender, RoutedEventArgs e)
        {
            if (Month.Text == "" || Expenditure.Text == "" || Profit.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым");
                return;
            }
            Close();
        }
    }
}
