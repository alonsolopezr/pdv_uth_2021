using LibPdvUTh2021.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPdvUTh2021.Products
{
    public class Category:CRUD
    {
        public Category(int id, string name, string description, string logo) : base("categories", new List<string>() { "id", "name", "description" })
        {
            Id = id;
            Name = name;
            Description = description;
   
        }
        public Category() : base("categories", new List<string>() { "id", "name", "description"})
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Acciones del CRUD

        //datos para combos
        public List<ParaCombo> comboData()
        {
            //como por default el Value es ID y el Display en Name, no necesito pasarle nada
            return this.index();
        }
    }
}
