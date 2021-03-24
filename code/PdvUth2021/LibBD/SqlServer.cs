using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

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
            this.US = us;
            this.PWD = pwd;
            this.PORT = port;
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
            throw new NotImplementedException();
        }

        public override bool delete(string table, int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
