using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Phrase
{
   
    public interface IFraceRepository : FraceUseCase<FracesEntity, Listar, ResultadoEntity,List<FracesEntity>> { }
}
