using System;
namespace online_ordering_api.Data
{
    public class DBColumnExtension
    {
        //Attribute for mapping column DB
        public sealed class DbColumn : Attribute
        {
            private string _column;
            public string Name => _column;
            public DbColumn(string column)
            {
                _column = column;
            }
        }
    }
}
