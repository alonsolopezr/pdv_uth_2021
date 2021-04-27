using System;
using System.Collections.Generic;

namespace LibBD
{
    /// <summary>
    /// This Super Class helps to define a CONCRETE class that will execute DB actions
    /// employing this attributes to generate the ConectionString to each DBMS.
    /// </summary>
    public abstract class BD
    {
        
        //props
        public static string ERROR;
        //db props
        public string SERVER { get; set; }
        public string DBNAME { get; set; }
        public string US { get; set; }
        public string PWD { get; set; }
        public string PORT { get; set; }


        //métodos abstractos
        /// <summary>
        /// Create a new ROW in the stablished table on the parameters. 
        /// </summary>
        /// <param name="table">The table that will creates a new row</param>
        /// <param name="data">The data that will be used to create the row. It includes the field name, type, and Value.</param>
        /// <returns>True if the row is created. False if there's an Error. </returns>
        public abstract bool create(string table, List<DataCollection> data);
        //INSERT INTO t (c1, c2, c3...CN) VALUES ('v1', 'v2', v3...'vN')
        /// <summary>
        /// Updates a ROW with the ID and Data provided in the parameters, on the table stablished.
        /// </summary>
        /// <param name="table">the table that will update the ROW with the ID on input.</param>
        /// <param name="data">The data that will UPDATE the ROW in question.</param>
        /// <param name="id">The ID of the ROW to be updated.</param>
        /// <returns>True if the row is updated. False if there's an Error. </returns>
        public abstract bool update(string table, List<DataCollection> data, int id);
        /// <summary>
        /// Deletes a ROW with the ID in the parameters, on the table stablished.
        /// </summary>
        /// <param name="table">the table that will deletes the ROW with the ID on input.</param>
        /// <param name="id">The ID of the ROW to be deleted.</param>
        /// <returns>True if the row is deleted. False if there's an Error. </returns>
        public abstract bool delete(string table, int id);

        /// <summary>
        /// Selects a ROW SET that will result of a Query on the Table stablished, with the search criteria on the input parameters.
        /// </summary>
        /// <param name="fields">The Fileds to be given on the ROW set</param>
        /// <param name="table">The table from which the query will be made</param>
        /// <param name="search">The search criteria and their logic</param>
        /// <returns>A list of objects that will represent a ROW each one of them, and every ROW is a List object.</returns>
        public abstract List<List<object>> read(List<string> fields, string table, List<SearchCollection> search);
        /// <summary>
        /// Selects a ROW SET that will result of a Query on the Tables on the
        /// intersection by an inner join, between table1 and table2, with OnFields list 
        /// and the search criteria on the input parameters.
        /// </summary>
        /// <param name="fields">The Fileds to be given on the ROW set</param>
        /// <param name="table1">The table 1 from which the query will be making an inner join</param>
        /// <param name="table1">The table 2 to which the inner join will be made</param>
        /// <param name="onFields">The fields conditionals the will allow to generate and intersection</param>
        /// <param name="search">The search criteria and their logic</param>
        /// <returns>A list of objects that will represent a ROW each one of them, and every ROW is a List object.</returns>
        public abstract List<List<object>> read(List<string> fields, string table1, string table2, List<string> onFields, List<SearchCollection> search);
        /// <summary>
        /// Retrieves the complete ROW SET of a table, order by the field in the input parametrs
        /// </summary>
        /// <param name="table">The table from which to retrive the complete ROW SET</param>
        /// <param name="order">The filed to be employ in the order, ASC or DESC</param>
        /// <returns>A list of objects that will represent a ROW each one of them, and every ROW is a List object</returns>
        public abstract List<List<object>> index(string table, OrderBy order); 
        
        /// <summary>
        /// Retrieves the Id and Name of the ROW SET of a table, for using with combos
        /// </summary>
        /// <param name="table">The table from which to retrive the complete ROW SET</param>
        /// <param name="order">The filed to be employ in the order, ASC or DESC</param>
        /// <returns>A list of objects that will represent a ROW each one of them, and every ROW is a List object</returns>
        public abstract List<List<object>> index( string table, string idField = "id", string display="name");

        /// <summary>
        /// Opens the connection to the SERVER stablished by the connectionString
        /// </summary>
        /// <returns></returns>
        public abstract bool Connect();

        /// <summary>
        /// Closes the connection to the SERVER stablished by the connectionString
        /// </summary>
        /// <returns></returns>
        public abstract bool Disconnect();
    }
}
