﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Providers.Cliente
{
   
    public interface ClienteUseCase<T, L, R, Rep> where T : new()
    {
        Task<R> Listar(L entiti);

        Task<R> Insertar(T entiti, string token);
        Task<R> Actualizar(T entiti, string token);
        Task<R> Eliminar(string id, string token);

        Task<R> Obtener(string id);
        Task<Rep> ExportarExcel(L entiti);

    }
}
