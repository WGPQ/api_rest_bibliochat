using api_bibliochat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Auth.Mail
{
    
    public interface MailUseCase<T> where T : new()
    {
        bool SendEmail(T entiti);
    }

    public interface IMailService : MailUseCase<MailEntity> { }


}
