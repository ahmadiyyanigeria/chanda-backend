using Application.Exceptions;
using Application.Queries;
using Application.Repositories;
using Domain.Entities;
using Moq;

namespace UniteTest.QueryHandlerTests
{
    public class GetChandaTypeTest
    {
        private readonly Mock<IChandaTypeRepository> _mockChandaTypeRepository;
        private readonly GetChandaType.Handler _handler;

        public GetChandaTypeTest()
        {
            _mockChandaTypeRepository = new Mock<IChandaTypeRepository>();
            _handler = new GetChandaType.Handler(_mockChandaTypeRepository.Object);
        }

        [Fact]
        public async Task Handle_ChandaTypeExists_ReturnsChandaTypeResponse()
        {
            // Arrange
            var chandaType = new ChandaType("chandaArm", "1022", " Chanda Arm", Guid.NewGuid(), "2186");
                
            

            _mockChandaTypeRepository
                .Setup(repo => repo.GetByIdAsync(chandaType.Id))
                .ReturnsAsync(chandaType);

            var query = new GetChandaType.Query(chandaType.Id);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(chandaType.Name, result.Name);
            Assert.Equal(chandaType.Code, result.Code);
            Assert.Equal(chandaType.IncomeAccountId, result.IncomeAccountId);
            Assert.Equal(chandaType.Description, result.Description);
        }

        [Fact]
        public async Task Handle_ChandaTypeDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var chandaTypeId = Guid.NewGuid();

            _mockChandaTypeRepository
                .Setup(repo => repo.GetByIdAsync(chandaTypeId))
                .ReturnsAsync((ChandaType?)null);

            var query = new GetChandaType.Query(chandaTypeId);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }

}
