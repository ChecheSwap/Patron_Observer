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
    public partial class DerivedTwo : genericForm, Observer
    {
        SubjectObserver father;
        xKernelCentral reference;
        public DerivedTwo(SubjectObserver father, object x)
        {
            this.father = father;
            this.reference = (xKernelCentral)father;
            this.MinimizeBox = false;
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void DerivedTwo_Load(object sender, EventArgs e) {
            reference.chart2.Enabled = false;
        }
        public void update(object g)
        {
            double[] z = (double[])g;
            this.label1.Text = z.Length.ToString();
        }

        private void DerivedTwo_FormClosed(object sender, FormClosedEventArgs e)
        {
            reference.chart2.Enabled = true;
            father.DeleteObject(this);
        }
    }
}
