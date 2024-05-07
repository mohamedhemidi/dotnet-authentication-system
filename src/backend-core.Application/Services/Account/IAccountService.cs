using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace backend_core.Application.Services.Account
{
    public interface IAccountService
    {
        ErrorOr<AccountResult> Login(
        string Email,
        string Password);
        ErrorOr<AccountResult> Register(
        string Username,
        string Email,
        string Password);
    }
}