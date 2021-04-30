using LibBD;
using LibPdvUTh2021.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibPdvUTh2021.Products
{
    public class Product : CRUD
    {


        //constructor                                         // id	name	description	price	bar_code	brand_id	subcategory_id	image	measure_unit	sku	
        public Product(): base("products", new List<string>() { "id","name", "description","price", "bar_code","brand_id","subcategory_id","image","measure_unit", "sku"})
        {
           
        }

        //attribs
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string barCode { get; set; }
        public int BrandId { get; set; }

        public int SubcategoryId { get; set; }

        public string MetricUnit { get; set; }

        public string Sku { get; set; }

        //constructor

        //CRUD
        public bool save( string name, string description, double price, string barCode, int brandId, int subCategoryId, string metricUnit, string image )//, string sku)
        {
            List<DataCollection> data = new List<DataCollection>();
           // data.Add(new DataCollection("id", Types.INT,id));
            data.Add(new DataCollection("name", Types.VARCHAR, name));
            data.Add(new DataCollection("description", Types.VARCHAR, description));
            data.Add(new DataCollection("bar_code", Types.VARCHAR, barCode));
            data.Add(new DataCollection("brand_id", Types.INT, brandId));
            data.Add(new DataCollection("subcategory_id", Types.INT, subCategoryId));
            data.Add(new DataCollection("price", Types.INT, price));
            data.Add(new DataCollection("measure_unit", Types.VARCHAR, metricUnit));
            data.Add(new DataCollection("image", Types.VARCHAR, image));
            //data.Add(new DataCollection("sku", Types.VARCHAR, sku));
            bool res= base.create(data);
            if (!res) this.ERROR = BD.ERROR;
            return res;
        }

    }
}
