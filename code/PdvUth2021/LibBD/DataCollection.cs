namespace LibBD
{
    public class DataCollection
    {
        //vars
        public string Name{get; set;}
        public Types FieldType { get; set; }
        private object fieldValue;
        public object Value 
        {
            get
            {
                if (this.valueHasQuotes())
                    return $"'{this.fieldValue}'";
                return this.fieldValue;
            }
            set
            {
                fieldValue = value;
            }
        }

        //constructor
        /// <summary>
        /// Create a FIELD that will work to MAP a COLUMN in a DB table.
        /// This object will be used to executer DB actions
        /// </summary>
        /// <param name="name">The name of the field in the table to be used.</param>
        /// <param name="fieldType">The Column Type used in the Table, that is included in the 'Types' Enumeration</param>
        /// <param name="value">The Column Value to be used in the DB action.</param>
        public DataCollection(string name, Types fieldType, object value)
        {
            Name = name;
            FieldType = fieldType;
            Value = value;
        }

        private bool valueHasQuotes()
        {
            //INT, 
            //TINYINT,
            //DOUBLE, 
            //FLOAT, 
            return !(this.FieldType == Types.INT || this.FieldType == Types.FLOAT || this.FieldType == Types.DOUBLE || this.FieldType == Types.TINYINT);
        }


        
    }
}