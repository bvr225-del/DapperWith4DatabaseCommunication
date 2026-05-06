using Microsoft.Data.SqlClient;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IConnectionFactory
    {
        //WE NEED TO CREATE A METHOD FOR EACH DATABASE CONNECTION STRING IN THE CONNECTION FACTORY INTERFACE AND IMPLEMENT IT IN THE CONNECTION FACTORY CLASS.
        SqlConnection MidLandSqlConnectionString();
        SqlConnection Northwind_DBSqlConnectionString();
        SqlConnection HotelmanagementsqlConnectionString();
        SqlConnection RestaurantDBSqlConnectionString();

    }
}
