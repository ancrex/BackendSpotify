namespace BackendSpotify.Core.Domain.Models.State;

public interface IStateRepository
{
    Task SaveState(StateRequest stateRequest);

    Task<StateRequest> GetStateRequestByState(String state);
}


