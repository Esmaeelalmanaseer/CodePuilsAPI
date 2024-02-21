namespace CodePuilsAPI.Models.Dtos
{
    public class LoginResponcedto
    {
        public string Email { get; set; }
        public string Toekn { get; set; }
        public List<string> Roles { get; set; }
    }
}
