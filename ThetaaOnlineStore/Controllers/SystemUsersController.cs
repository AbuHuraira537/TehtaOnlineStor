using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThetaaOnlineStore.Models;

namespace ThetaaOnlineStore.Controllers
{
   
    public class SystemUsersController : Controller
    {
        ThetaOnlineStoreContext ORM = null;
        public SystemUsersController(ThetaOnlineStoreContext orm)
        {
            ORM = orm;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName", "Password", "Email", "CreatedBy", "Role", "Status", "Mobile")] SystemUser sm)
        {
            if(ModelState.IsValid)
            {
                ORM.SystemUser.Add(sm);
                await ORM.SaveChangesAsync();
            }

            return View();
        }
    }
}