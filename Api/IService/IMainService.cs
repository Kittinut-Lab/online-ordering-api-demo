using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using online_ordering_api.Models;

namespace online_ordering_api.IService
{
    public interface IMainService
    {
        LoginResponseEntity LoginService(LoginRequestEntity request);

    }
}
