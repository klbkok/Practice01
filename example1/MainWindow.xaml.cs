using System;
using System.Collections.Generic;
using System.Data;
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
using System.Configuration;
using System.Data.SqlClient;

namespace example1
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private SqlConnection sqlConnection = null;

		public class DataProduct
		{
            public int IdProduct { get; set; }
			public string NameProduct { get; set; }
			public string PriceProduct { get; set; }
			public string AmountProduct { get; set; }
			public string FirmProduct { get; set; }

			public string MonthStat { get; set; }
			public string YearStat { get; set; }
			public string ExpenditureStat { get; set; }
			public string ProfitStat { get; set; }
		}
		public MainWindow()
		{
			InitializeComponent();
			sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
			sqlConnection.Open();

			if (sqlConnection.State == ConnectionState.Open)
            {
				MessageBox.Show("Подключение установлено!");
            }
		}

        private void Button_Products(object sender, RoutedEventArgs e)
        {
			MyDataStats.Visibility = Visibility.Hidden;
			MyDataOrders.Visibility = Visibility.Hidden;
            MyData.Visibility = Visibility.Visible;
			MyData.Items.Clear();

			SqlDataReader dataReader = null;
            try
            {
				SqlCommand command = new SqlCommand(
					"SELECT Id, Name, Price, Amount FROM Products ",
					sqlConnection);
				dataReader = command.ExecuteReader();

				while (dataReader.Read())
                {
					DataProduct tempData = new DataProduct();
					tempData.IdProduct = Convert.ToInt32(dataReader["Id"]);
					tempData.NameProduct = Convert.ToString(dataReader["Name"]);
					tempData.PriceProduct = $"{dataReader["Price"]:N2} ₽";
					tempData.AmountProduct = $"{dataReader["Amount"]} шт.";
					MyData.Items.Add(tempData);
				}
            }
			catch (Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
            finally
            {
				if (dataReader != null && !dataReader.IsClosed)
                {
					dataReader.Close();
                }
            }
		}

        private void Button_Orders(object sender, RoutedEventArgs e)
        {
			MyDataStats.Visibility = Visibility.Hidden;
			MyData.Visibility = Visibility.Hidden;
			MyDataOrders.Visibility = Visibility.Visible;
			MyDataOrders.Items.Clear();

			SqlDataReader dataReader = null;
			try
			{
				SqlCommand command = new SqlCommand(
					"SELECT Id, Name, Price, Amount, Firm FROM Orders ",
					sqlConnection);
				dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					DataProduct tempData = new DataProduct();
					tempData.IdProduct = Convert.ToInt32(dataReader["Id"]);
					tempData.NameProduct = Convert.ToString(dataReader["Name"]);
                    tempData.PriceProduct = $"{dataReader["Price"]:N2} ₽";
					tempData.AmountProduct = $"{dataReader["Amount"]} шт.";
					tempData.FirmProduct = Convert.ToString(dataReader["Firm"]);
					MyDataOrders.Items.Add(tempData);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if (dataReader != null && !dataReader.IsClosed)
				{
					dataReader.Close();
				}
			}
		}

        private void Button_ProductAdd(object sender, RoutedEventArgs e)
        {
			ProductAdd formAdd = new ProductAdd();
			formAdd.ShowDialog();
			string nameProduct = formAdd.NameProduct.Text;
			int priceProduct = Convert.ToInt32(formAdd.PriceProduct.Text);
			int amountProduct = Convert.ToInt32(formAdd.AmountProduct.Text);

			SqlCommand command = new SqlCommand(
				"INSERT INTO [Products] (Name, Price, Amount) " +
				$"VALUES (N'{nameProduct}', N'{priceProduct}', N'{amountProduct}')",
				sqlConnection);
			command.ExecuteNonQuery();
			
			MessageBox.Show("Товар успешно добавлен в базу!");
			Button_Products(sender, e);
		}

        private void Button_OrderAdd(object sender, RoutedEventArgs e)
        {
			OrderAdd orderAdd = new OrderAdd();
			orderAdd.ShowDialog();
			string nameProduct = orderAdd.NameProduct.Text;
			int priceProduct = Convert.ToInt32(orderAdd.PriceProduct.Text);
			int amountProduct = Convert.ToInt32(orderAdd.AmountProduct.Text);
			string firmProduct = orderAdd.FirmProduct.Text;

			SqlCommand command = new SqlCommand(
				"INSERT INTO [Orders] (Name, Price, Amount, Firm) " +
				$"VALUES (N'{nameProduct}', N'{priceProduct}', N'{amountProduct}', N'{firmProduct}')",
				sqlConnection);
			command.ExecuteNonQuery();

			MessageBox.Show("Заказ успешно добавлен в базу!");
			Button_Orders(sender, e);
		}
		 
        private void MyDataOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			var rowview = MyDataOrders.SelectedItem;
			MessageBox.Show(Convert.ToString(rowview)); 
			SqlCommand command = new SqlCommand(
				$"INSERT INTO [TableTest] (Test) VALUES (N'{rowview}')",
				sqlConnection);
			command.ExecuteNonQuery();

			MessageBox.Show("Успешно");
			//MessageBox.Show(MyDataOrders.Items.IndexOf(e.Source).ToString());
		}

        private void Button_Stats(object sender, RoutedEventArgs e)
        {
			MyData.Visibility = Visibility.Hidden;
			MyDataOrders.Visibility = Visibility.Hidden;
			MyDataStats.Visibility = Visibility.Visible;
			MyDataStats.Items.Clear();

			SqlDataReader dataReader = null;
			try
			{
				SqlCommand command = new SqlCommand(
					"SELECT Id, Month, Year, Expenditure, Profit FROM [Statistics] ",
					sqlConnection);
				dataReader = command.ExecuteReader();

				while (dataReader.Read())
				{
					DataProduct tempData = new DataProduct();
					tempData.IdProduct = Convert.ToInt32(dataReader["Id"]);
					tempData.MonthStat = Convert.ToString(dataReader["Month"]);
					tempData.YearStat = Convert.ToString(dataReader["Year"]);
					tempData.ExpenditureStat = $"{dataReader["Expenditure"]:N2} ₽";
					tempData.ProfitStat = $"{dataReader["Profit"]:N2} ₽";
					MyDataStats.Items.Add(tempData);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				if (dataReader != null && !dataReader.IsClosed)
				{
					dataReader.Close();
				}
			}
		}

        private void Button_StatUpdate(object sender, RoutedEventArgs e)
        {
			StatisticsUpdate statUpdate = new StatisticsUpdate();
			statUpdate.ShowDialog();
			if (statUpdate.DialogResult.Value)
            {

            }

			string month = statUpdate.Month.Text;
			string year = statUpdate.Year.Text;
			int expenditure = Convert.ToInt32(statUpdate.Expenditure.Text);
			int profit = Convert.ToInt32(statUpdate.Profit.Text);

			SqlCommand command = new SqlCommand(
				"INSERT INTO [Statistics] (Month, Year, Expenditure, Profit) " +
				$"VALUES (N'{month}', N'{year}', N'{expenditure}', N'{profit}')",
				sqlConnection);
			command.ExecuteNonQuery();

			MessageBox.Show("Статистика успешно обновлена!");
			Button_Stats(sender, e);
		}
    }
}