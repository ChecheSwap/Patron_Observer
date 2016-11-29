using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Patron_Observer
{
    public partial class DerivedOne : genericForm , Observer
    {
        
        private SubjectObserver father;
        private xKernelCentral dependency;
        private int[] seriesArray;
        private double[] pointsarray;

        public DerivedOne(SubjectObserver father,Object x)
        {
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            InitializeComponent();
            this.father = father;
            this.dependency = (xKernelCentral)father;
            this.update(x);
            
        }
        
        private void DerivedOne_Load(object sender, EventArgs e)
        {
            this.dependency.chart1.Enabled = false;
            /*string[] seriesArray = new string[] {"Cat","Dogs"};
            int[] pointsArray = new int[] { 1, 2 };
            this.chart1.Titles.Add("Ejemplo");
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            for(int x=0; x<seriesArray.Length;++x)
            {
                Series series = this.chart1.Series.Add(seriesArray[x]);
                series.Points.Add(pointsArray[x]);      
            }
            */
            
        }

        public void update(Object g)
        {
            this.chart1.Series.Clear();
            this.chart1.Titles.Clear();
            if(g.GetType().Equals(typeof(double[])))
            {
                this.pointsarray = (double[])g;
                this.seriesArray = new int[this.pointsarray.Length];

                Array.Sort(this.pointsarray);

                Series newserie = chart1.Series.Add("Valores");
                for (int x=0; x<pointsarray.Length; ++x)
                {
                    seriesArray[x] = x + 1;
                    
                    newserie.Points.AddXY(this.seriesArray[x],this.pointsarray[x]); 
                }
                this.chart1.Titles.Add("Valores Ordenados km/h");
                
            }
        }

        private void DerivedOne_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.dependency.chart1.Enabled = true;
            this.dependency.DeleteObject(this);
        }

        private void guardardatos_Click(object sender, EventArgs e)
        {
            DialogResult RESULT = this.fbd1.ShowDialog();
            if(RESULT == DialogResult.OK)
            {
                Definitions.Imagencount++;
                this.chart1.SaveImage(fbd1.SelectedPath + "\\chart"+Definitions.Imagencount.ToString()+".png", ChartImageFormat.Png);
                Definitions.correctMessage("La Imagen se ha creado satisfactoriamente!");
            }
        }

        private void DerivedOne_FormClosing(object sender, FormClosingEventArgs e)
        {}
    }
}
