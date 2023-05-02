using Data.Sorts.Enums;

namespace Data.Sorts.Models;

[Serializable]
public class Field
{
    public string Name { get; set; }
    public SortingOrder Order { get; set; }
}
