using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Bot
{

    public interface IBotRepository : BotUseCase<ConfigBotEntity, Listar, ResultadoEntity,DisponibilidadEntity> { }
}
