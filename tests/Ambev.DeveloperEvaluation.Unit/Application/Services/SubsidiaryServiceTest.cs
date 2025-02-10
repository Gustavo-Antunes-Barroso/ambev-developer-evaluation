using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    public class SubsidiaryServiceTest : TestBase
    {
        private readonly SubsidiaryService _service;

        public SubsidiaryServiceTest()
        { 
            _service = new (_subsidiaryRepository); 
        }

        [Fact]
        public async Task GetAndValidateSubsidiary_ReturnSucess_Async()
        {
            _subsidiaryRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Subsidiary());

            Subsidiary subsidiary = await _service.GetAndValidateSubsidiary(Guid.NewGuid(), cancellationToken);
            Assert.NotNull(subsidiary);
        }

        [Fact]
        public async Task GetAndValidateSubsidiary_ReturnKeyNotFoundException_Async()
        {
            _subsidiaryRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAndValidateSubsidiary(Guid.NewGuid(), cancellationToken));
        }
    }
}
