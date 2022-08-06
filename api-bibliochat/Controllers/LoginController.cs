using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using api_bibliochat.Hubs;
using api_bibliochat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace api_bibliochat.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoriesBot data;

        

        public LoginController(IRepositoriesBot data)
        {
            this.data = data;
            
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login/portal")]
        public Task<ResultadoLogin> Login(LoginParametros parametros)
        {
            try
            {
                
                return data.AuthRepository.Autenticacion(parametros);
            }
            catch (System.Exception)
            {

                throw new System.Exception("Ocurrio un error");
            }
        }

        [HttpPost]
        [Route("actualizar")]
        public Task<ResultadoEntity> Actualizar(ActualizarPass entiti)
        {

            var token = HttpContext.Request.Headers["Authorization"];
            return data.AuthRepository.ActualizarContrasenia(entiti, token);


        }


        [HttpGet]
        [Route("resetear/{id}")]
        public async Task<ResultadoEntity> Reset(string id)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.AuthRepository.ResetearContrasenia(id, token);
        }

        [HttpGet]
        [Route("exit")]
        public async Task<ResultadoEntity> Logout()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.AuthRepository.Logout(token);
        }

        [HttpGet]
        [Route("verificar/token")]
        public Task<ResultadoEntity> Verificar()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return data.AuthRepository.VerificarToken(token);
        }
        
        [HttpGet]
        [Route("verificar/token/cliente")]
        public Task<ResultadoEntity> VerificarCliente()
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return data.AuthRepository.VerificarTokenCliente(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login/chatbot")]
        public Task<ResultadoLogin> AuthBot(LoginParametros parametro)
        {
            try
            {
                return data.AuthRepository.Autenticacion(parametro);
            }
            catch (System.Exception)
            {

                throw new System.Exception("Ocurrio un error");
            }
         
        } 
        
        //[HttpPost]
        //[AllowAnonymous]
        //[Route("auth/cliente")]
        //public Task<ResultadoEntity> AuthCliente(ParametroCorreoEntity parametro)
        //{

        //    return data.AuthRepository.AutenticacionCliente(parametro.correo);
        //}
    }
}
