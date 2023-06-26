using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Application.State.Services;
using BackendSpotify.Core.Domain.Models.State;
using Moq;

namespace Application.Test.State.Services;

public class StateServiceTest
{

    private readonly Mock<IStateRepository> stateRepository = new();
    private readonly Mock<ISpotifyConfigService> spotifyConfigService = new();

    [Fact]
    public async Task WhenCreatedAt_And_Add_ExpireIn_Is_Greater_Than_Now_Then_Return_True()
    {
        stateRepository.Setup(a => a.GetStateRequestByState(It.IsAny<String>())).Returns(
            Task.FromResult(new StateRequest
            {
                ExpiresIn = 3,
                CreatedAt = DateTime.UtcNow
            }
            ));


        StateService stateService = new StateService(stateRepository.Object, spotifyConfigService.Object);

        Boolean result = await stateService.IsValidState("");

        Assert.True(result);
    }

    [Fact]
    public async Task WhenCreatedAt_And_Add_ExpireIn_Is_Less_Than_Now_Then_Return_False()
    {
        stateRepository.Setup(a => a.GetStateRequestByState(It.IsAny<String>())).Returns(
            Task.FromResult(new StateRequest
            {
                ExpiresIn = 3,
                CreatedAt = DateTime.UtcNow.AddMinutes(-4)
            }
            ));


        StateService stateService = new StateService(stateRepository.Object, spotifyConfigService.Object);

        Boolean result = await stateService.IsValidState("");

        Assert.False(result);
    }

    [Fact]
    public async Task When_State_Not_Exists_Then_Return_False()
    {
        stateRepository.Setup(a => a.GetStateRequestByState(It.IsAny<String>())).Returns(
            Task.FromResult<StateRequest>(null));


        StateService stateService = new StateService(stateRepository.Object, spotifyConfigService.Object);

        Boolean result = await stateService.IsValidState("");

        Assert.False(result);
    }
}

