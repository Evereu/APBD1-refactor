using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;
        private readonly IUserDataAccess _userDataAccess;

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
            _userDataAccess = new UserDataAccessAdapter();
        }
        
        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataAccess userDataAccess)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _userDataAccess = userDataAccess;
        }
        
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var clientRepository = _clientRepository;
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                var userCreditService = _userCreditService;
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                var userCreditService = _userCreditService;
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
                
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            
            AddUser(user);
            
            return true;
        }

        public void AddUser(User user)
        {
            _userDataAccess.AddUser(user);
        }

        

    }
}
