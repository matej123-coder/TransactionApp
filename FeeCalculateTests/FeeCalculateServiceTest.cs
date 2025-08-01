using AutoMapper;
using Domain.DTOS;
using Domain.Entites;
using Domain.Repository;
using FluentValidation;
using Moq;
using Services.Abstraction;
using Servicess;
using Xunit;

namespace FeeCalculateTests

{
    public class FeeCalculateServiceTest
    {
        private readonly Mock<IFeeLogRepository> _feeLogRepoMock = new();
        private readonly Mock<ITransaction> _transactionRepoMock = new();
        private readonly Mock<IClientInfoRepository> _clientRepoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<TransactionDTO>> _validatorMock = new();

        private FeeCalculationService CreateService(IEnumerable<IFeeRule> rules)
        {
            return new FeeCalculationService(
                rules,
                _feeLogRepoMock.Object,
                _mapperMock.Object,
                _transactionRepoMock.Object,
                _clientRepoMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task CalculateFeeAsync_ValidPOS_ReturnsCorrectFee()
        {
            // Arrange
            var transactionDto = new TransactionDTO
            {
                Type = "POS",
                Amount = 80,
                Currency = "EUR",
                Client = new ClientDto { CreditScore = 450, Segment = "STANDARD", HasPromotion = false }
            };

            var posRule = new Mock<IFeeRule>();
            posRule.Setup(r => r.IsApplicable(It.IsAny<TransactionDTO>())).Returns(true);
            posRule.Setup(r => r.CalculateFee(It.IsAny<TransactionDTO>())).Returns(0.2m);

            _validatorMock.Setup(v => v.ValidateAsync(transactionDto, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mapperMock.Setup(m => m.Map<Transaction>(transactionDto)).Returns(new Transaction());
            _mapperMock.Setup(m => m.Map<ClientInfo>(transactionDto.Client)).Returns(new ClientInfo());
            _mapperMock.Setup(m => m.Map<FeeLogDto>(It.IsAny<FeeLog>())).Returns(new FeeLogDto { FeeAmount = 0.2m });

            var service = CreateService(new[] { posRule.Object });

            // Act
            var result = await service.CalculateFeeAsync(transactionDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0.2m, result.FeeAmount);
            _feeLogRepoMock.Verify(repo => repo.SaveFeeLogAsync(It.IsAny<FeeLog>()), Times.Once);
            _transactionRepoMock.Verify(repo => repo.SaveTransactionAsync(It.IsAny<Transaction>()), Times.Once);
          
        }

        [Fact]
        public async Task CalculateFeesBatchAsync_MultipleTransactions_ReturnsAllResults()
        {
          
            var tx1 = new TransactionDTO
            {
                Type = "POS",
                Amount = 50,
                Currency = "EUR",
                Client = new ClientDto()
            };

            var tx2 = new TransactionDTO
            {
                Type = "POS",
                Amount = 200,
                Currency = "EUR",
                Client = new ClientDto()
            };

            var posRule = new Mock<IFeeRule>();
            posRule.Setup(r => r.IsApplicable(It.IsAny<TransactionDTO>())).Returns(true);
            posRule.Setup(r => r.CalculateFee(It.IsAny<TransactionDTO>())).Returns<TransactionDTO>(tx =>
                tx.Amount <= 100 ? 0.2m : (decimal)(tx.Amount * 0.002m));

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<TransactionDTO>(), default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _mapperMock.Setup(m => m.Map<Transaction>(It.IsAny<TransactionDTO>())).Returns(new Transaction());
            _mapperMock.Setup(m => m.Map<ClientInfo>(It.IsAny<ClientDto>())).Returns(new ClientInfo());
            _mapperMock.Setup(m => m.Map<FeeLogDto>(It.IsAny<FeeLog>())).Returns<FeeLog>(log => new FeeLogDto
            {
                FeeAmount = log.FeeAmount,
            });

            var service = CreateService(new[] { posRule.Object });

        
            var result = (await service.CalculateFeesBatchAsync(new[] { tx1, tx2 })).ToList();

           
            Assert.Equal(2, result.Count);
            Assert.Equal(0.2m, result[0].FeeAmount);
            Assert.Equal(0.4m, result[1].FeeAmount);
        }

        [Fact]
        public async Task GetFeeLogs_ReturnsMappedLogs()
        {
            
            var logs = new List<FeeLog>
        {
            new FeeLog { FeeAmount = 0.25m },
            new FeeLog { FeeAmount = 0.75m }
        };

            _feeLogRepoMock.Setup(repo => repo.GetAllFeeLogs()).ReturnsAsync(logs);
            _mapperMock.Setup(m => m.Map<FeeLogDto>(It.IsAny<FeeLog>()))
                       .Returns<FeeLog>(log => new FeeLogDto { FeeAmount = log.FeeAmount });

            var service = CreateService(Enumerable.Empty<IFeeRule>());

            
            var result = await service.GetFeeLogs();

            
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.FeeAmount == 0.25m);
            Assert.Contains(result, r => r.FeeAmount == 0.75m);
        }
    }
}