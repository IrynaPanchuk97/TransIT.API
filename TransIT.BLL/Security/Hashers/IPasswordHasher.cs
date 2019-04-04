namespace TransIT.BLL.Security.Hashers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool CheckMatch(string password, string hashedPassword);
    }
}
