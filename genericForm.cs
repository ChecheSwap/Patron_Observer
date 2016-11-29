using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Patron_Observer
{
    public partial class genericForm : Form
    {
        public genericForm()
        {
            InitializeComponent();
            this.Text = null;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void genericForm_Load(object sender, EventArgs e)
        {}

        private void genericForm_FormClosing(object sender, FormClosingEventArgs e)
        {}
    }
}
