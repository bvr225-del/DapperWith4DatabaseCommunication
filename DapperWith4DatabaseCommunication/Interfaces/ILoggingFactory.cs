namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface ILoggingFactory
    {
        Task<bool> AddLoggingMessages(string userName, string logLevel, string messageTemplate);
         Task<bool> Add_OrderLoggingMessages(string userName, string logLevel, string messageTemplate);

        Task<bool> Add_DepartmentLoggingMessages(string userName, string logLevel, string messageTemplate);

        Task<bool> Add_RestaurantLoggingMessages(string userName, string logLevel, string messageTemplate);



    }
}
