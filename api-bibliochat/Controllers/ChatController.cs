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
        [Route("send/Chat")]
        public async Task<ResultadoEntity> SenChat(SendChatEntity send)
        {
            return await this.data.ChatRepository.SendChat(send);
        }

        [HttpGet]
        [Route("mensaje/Listar")]
        public async Task<ResultadoEntity> Mensajes(string chat, string columna, string nombre, int offset, int limit, string sort)
        {
            Listar listar = new Listar();
            listar.columna = columna;
            listar.nombre = nombre;
            listar.offset = offset;
            listar.limit = limit;
            listar.sort = sort;
            return await this.data.ChatRepository.Mensajes(chat,listar);
        }

        [HttpGet]
        [Route("mensajes/Session")]
        public async Task<ResultadoEntity> MensajesBySession(string session)
        {
            return await this.data.ChatRepository.MessagesBySession(session);
        }

        [HttpGet]
        [Route("sesiones/Listar")]
        public async Task<ResultadoEntity> Sesiones(string usuario, string columna, string nombre, int offset, int limit, string sort)
        {
            Listar listar = new Listar();
            listar.columna = columna;
            listar.nombre = nombre;
            listar.offset = offset;
            listar.limit = limit;
            listar.sort = sort;
            return await this.data.ChatRepository.SessionesByUser(usuario, listar);
        }


        [HttpGet]
        [Route("interaccion/Listar")]
        public async Task<ResultadoEntity> Interacciones(string columna, string nombre, int offset, int limit, string sort)
        {
            Listar listar = new Listar();
            listar.columna = columna;
            listar.nombre = nombre;
            listar.offset = offset;
            listar.limit = limit;
            listar.sort = sort;
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ChatRepository.Interacciones(listar,token);
        }



    }
}
