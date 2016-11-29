using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Data;

namespace Patron_Observer
{
    public class dataBase
    {
        private static readonly string stringconn = "server=localhost;database=speeds;uid=root;pwd=master;";
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataAdapter mysqladapter;
        private MySqlDataReader xreader;
        
        public dataBase(bool verifier)
        {
            if (verifier) {
                try
                {
                    this.connection = new MySqlConnection(stringconn);
                    this.connection.Open();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Database System Advisor", "Error al intentar conexion a base de datos"+ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
         
                }
        }
        public bool checkContent()
        {
            this.command = this.connection.CreateCommand();
            command.CommandText = "SELECT * FROM speeds";
            this.xreader = command.ExecuteReader();

            if (xreader.HasRows)
            {
                xreader.Close();
                return true;
            }
            xreader.Close();
            return false;
        }

        public void InsertRecord(object[]sets)
        {
            if(checkState())
            {
                try
                {
                    this.command = this.connection.CreateCommand();
                    this.command.CommandText = "INSERT INTO speeds(value,motor) VALUES(@a,@b)";
                    this.command.Parameters.AddWithValue("@a", Convert.ToDouble(sets[0]));
                    this.command.Parameters.AddWithValue("@b", sets[1].ToString());
                    command.ExecuteNonQuery();
                    command.Dispose();
                    Definitions.correctMessage("Se ha agregado satisfactoriamente el registro!");
                }
                catch(Exception ex)
                {
                    Definitions.errorMessage(ex.ToString());
                }
               
            }
            
        }

        public void deleteRecord(int id)
        {
            if (checkState())
            {
                if(checkContent())
                { 
                try
                {
                    this.command = this.connection.CreateCommand();
                    this.command.CommandText = "DELETE FROM speeds WHERE idspeed = @x";
                    this.command.Parameters.AddWithValue("@x", id);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    Definitions.correctMessage("Se ha eliminado el registro!");
                }
                catch (Exception ex)
                {
                    Definitions.errorMessage(ex.ToString());
                }

            }
            }
            else
            {
                Definitions.errorMessage("La tabla esta vacia");
            }
        }

        public void deletaAll(bool x)
        {
            if(x)
            {
                if (checkState())
                {
                    if (checkContent())
                    {
                        try
                        {
                            this.command = this.connection.CreateCommand();
                            this.command.CommandText = "DELETE FROM speeds";
                            command.ExecuteNonQuery();
                            this.command.CommandText = "ALTER TABLE speeds auto_increment = 1";
                            command.ExecuteNonQuery();
                            command.Dispose();
                            Definitions.correctMessage("Echo!");
                        }
                        catch (Exception ex)
                        {
                            Definitions.errorMessage(ex.ToString());
                        }

                    }
                    else
                    {
                        Definitions.errorMessage("La tabla esta Vacia!");
                    }
                }
                

            }
        }

        public DataSet chargeTable()
        {
            DataSet grouptable  = new DataSet();
            if (checkState())
            {
                this.command = new MySqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandText = "SELECT * FROM speeds";
                this.mysqladapter = new MySqlDataAdapter(command);
                try
                {
                    mysqladapter.Fill(grouptable);
                }
                catch(Exception ex)
                {
                    Definitions.errorMessage(ex.ToString());
                }
                
            }
            return grouptable;
        }
        public bool checkState()
        {
            if (connection.State == ConnectionState.Open)
                return true;
            return false;
        }
        public void Open()
        {
            if (!(connection.State == System.Data.ConnectionState.Open))
                this.connection.Open();
        }
        public void close()

        {
            if (!(connection.State == System.Data.ConnectionState.Closed))
                this.connection.Close();
        }
    }
}
