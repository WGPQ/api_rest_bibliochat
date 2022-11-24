using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using System.Threading.Tasks;

namespace api_bibliochat.Providers
{

    public interface BotUseCase<T, L, R,Rep> where T : new()
    {
        Task<R> Listar(L entiti);

        Task<R> Insertar(T entiti, string token);
        Task<R> Actualizar(T entiti, string token);
        Task<R> Eliminar(string id, string token);

        Task<R> Obtener(string id);
        Task<Rep> ExportarExcel(L entiti);

    }

}
