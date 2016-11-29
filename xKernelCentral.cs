using System;
using System.Collections;
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
    public partial class xKernelCentral : genericForm,SubjectObserver
    {
        private IList observers;
        dataBase db;
        private static bool KeyOkOne;
        private static int dotCount;

        public xKernelCentral()
        {
            InitializeComponent();

            db = new dataBase(true);
            KeyOkOne = false;
            dotCount = 0;
            this.observers = new List<Observer>();
            updateGrid();
            this.dataGridView1.Enabled = true;
            this.dataGridView1.ReadOnly = true;
            fillCbox();
            this.label4.Visible = false;
           
        }

        public void insertValues()
        {
            Object [] send = new object[] { Convert.ToDouble(this.value.Text.Trim()), this.motor.Text.Trim() };
            db.InsertRecord(send);
            clearSegments();
            this.updateGrid();
            cbRecords.Items.Clear();
            fillCbox();
        }

        public void clearSegments()
        {
            this.value.Text = null;
            this.motor.Text = null;
            cbRecords.Items.Clear();            
            this.cbRecords.Text = null;
            this.value.Focus();
        }

       /* public double Cantidad
        {
            set
            {
                if (!(value == this.cantidad))
                {
                    this.cantidad = value;
                }
            }
            get
            {
                return this.cantidad;
            }
        }
        */
        public void AddObject(Observer x)
        {
            if (!(observers.Contains(x)))
                observers.Add(x);
        }
        public void DeleteObject(Observer x)
        {
            if (observers.Contains(x))
                observers.Remove(x);
        }
        public void NotifyObjects()
        {
            Observer gen;
            foreach (Object z in observers)
            {
                gen = (Observer)z;
                gen.update(this.getValues());
            }
        }

        private void xKernelCentral_Load(object sender, EventArgs e){}

        private void textBox1_TextChanged(object sender, EventArgs e){}

        private void button2_Click(object sender, EventArgs e)
        {
            db.deletaAll(true);
            updateGrid();
            this.NotifyObjects();
            clearSegments();
            fillCbox();
        }

        private void updateGrid()
        {
            DataSet temp = db.chargeTable();
            temp.Tables[0].Columns["idspeed"].ColumnName = "ID";
            temp.Tables[0].Columns["value"].ColumnName = "VALOR";
            temp.Tables[0].Columns["motor"].ColumnName = "MOTOR";
            this.dataGridView1.DataSource = temp.Tables[0].DefaultView;
        }

        private void value_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void value_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Decimal) || (e.KeyCode == Keys.Oemcomma) || (e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.C))
            {
                switch (e.KeyCode)
                {
                    case (Keys.C):
                        {
                            this.value.Text = null;
                            dotCount = 0;
                            KeyOkOne = false;
                            return;
                        }
                    case (Keys.Decimal):
                        {
                            ++dotCount;
                            if (dotCount > 1)
                            {
                                KeyOkOne = false;
                                return;
                            }
                            return;
                        }
                    case (Keys.Enter):
                        {
                            this.motor.Focus();
                            KeyOkOne = false;
                            return;
                        }
                    default:
                        {
                            KeyOkOne = true;
                            return;
                        }
                } }
            else
                KeyOkOne = false;           
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!KeyOkOne)
                e.Handled = true;
        }

        private void motor_TextChanged(object sender, EventArgs e)  {}

        private void motor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (!(this.motor.Text.Trim() == "" || this.value.Text.Trim() == ""))
                {
                    double temp = Convert.ToDouble(this.value.Text.Trim());
                    if(!(temp <= 0 || temp > 350))
                    {
                        this.insertValues();
                        this.NotifyObjects();
                    }
                        
                    else
                    {
                        Definitions.errorMessage("Ingrese Valores Validos!");
                        clearSegments();
                        this.value.Focus();
                        return;
                    }

                }
                else
                {
                    Definitions.errorMessage("Ingrese Valores Validos!");
                    clearSegments();
                    this.value.Focus();
                }
               }
        }

        private void fillCbox()
        {
            for(int x=0; x< dataGridView1.RowCount; ++x)
            {
                
                this.cbRecords.Items.Add(dataGridView1.Rows[x].Cells[0].Value.ToString());
            }
        }

        private void cbRecords_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(cbRecords.Text == ""))
            {
                deleteSpecificRow();
                updateGrid();
                this.NotifyObjects();
                clearSegments();
                fillCbox();
            }
                
        }

        private void deleteSpecificRow()
        {
            db.deleteRecord(Convert.ToInt16(cbRecords.Text.Trim()));
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DerivedOne myElement = new DerivedOne(this, this.getValues());
            myElement.Show();
            this.observers.Add(myElement);
        }

        private double [] getValues()
        {
            double [] temp = new double[dataGridView1.RowCount];
            for(int x = 0; x<temp.Length; ++x)
            {
                temp[x] = (Double)dataGridView1.Rows[x].Cells[1].Value;
            }
            return temp;
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            DerivedTwo temp = new DerivedTwo(this, this.getValues());
            this.observers.Add(temp);
            temp.Show();
        }

        private void xKernelCentral_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Desea Salir?", "Close Agent", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
               Environment.Exit(0);
            }
            e.Cancel = true;  
            
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            this.label4.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.label4.Visible = false;
        }
    }
}
