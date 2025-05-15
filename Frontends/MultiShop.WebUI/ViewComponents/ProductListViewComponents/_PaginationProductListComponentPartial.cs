using Microsoft.AspNetCore.Mvc;

public class _PaginationProductListComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke(int currentPage, int totalPages, string? productName,string range)
    {
        ViewData["CurrentPage"] = currentPage;
        ViewData["TotalPages"] = totalPages;
        ViewData["ProductName"] = productName;
        ViewData["range"] = range;
        return View();
    }
}
