using System;

namespace LegacyApp;

public interface IUserCreditService
{
    /// <summary>
    /// This method is simulating contact with remote service which is used to get info about someone's credit limit
    /// </summary>
    /// <returns>Client's credit limit</returns>
    int GetCreditLimit(string lastName, DateTime dateOfBirth);
}