using Data.Paginations.Extensions;
using Data.Sorts.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paginations.Logics;
using System.Diagnostics;
using PageModel = Paginations.Models;

namespace Paginations.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonService _personService;

    public HomeController(ILogger<HomeController> logger, IPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] Dtos.PaginationUI<PageModel.Person> pagination, int RecordPerPage_Id)
    {
        var data = _personService.GetPerson().Result;

        if (RecordPerPage_Id != 0)
        {
            // Get the session stored values to compare with the current state
            var sessionStoredPagination = JsonConvert.DeserializeObject<Dtos.PaginationUI<PageModel.Person>>(HttpContext.Session.GetString("Pagination") ?? "");
            
            // Handle the search string value
            if (pagination.SearchString == null && sessionStoredPagination.SearchString != null && !pagination.IsFieldOrderChanged)
                sessionStoredPagination.SearchString = null;
            
            // Update the property based on session stored contents
            pagination = pagination.UpdateMismatchedPropertyValue(sessionStoredPagination);
        }

        // Search
        if (!String.IsNullOrEmpty(pagination.SearchString) && !String.IsNullOrWhiteSpace(pagination.SearchString))
        {
            data = data.Where(s => s.Name.ToLower().Contains(pagination.SearchString.ToLower())).ToList();
        }

        // Sort
        pagination.Field = pagination.SortField.GetFieldSortOrder(pagination.Field, "Id", pagination.IsFieldOrderChanged);
        data = data.GetSortedList<PageModel.Person>(pagination.Field);

        // Record Per Page
        pagination.RecordPerPage.Id = RecordPerPage_Id == 0 ? 1 : RecordPerPage_Id;
        pagination.RecordPerPage.Value = RecordPerPage_Id == 0 ? 10 : pagination.PageRecord.FirstOrDefault(i => i.Id == RecordPerPage_Id).Value;

        // Set to session to keep the current state
        // actual data is not included in the session to keep the session object limited
        HttpContext.Session.SetString("Pagination", JsonConvert.SerializeObject(pagination));

        // Pagination
        pagination.Page = data.AsQueryable().GetPaged<PageModel.Person>(pagination.CurrentPage, pagination.RecordPerPage.Value);

        return View(pagination);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new PageModel.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

/*
 * Ref
 * https://www.mikesdotnetting.com/article/328/simple-paging-in-asp-net-core-razor-pages
 */ 