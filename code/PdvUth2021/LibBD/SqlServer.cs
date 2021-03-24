using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace LibBD 
{
    public class SqlServer : BD
    {

        //attribs de SQLServer
        SqlConnection con;
        SqlCommand com;
        //SqlDataReader dr;

        //COnectionString
        string connectionString;

        //constructor
        public SqlServer(string server, string db, string us, string pwd, string port="1433")
        {
            //initialize attribs
            this.SERVER = server;
            this.DBNAME = db;
            this.US     = us;
            this.PWD    = pwd;
            this.PORT   = port;
            //concatenates the connectionString
            this.connectionString = $"Server={this.SERVER},{this.PORT};Database={this.DBNAME};User Id={this.US};Password={this.PWD};";
            //instanciate the connection
            this.con = new SqlConnection(this.connectionString);
        }



        //BND actions

        public override bool Connect()
        {
            bool res = false;
            try 
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                else if (con.State == ConnectionState.Broken) 
                {
                    con.Close(); con.Open();
                }
                //statnlish coprrect executing 
                res = true;
            }
            catch(SqlException sqlex)
            {
                BD.ERROR = "SQL Error at opening SqlServer Connection. " + sqlex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "Error at opening SqlServer Connection. " + ex.Message;
            }
            //return the method result
            return res;
        }
        public override bool Disconnect()
        {
            bool res = false;
            try
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
             
                //statnlish coprrect executing 
                res = true;
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL Error at closing SqlServer Connection. " + sqlex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "Error at closing SqlServer Connection. " + ex.Message;
            }
            //return the method result
            return res;
        }

        public override bool create(string table, List<DataCollection> data)
        {
            //result variable
            bool res = false;

            //bloque try catch
            try
            {
                //open conex
                this.Connect();
                //parse the columns names using the dataCollection
                //new string for the columns
                string columns = "";
                foreach (DataCollection column in data)
                    columns += column.Name + ", ";
                columns = columns.Remove(columns.Length - 2);
                //parse the columns values from the dataCollection
                //string for the columns values
                string values = "";
                foreach (DataCollection column in data)
                    values += column.Value+", ";
                //remove the last comma ','
                values = values.Remove(values.Length - 2);
                //create insert query
                string query = $"INSERT INTO {table} ({columns}) VALUES ({values})";
                //instanciates the command
                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at insert DB action.";              
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at INSERT sentence. "+sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = "I/O ERROR at INSERT sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "General ERROR at INSERT sentence. " + ex.Message;
            }
            finally
            {
                Disconnect();
            }
            //return the create resuklt
            return res;
        }

        public override bool delete(string table, int id)
        {
            //result variable
            bool res = false;

            //bloque try catch
            try
            {
                //open conex
                this.Connect();
                //create delete query
                string query = $"DELETE FROM {table} WHERE id = {id}  ";
                //instanciates the command
                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at Delete DB action.";
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at Delete sentence. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = "I/O ERROR at Delete sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "General ERROR at Delete sentence. " + ex.Message;
            }
            finally
            {
                Disconnect();
            }
            //return the create resuklt
            return res;
        }

      

        public override List<object> index(string table, OrderBy order)
        {
            throw new NotImplementedException();
        }

        public override List<object> read(List<string> fields, string table, List<SearchCollection> search)
        {
            throw new NotImplementedException();
        }

        public override List<object> read(List<string> fields, string table1, string table2, List<string> onFields, List<SearchCollection> search)
        {
            throw new NotImplementedException();
        }

        public override bool update(string table, List<DataCollection> data, int id)
        {
            //result variable
            bool res = false;

            //bloque try catch
            try
            {
                //open conex
                this.Connect();
                //parse the columns names using the dataCollection
                //new string for the columns
                string columnsValues = "";
                foreach (DataCollection column in data)
                    columnsValues += $" {column.Name}={column.Value}, ";
                //remove the last comma ','
                columnsValues = columnsValues.Remove(columnsValues.Length - 2);
                //create insert query
                string query = $"UPDATE {table} SET {columnsValues} WHERE id = {id}  ";
                //instanciates the command
                com = new SqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at update DB action.";
            }
            catch (SqlException sqlex)
            {
                BD.ERROR = "SQL ERROR at UPDATE sentence. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = "I/O ERROR at UPDATE sentence. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "General ERROR at UPDATE sentence. " + ex.Message;
            }
            finally
            {
                Disconnect();
            }
            //return the create resuklt
            return res;
        }
    }
}
