namespace AuthService.Dtos.Auth
{
    public class JwtConfigurations
    {
        public string Issuer { get; set; }

        public string Audience { get; set; } 

        public string Key { get; set; } 
    }
}
