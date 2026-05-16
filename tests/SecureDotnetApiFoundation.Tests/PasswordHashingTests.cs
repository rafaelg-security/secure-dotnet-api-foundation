using Xunit;

namespace SecureDotnetApiFoundation.Tests;
public class PasswordHashingTests
{
    [Fact]
    public void BCrypt_ShouldVerifyCorrectPassword()
    {
        var password = "Doctor123!";
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        Assert.True(BCrypt.Net.BCrypt.Verify(password, hash));
        Assert.False(BCrypt.Net.BCrypt.Verify("WrongPassword123!", hash));
    }
}
