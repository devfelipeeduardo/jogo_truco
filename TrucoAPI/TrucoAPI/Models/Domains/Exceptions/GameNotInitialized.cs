namespace TrucoAPI.Models.Domains.Exceptions
{
    public class GameNotInitialized : Exception
    {
        public GameNotInitialized(string message) : base(message) { }
    }
}
