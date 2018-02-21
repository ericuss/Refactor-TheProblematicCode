using Refactor.CodeSmells.Filtering.Helpers;
using Refactor.CodeSmells.Filtering.Object;
using Refactor.CodeSmells.Filtering.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Refactor.CodeSmells.Filtering.Services
{
    using Helpers;
    public class AppointmentServiceV2 : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentServiceV2(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public Task<List<Appointment>> GetFiltered(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to, int? page, int? items)
        {
            /// OLD
            Expression<Func<Appointment, bool>> dateExpression = a => true; // setted to return true to avoid the all evaluators of "if(dateExpression == null)...."
            Expression<Func<Appointment, bool>> basicExpression = a => a.CustomerName.Contains(customerName)
                                                                        && a.CustomerName.Contains(customerLastname)
                                                                        && a.CustomerPhone.Contains(customerPhone);
            if (to.HasValue)
            {
                to = to.Value.SetTimeToMax();
                dateExpression = dateExpression.And(a => a.When.IsLesserOrEqualThan(to.Value));
            }

            if (from.HasValue)
            {
                from = from.Value.SetTimeToMin();
                dateExpression = dateExpression.And(a => a.When.IsGreatterOrEqualThan(from.Value));
            }

            var finalExpression = dateExpression.And(basicExpression);

            return this._appointmentRepository
                        .Get(finalExpression,
                            x => x.OrderBy(a => a.When),
                            includes: "Purpose, Book, Book.SpecialistBook.Collaborator",
                            page: page,
                            pageSize: items);
        }

        public async Task<int> GetFilteredTotal(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to)
        {
            return (await GetFiltered(customerName, customerLastname, customerPhone, from, to, null, null)).Count();
        }
    }
}
