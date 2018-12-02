using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemBuddy
{
    public class Element
    {
        //element attributes
        public string name;
        public string symbol;
        public string category; //i.e. metal, noble gas, etc.
        public string electronConfig;
        public string standardPhase;
        public double atomicMass;
        public double boilingPoint;
        public double meltingPoint;
        public double density;
        public int atomicNumber;


        //new constructor
        public Element(string n, string s, string c, string ec, string sp, double d, double am, double bp, double mp, int an)
        {
            this.name = n;
            this.symbol = s;
            this.category = c;
            this.electronConfig = ec; 
            this.standardPhase = sp;
            this.density = d;
            this.atomicMass = am;
            this.boilingPoint = bp;
            this.meltingPoint = mp;
            this.atomicNumber = an;
        }

        public string DisplayElementInfo()
        {
            return "Name: " + name + " Symbol: " + symbol + " Category: " + category + " Electron config: " + electronConfig + " Standard phase: " + standardPhase + " Density: " + density + " Atomic mass: "
                + atomicMass + " Boiling point: " + boilingPoint + " Melting point: " + meltingPoint + " Atomic number: " + atomicNumber;
        }
    }
}
