using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_core.Application.Services.Account
{
    public interface IAccountService
    {
        AccountResult Login(
        string Email,
        string Password);
        AccountResult Register(
        string Username,
        string Email,
        string Password);
    }
}