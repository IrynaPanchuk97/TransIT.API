namespace TransIT.BLL.Security.Hashers
{
    /// <summary>
    /// Interface for classes capable of password hashing
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes given password and returns the result.
        /// </summary>
        /// <param name="password">Password to be hashed</param>
        /// <returns>Hash of the given password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Checks if the given password matches the hashed password.
        /// </summary>
        /// <param name="password">Given password to be matched</param>
        /// <param name="hashedPassword">Hashed password to be matched</param>
        /// <returns>True if passwords matched, else returns false</returns>
        bool CheckMatch(string password, string hashedPassword);
    }
}
