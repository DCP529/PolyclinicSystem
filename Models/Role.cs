
using Models.ModelsDb;

namespace Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }

        public List<Account> Accounts = new List<Account>();
    }
}
