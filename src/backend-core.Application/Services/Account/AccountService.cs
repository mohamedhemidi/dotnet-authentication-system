using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        public AccountResult Login(string Email, string Password)
        {
            return new AccountResult(Guid.NewGuid(), "Username", Email,"token");
        }

        public AccountResult Register(string Username, string Email, string Password)
        {
            return new AccountResult(Guid.NewGuid(), Username, Email,"token");
        }
    }
}