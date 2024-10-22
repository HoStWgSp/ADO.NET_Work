
namespace SkillFactory;

internal class Table
{
    public Table()
    {
        Fields = new List<string>();
    }
    /// <summary>
    /// Имя таблицы
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список столбцов таблицы
    /// </summary>
    public List < string > Fields { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ImportantField{ get; set; }
}