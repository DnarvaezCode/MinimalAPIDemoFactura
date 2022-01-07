namespace APIFacturaV1.DTOs
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
