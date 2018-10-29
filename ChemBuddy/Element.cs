using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemBuddy
{
    class Element
    {
        //element attributes
        string name;
        string symbol;
        string category; //i.e. metal, noble gas, etc.
        string electronConfig;
        string standardPhase;
        float atomicMass;
        float boilingPoint;
        float meltingPoint;
        float density;
        int atomicNumber;


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
