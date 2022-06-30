﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Auth.Jwt
{
    public interface IJWTManagerRepository
    {
        Tokens generateJwtToken(string idusers,int time);

        string verificarToken(string token);
    }
}
