using DataAccess.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class MateriasRepository : IMateriasRepository
    {
        private IMateriaContext context;

        public MateriasRepository(IMateriaContext context)
        {
            this.context = context;
        }

        public void Create(IList<Materia> materiasDomain)
        {
            materiasDomain.ToList().
                ForEach(mat => context.Materias.Add(mat));
            context.SaveChanges();
        }

        public IList<Materia> GetAllMaterias()
        {
            return context.Materias.ToList();
        }

        public void Update(IList<Materia> materiasDomain)
        {
            foreach (Materia mat in materiasDomain)
            {
                if (Exist(mat))
                {
                    context.Materias.Update(mat);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Materia with Id = {mat.Id} doesn't exist");
                }
            }
        }

        public bool Exist(Materia mat)
        {
            return context.Materias.Where(x => x.Id == mat.Id).Count() > 0;
        }

        public bool Exist(int id)
        {
            return context.Materias.Where(x => x.Id == id).Count() > 0;
        }

        public void Delete(int id)
        {
            if (Exist(id)){
                context.Materias.Remove(context.Materias.Where(x => x.Id == id).SingleOrDefault());
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Materia with Id = {id} doesn't exist");
            }
        }

        public void Delete(IList<Materia> materiasDomain)
        {
            foreach (Materia mat in materiasDomain)
            {
                if (Exist(mat))
                {
                    context.Materias.Remove(mat);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Materia with Id = {mat.Id} doesn't exist");
                }
            }
        }
    }
}
