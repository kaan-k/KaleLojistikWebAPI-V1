﻿using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateTokenForUser(BusinessUser user);
    }
}
