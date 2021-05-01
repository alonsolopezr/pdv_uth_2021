using LibBD;
using System;
using System.Collections.Generic;

namespace LibPdvUTh2021.App
{
    public abstract class CRUD
    {
        //attribs
        protected string Table { get; set; }
        protected List<string> FieldList { get; set; }
        public  string ERROR { get; set; }


        //hardcoded Database connection
        //BD libBD = new SqlServer("locahost", "bd_pdvuth_2021", "SA", "SqlServer2017"); "db_pdvuth2021.brands
        BD libBD = new LibBD.MySql("127.0.0.1", "db_pdvuth2021", "us_db_pdvuth2021", "123pwd_laravel321 ", "3306");

        public CRUD(string table, List<string> fieldList)
        {
            Table = table;
            FieldList = fieldList;
        }

        public bool create( List<DataCollection> data)
        {
            return libBD.create(this.Table, data);
        }
        public bool update( List<DataCollection> data, int id)
        {
            return libBD.update(this.Table, data, id);
        }
        public bool delete(int id)
        {
            return libBD.delete(this.Table, id);
        }

        public List<List<object>> index( OrderBy order)
        {

            return libBD.index(this.Table, order);
        }

        /// <summary>
        /// Returns a collection of 'ParaCombo' objects, 
        /// that contains Value and Display members to assign in ComboBoxes
        /// </summary>
        /// <param name="idField">The name of the VALUE to bind to the combo. 'id' by default.</param>
        /// <param name="display">The name of the DISPLAY to bind to the combo. 'name' by default.</param>
        /// <returns>A List of ParaCombo objects to assign to ComboBoxes</returns>
        public List<ParaCombo> index(string idField ="id", string display="name")
        {
            List<ParaCombo> res = new List<ParaCombo>();
            List<List<object>> lista = libBD.index(this.Table, idField, display);
            //iterar 
            foreach (List<object> dato in lista)
            {
                res.Add(new ParaCombo(int.Parse(dato[0].ToString()), dato[1].ToString()));
            }
            return res;
        }

        public List<List<object>> read( List<SearchCollection> search)
        {

            return libBD.read(this.FieldList, this.Table, search);
        }

        public List<List<object>> read(List<string> fieldList, string table2, List<string> on, List<SearchCollection> search)
        {

            return libBD.read(fieldList, this.Table, table2, on, search);
        }
    }
}
