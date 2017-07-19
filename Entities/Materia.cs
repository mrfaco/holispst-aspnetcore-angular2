using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public DateTime DateModified { get; set; }

    }
}
