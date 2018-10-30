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

        private void HydrogenButton_Click(object sender, RoutedEventArgs e)
        {
            Element ele = new Element("Hydrogen", "s", "c", "ec", "sp", 1.0f, 1, 1, 1, 1);
            ElementDetails ed = new ElementDetails(ele);
            ed.Show();
        }

        private void HeliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LithiumButton_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BerylliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BoronButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CarbonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NitrogenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OxygenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FluorineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NeonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SodiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MagnesiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AluminumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SiliconButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhosphorusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SulfurButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChlorineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArgonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PotassiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CalciumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TitaniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VanadiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChromiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ManganeseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ScandiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IronButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CobaltButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NickelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopperButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZincButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GalliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StrontiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GermaniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ArsenicButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BromineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KryptonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RubidiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YttriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ZirconiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NiobiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MolybdenurButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MolybdenumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TechnetiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RutheniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RhodiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PalladiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SilverButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CadmiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IndiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TinButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AntimonyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TelluriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IodineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XenonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CaesiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BariumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HafniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TantalumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TungstenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RheniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OsmiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IridiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlatinumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MercuryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThalliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LeadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BismuthButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PoloniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AstatineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FranciumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Num89_103Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RutherfordiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DubniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SeaborgiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BohriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HassiunButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MeitneriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DarmstadtiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RoentgeniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CoperniciumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NihoniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FleroviumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoscoviumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LivermoriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TennessineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OganessonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SeleniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoldButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UnuntriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UnunpentiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UnunseptiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UnunoctiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LanthanumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CeriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PraseodymiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NeodymiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PromethiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EuropiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GadoliniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TerbiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TeribiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DysprosiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HolmiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ErbiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThuliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YtterbiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LutetiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActiniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ThoriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProtactiniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UraniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NeptuniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlutoniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AmericiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CuriumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BerkeliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CaliforniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EinsteiniumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FermiumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MendeleviumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NobeliumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LawrenciumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SamariumButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
