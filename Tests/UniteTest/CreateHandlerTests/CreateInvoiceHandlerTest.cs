using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using static Application.Commands.CreateInvoice;
using System.Linq.Expressions;
using Domain.Enums;
using Application.Exceptions;

namespace UniteTest.CreateHandlerTests
{
    public class CreateInvoiceHandlerTest
    {
        private readonly Mock<IJamaatRepository> _jamaatRepositoryMock;
        private readonly Mock<IMemberRepository> _memberRepositoryMock;
        private readonly Mock<IChandaTypeRepository> _chandaTypeRepositoryMock;
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Handler _handler;

        public CreateInvoiceHandlerTest()
        {
            _jamaatRepositoryMock = new Mock<IJamaatRepository>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _chandaTypeRepositoryMock = new Mock<IChandaTypeRepository>();
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new Handler(
                _unitOfWorkMock.Object,
                _invoiceRepositoryMock.Object,
                _jamaatRepositoryMock.Object,
                _memberRepositoryMock.Object,
                _chandaTypeRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_InvalidJamaatId_ShouldThrowDomainException()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "0001");
            var jamaat = new Jamaat("Lafiaji", circuit.Id, circuit, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Usman Tijani", "johndoe@mail.com", "08011111111", jamaat.Id, memberLedger.Id, jamaat, memberLedger, "0001");
            var chandaType = new ChandaType("Chanda Wasiyyat", "CHA-WAS", "Chanda Wasiyyat", new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), "0001");


            var command = new Command
            {
                JamaatId = jamaat.Id,
                InitiatorChandaNo = member.ChandaNo,
                InvoiceItems = new List<InvoiceItemCommand>
                {
                    new InvoiceItemCommand
                    {
                        PayerId = member.Id,
                        MonthPaidFor = MonthOfTheYear.January,
                        Year = 2024,
                        ChandaItems = new List<ChandaItemCommand>
                        {
                            new ChandaItemCommand( chandaType.Id, 100m )
                        }
                    }
                }
            };

            _jamaatRepositoryMock.Setup(j => j.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync((Jamaat?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal(ExceptionCodes.InvalidJamaat.ToString(), exception.ErrorCode.ToString());
        }

        [Fact]
        public async Task Handle_UnrecognizedInitiator_ShouldThrowDomainException()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "0001");
            var jamaat = new Jamaat("Lafiaji", circuit.Id, circuit, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Usman Tijani", "johndoe@mail.com", "08011111111", jamaat.Id, memberLedger.Id, jamaat, memberLedger, "0001");
            var chandaType = new ChandaType("Chanda Wasiyyat", "CHA-WAS", "Chanda Wasiyyat", new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), "0001");


            var command = new Command
            {
                JamaatId = jamaat.Id,
                InitiatorChandaNo = member.ChandaNo,
                InvoiceItems = new List<InvoiceItemCommand>
                {
                    new InvoiceItemCommand
                    {
                        PayerId = member.Id,
                        MonthPaidFor = MonthOfTheYear.January,
                        Year = 2024,
                        ChandaItems = new List<ChandaItemCommand>
                        {
                            new ChandaItemCommand( chandaType.Id, 100m )
                        }
                    }
                }
            };

            _jamaatRepositoryMock.Setup(j => j.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync(jamaat);

            _memberRepositoryMock.Setup(m => m.ExistsByChandaNo(command.InitiatorChandaNo))
                .Returns(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal(ExceptionCodes.MemberNotFound.ToString(), exception.ErrorCode.ToString());
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnInvoiceResponse()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "0001");
            var jamaat = new Jamaat("Lafiaji", circuit.Id, circuit, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Usman Tijani", "johndoe@mail.com", "08011111111", jamaat.Id, memberLedger.Id, jamaat, memberLedger, "0001");
            var chandaType = new ChandaType("Chanda Wasiyyat", "CHA-WAS", "Chanda Wasiyyat", new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), "0001");


            var command = new Command
            {
                JamaatId = jamaat.Id,
                InitiatorChandaNo = member.ChandaNo,
                InvoiceItems = new List<InvoiceItemCommand>
                {
                    new InvoiceItemCommand
                    {
                        PayerId = member.Id,
                        MonthPaidFor = MonthOfTheYear.January,
                        Year = 2024,
                        ChandaItems = new List<ChandaItemCommand>
                        {
                            new ChandaItemCommand( chandaType.Id, 100m )
                        }
                    }
                }
            };

            _jamaatRepositoryMock.Setup(j => j.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync(jamaat);

            _memberRepositoryMock.Setup(m => m.ExistsByChandaNo(command.InitiatorChandaNo))
                .Returns(true);

            _chandaTypeRepositoryMock.Setup(c => c.GetChandaTypes(It.IsAny<List<Guid>>()))
                .Returns(new List<ChandaType> { chandaType });

            _memberRepositoryMock.Setup(m => m.GetMembers(It.IsAny<Expression<Func<Member, bool>>>()))
                .Returns(new List<Member> { member });

            _invoiceRepositoryMock.Setup(i => i.AddAsync(It.IsAny<Invoice>()))
                .ReturnsAsync(new Invoice(new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), jamaat.Id, "INV-AE23GHS", 100m, InvoiceStatus.Pending, jamaat, member.ChandaNo));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(jamaat.Name, result.JamaatName);
            Assert.Equal(command.InvoiceItems.Count, result.InvoiceItems.Count);
            Assert.All(result.InvoiceItems, item =>
                Assert.All(item.ChandaItems, chanda =>
                    Assert.Contains(command.InvoiceItems
                        .SelectMany(i => i.ChandaItems)
                        .Select(ci => ci.Amount), id => id == chanda.Amount)));
        }
    }
}
