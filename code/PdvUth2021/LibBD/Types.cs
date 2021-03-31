namespace LibBD
{
    /// <summary>
    /// These are the Filed Types most common used for database table design.
    /// These are used by the programmer to stablish the TYPE of the FIELD 
    /// in the DataCollection, for executing DB actions.
    /// </summary>
    public enum Types
    {
        VARCHAR,
        NVARCHAR,
        TEXT,
        INT, 
        TINYINT,
        DOUBLE, 
        FLOAT, 
        DATE, 
        TIME,
        DATETIME,
        BLOB,
    }
}