using Paginations.Data;
using Paginations.Dtos;

namespace Paginations.Logics;

public class PerPageRecord
{
    public static List<RecordPerPage> GetPageRecords()
    {
        var parPageRecords = new List<RecordPerPage>();

        for (int iCount = 0; iCount < PerPageData.PerPageRecord.Count; iCount++)
        {
            parPageRecords.Add(new RecordPerPage() { Id = (iCount + 1), Value = PerPageData.PerPageRecord[iCount] });
        }

        return parPageRecords;
    }
}
