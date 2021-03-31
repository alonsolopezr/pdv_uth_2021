namespace LibBD
{
    public class SearchCollection
    {
        public string Name { get; set; }
        public CriteriaOperator Operator {  get;  set;  }
        public object Value { get; set; }
        
        public bool IsVarchar { get; set; }
        public LogicOperator LogicOp { get; set; }

        public string parseOperator(CriteriaOperator op) 
        {
            //var to return the operator string
            string res="";
            //=  EQUAL,
            //!= NOT_EQUAL,
            //> GREATER_THAN,
            //< LESS_THAN,
            //>= GREATER_THAN_EQ,
            //<= LESS_THAN_EQ,
            //like LIKE
            switch(op)
            {
                case CriteriaOperator.EQUAL:
                    res = "=";
                    break;
                case CriteriaOperator.NOT_EQUAL:
                    res = "!=";
                    break;
                case CriteriaOperator.GREATER_THAN:
                    res = ">";
                    break;
                case CriteriaOperator.LESS_THAN:
                    res = "<";
                    break;
                case CriteriaOperator.GREATER_THAN_EQ:
                    res = ">=";
                    break;
                case CriteriaOperator.LESS_THAN_EQ:
                    res = "<=";
                    break;
                case CriteriaOperator.LIKE:
                    res = " LIKE ";
                    break;
                default:
                   
                    break;
            }
            return res;
        }
        public string parseLogicOperator(LogicOperator op)
        {
            //var to return the operator string
            string res = "";
            //AND OR o ""
            switch (op)
            {
                case LogicOperator.AND:
                    res = "AND";
                    break;
                case LogicOperator.OR:
                    res = "OR";
                    break;
                case LogicOperator.NOTHING:
                    res = " ";
                    break;
              
                default:

                    break;
            }
            return res;
        }
    }
}