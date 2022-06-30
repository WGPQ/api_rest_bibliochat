using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.User
{
  
    public interface IUsuarioRepository : UsuarioUseCase<UsuarioEntity, Listar, ResultadoEntity,List<UsuarioEntity>> { }
}
