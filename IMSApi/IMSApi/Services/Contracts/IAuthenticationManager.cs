using IMSApi.Common.Models;
using IMSApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSApi.Services.Contracts
{
    public interface IAuthenticationManager
    {
        Task<Result> RegisterUser(RegisterUserRequest registerUser);

        AuthenticateResponse Authenticate(AuthenticateRequest request);
        
    }
}
