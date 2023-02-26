using System;
using System.Collections.Generic;

namespace online_ordering_api.Data
{
    public interface IDataManager
    {
        List<T> ExecuteReaderDataSet<T>(string queryString) where T : new();
        T ExecuteReaderData<T>(string queryString) where T : class, new();
        void ExecuteNoneQuery(string queryString);
        void WithConectionString(string connectionString);
        string GetConString();
    }
}
