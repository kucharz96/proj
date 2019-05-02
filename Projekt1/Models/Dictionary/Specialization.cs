using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.Models.Dictionary
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}