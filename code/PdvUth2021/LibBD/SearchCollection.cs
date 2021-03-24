namespace LibBD
{
    public class SearchCollection
    {
        public string Name { get; set; }
        public CriteriaOperator Operator {  get;  set;  }
        public object Value { get; set; }
        
        public bool IsVarchar { get; set; }
        public LogicOperator LogicOp { get; set; }
}
}