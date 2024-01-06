using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MvcMovie.Controllers;

public class UsersController : Controller
{

   public IActionResult Index(){
       return View();
   }
   
   public IActionResult Edit() {
       return View();
   }

}
