namespace ScoreManager.Api
{
    public interface IApiBase
    {
        Task LoginAsync();
        Task LogoutAsync();
        bool IsAuthenticated();
        bool IsNotAuthenticated();
        string GetUsername();
        string GetEmail();
        string Username { get; }
    }
}