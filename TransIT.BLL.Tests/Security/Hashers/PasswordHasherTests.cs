using System.Collections.Generic;
using TransIT.BLL.Security.Hashers;
using Xunit;

namespace TransIT.BLL.Tests.Security.Hashers
{
    public class PasswordHasherTests
    {
        private PasswordHasher _hasher = new PasswordHasher();

        [Theory]
        [MemberData(nameof(GetPasswordSampleData))]
        public void HashPassword_GivenPassword_ReturnsHashedPassword(string password)
        {
            var hash = _hasher.HashPassword(password);

            Assert.False(string.IsNullOrEmpty(hash));
        }

        [Theory]
        [MemberData(nameof(GetPasswordSampleData))]
        public void HashPassword_GivenPassword_ContainsSalt(string password)
        {
            var hash = _hasher.HashPassword(password);

            var parts = hash.Split(":");
            Assert.True(parts.Length == 2);
        }

        [Theory]
        [MemberData(nameof(GetPasswordSampleData))]
        public void HashPassword_GivenSamePassword_ReturnsDifferentHashes(string password)
        {
            var firstHash = _hasher.HashPassword(password);
            var secondHash = _hasher.HashPassword(password);

            Assert.NotEqual(firstHash, secondHash);
        }

        [Theory]
        [MemberData(nameof(GetPasswordSampleData))]
        public void CheckMatch_GivenPasswordAndItsHash_ReturnsTrue(string password)
        {
            var hash = _hasher.HashPassword(password);

            var match = _hasher.CheckMatch(password, hash);

            Assert.True(match);
        }

        [Theory]
        [MemberData(nameof(GetPasswordSampleData))]
        public void CheckMatch_GivenPasswordHashAndFakePassword_ReturnsFalse(string fakePassword)
        {
            var hash = _hasher.HashPassword("i am password");

            var match = _hasher.CheckMatch(fakePassword, hash);

            Assert.False(match);
        }

        public static IEnumerable<object[]> GetPasswordSampleData() =>
            new[]
            {
                new object[] { "" },
                new object[] { "123456" },
                new object[] { "somepassword" },
                new object[] { "I Am password" }
            };
    }
}
