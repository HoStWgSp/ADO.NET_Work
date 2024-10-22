
namespace SkillFactory;

public class UserData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string Role { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    //public UserCredential UserCredential { get; set; }
    //ublic List<Topic> Topics { get; set; } = new List<Topic>();
}