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
        private readonly Mock<ICurrentUser> _currentUserMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Handler _handler;

        public CreateInvoiceHandlerTest()
        {
            _jamaatRepositoryMock = new Mock<IJamaatRepository>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _chandaTypeRepositoryMock = new Mock<IChandaTypeRepository>();
            _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            _currentUserMock = new Mock<ICurrentUser>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new Handler(
                _unitOfWorkMock.Object,
                _currentUserMock.Object,
                _invoiceRepositoryMock.Object,
                _jamaatRepositoryMock.Object,
                _memberRepositoryMock.Object,
                _chandaTypeRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_NoValidChandaTypeSelected_ShouldThrowAnException()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "ABK", "0001");
            var jamaatLedger = new JamaatLedger(Guid.NewGuid(), "0001");
            var jamaat = new Jamaat("Lafiaji","ABK-L",circuit.Id, jamaatLedger.Id, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Usman Tijani", "johndoe@mail.com", "08011111111", jamaat.Id, memberLedger.Id, "0001");
            var chandaType = new ChandaType("Chanda Wasiyyat", "CHA-WAS", "Chanda Wasiyyat", new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), "0001");


            var command = new Command
            {
                InvoiceItems = new List<InvoiceItemCommand>
                {
                    new InvoiceItemCommand
                    {
                        MonthPaidFor = MonthOfTheYear.January,
                        Year = 2024,
                        ChandaItems = new List<ChandaItemCommand>
                        {
                            new ChandaItemCommand( chandaType.Name, 100m )
                        }
                    }
                }
            };

            _chandaTypeRepositoryMock.Setup(ct => ct.GetChandaTypes(It.IsAny<List<string>>()))
                .Returns(new List<ChandaType>());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal(ExceptionCodes.NoValidChandaTypeSelected.ToString(), exception.ErrorCode.ToString());
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnInvoiceResponse()
        {
            // Arrange
            var circuit = new Circuit("Abeokuta", "ABK", "0001");
            var jamaatLedger = new JamaatLedger(Guid.NewGuid(), "0001");
            var jamaat = new Jamaat("Lafiaji", "ABK-L", circuit.Id, jamaatLedger.Id, "0001");
            var memberLedger = new MemberLedger(Guid.NewGuid(), "0001");
            var member = new Member("0001", "Usman Tijani", "johndoe@mail.com", "08011111111", jamaat.Id, memberLedger.Id, "0001");
            var chandaType = new ChandaType("Chanda Wasiyyat", "CHA-WAS", "Chanda Wasiyyat", new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), "0001");


            var command = new Command
            {
                InvoiceItems = new List<InvoiceItemCommand>
                {
                    new InvoiceItemCommand
                    {
                        MonthPaidFor = MonthOfTheYear.January,
                        Year = 2024,
                        ReceiptNo = "REC-0001",
                        ChandaItems = new List<ChandaItemCommand>
                        {
                            new ChandaItemCommand( chandaType.Name, 100m )
                        }
                    }
                }
            };

            var invoice = new Invoice(new Guid("e041f7a3-7b3e-411c-a679-428ba1b1a884"), jamaat.Id, "INV-AE23GHS", 100m, InvoiceStatus.Pending, member.ChandaNo);

            _jamaatRepositoryMock.Setup(j => j.Get(It.IsAny<Expression<Func<Jamaat, bool>>>()))
                .ReturnsAsync(jamaat);

            _chandaTypeRepositoryMock.Setup(c => c.GetChandaTypes(It.IsAny<List<string>>()))
                .Returns(new List<ChandaType> { chandaType });

            _memberRepositoryMock.Setup(m => m.GetMemberAsync(It.IsAny<Expression<Func<Member, bool>>>()))
                .ReturnsAsync(member);

            _invoiceRepositoryMock.Setup(i => i.AddAsync(It.IsAny<Invoice>()))
                .ReturnsAsync(invoice);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Amount, command.InvoiceItems
                .SelectMany(item => item.ChandaItems).Sum(chanda => chanda.Amount));
            Assert.Equal(result.Status.ToString(), InvoiceStatus.Pending.ToString());
        }
    }
}
