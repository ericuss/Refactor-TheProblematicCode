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
    using Refactor.CodeSmells.Filtering.Specification;

    public class AppointmentServiceV4 : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentServiceV4(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public Task<List<Appointment>> GetFiltered(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to, int? page, int? items)
        {
            var filterExpression = AppointmentFilterFluent.Select()
                .HasName(customerName)
                .HasName(customerLastname)
                .HasPhone(customerPhone)
                .From(from)
                .To(to);

            return this._appointmentRepository
                        .Get(filterExpression,
                            x => x.OrderBy(a => a.When),
                            includes: "Purpose, Book, Book.SpecialistBook.Collaborator",
                            page: page,
                            pageSize: items);
        }

        public Task<int> GetFilteredTotal(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to)
        {
            var filterExpression = AppointmentFilterFluent.Select()
                .HasName(customerName)
                .HasName(customerLastname)
                .HasPhone(customerPhone)
                .From(from)
                .To(to);

            return this._appointmentRepository.Count(filterExpression);
        }

    }
}
