using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Data.Models;

[Serializable]
public class Pagination<T> where T : class
{
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; }
    public List<T> Data { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int PageSize { get; set; }
    public int RecordCount { get; set; }
    public int TotalPage { get; set; }
}
