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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> GetFiltered(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to, int? page, int? items)
        {
            /// OLD


            if (!from.HasValue && to.HasValue)
            {
                to = to.Value.ChangeTime(0, 0, 0);
                Expression<Func<Appointment, bool>> filterWithoutFromDate = a => a.When < to.Value && a.CustomerName.Contains(customerName) && a.CustomerName.Contains(customerLastname) && a.CustomerPhone.Contains(customerPhone);

                return await this._appointmentRepository
                            .Get(
                    filterWithoutFromDate,
                    x => x.OrderBy(a => a.When),
                    includes: "Purpose, Book, Book.SpecialistBook.Collaborator",
                    page: page,
                    pageSize: items);
            }
            else if (from.HasValue && !to.HasValue)
            {
                from = from.Value.ChangeTime(0, 0, 0);
                Expression<Func<Appointment, bool>> filterWithoutToDate = a => a.When > from.Value && a.CustomerName.Contains(customerName) && a.CustomerName.Contains(customerLastname) && a.CustomerPhone.Contains(customerPhone);

                return await this._appointmentRepository
                            .Get(
                    filterWithoutToDate,
                    x => x.OrderBy(a => a.When),
                    includes: "Purpose, Book, Book.SpecialistBook.Collaborator",
                    page: page,
                    pageSize: items);
            }
            else if (!from.HasValue && !to.HasValue)
            {
                Expression<Func<Appointment, bool>> filterWithoutAnyDate = a => a.CustomerName.Contains(customerName) && a.CustomerName.Contains(customerLastname) && a.CustomerPhone.Contains(customerPhone);

                return await this._appointmentRepository
                            .Get(
                    filterWithoutAnyDate,
                    x => x.OrderBy(a => a.When),
                    includes: "Purpose, Book, Book.SpecialistBook.Collaborator",
                    page: page,
                    pageSize: items);
            }

            from = from.Value.ChangeTime(0, 0, 0);
            to = to.Value.ChangeTime(0, 0, 0);

            return await this._appointmentRepository
                       .Get(
               a => a.When > from.Value && a.When < to.Value &&
               a.CustomerName.Contains(customerName) &&
               a.CustomerName.Contains(customerLastname) &&
               a.CustomerPhone.Contains(customerPhone),
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
