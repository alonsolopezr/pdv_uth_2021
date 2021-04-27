using LibPdvUTh2021.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPdvUTh2021.Products
{
    public class Brand : CRUD
    {
        public Brand(int id, string name, string description, string logo):base("brands", new List<string>() { "id", "name", "description", "logo"})
        {
            Id = id;
            Name = name;
            Description = description;
            Logo = logo;
        } public Brand():base("brands", new List<string>() { "id", "name", "description", "logo"})
        {
         
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }

        //Acciones del CRUD

        //datos para combos
        public List<ParaCombo> comboData() 
        {
            //como por default el Value es ID y el Display en Name, no necesito pasarle nada
            return this.index();
        }

    }
}
