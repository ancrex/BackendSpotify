using BackendSpotify.Core.Application.Configuration.Interfaces;
using BackendSpotify.Core.Application.State.Interfaces;
using BackendSpotify.Core.Domain.Models.State;

namespace BackendSpotify.Core.Application.State.Services;
public class StateService : IStateService
{ 
    private readonly IStateRepository _stateRepository;
    private readonly ISpotifyConfigService _spotifyConfigService;

    public StateService(IStateRepository stateRepository,
        ISpotifyConfigService spotifyConfigService)
    {
        _stateRepository = stateRepository;
        _spotifyConfigService = spotifyConfigService;
    }

    public async Task<StateRequest> GenerateState()
    {
        StateRequest stateRequest = new StateRequest() {
           State = Guid.NewGuid().ToString(),
           ExpiresIn = _spotifyConfigService.TtlState,
           CreatedAt = DateTime.UtcNow
        };
        await _stateRepository.SaveState(stateRequest);

        return stateRequest;
    }

    public async Task<bool> IsValidState(string state)
    {
        StateRequest stateRequest = await _stateRepository.GetStateRequestByState(state);

        if(stateRequest == null) {
            return false;
        }


        return DateTime.UtcNow.CompareTo(stateRequest.CreatedAt.AddMinutes(stateRequest.ExpiresIn)) < 1;


    }
}

