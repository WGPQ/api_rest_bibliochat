﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Domain.Entities
{
    public class SendChatEntity
    {
        public string comentario { get; set; }
        public string image { get; set; }
        public UsuarioEntity usuario { get; set; }
    }
}
