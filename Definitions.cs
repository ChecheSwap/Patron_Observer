using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Patron_Observer
{
    public static class Definitions
    {
        public  static int Imagencount = 0;
        public static void errorMessage(string value)
        {
            MessageBox.Show(value, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void correctMessage(string value)
        {
            MessageBox.Show(value,"Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
