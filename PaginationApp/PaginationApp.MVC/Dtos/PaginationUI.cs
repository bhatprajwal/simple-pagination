using Paginations.Logics;
using Model = Data.Models;
using Sort = Data.Sorts.Models;

namespace Paginations.Dtos;

public class PaginationUI<T> where T : class
{
    public Sort.Field Field { get; set; }
    public Model.Pagination<T> Page { get; set; }
    public string SortField { get; set; }
    public string SearchString { get; set; }
    public int CurrentPage { get; set; } = 1;
    public string TagName { get; set; }
    public bool IsFieldOrderChanged { get; set; }
    public List<Dtos.RecordPerPage> PageRecord { get; set; } = PerPageRecord.GetPageRecords();
    public Dtos.RecordPerPage RecordPerPage { get; set; } = new();
}
