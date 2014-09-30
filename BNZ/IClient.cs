namespace MoneyTrack.BNZ
{
    public interface IClient
    {
        ILoggedInClient Login(string accessid, string pw);
    }
}