using BackendSpotify.Core.Domain.Models.State;
using BackendSpotify.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BackendSpotify.Infraestructure.Repositories.State;
public class StateRepository: IStateRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StateRequest> GetStateRequestByState(string state)
    {
        return await _dbContext.States
            .Where(stateRequest => state.Equals(stateRequest.State))
            .FirstOrDefaultAsync();
    }

    public async Task SaveState(StateRequest stateRequest)
    {
        _dbContext.States.Add(stateRequest);
        await _dbContext.SaveChangesAsync();
    }
}


