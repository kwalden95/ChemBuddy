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
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Window
    {
        public ListView()
        {
            InitializeComponent();
            BindDataGrid();
        }

        public void BindDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ChemBuddy"].ConnectionString;
            string CmdString = string.Empty;
        
            try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    CmdString = "USE CHEMBUDDY SELECT * FROM ELEMENT";
                    SqlCommand cmd = new SqlCommand(CmdString, con);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Data_Table");
                    sda.Fill(dt);
                    ElementDataGrid.ItemsSource = dt.DefaultView;
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

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int atomicNum = Convert.ToInt32(GetSelectedValue(ElementDataGrid, 0));
            DisplayElementDetails(atomicNum);
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

        private void DisplayElementDetails(int atomicNum)
        {
            ElementDetails ed = new ElementDetails(GetElementInfo(atomicNum));
            ed.Show();
        }

        private void HydrogenButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(1);
        }

        private void HeliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(2);
        }

        private void LithiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(3);

        }

        private void BerylliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(4);

        }

        private void BoronButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(5);

        }

        private void CarbonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(6);

        }

        private void NitrogenButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(7);

        }

        private void OxygenButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(8);

        }

        private void FluorineButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(9);

        }

        private void NeonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(10);

        }

        private void SodiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(11);

        }

        private void MagnesiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(12);

        }

        private void AluminumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(13);

        }

        private void SiliconButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(14);

        }

        private void PhosphorusButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(15);

        }

        private void SulfurButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(16);

        }

        private void ChlorineButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(17);

        }

        private void ArgonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(18);

        }

        private void PotassiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(19);

        }

        private void CalciumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(20);

        }

        private void ScandiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(21);

        }

        private void TitaniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(22);

        }

        private void VanadiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(23);

        }

        private void ChromiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(24);

        }

        private void ManganeseButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(25);

        }

        private void IronButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(26);

        }

        private void CobaltButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(27);

        }

        private void NickelButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(28);
        }

        private void CopperButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(29);

        }

        private void ZincButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(30);

        }

        private void GalliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(31);

        }

        private void GermaniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(32);

        }

        private void ArsenicButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(33);

        }

        private void SeleniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(34);

        }

        private void BromineButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(35);

        }

        private void KryptonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(36);

        }

        private void RubidiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(37);

        }

        private void StrontiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(38);

        }

        private void YttriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(39);

        }

        private void ZirconiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(40);

        }

        private void NiobiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(41);

        }


        private void MolybdenumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(42);

        }

        private void TechnetiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(43);

        }

        private void RutheniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(44);

        }

        private void RhodiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(45);

        }

        private void PalladiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(46);

        }

        private void SilverButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(47);

        }

        private void CadmiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(48);

        }

        private void IndiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(49);

        }

        private void TinButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(50);

        }

        private void AntimonyButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(51);

        }

        private void TelluriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(52);

        }

        private void IodineButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(53);

        }

        private void XenonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(54);

        }

        private void CaesiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(55);

        }

        private void BariumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(56);

        }

        private void HafniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(72);

        }

        private void TantalumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(73);

        }

        private void TungstenButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(74);

        }

        private void RheniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(75);

        }

        private void OsmiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(76);

        }

        private void IridiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(77);

        }

        private void PlatinumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(78);

        }

        private void GoldButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(79);

        }

        private void MercuryButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(80);

        }

        private void ThalliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(81);

        }

        private void LeadButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(82);
        }

        private void BismuthButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(83);
        }

        private void PoloniumButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(84);
        }

        private void AstatineButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(85);
        }

        private void RadonButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(86);
        }

        private void FranciumButton_Click(object sender, RoutedEventArgs e)
        {

            DisplayElementDetails(87);
        }

        private void RadiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(88);

        }

        private void Num89_103Button_Click(object sender, RoutedEventArgs e)
        {
            // do nothing
        }

        private void RutherfordiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(104);

        }

        private void DubniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(105);

        }

        private void SeaborgiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(106);

        }

        private void BohriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(107);

        }

        private void HassiunButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(108);

        }

        private void MeitneriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(109);

        }

        private void DarmstadtiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(110);

        }

        private void RoentgeniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(111);

        }

        private void CoperniciumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(112);

        }

        private void NihoniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(113);

        }

        private void FleroviumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(114);

        }

        private void MoscoviumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(115);

        }

        private void LivermoriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(116);

        }

        private void TennessineButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(117);

        }

        private void OganessonButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(1);

        }

        private void UnuntriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(113);

        }

        private void UnunpentiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(115);

        }

        private void UnunseptiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(117);

        }

        private void UnunoctiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(118);

        }

        private void LanthanumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(57);

        }

        private void CeriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(58);

        }

        private void PraseodymiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(59);

        }

        private void NeodymiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(60);

        }

        private void PromethiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(61);

        }

        private void SamariumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(62);

        }

        private void EuropiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(63);

        }

        private void GadoliniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(64);

        }

        private void TeribiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(65);

        }

        private void DysprosiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(66);

        }

        private void HolmiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(67);

        }

        private void ErbiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(68);

        }

        private void ThuliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(69);

        }

        private void YtterbiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(70);

        }

        private void LutetiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(71);

        }

        private void ActiniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(89);

        }

        private void ThoriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(90);

        }

        private void ProtactiniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(91);

        }

        private void UraniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(92);

        }

        private void NeptuniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(93);

        }

        private void PlutoniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(94);

        }

        private void AmericiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(95);

        }

        private void CuriumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(96);

        }

        private void BerkeliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(97);

        }

        private void CaliforniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(98);

        }

        private void EinsteiniumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(99);

        }

        private void FermiumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(100);

        }

        private void MendeleviumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(101);

        }

        private void NobeliumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(102);

        }

        private void LawrenciumButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayElementDetails(103);

        }

        private void ElementDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
