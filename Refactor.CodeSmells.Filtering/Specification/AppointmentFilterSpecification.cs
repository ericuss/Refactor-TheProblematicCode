using Refactor.CodeSmells.Filtering.Object;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Refactor.CodeSmells.Filtering.Specification
{
    using Helpers;
    public static class AppointmentFilterFluent
    {

        public static Expression<Func<Appointment, bool>> HasName(this Expression<Func<Appointment, bool>> filter, string name)
        {
            return filter.And(x => x.CustomerName.Contains(name));
        }

        public static Expression<Func<Appointment, bool>> HasPhone(this Expression<Func<Appointment, bool>> filter, string phone)
        {
            return filter.And(x => x.CustomerPhone.Contains(phone));
        }

        public static Expression<Func<Appointment, bool>> From(this Expression<Func<Appointment, bool>> filter, DateTime? from)
        {
            if (!from.HasValue) return filter;

            from = from.Value.SetTimeToMin();

            return filter.And(x => x.When.IsGreatterOrEqualThan(from.Value));
        }

        public static Expression<Func<Appointment, bool>> To(this Expression<Func<Appointment, bool>> filter, DateTime? to)
        {
            if (!to.HasValue) return filter;

            to = to.Value.SetTimeToMax();

            return filter.And(x => x.When.IsLesserOrEqualThan(to.Value));
        }

        public static Expression<Func<Appointment, bool>> Select()
        {
            return x => true;
        }
    }
}
