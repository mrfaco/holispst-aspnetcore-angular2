using DataAccess.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holispst.Controllers.API
{
    public class MateriasController : Controller
    {
        private IMateriasRepository materiasRepository;

        public MateriasController(IMateriasRepository materiasRepository)
        {
            this.materiasRepository = materiasRepository;
        }

        [HttpGet]
        [Route("/api/materias")]
        public IActionResult Get()
        {
            try
            {
                return Ok(materiasRepository.GetAllMaterias());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("/api/materias")]
        public IActionResult Create([FromBody] IList<MateriaInput> materias)
        {
            try
            {
                IList<Materia> materiasDomain = new List<Materia>();
                materias.ToList().ForEach(x => materiasDomain.Add(x.ToDomain()));
                materiasRepository.Create(materiasDomain);
                return Ok("Added successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        [Route("/api/materias")]
        public IActionResult Update([FromBody] IList<MateriaInput> materias)
        {
            try
            {
                IList<Materia> materiasDomain = new List<Materia>();
                materias.ToList().ForEach(x => materiasDomain.Add(x.ToDomain()));
                materiasRepository.Update(materiasDomain);
                return Ok("Updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("/api/materias/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                materiasRepository.Delete(id);
                return Ok("Deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
