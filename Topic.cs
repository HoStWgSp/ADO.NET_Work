using System;

namespace SkillFactory;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Навигационное свойство
    public List<UserData> Users { get; set; } = new List<UserData>();
}
