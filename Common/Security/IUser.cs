namespace Codell.Pies.Common.Security
{
    public interface IUser
    {
        string Email { get; }
        string Nickname { get; }
        string FullName { get; }
    }
}