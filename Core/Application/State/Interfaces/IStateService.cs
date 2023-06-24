using BackendSpotify.Core.Domain.Models.State;

namespace BackendSpotify.Core.Application.State.Interfaces;
public interface IStateService
{
    Task<StateRequest> GenerateState();
    Task<Boolean> IsValidState(string state);
}

