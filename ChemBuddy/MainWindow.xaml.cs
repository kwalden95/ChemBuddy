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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChemBuddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
                SearchWindow sw = new SearchWindow();
                sw.Owner = this;
                sw.Show();
        }

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private void ToggleLegendButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActinideImage.Visibility == Visibility.Visible)
                hideLegend();
            else if (ActinideImage.Visibility == Visibility.Hidden)
                showLegend();
        }

        private void showLegend()
        {
            ActinideImage.Visibility = Visibility.Visible;
            ActinideTextBlock.Visibility = Visibility.Visible;
            AlkaliMetalImage.Visibility = Visibility.Visible;
            AlkaliMetalTextBlock.Visibility = Visibility.Visible;
            AlkalineEarthMetalImage.Visibility = Visibility.Visible;
            AlkalineEarthMetalTextBlock.Visibility = Visibility.Visible;
            HalogenImage.Visibility = Visibility.Visible;
            HalogenTextBlock.Visibility = Visibility.Visible;
            LanthanideImage.Visibility = Visibility.Visible;
            LanthanideTextBlock.Visibility = Visibility.Visible;
            MetalImage.Visibility = Visibility.Visible;
            MetalTextBlock.Visibility = Visibility.Visible;
            MetalloidImage.Visibility = Visibility.Visible;
            MetalloidTextBlock.Visibility = Visibility.Visible;
            NobleGasImage.Visibility = Visibility.Visible;
            NobleGasTextBlock.Visibility = Visibility.Visible;
            NonmetalImage.Visibility = Visibility.Visible;
            NonmetalTextBlock.Visibility = Visibility.Visible;
            TransitionMetalImage.Visibility = Visibility.Visible;
            TransitionMetalTextBlock.Visibility = Visibility.Visible;
        }

        private void hideLegend()
        {
            ActinideImage.Visibility = Visibility.Hidden;
            ActinideTextBlock.Visibility = Visibility.Hidden;
            AlkaliMetalImage.Visibility = Visibility.Hidden;
            AlkaliMetalTextBlock.Visibility = Visibility.Hidden;
            AlkalineEarthMetalImage.Visibility = Visibility.Hidden;
            AlkalineEarthMetalTextBlock.Visibility = Visibility.Hidden;
            HalogenImage.Visibility = Visibility.Hidden;
            HalogenTextBlock.Visibility = Visibility.Hidden;
            LanthanideImage.Visibility = Visibility.Hidden;
            LanthanideTextBlock.Visibility = Visibility.Hidden;
            MetalImage.Visibility = Visibility.Hidden;
            MetalTextBlock.Visibility = Visibility.Hidden;
            MetalloidImage.Visibility = Visibility.Hidden;
            MetalloidTextBlock.Visibility = Visibility.Hidden;
            NobleGasImage.Visibility = Visibility.Hidden;
            NobleGasTextBlock.Visibility = Visibility.Hidden;
            NonmetalImage.Visibility = Visibility.Hidden;
            NonmetalTextBlock.Visibility = Visibility.Hidden;
            TransitionMetalImage.Visibility = Visibility.Hidden;
            TransitionMetalTextBlock.Visibility = Visibility.Hidden;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void DisplayElementDetails(int atomicNum)
        {
            ElementDetails ed = new ElementDetails(GetElementInfo(atomicNum));
            ed.Show();
        }

        private void AddMass(int atomicNum)
        {
            double previousMass = Convert.ToDouble(this.MolarMassSumTextBlock.Text);
            double newMass = previousMass + GetMass(atomicNum);
            this.MolarMassSumTextBlock.Text = newMass.ToString();
        }

        private double GetMass(int atomicNum)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ChemBuddy"].ConnectionString;
            string CmdString = string.Empty;

            //try
            {
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    using (SqlCommand cmd = new SqlCommand("GET_ATOMIC_MASS", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter atomicNumParameter = new SqlParameter("@ATOMIC_NUMBER", SqlDbType.VarChar)
                        {
                            Value = atomicNum
                        };

                        //add parameters to cmd
                        cmd.Parameters.Add(atomicNumParameter);

                        //execute
                        double result = Convert.ToDouble(cmd.ExecuteScalar());
                        con.Close();
                        return result;
                    }
                }
            }
        }

        private void HydrogenButton_Click(object sender, RoutedEventArgs e)
        {
            if(MolarMassStackPanel.Visibility == Visibility.Visible)
                AddMass(1);
            
            else
                DisplayElementDetails(1);
        }

     
        private void HeliumButton_Click(object sender, RoutedEventArgs e)
        {
            if (MolarMassStackPanel.Visibility == Visibility.Visible)
                AddMass(2);

            else
                DisplayElementDetails(2);
        }

        private void LithiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(3); else DisplayElementDetails(3);

        }

        private void BerylliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(4); else DisplayElementDetails(4);

        }

        private void BoronButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(5); else DisplayElementDetails(5);

        }

        private void CarbonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(6); else DisplayElementDetails(6);

        }

        private void NitrogenButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(7); else DisplayElementDetails(7);

        }

        private void OxygenButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(8); else DisplayElementDetails(8);

        }

        private void FluorineButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(9); else DisplayElementDetails(9);

        }

        private void NeonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(10); else DisplayElementDetails(10);

        }

        private void SodiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(11); else DisplayElementDetails(11);

        }

        private void MagnesiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(12); else DisplayElementDetails(12);

        }

        private void AluminumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(13); else DisplayElementDetails(13);

        }

        private void SiliconButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(14); else DisplayElementDetails(14);

        }

        private void PhosphorusButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(15); else DisplayElementDetails(15);

        }

        private void SulfurButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(16); else DisplayElementDetails(16);

        }

        private void ChlorineButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(17); else DisplayElementDetails(17);

        }

        private void ArgonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(18); else DisplayElementDetails(18);

        }

        private void PotassiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(19); else DisplayElementDetails(19);

        }

        private void CalciumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(20); else DisplayElementDetails(20);

        }

        private void ScandiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(21); else DisplayElementDetails(21);

        }

        private void TitaniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(22); else DisplayElementDetails(22);

        }

        private void VanadiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(23); else DisplayElementDetails(23);

        }

        private void ChromiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(24); else DisplayElementDetails(24);

        }

        private void ManganeseButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(25); else DisplayElementDetails(25);

        }

        private void IronButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(26); else DisplayElementDetails(26);

        }

        private void CobaltButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(27); else DisplayElementDetails(27);

        }

        private void NickelButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(28); else DisplayElementDetails(28);
        }

        private void CopperButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(29); else DisplayElementDetails(29);

        }

        private void ZincButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(30); else DisplayElementDetails(30);

        }

        private void GalliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(31); else DisplayElementDetails(31);

        }

        private void GermaniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(32); else DisplayElementDetails(32);

        }

        private void ArsenicButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(33); else DisplayElementDetails(33);

        }

        private void SeleniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(34); else DisplayElementDetails(34);

        }

        private void BromineButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(35); else DisplayElementDetails(35);

        }

        private void KryptonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(36); else DisplayElementDetails(36);

        }

        private void RubidiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(37); else DisplayElementDetails(37);

        }

        private void StrontiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(38); else DisplayElementDetails(38);

        }

        private void YttriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(39); else DisplayElementDetails(39);

        }

        private void ZirconiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(40); else DisplayElementDetails(40);

        }

        private void NiobiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(41); else DisplayElementDetails(41);

        }

  
        private void MolybdenumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(42); else DisplayElementDetails(42);

        }

        private void TechnetiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(43); else DisplayElementDetails(43);

        }

        private void RutheniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(44); else DisplayElementDetails(44);

        }

        private void RhodiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(45); else DisplayElementDetails(45);

        }

        private void PalladiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(46); else DisplayElementDetails(46);

        }

        private void SilverButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(47); else DisplayElementDetails(47);

        }

        private void CadmiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(48); else DisplayElementDetails(48);

        }

        private void IndiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(49); else DisplayElementDetails(49);

        }

        private void TinButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(50); else DisplayElementDetails(50);

        }

        private void AntimonyButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(51); else DisplayElementDetails(51);

        }

        private void TelluriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(52); else DisplayElementDetails(52);

        }

        private void IodineButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(53); else DisplayElementDetails(53);

        }

        private void XenonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(54); else DisplayElementDetails(54);

        }

        private void CaesiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(55); else DisplayElementDetails(55);

        }

        private void BariumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(56); else DisplayElementDetails(56);

        }

        private void HafniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(72); else DisplayElementDetails(72);

        }

        private void TantalumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(73); else DisplayElementDetails(73);

        }

        private void TungstenButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(74); else DisplayElementDetails(74);

        }

        private void RheniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(75); else DisplayElementDetails(75);

        }

        private void OsmiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(76); else DisplayElementDetails(76);

        }

        private void IridiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(77); else DisplayElementDetails(77);

        }

        private void PlatinumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(78); else DisplayElementDetails(78);

        }

        private void GoldButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(79); else DisplayElementDetails(79);

        }

        private void MercuryButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(80); else DisplayElementDetails(80);

        }

        private void ThalliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(81); else DisplayElementDetails(81);

        }

        private void LeadButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(82); else DisplayElementDetails(82);
        }

        private void BismuthButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(83); else DisplayElementDetails(83);
        }

        private void PoloniumButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(84); else DisplayElementDetails(84);
        }

        private void AstatineButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(85); else DisplayElementDetails(85);
        }

        private void RadonButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(86); else DisplayElementDetails(86);
        }

        private void FranciumButton_Click(object sender, RoutedEventArgs e)
        {

                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(87); else DisplayElementDetails(87);
        }

        private void RadiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(88); else DisplayElementDetails(88);

        }

        private void Num89_103Button_Click(object sender, RoutedEventArgs e)
        {
            // do nothing
        }

        private void RutherfordiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(104); else DisplayElementDetails(104);

        }

        private void DubniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(105); else DisplayElementDetails(105);

        }

        private void SeaborgiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(106); else DisplayElementDetails(106);

        }

        private void BohriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(107); else DisplayElementDetails(107);

        }

        private void HassiunButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(108); else DisplayElementDetails(108);

        }

        private void MeitneriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(109); else DisplayElementDetails(109);

        }

        private void DarmstadtiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(110); else DisplayElementDetails(110);

        }

        private void RoentgeniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(111); else DisplayElementDetails(111);

        }

        private void CoperniciumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(112); else DisplayElementDetails(112);

        }

        private void NihoniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(113); else DisplayElementDetails(113);

        }

        private void FleroviumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(114); else DisplayElementDetails(114);

        }

        private void MoscoviumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(115); else DisplayElementDetails(115);

        }

        private void LivermoriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(116); else DisplayElementDetails(116);

        }

        private void TennessineButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(117); else DisplayElementDetails(117);

        }

        private void OganessonButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(2); else DisplayElementDetails(1);

        }

        private void UnuntriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(113); else DisplayElementDetails(113);

        }

        private void UnunpentiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(115); else DisplayElementDetails(115);

        }

        private void UnunseptiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(117); else DisplayElementDetails(117);

        }

        private void UnunoctiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(118); else DisplayElementDetails(118);

        }

        private void LanthanumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(57); else DisplayElementDetails(57);

        }

        private void CeriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(58); else DisplayElementDetails(58);

        }

        private void PraseodymiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(59); else DisplayElementDetails(59);

        }

        private void NeodymiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(60); else DisplayElementDetails(60);

        }

        private void PromethiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(61); else DisplayElementDetails(61);

        }

        private void SamariumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(62); else DisplayElementDetails(62);

        }

        private void EuropiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(63); else DisplayElementDetails(63);

        }

        private void GadoliniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(64); else DisplayElementDetails(64);

        }

        private void TeribiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(65); else DisplayElementDetails(65);

        }

        private void DysprosiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(66); else DisplayElementDetails(66);

        }

        private void HolmiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(67); else DisplayElementDetails(67);

        }

        private void ErbiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(68); else DisplayElementDetails(68);

        }

        private void ThuliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(69); else DisplayElementDetails(69);

        }

        private void YtterbiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(70); else DisplayElementDetails(70);

        }

        private void LutetiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(71); else DisplayElementDetails(71);

        }

        private void ActiniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(89); else DisplayElementDetails(89);

        }

        private void ThoriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(90); else DisplayElementDetails(90);

        }

        private void ProtactiniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(91); else DisplayElementDetails(91);

        }

        private void UraniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(92); else DisplayElementDetails(92);

        }

        private void NeptuniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(93); else DisplayElementDetails(93);

        }

        private void PlutoniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(94); else DisplayElementDetails(94);

        }

        private void AmericiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(95); else DisplayElementDetails(95);

        }

        private void CuriumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(96); else DisplayElementDetails(96);

        }

        private void BerkeliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(97); else DisplayElementDetails(97);

        }

        private void CaliforniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(98); else DisplayElementDetails(98);

        }

        private void EinsteiniumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(99); else DisplayElementDetails(99);

        }

        private void FermiumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(100); else DisplayElementDetails(100);

        }

        private void MendeleviumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(101); else DisplayElementDetails(101);

        }

        private void NobeliumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(102); else DisplayElementDetails(102);

        }

        private void LawrenciumButton_Click(object sender, RoutedEventArgs e)
        {
                        if (MolarMassStackPanel.Visibility == Visibility.Visible) AddMass(103); else DisplayElementDetails(103);

        }

        private void ListViewButton_Click(object sender, RoutedEventArgs e)
        {
            ListView lv = new ListView();
            lv.Show();
        }

        private void CalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (MolarMassStackPanel.Visibility == Visibility.Hidden) {
                MolarMassStackPanel.Visibility = Visibility.Visible;
                MolarMassSumTextBlock.Visibility = Visibility.Visible;
                ClearButton.Visibility = Visibility.Visible;
            }
            else
            {
                MolarMassStackPanel.Visibility = Visibility.Hidden;
                MolarMassSumTextBlock.Visibility = Visibility.Hidden;
                ClearButton.Visibility = Visibility.Hidden;
                MolarMassSumTextBlock.Text = "0.00";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.MolarMassSumTextBlock.Text = "0.00";
        }
    }
}
