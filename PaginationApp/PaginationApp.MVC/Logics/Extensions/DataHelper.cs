using Dtos = Paginations.Dtos;
using PageModel = Paginations.Models;

namespace Paginations.Logics;
public static class DataHelper
{
    public static Dtos.PaginationUI<PageModel.Person> UpdateMismatchedPropertyValue(this Dtos.PaginationUI<PageModel.Person> pagination, Dtos.PaginationUI<PageModel.Person> existing)
    {
        if (existing == null)
            return pagination;

        if(pagination.Page == null)
            pagination.Page = existing.Page;

        if (pagination.Field == null)
            pagination.Field = existing.Field;

        if(pagination.SortField == null)
            pagination.SortField = existing.Field.Name;

        if (pagination.SearchString == null)
            pagination.SearchString = existing.SearchString;

        return pagination;
    }
}
