namespace QualiFlow.Identity.API.Models;

internal record HashedSecret(byte[] Secret, byte[] Salt)
{
    public static HashedSecret FromBytes(byte[] Secret, byte[] salt) => new HashedSecret(Secret, salt);
    public static HashedSecret FromString(string secret, string salt) => new(Decode(secret), Decode(salt));

    public string EncodeSecret() => Encode(Secret);
    public string EncodeSalt() => Encode(Salt);

    private static byte[] Decode(string value) => Convert.FromBase64String(value);
    private static string Encode(byte[] value) => Convert.ToBase64String(value);

}
