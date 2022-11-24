using System.Threading.Tasks;

namespace api_bibliochat.Providers.Bot
{

    public interface BotUseCase<T, L, R ,U> where T : new()
    {
        Task<R> Listar(L entiti);
        Task<R> Obtener(string id);
        Task<U>Disponibilidad();
        Task<R> Actualizar(T entiti, string token);

    }

}
