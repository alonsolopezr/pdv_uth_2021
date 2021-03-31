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
        //BD libBD = new SqlServer("locahost", "bd_pdvuth_2021", "SA", "SqlServer2017");
        BD libBD = new LibBD.MySql("locahost", "db_pdvuth2021", "root", " ", "3306");

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
