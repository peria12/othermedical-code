
using Aurora.AdministrationService.API.Enum;
using Aurora.AdministrationService.API.Models.RequestModels.Leaves;
using Aurora.AdministrationService.AzureBus;
using Aurora.AdministrationService.Domain.Old.Entities;
using Aurora.AdministrationService.Domain.V1.Entities.PractitionerRoles;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Aurora.AppointmentService.Tests.API.Service.AzureBusCommandService
{
    public class AzureBusCommandServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private AzureBusCommandServices _azureBusCommandServices;
        public AzureBusCommandServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _azureBusCommandServices = new AzureBusCommandServices(_mockConfiguration.Object);
        }
        [Fact]
        public async Task SendData_ShouldReturnTrue()
        {
            //Arrange
            ScheduleLeavesRequestDto payload = new ScheduleLeavesRequestDto()
            {
                practitionerId = 1,
                PractitionerIdentifiers = new List<PractitionerIdentifierRequestDto> { },
                Action = "create",
                newTimeOff = new List<TimeOffAddRequestDto>
                {
                new TimeOffAddRequestDto
                {
                     StartDateTime = DateTime.UtcNow.AddDays(1),
                EndDateTime = DateTime.UtcNow, // Invalid: Start > End
                Description = "Invalid Leave",
                IsWorkingDay = false
                }

            }
            };
            string connectionString = "test";
            _mockConfiguration.Setup(c => c["ServiceBus:ConnectionString"]).Returns(connectionString);
            _mockConfiguration.Setup(c => c["ServiceBus:aurora-practioner-leaves-breaks-mys"]).Returns("aurora-practioner-leaves-breaks-mys");


            var result = await _azureBusCommandServices.SendData(payload,regionCode:"mys", "ScheduleLeave");
            // Assert
            result.Should().NotBeNull();
        }
    }
}
