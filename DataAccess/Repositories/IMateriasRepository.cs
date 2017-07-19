using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public interface IMateriasRepository
    {
        IList<Materia> GetAllMaterias();
        void Create(IList<Materia> materiasDomain);
        void Update(IList<Materia> materiasDomain);
        void Delete(IList<Materia> materiasDomain);
        void Delete(int id);
    }
}
