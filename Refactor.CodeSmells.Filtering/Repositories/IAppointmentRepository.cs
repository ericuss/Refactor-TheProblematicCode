namespace Refactor.CodeSmells.Filtering.Repositories
{
    using Refactor.CodeSmells.Filtering.Object;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IAppointmentRepository
    {
        Task<List<Appointment>> Get(Expression<Func<Appointment, bool>> filter, Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy, string includes, int? page, int? pageSize);
        Task<int> Count(Expression<Func<Appointment, bool>> filter);
    }
}
