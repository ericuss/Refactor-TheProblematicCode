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
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetFiltered(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to, int? page, int? items);
        Task<int> GetFilteredTotal(string customerName, string customerLastname, string customerPhone, DateTime? from, DateTime? to);
    }
}
