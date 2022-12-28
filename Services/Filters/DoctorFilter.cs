using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Filters
{
    public class DoctorFilter
    {
        public Guid DoctorId { get; set; }
        public string FIO { get; set; }

        public string Specialization { get; set; }
    }
}
