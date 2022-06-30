using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using api_bibliochat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IRepositoriesBot data;

        public ChatController(IRepositoriesBot data)
        {
            this.data = data;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ResultadoEntity> Chat(ChatRequest chatRequest)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ChatRepository.Chat(chatRequest,token);
        }

        [HttpPost]
        [Route("mensaje/crear")]
        public async Task<ResultadoEntity> Mesnsaje(MessageEntity mensaje)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ChatRepository.CrearMensaje(mensaje,token);
        }

        [HttpPost]
        [Route("mensaje/{id_chat}/Listar")]
        public async Task<ResultadoEntity> Mensajes(string id_chat,Listar listar)
        {
            return await this.data.ChatRepository.Mensajes(id_chat,listar);
        } 
        [HttpPost]
        [Route("interaccion/Listar")]
        public async Task<ResultadoEntity> Interacciones(string id_user,Listar listar)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ChatRepository.Interacciones(listar,token);
        }



    }
}
