namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface ILoggingFactory
    {
        Task<bool> AddLoggingMessages(string userName, string logLevel, string messageTemplate);

    }
}
