namespace LegacyApp;

public class UserDataAccessAdapter : IUserDataAccess
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}