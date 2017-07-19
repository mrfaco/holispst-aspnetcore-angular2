using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class MateriaContext: DbContext, IMateriaContext
    {

        public DbSet<Materia> Materias { get; set; }


        public MateriaContext()
        {
            this.Database.Migrate();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            builder.UseSqlite("Data Source = C:/Users/facundo.osimi/Documents/visual studio 2017/Projects/holispst/holispst.db");
        }


    }
}
