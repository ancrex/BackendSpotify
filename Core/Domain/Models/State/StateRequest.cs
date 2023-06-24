namespace BackendSpotify.Core.Domain.Models.State;

public class StateRequest
{
    public int Id { get; set; }
    public string State { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime CreatedAt { get; set; }
}

