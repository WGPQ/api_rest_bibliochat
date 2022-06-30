using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Chat
{
    public interface IChatRepository : ChatUseCase<ChatRequest, MessageEntity, ResultadoEntity, Listar> { }

}
