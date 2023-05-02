using Data.Models;

namespace Data.Paginations.Extensions;

/// <summary>
/// Pagination Helper
/// </summary>
public static class PaginationExtension
{
    /// <summary>
    /// Get pagination model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="currentPage"></param>
    /// <param name="recordPerPage"></param>
    /// <returns></returns>
    public static Pagination<T> GetPaged<T>
    (
        this IQueryable<T> data
        , int currentPage
        , int recordPerPage
    ) where T : class
    {
        var pagination = new Pagination<T>();

        pagination.CurrentPage = currentPage;
        pagination.PageSize = recordPerPage;
        pagination.RecordCount = data.Count();

        var pageCount = (double)pagination.RecordCount / recordPerPage;
        pagination.RecordCount = (int)Math.Ceiling(pageCount);

        pagination.TotalPage = (int)Math.Ceiling(decimal.Divide(data.Count(), pagination.PageSize));

        pagination.HasPrevious = pagination.CurrentPage > 1;
        pagination.HasNext = pagination.CurrentPage < pagination.TotalPage;

        var skip = (currentPage - 1) * recordPerPage;
        pagination.Data = data
            .Skip(skip)
            .Take(recordPerPage)
            .ToList();

        return pagination;
    }
}
