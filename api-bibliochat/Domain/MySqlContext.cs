﻿using api_bibliochat.Domain.Entities;
using api_bibliochat.Providers.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api_bibliochat.Domain
{
    public class MySqlContext:DbContext
    {


        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here   
            modelBuilder.Entity<ResultadoEntity>().HasNoKey();
            modelBuilder.Entity<ResultadoLogin>().HasNoKey();
            modelBuilder.Entity<ChatEntity>().HasNoKey();
            modelBuilder.Entity<NuevosMessages>().HasNoKey();
        }

        public virtual DbSet<UsuarioEntity> Usuarios { get; set; }
        public virtual DbSet<ClienteEntity> Clientes { get; set; }

        public virtual DbSet<ResultadoEntity> Respuesta { get; set; }
        public virtual DbSet<RolEntity> Rol { get; set; }

        public virtual DbSet<ResultadoLogin> Login { get; set; }

        public virtual DbSet<IntencionesEntity> Intenciones { get; set; }

        public virtual DbSet<FracesEntity> Fraces { get; set; }
        public virtual DbSet<MessageEntity> Mensajes { get; set; }
        public virtual DbSet<ChatEntity> Chat { get; set; }
        public virtual DbSet<NuevosMessages> Mensajesnuevos { get; set; }
        public virtual DbSet<InteracionEntity> Interacciones { get; set; }
    }
}
