namespace LearnNet7AuthenAndAuthorB01.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
