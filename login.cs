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
    public partial class login : genericForm
    {
        public login()
        {
            InitializeComponent();
            init();
        }

        private void login_Load(object sender, EventArgs e)
        {
            init();
        }
        protected void init()
        {
            this.dos0.Visible = true;
            this.dos2.Visible = false;
            this.dos3.Visible = false;
            this.dos4.Visible = false;
            this.dos5.Visible = false;
            
        }
        protected void onit()
        {
            this.dos0.Visible = false;
            this.dos2.Visible = true;
            this.dos3.Visible = true;
            this.dos4.Visible = true;
            this.dos5.Visible = true;
            this.dos5.Text = null;
            this.dos5.Focus();
        }


        private void dos0_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 100;
            onit();
            this.dos5.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dos5.Text.Trim() == "33")
            {
                xKernelCentral win = new xKernelCentral();
                win.Show();
                this.Visible = false;
                return;
            }
            
                Definitions.errorMessage("Contraseña Incorrecta!");
            onit();

        }

        private void dos5_TextChanged(object sender, EventArgs e)
        {}

        private void dos5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.button1_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Definitions.correctMessage("Hasta Luego!");
        }
    }
}
