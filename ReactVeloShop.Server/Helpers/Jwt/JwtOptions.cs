namespace ReactVeloShop.Server.Helpers.Jwt
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string SecretRefreshKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}
