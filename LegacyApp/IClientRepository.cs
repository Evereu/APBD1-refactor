namespace LegacyApp;

public interface IClientRepository
{
    /// <summary>
    /// Simulating fetching a client from remote database
    /// </summary>
    /// <returns>Returning client object</returns>
    Client GetById(int clientId);
}