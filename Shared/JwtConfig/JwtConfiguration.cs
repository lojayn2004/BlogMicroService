namespace Shared.JwtConfig
{
    public class JwtConfiguration
    {
        public readonly static string SectionName = "JWT";
        public string Issuer { get; set; }

        public string Audience { get; set; } 

        public string Key { get; set; }

        public int ExpirationInMinutes { get; set; }
    }
}
