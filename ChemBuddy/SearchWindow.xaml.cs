using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace ChemBuddy
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ChemBuddy"].ConnectionString;//
           
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    using (SqlCommand cmd = new SqlCommand("SEARCH", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter nameParamater = new SqlParameter("@ELEMENT_NAME", SqlDbType.VarChar)
                        {
                            Value = SearchTextBox.Text
                        };
                    
                        //add parameters to cmd
                        cmd.Parameters.Add(nameParamater);
                       
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable("Data_Table");
                        sda.Fill(dt);
                        SearchResultsDataGrid.ItemsSource = dt.DefaultView;
                        con.Close();
                    }
                }
            }
            //catch SQL exception (probably incorrect syntax in query)
            catch (System.Data.SqlClient.SqlException sqlex)
            {
                string message = "An SQL exception occured.\r\nDetails: " + sqlex.Message;
                string caption = "SQL Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Error;

                // display messagebox
                MessageBoxResult SQLException = MessageBox.Show(message, caption, buttons, image);
            }
            //catch other exceptions
            catch (Exception ex)
            {
                string message = "An unexpected error occured.\r\nDetails: " + ex.Message;
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage image = MessageBoxImage.Error;

                // display messagebox
                MessageBoxResult OtherException = MessageBox.Show(message, caption, buttons, image);
            }
        }

        private void SearchResultsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ATOMIC_NUMBER")
                (e.Column as DataGridTextColumn).Visibility = Visibility.Hidden;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int atomicNum = Convert.ToInt32(GetSelectedValue(SearchResultsDataGrid, 0));
            DisplayElementDetails(atomicNum);
        }

        private void DisplayElementDetails(int atomicNum)
        {
            ElementDetails ed = new ElementDetails(GetElementInfo(atomicNum));
            ed.Show();
        }

        private Element GetElementInfo(int atomicNum)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ChemBuddy"].ConnectionString;
            string CmdString = string.Empty;

            //try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    using (SqlCommand cmd = new SqlCommand("GET_ELEMENT", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter atomicNumParameter = new SqlParameter("@ATOMIC_NUMBER", SqlDbType.VarChar)
                        {
                            Value = atomicNum
                        };

                        //add parameters to cmd
                        cmd.Parameters.Add(atomicNumParameter);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            //get values from reader and assign them to variables
                            int atomicNumber = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string symbol = reader.GetString(2);
                            string category = reader.GetString(3);
                            string electronConfig = reader.GetString(4);
                            string standardPhase = reader.GetString(5);
                            double atomicMass = reader.GetDouble(6);
                            double boilingPoint = reader.GetDouble(7);
                            double meltingPoint = reader.GetDouble(8);
                            double density = reader.GetDouble(9);

                            //create new instance of Element, using above variables in constructor
                            Element e = new Element(name, symbol, category, electronConfig, standardPhase, density, atomicMass, boilingPoint, meltingPoint, atomicNumber);
                            return e;
                        }
                        else
                        {
                            string message = "Element details could not be retrieved due to a database error";
                            string caption = "Error";
                            MessageBoxButton buttons = MessageBoxButton.OK;
                            MessageBoxImage image = MessageBoxImage.Error;

                            // display messagebox
                            MessageBoxResult SQLException = MessageBox.Show(message, caption, buttons, image);

                            reader.Close();
                            return null;
                        }
                    }
                }
            }
        }

        private string GetSelectedValue(DataGrid grid, int col)
        {
            DataGridCellInfo cellInfo1 = grid.SelectedCells[col];
            if (cellInfo1 == null) return null;

            DataGridBoundColumn column = cellInfo1.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo1.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((MainWindow)this.Owner).Focus();
        }
    }
}
