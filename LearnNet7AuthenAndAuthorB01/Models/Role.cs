namespace LearnNet7AuthenAndAuthorB01.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }    
        public List<Account> Accounts { get; set; }
    }
}
