namespace WebApiAdvance.Entities.Dtos.Authentication;

public class TokenOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}
