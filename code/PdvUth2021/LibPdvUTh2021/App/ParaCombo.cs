using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPdvUTh2021.App
{
    public class ParaCombo
    {
        public ParaCombo(int value, string display)
        {
            Value = value;
            Display = display;
        }

        public int Value { get; set; }
        public string Display { get; set; }
    }
}
