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

namespace ChemBuddy
{
    /// <summary>
    /// Interaction logic for ElementDetails.xaml
    /// </summary>
    public partial class ElementDetails : Window
    {
        public ElementDetails(Element e)
        {
            InitializeComponent();
            this.Title = e.name + " Details";
            ElementNameHeaderTextBlock.Text = e.name;
            ElementNameValue.Text = e.name;
            AtomicMassValue.Text = e.atomicMass.ToString();
            BoilingPointValue.Text = e.boilingPoint.ToString();
            MeltingPointValue.Text = e.meltingPoint.ToString();
            DensityValue.Text = e.density.ToString();
            AtomicNumberValue.Text = e.atomicNumber.ToString();
            ElectronConfigurationValue.Text = e.electronConfig;
            CategoryValue.Text = e.category;
            ChemicalSymbolValue.Text = e.symbol;
            StandardPhaseValue.Text = e.standardPhase;
        }
    }
}
