using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B19Ex05_LidorTevet_312465040_SaharHagbi_308145853
{
    public class Utilities
    {
        public enum eBoardSize
        {
            SIZE_6X6 = 6,
            SIZE_8X8 = 8,
            SIZE_10X10 = 8,
            SIZE_12X12 = 12
        }

        public enum eButtonSize
        {
            WIDTH = 50,
            HEIGHT = 50
        }

        public enum eHowTheEndFormClose
        {
            YesButtonClicked = 0,
            NoButtonClicked = 1,
            ClosedButtonClicked = 2
        }

        public static bool Human
        {
            get { return false; }
        }

        public static bool Computer
        {
            get { return true; }
        }
    }
}