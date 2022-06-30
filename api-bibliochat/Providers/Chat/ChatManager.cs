using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using api_bibliochat.Providers.Auth.Jwt;
using api_bibliochat.Providers.Helpers;
using api_bibliochat.Providers.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace api_bibliochat.Providers.Chat
{
    public class ChatManager : IChatRepository
    {
        private readonly MySqlContext context;
        private readonly IJWTManagerRepository _jWTManager;


        public ChatManager(MySqlContext context, IJWTManagerRepository jWTManager)
        {
            this.context = context;
            this._jWTManager = jWTManager;
        }

        public async Task<ResultadoEntity> Chat(ChatRequest usuarios,string token)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;
            try
            {
                string idUser = _jWTManager.verificarToken(token);
                usuarios.usuario_created = Encript64.DecryptString(usuarios.usuario_created);
                usuarios.usuario_interacted = Encript64.DecryptString(usuarios.usuario_interacted);
                string query = "CALL sp_crear_chat_usuario (" + usuarios.usuario_created + "," + usuarios.usuario_interacted +","+idUser+ ")";

                var chat = await this.context.Chat.FromSqlRaw(query).ToListAsync();
                ChatEntity chatEntity = chat.FirstOrDefault();
                if (chatEntity != null)
                {
                    result.exito = true;
                    chatEntity.chat = Encript64.EncryptString(chatEntity.chat);
                    result.data = chatEntity;
                    result.message = "Correcto";
                }

            }
            catch (Exception ex)
            {

                result.exito = false;
                result.message = ex.Message;
            }

            return result;
        }

        public async Task<ResultadoEntity> CrearMensaje(MessageEntity mensaje, string token)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;
            try
            {
                string idUser = _jWTManager.verificarToken(token);
                mensaje.usuario = Encript64.DecryptString(mensaje.usuario);
                mensaje.chat = Encript64.DecryptString(mensaje.chat);
                Thread.Sleep(200);
                string query = "CALL sp_crear_mensaje (" + mensaje.usuario + "," + mensaje.chat + ",'" + mensaje.contenido + "',"+idUser+")";

                var list = await this.context.Respuesta.FromSqlRaw(query).ToListAsync();
                result = list.FirstOrDefault();
                if (result.exito)
                {
                    mensaje.Id = result.message.Split("-")[1];
                    result.data = await transformar(mensaje);
                    result.message = result.message.Split("-")[0];
                }


            }
            catch (Exception ex)
            {

                result.exito = false;
                result.message = ex.Message;
            }

            return result;
        }

        public async Task<ResultadoEntity> Interacciones(Listar listar,string token)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;

            try
            {
                string idUser = _jWTManager.verificarToken(token);
                string query = "CALL sp_listar_interacciones (" + idUser + ",'" + listar.columna + "','" + listar.nombre + "'," + listar.offset + "," + listar.limit + ",'" + listar.sort + "')";
                var interacciones = await this.context.Interacciones.FromSqlRaw(query).ToListAsync();
                result.exito = true;

                result.data = interacciones.Select(async i => await transformarInteracciones(i));
                result.message = "Lista de interacciones";
            }
            catch (Exception ex)
            {

                result.exito = false;
                result.message = ex.Message;
            }

            return result;
        }




        public async Task<ResultadoEntity> Mensajes(string chat, Listar listar)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;

            try
            {
                chat = Encript64.DecryptString(chat);
                string query = "CALL sp_listar_mensajes (" + chat + ",'" + listar.columna + "','" + listar.nombre + "'," + listar.offset + "," + listar.limit + ",'" + listar.sort + "')";
                var mensajes = await this.context.Mensajes.FromSqlRaw(query).ToListAsync();
                result.exito = true;
                result.data = mensajes.Select(async m => await transformar(m));
                result.message = "Lista de mensajes";
            }
            catch (Exception ex)
            {

                result.exito = false;
                result.message = ex.Message;
            }

            return result;
        }

        async Task<Dictionary<string, dynamic>> transformar(MessageEntity messageEntity)
        {
            var respuesta = new Dictionary<string, dynamic>();
            messageEntity.Id = Encript64.EncryptString(messageEntity.Id);
            messageEntity.chat = Encript64.EncryptString(messageEntity.chat);

            respuesta.Add("id", messageEntity.Id);
            respuesta.Add("chat", messageEntity.chat);
            respuesta.Add("usuario", await usuario(messageEntity.usuario));
            respuesta.Add("contenido", messageEntity.contenido);
            respuesta.Add("createdAt", messageEntity.createdAt);
            return respuesta;
        }



        public async Task<Dictionary<string, dynamic>> transformarInteracciones(InteracionEntity interacionEntity)
        {
            interacionEntity.Id = Encript64.EncryptString(interacionEntity.Id);
            var respuesta = new Dictionary<string, dynamic>
           {
                {   "id",interacionEntity.Id},
                {   "chat", await chat(interacionEntity.chat)},
                {   "usuario_created", await usuario(interacionEntity.usuario_created)},
                {   "usuario_interacted", await usuario(interacionEntity.usuario_interacted)},
            };

            return respuesta;
        }



        public async Task<UsuarioEntity> usuario(string idUsuario)
        {
            string query = "CALL sp_obtener_usuario (" + idUsuario + ")";
            var list = await this.context.Usuarios.FromSqlRaw(query).ToListAsync();
            UsuarioEntity usuario = list.FirstOrDefault();
            if (usuario != null)
            {
                usuario.Id = Encript64.EncryptString(idUsuario);
            }

            return usuario;
        }
        public async Task<int> mensajesnuevos(string idChat)
        {
            string query = "CALL sp_mensajes_nuevos (" + idChat + ")";
            var list = await this.context.Mensajesnuevos.FromSqlRaw(query).ToListAsync();
            NuevosMessages numMensajesNuevos = list.FirstOrDefault();
            if (numMensajesNuevos != null)
            {
                return numMensajesNuevos.nuevos;
            }

            return 0;
        }


        public async Task<Dictionary<string, dynamic>> chat(string idChat)
        {
            var respuesta = new Dictionary<string, dynamic>();
            string query = "CALL sp_obtener_chat (" + idChat + ")";
            var list = await this.context.Mensajes.FromSqlRaw(query).ToListAsync();
            MessageEntity message = list.FirstOrDefault();
            if (message != null)
            {
                message.Id = Encript64.EncryptString(message.Id);
                respuesta.Add("nuevos", await mensajesnuevos(message.chat));
                message.chat = Encript64.EncryptString(message.chat);
                respuesta.Add("id", message.Id);
                respuesta.Add("chat", message.chat);
                respuesta.Add("usuario", await usuario(message.usuario));
                respuesta.Add("visto", message.visto);
                respuesta.Add("contenido", message.contenido);
                respuesta.Add("createdAt", message.createdAt);
            }
            return respuesta;
        }




    }
}
