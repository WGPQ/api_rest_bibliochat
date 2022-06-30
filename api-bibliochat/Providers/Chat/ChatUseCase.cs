using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Chat
{
  
    public interface ChatUseCase<T, M, R, L> where T : new()
    {
        Task<R> Chat(T usuarios, string token);

        Task<R> CrearMensaje(M mensaje, string token);

        Task<R> Mensajes(string chat, L listar);
        Task<R> Interacciones(L listar, string token);
    }

   
}
