using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    public class UserServiceTest : TestBase
    {
        private readonly UserService _userService;
        public UserServiceTest()
        {
            _userService = new(_userRepository);
        }

        [Fact]
        public async Task GetAndValidateUser_ReturnSuccess_Async()
        {
            _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new User());

            User user = await _userService.GetAndValidateUser(Guid.NewGuid(), cancellationToken);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetAndValidateUser_ReturnKeyNotFoundException_Async()
        {
            _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.GetAndValidateUser(Guid.NewGuid(), cancellationToken));
        }
    }
}
