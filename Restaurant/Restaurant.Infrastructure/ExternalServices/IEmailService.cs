﻿using Restaurant.Infrastructure.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.ExternalServices
{
    public interface IEmailService
    {
        void SendMail(EmailRequest content);
    }
}
