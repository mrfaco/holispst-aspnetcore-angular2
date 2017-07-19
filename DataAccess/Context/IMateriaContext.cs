using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public interface IMateriaContext : IDisposable
    {
        DbSet<Materia> Materias { get; set; }

        int SaveChanges();
    }
}
