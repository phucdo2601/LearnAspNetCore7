namespace LearnNet7AuthenAndAuthorB01.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }

        public Account Account { get; set; }

        public int AccountId { get; set; }
    }
}
