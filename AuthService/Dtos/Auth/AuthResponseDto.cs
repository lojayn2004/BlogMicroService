using System.Text.Json.Serialization;

namespace AuthService.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        [JsonIgnore]
        public string RefreshToken { get; set; } = string.Empty;
    

        public DateTime RefreshTokenExpiration { get; set; }
    
    
    }
}
