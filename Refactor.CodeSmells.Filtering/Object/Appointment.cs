using System;
using System.Collections.Generic;
using System.Text;

namespace Refactor.CodeSmells.Filtering.Object
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime When { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
