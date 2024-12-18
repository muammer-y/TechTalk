namespace Application.Tests.Helpers;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Test]
    [Repeat(5)]
    [Arguments("Test1234")]
    [Arguments("D'!EDASD'R32r23ırko2321")]
    public async Task Hash_ShouldGenerateDifferentHashesForSamePassword(string password)
    {
        var hashes = Enumerable.Range(0, 5)
            .Select(_ => _passwordHasher.HashPassword(password))
            .ToList();

        hashes.ForEach(async hash => await Assert.That(hash).IsNotNull());

        var distinctCount = hashes.Distinct().Count();

        await Assert.That(distinctCount).IsEqualTo(hashes.Count);
    }
}