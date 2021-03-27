using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace LibBD
{
    public class MySql : BD
    {

        //attribs de MySqlServer
        MySqlConnection con;
        MySqlCommand com;
        //MySqlDataReader dr;

        //ConnectionString
        string connectionString;

        //constructor
        public MySql(string server, string db, string us, string pwd, string port = "3306")
        {
            //initialize attribs
            this.SERVER = server;
            this.DBNAME = db;
            this.US = us;
            this.PWD = pwd;
            this.PORT = port;
            //concatenates the connectionString                                   
             this.connectionString = $"Server={this.SERVER};Port={this.PORT};Database={this.DBNAME};Uid={this.US};Pwd={this.PWD};";
           // this.connectionString = $"Server=localhost;Port=3306;Database=db_pdvuth2021;Uid=root;Pwd= ;";

            //instanciate the connection
            this.con = new MySqlConnection(this.connectionString);
        }



        //BD actions

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
            catch (MySqlException MySqlex)
            {
                BD.ERROR = "MySql Error at opening MySqlServer Connection. " + MySqlex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "Error at opening MySqlServer Connection. " + ex.Message;
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
            catch (MySqlException MySqlex)
            {
                BD.ERROR = "MySql Error at closing MySqlServer Connection. " + MySqlex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = "Error at closing MySqlServer Connection. " + ex.Message;
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
                    values += column.Value + ", ";
                //remove the last comma ','
                values = values.Remove(values.Length - 2);
                //create insert query
                string query = $"INSERT INTO {table} ({columns}) VALUES ({values})";
                //instanciates the command
                com = new MySqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at insert DB action.";
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = "MySql ERROR at INSERT sentence. " + MySqlex.Message;
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
                com = new MySqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at update DB action.";
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = "MySql ERROR at UPDATE sentence. " + MySqlex.Message;
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
                com = new MySqlCommand(query, con);
                //execute the insert sentence
                int rows = com.ExecuteNonQuery();
                //validate the execution result
                if (rows == 1) res = true;
                else BD.ERROR = "ERROR, malfunction query at Delete DB action.";
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = "MySql ERROR at Delete sentence. " + MySqlex.Message;
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

        //SELECT *  FROM tabla WHERE 1 ORDER BY campo ASC/DESC
        public override List<List<object>> index(string table, OrderBy order)
        {
            //returns a dynamic list of RECORDS/ROWS, each of these are List<object>
            List<List<object>> res = new List<List<object>>();
            //try catch
            try
            {
                //connect
                this.Connect();
                //create the select query
                string query = $"SELECT * FROM {table} WHERE 1 ORDER BY {order.Name} {order.OrderCriteria}";
                //instanciate the MySql command
                com = new MySqlCommand(query, con);
                //execute (READER) the query
                MySqlDataReader dr = com.ExecuteReader();
                //parse the dataReader
                //is there any RECOIRDS/ROWS from the SELECT
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //lets create a List<object> for each RECORD
                        List<object> row = new List<object>();
                        //read every column of EVERY EACH of the ROWS
                        for (int i = 0; i < dr.FieldCount; i++)
                            row.Add(dr.GetValue(i));
                        //we ADD this List to the res Collection  
                        res.Add(row);
                    }
                }
                else
                {
                    BD.ERROR = "EMPTY TABLE, NO ROWS RESULTED in index.";
                }
                //return
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = $"MySql ERROR in reading the table -{table}- index . " + MySqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = $"I/O ERROR in reading the table -{table}- index. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = $"General ERROR in reading the table -{table}- index. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            //return the Records collection
            return res;
        }

        public override List<List<object>> read(List<string> fields, string table, List<SearchCollection> search)
        {
            //returns a dynamic list of RECORDS/ROWS, each of these are List<object>
            List<List<object>> res = new List<List<object>>();


            //try catch
            try
            {
                //connect
                this.Connect();
                string parsedFields = "";
                //parse the fields collections
                foreach (string col in fields)
                    parsedFields += $"{col},";
                //remove the last comma
                parsedFields = parsedFields.Remove(parsedFields.Length - 2);
                string parsedWhere = "";
                //Parse the search collections
                foreach (SearchCollection criteria in search)
                {   //SELECT * FROM products WHERE -> codBarras like '%12345678%' OR/AND marca_id = 2 (---)  (ORDER BY ??)
                    parsedWhere += $" {criteria.Name} {criteria.parseOperator(criteria.Operator)} {criteria.Value} {criteria.parseLogicOperator(criteria.LogicOp) }";
                }
                //create the select query
                string query = $"SELECT {parsedFields} FROM {table} WHERE {parsedWhere} "; // ORDER BY {order.Name} {order.OrderCriteria}";
                //instanciate the MySql command
                com = new MySqlCommand(query, con);
                //execute (READER) the query
                MySqlDataReader dr = com.ExecuteReader();
                //parse the dataReader
                //is there any RECOIRDS/ROWS from the SELECT
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //lets create a List<object> for each RECORD
                        List<object> row = new List<object>();
                        //read every column of EVERY EACH of the ROWS
                        for (int i = 0; i < dr.FieldCount; i++)
                            row.Add(dr.GetValue(i));
                        //we ADD this List to the res Collection  
                        res.Add(row);
                    }
                }
                else
                {
                    BD.ERROR = "EMPTY TABLE, NO ROWS RESULTED.";
                }
                //return
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = $"MySql ERROR in reading the table -{table}-. " + MySqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = $"I/O ERROR in reading the table -{table}-. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = $"General ERROR in reading the table -{table}-. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            //return the Records collection
            return res;
        }

        public override List<List<object>> read(List<string> fields, string table1, string table2, List<string> onFields, List<SearchCollection> search)
        {
            //returns a dynamic list of RECORDS/ROWS, each of these are List<object>
            List<List<object>> res = new List<List<object>>();
            //try catch
            try
            {
                //connect
                this.Connect();
                string parsedFields = "";
                //parse the fields collections
                foreach (string col in fields)
                    parsedFields += $"{col},";
                //remove the last comma
                parsedFields = parsedFields.Remove(parsedFields.Length - 2);
                string parsedWhere = "";
                //Parse the search collections
                foreach (SearchCollection criteria in search)
                {   //SELECT * FROM products WHERE -> codBarras like '%12345678%' OR/AND marca_id = 2 (---)  (ORDER BY ??)
                    parsedWhere += $" {criteria.Name} {criteria.parseOperator(criteria.Operator)} {criteria.Value} {criteria.parseLogicOperator(criteria.LogicOp) }";
                }
                //parse the onQuery
                //the Onstring
                string onInnerString = "";
                foreach (string col in onFields)
                    onInnerString += $" {col} "; //table1.id = tabla2.fk AND products.id = sales_details.product_id
                //create the select query
                string query = $"SELECT {parsedFields} FROM {table1} INNER JOIN {table2} ON {onInnerString}  WHERE {parsedWhere} "; // ORDER BY {order.Name} {order.OrderCriteria}";
                //instanciate the MySql command
                com = new MySqlCommand(query, con);
                //execute (READER) the query
                MySqlDataReader dr = com.ExecuteReader();
                //parse the dataReader
                //is there any RECOIRDS/ROWS from the SELECT
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //lets create a List<object> for each RECORD
                        List<object> row = new List<object>();
                        //read every column of EVERY EACH of the ROWS
                        for (int i = 0; i < dr.FieldCount; i++)
                            row.Add(dr.GetValue(i));
                        //we ADD this List to the res Collection  
                        res.Add(row);
                    }
                }
                else
                {
                    BD.ERROR = "EMPTY TABLE, NO ROWS RESULTED on INNER JOIN QUERY.";
                }
                //return
            }
            catch (MySqlException MySqlex)
            {
                BD.ERROR = $"MySql ERROR in reading on INNER JOIN QUERY the table -{table1}- and table -{table2}-. " + MySqlex.Message;
            }
            catch (IOException ioex)
            {
                BD.ERROR = $"I/O ERROR in reading on INNER JOIN QUERY the table -{table1}- and table -{table2}-. " + ioex.Message;
            }
            catch (Exception ex)
            {
                BD.ERROR = $"General ERROR in reading on INNER JOIN QUERY the table -{table1}- and table -{table2}-. " + ex.Message;
            }
            finally
            {
                this.Disconnect();
            }
            //return the Records collection
            return res;
        }



        public override string ToString()
        {
            return base.ToString();
        }
    }
}
