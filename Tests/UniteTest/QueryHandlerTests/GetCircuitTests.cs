using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.GetCircuit;

namespace UniteTest.QueryHandlerTests
{
    public class GetCircuitTests
    {
            private readonly Mock<ICircuitRepository> _circuitRepositoryMock;
            private readonly Handler _handler;

            public GetCircuitTests()
            {
                _circuitRepositoryMock = new Mock<ICircuitRepository>();
                _handler = new Handler(_circuitRepositoryMock.Object);
            }

            [Fact]
            public async Task Handle_CircuitExists_ReturnsCircuitResponse()
            {
                // Arrange
                var circuitId = Guid.NewGuid();
                var circuit = new Circuit("Abeokuta", "001", "Amir")
                {
                    Id = circuitId,
                    CreatedOn = DateTime.UtcNow,
                };

                _circuitRepositoryMock
                    .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Circuit, bool>>>()))
                    .ReturnsAsync(circuit);

                var query = new Query(circuitId);

                // Act
                var result = await _handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(circuit.Name, result.Name);
                Assert.Equal(circuit.CreatedBy, result.CreatedBy);
                Assert.Equal(circuit.CreatedOn, result.CreatedOn);

            }

            [Fact]
            public async Task Handle_CircuitDoesNotExist_ThrowsNotFoundException()
            {
                // Arrange
                var circuitId = Guid.NewGuid();

                 _circuitRepositoryMock
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Circuit, bool>>>()))
                    .ReturnsAsync((Circuit?)null);

                var query = new Query(circuitId);

                // Act & Assert
                await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
            }

    }
    
}
