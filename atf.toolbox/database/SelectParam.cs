using System;

namespace atf.toolbox.database
{
    /// <summary>
    /// SelectParam
    /// </summary>
    public class SelectParam
    {
        public string ColumnName { get; set; }
        public object ColumnValue { get; set; }

        public SelectParam(string name, object value)
        {
            ColumnName = name;
            ColumnValue = value;
        }
    }
}
