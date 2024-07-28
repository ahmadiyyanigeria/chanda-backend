using Application.Exceptions;
using Application.Queries;
using Application.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.GetJamaat;

namespace UniteTest.QueryHandlerTests
{
    public class GetJamaatTests
    {
        private readonly Mock<IJamaatRepository> _jamaatRepositoryMock;
        private readonly Handler _handler;

        public GetJamaatTests()
        {
            _jamaatRepositoryMock = new Mock<IJamaatRepository>();
            _handler = new Handler(_jamaatRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_JamaatExists_ReturnsJamaatResponse()
        {
            // Arrange
            var jamaatId = Guid.NewGuid();
            var jamaat = new Jamaat("Camp Jamaat", Guid.NewGuid(), "Amir")
            {
                Id = jamaatId,
                CreatedOn = DateTime.UtcNow,
            };

            _jamaatRepositoryMock
                .Setup(repo => repo.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync(jamaat);

            var query = new Query(jamaatId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
             Assert.NotNull(result);
             Assert.Equal(jamaat.Name, result.Name);
             Assert.Equal(jamaat.CircuitId, result.CircuitId);
             Assert.Equal(jamaat.CreatedBy, result.CreatedBy);
             Assert.Equal(jamaat.CreatedOn, result.CreatedOn);

        }

        [Fact]
        public async Task Handle_JamaatDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var jamaatId = Guid.NewGuid();

            _jamaatRepositoryMock
                .Setup(repo => repo.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync((Jamaat?)null);

            var query = new Query(jamaatId);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

    }
}
