using Application.Exceptions;
using Application.Queries;
using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;

namespace UniteTest.QueryHandlerTests
{
    public class GetMemberTest
    {
        private readonly Mock<IMemberRepository> _memberRepositoryMock;
        private readonly GetMember.Handler _handler;

        public GetMemberTest()
        {
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _handler = new GetMember.Handler(_memberRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_MemberExists_ReturnsMemberResponse()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "ABK", "0001");
            var jamaat = new Jamaat("Lafiaji", "ABK-L", circuit.Id, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Ade Ola", "adeola@example.com", "08011111111", jamaat.Id, memberLedger.Id, "0001");
           
            _memberRepositoryMock.Setup(repo => repo.GetMemberAsync(It.IsAny<Expression<Func<Member, bool>>>()))
                .ReturnsAsync(member);

            var query = new GetMember.Query(member.ChandaNo);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(member.Id);
            result.ChandaNo.Should().Be(member.ChandaNo);
            result.Name.Should().Be(member.Name);
            result.Email.Should().Be(member.Email);
            result.PhoneNo.Should().Be(member.PhoneNo);
        }

        [Fact]
        public async Task Handle_MemberDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var memberId = "0000";

            _memberRepositoryMock.Setup(repo => repo.GetMemberAsync(It.IsAny<Expression<Func<Member, bool>>>()))
                .ReturnsAsync((Member?)null);

            var query = new GetMember.Query(memberId);

            // Act & Assert
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>()
                .Where(ex => ex.Message == "Member not found" && ex.ErrorCode.ToString() == ExceptionCodes.MemberNotFound.ToString());
        }
    }
}
