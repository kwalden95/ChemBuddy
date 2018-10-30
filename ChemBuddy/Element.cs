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
        public float atomicMass;
        public float boilingPoint;
        public float meltingPoint;
        public float density;
        public int atomicNumber;


        //new constructor
        public Element(string n, string s, string c, string ec, string sp, float d, float am, float bp, float mp, int an)
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
    }
}
