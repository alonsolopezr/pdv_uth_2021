using LibPdvUTh2021.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPdvUTh2021.Products
{
    public class Subcategory:CRUD
    {
        public Subcategory(int id, string name, string description, int categoryId) : base("subcategories", new List<string>() { "id", "name", "description", "category_id" })
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;


        }
        public Subcategory() : base("subcategories", new List<string>() { "id", "name", "description", "category_id"})
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        //Acciones del CRUD

        //datos para combos
        public List<ParaCombo> comboData()
        {
            //como por default el Value es ID y el Display en Name, no necesito pasarle nada
            return this.index();
        }
    }
}
