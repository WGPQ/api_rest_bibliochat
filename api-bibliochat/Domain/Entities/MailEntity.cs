﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Domain.Entities
{
    public class MailEntity
    {
        public string toEmail { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
    }
}
