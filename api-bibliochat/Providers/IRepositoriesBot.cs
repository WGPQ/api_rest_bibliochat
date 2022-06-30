using api_bibliochat.Providers.Auth;
using api_bibliochat.Providers.Chat;
using api_bibliochat.Providers.Cliente;
using api_bibliochat.Providers.Intention;
using api_bibliochat.Providers.Phrase;
using api_bibliochat.Providers.Rol;
using api_bibliochat.Providers.User;

namespace api_bibliochat.Providers
{
    public interface IRepositoriesBot 
    {

        IUsuarioRepository UsuarioRepository { get; }
        IClienteRepository ClienteRepository { get; }
        IRolRepository RolRepository { get; }
        IAuthRepository AuthRepository { get; }
        IIntencionRepository IntencionRepository { get; }
        IFraceRepository FraceRepository { get; }
        IChatRepository ChatRepository { get; }
    }
}