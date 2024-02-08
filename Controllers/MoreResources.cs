using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MentalNote.Controllers;

[Authorize]
public class MoreResourcesController : Controller
{

    public IActionResult Index()
    {
        return View();
        }
}