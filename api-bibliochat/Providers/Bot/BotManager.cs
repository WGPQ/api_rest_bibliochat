using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using api_bibliochat.Providers.Auth.Jwt;
using api_bibliochat.Providers.Helpers;
using api_bibliochat.Providers.Bot;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace api_bibliochat.Providers.Bot
{
    public class BotManager : IBotRepository
    {
        private readonly MySqlContext context;
        private readonly IJWTManagerRepository _jWTManager;

        public BotManager(MySqlContext context, IJWTManagerRepository jWTManager)
        {
            this.context = context;
            this._jWTManager = jWTManager;
        }

        public async Task<ResultadoEntity> Actualizar(ConfigBotEntity entity, string token)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;
            try
            {
                string idUser = _jWTManager.verificarToken(token);
                entity.Id = Encript64.DecryptString(entity.Id);
                string query = "CALL sp_actualizar_disponibilidad (" + entity.Id + ",'" + entity.dia + "','" + entity.hora_inicio + "','" + entity.hora_fin + "'," + entity.activo + "," + idUser + ")";

                var list = await this.context.Respuesta.FromSqlRaw(query).ToListAsync();
                result = list.FirstOrDefault();
                if (result.exito)
                {
                    result.data = entity;
                    entity.Id = Encript64.EncryptString(entity.Id);
                }

            }
            catch (Exception ex)
            {

                result.exito = false;
                result.message = ex.Message;
            }


            return result;
        }

        public async Task<ResultadoEntity> Listar(Listar listar)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;
            try
            {
                string query = "CALL sp_listar_disponiblidad('" + listar.columna + "','" + listar.nombre + "'," + listar.offset + "," + listar.limit + ",'" + listar.sort + "')";
                var disponibilidad = await this.context.Bot.FromSqlRaw(query).ToListAsync();
                result.exito = true;
                result.data = disponibilidad.Select(r => EncryptId(r)).ToList();
                result.message = "Resultados obtenidos";
            }
            catch (Exception ex)
            {
                result.exito = false;
                result.message = ex.Message;

            }

            return result;
        }

        public ConfigBotEntity EncryptId(ConfigBotEntity config)
        {
            config.Id = Encript64.EncryptString(config.Id);
            return config;
        }
        public async Task<ResultadoEntity> Obtener(string id)
        {
            ResultadoEntity result = new ResultadoEntity();
            result.exito = false;
            try
            {
                id = Encript64.DecryptString(id);
                string query = "CALL sp_obtener_disponibilidad(" + id + ")";
                var list = await this.context.Bot.FromSqlRaw(query).ToListAsync();
                ConfigBotEntity bot = list.FirstOrDefault();
                if (bot != null)
                {
                    result.exito = true;
                    result.data = bot;
                    bot.Id = Encript64.EncryptString(bot.Id);
                    result.message = "Correcto";
                }
                else
                {
                    result.message = "Registro no encontrado";
                }
            }
            catch (Exception ex)
            {
                result.exito = false;
                result.message = ex.Message;
            }

            return result;
        }

        public async Task<List<ConfigBotEntity>> ExportarExcel(Listar listar)
        {
            List<ConfigBotEntity> roles = new List<ConfigBotEntity>();
            try
            {
                string query = "CALL sp_listar_disponiblidad('" + listar.columna + "','" + listar.nombre + "'," + listar.offset + "," + listar.limit + ",'" + listar.sort + "')";
                roles = await this.context.Bot.FromSqlRaw(query).ToListAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

            return roles;
        }

        public async Task<DisponibilidadEntity> Disponibilidad()
        {
            DisponibilidadEntity disponibilidad = new DisponibilidadEntity();
            var day = DateTime.Now.ToString("dddd", new CultureInfo("es-ES"));
            var hour = DateTime.Now.ToString("HH:mm");
            try
            {
                string query = "CALL sp_verificar_disponibilidad('" + day + "','" + hour + "')";
                var result = await this.context.Disponible.FromSqlRaw(query).ToListAsync();
                disponibilidad = result.FirstOrDefault();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

            return disponibilidad;
        }
    }
}
