using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System.Threading.Tasks;

namespace api_bibliochat.Providers
{
    

    public interface AuthUseCase<T, R, R2> where T : new()
    {
        Task<R2> AutenticacionChat(string correo);
        Task<R2> AutenticacionCliente(string correo);
        Task<R> Autenticacion(T entiti);
        Task<R2> ResetearContrasenia(string id, string token);
        Task<R2> ActualizarContrasenia(ActualizarPass data, string token);
        Task<R2> VerificarToken(string token);
        Task<R2> VerificarTokenCliente(string token);
        Task<R2> Logout(string session,string token);
    }
  
}
