using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class MateriaInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }


        public Materia ToDomain()
        {
            return new Materia
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Stock = Stock,
                DateModified = DateTime.Now
            };
        }
    }
}
