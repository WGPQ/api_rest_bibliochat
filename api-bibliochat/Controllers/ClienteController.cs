using api_bibliochat.Domain;
using api_bibliochat.Domain.Entities;
using api_bibliochat.Providers;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IRepositoriesBot data;

        public ClienteController(IRepositoriesBot data)
        {
            this.data = data;
        }

        [HttpPost]
        [Route("Insertar")]
        public async Task<ResultadoEntity> Post(ClienteEntity cliente)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ClienteRepository.Insertar(cliente, token);

        }
        [HttpPost]
        [Route("Actualizar")]
        public async Task<ResultadoEntity> Update(ClienteEntity clientel)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ClienteRepository.Actualizar(clientel, token);

        }
        [HttpGet]
        [Route("Obtener/{id}")]
        public async Task<ResultadoEntity> GetBayId(string id)
        {
            return await this.data.ClienteRepository.Obtener(id);
        }

        ///// <summary>
        ///// Este enpoint lista todo los registros de la tabla rol.
        ///// </summary>
        ///// <param name="columna"></param>
        ///// <param name="nombre"></param>
        ///// <param name="offset"></param>
        ///// <param name="limit"></param>
        ///// <param name="sort"></param>
        ///// 
        ///    /// <summary>
        /// Este enpoint lista todo los registros de la tabla rol.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="nombre"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// 
        /// <returns></returns>
        [HttpGet]
        [Route("Listar")]
        public async Task<ResultadoEntity> Get(string columna, string nombre, int offset, int limit, string sort)
        {
            Listar listar = new Listar();
            listar.columna = columna;
            listar.nombre = nombre;
            listar.offset = offset;
            listar.limit = limit;
            listar.sort = sort;

            return await this.data.ClienteRepository.Listar(listar);

        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ResultadoEntity> Delete(string id)
        {
            var token = HttpContext.Request.Headers["Authorization"];
            return await this.data.ClienteRepository.Eliminar(id, token);
        }

        [HttpGet]
        [Route("Exportar")]
        public async Task<IActionResult> Exportar(string columna, string nombre, int offset, int limit, string sort)
        {
            Listar listar = new Listar();
            listar.columna = columna;
            listar.nombre = nombre;
            listar.offset = offset;
            listar.limit = limit;
            listar.sort = sort;
            var listaClientes = await this.data.ClienteRepository.ExportarExcel(listar);
            using (var workbook = new XLWorkbook())
            {
                var hojaTrabajo = workbook.Worksheets.Add("Clientes");
                var filaActual = 1;
                hojaTrabajo.Cell(filaActual, 1).Value = "Id";
                hojaTrabajo.Cell(filaActual, 2).Value = "Nombre";
                hojaTrabajo.Cell(filaActual, 3).Value = "Correo";
                hojaTrabajo.Cell(filaActual, 4).Value = "Rol";

                foreach (var cliente in listaClientes)
                {
                    filaActual++;
                    hojaTrabajo.Cell(filaActual, 1).Value = cliente.Id;
                    hojaTrabajo.Cell(filaActual, 2).Value = cliente.nombre;
                    hojaTrabajo.Cell(filaActual, 3).Value = cliente.correo;
                    hojaTrabajo.Cell(filaActual, 4).Value = cliente.rol;

                }

                using (var memoia = new MemoryStream())
                {
                    workbook.SaveAs(memoia);
                    var contenido = memoia.ToArray();
                    return File(contenido, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Clientes.xlsx");
                }
            }
        }
    }
}
