using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactor.CodeSmells.Filtering.Services;
using System.Threading.Tasks;
using Refactor.CodeSmells.Filtering.Tests.Mocks;

namespace Refactor.CodeSmells.Filtering.Tests.Services
{
    [TestClass]
    public class AppointmentServiceTests
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentServiceTests()
        {
            this._appointmentService = new AppointmentService(new AppointmentRepository());
        }

        [TestMethod]
        public async Task EmptyFiltersAndPageSize()
        {
            // Arrange
            var dummyParameters = new
            {
                customerName = string.Empty,
                customerLastname = string.Empty,
                customerPhone = string.Empty,
                from = (DateTime?)null,
                to = (DateTime?)null,
                page = (int?)null,
                items = (int?)null,
            };

            var result_expected = 11;

            // Act
            var result = await this._appointmentService.GetFiltered(dummyParameters.customerName,
                                                                dummyParameters.customerLastname,
                                                                dummyParameters.customerPhone,
                                                                dummyParameters.from,
                                                                dummyParameters.to,
                                                                dummyParameters.page,
                                                                dummyParameters.items
                                                                );

            // Assert
            Assert.AreEqual(result_expected, result.Count);
        }




        [TestMethod]
        public async Task WhenTryToFilterAppointments_From1YearBeforeTo1YearAfterAndPageSize_ThenRetturn10Items()
        {
            // Arrange
            var dummyParameters = new
            {
                customerName = string.Empty,
                customerLastname = string.Empty,
                customerPhone = string.Empty,
                from = (DateTime.Now).AddYears(-1),
                to = (DateTime.Now).AddYears(1),
                page = 0,
                items = 10,
            };

            var result_expected = 10;

            // Act
            var result = await this._appointmentService.GetFiltered(dummyParameters.customerName,
                                                                dummyParameters.customerLastname,
                                                                dummyParameters.customerPhone,
                                                                dummyParameters.from,
                                                                dummyParameters.to,
                                                                dummyParameters.page,
                                                                dummyParameters.items
                                                                );

            // Assert
            Assert.AreEqual(result_expected, result.Count);
        }







        [TestMethod]
        public async Task FromNow_ToNow()
        {
            // Arrange
            var dummyParameters = new
            {
                customerName = string.Empty,
                customerLastname = string.Empty,
                customerPhone = string.Empty,
                from = DateTime.Now,
                to = DateTime.Now,
                page = 0,
                items = 10,
            };

            var result_expected = 10;

            // Act
            var result = await this._appointmentService.GetFiltered(dummyParameters.customerName,
                                                                dummyParameters.customerLastname,
                                                                dummyParameters.customerPhone,
                                                                dummyParameters.from,
                                                                dummyParameters.to,
                                                                dummyParameters.page,
                                                                dummyParameters.items
                                                                );

            // Assert
            Assert.AreEqual(result_expected, result.Count);
        }



    }
}
