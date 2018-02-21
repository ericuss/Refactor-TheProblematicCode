namespace Refactor.CodeSmells.Filtering.Tests.Mocks
{
    using Refactor.CodeSmells.Filtering.Object;
    using Refactor.CodeSmells.Filtering.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IEnumerable<Appointment> _appointments;

        public AppointmentRepository()
        {
            this._appointments = new List<Appointment>()
            {
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Robert Posteguillo", CustomerPhone = "666666661", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Pretty Little Tower", CustomerPhone = "666666662", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Jose", CustomerPhone = "666666663", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Antonio", CustomerPhone = "666666664", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Gurpegui", CustomerPhone = "666666665", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Gerard", CustomerPhone = "666666666", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Jordi", CustomerPhone = "666666667", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Alberto", CustomerPhone = "666666668", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Jorge", CustomerPhone = "666666669", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Augusto", CustomerPhone = "666666620", When = DateTime.Now, },
                new Appointment() { Id= Guid.NewGuid(), CustomerName = "Roman", CustomerPhone = "666666632", When = DateTime.Now, },
            };
        }

        public async Task<int> Count(Expression<Func<Appointment, bool>> filter)
        {
            var result = _appointments;


            if (filter != null)
            {
                result = _appointments.AsQueryable().Where(filter);
            }

            await Task.Run(() => result);
            return result.Count();
        }

        public async Task<List<Appointment>> Get(Expression<Func<Appointment, bool>> filter,
                                                    Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy,
                                                    string includes, int? page, int? pageSize)
        {
            var result = _appointments;


            if (filter != null)
            {
                result = _appointments.AsQueryable().Where(filter);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                result = result.Skip(page.Value).Take(pageSize.Value);
            }

            await Task.Run(() => result);
            return result.ToList();
        }
    }
}
