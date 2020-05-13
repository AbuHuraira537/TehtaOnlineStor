using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Register([Bind("UserName", "Password", "DisplayName", "Email", "Mobile", "Status", "Role", "CreatedDate", "CreatedBy")] SystemUser sm)
        {
            if(ModelState.IsValid)
            {
                ORM.SystemUser.Add(sm);
                await ORM.SaveChangesAsync();
                ViewBag.Message = "User added sucessfully!";
            }
            else
            {
                ViewBag.Message = "Not added sucessfully check fields!";
            }


            return View();
        }
        public IActionResult ShowAll(string searchquery)
        {
            if(searchquery!=null)
            {
                return View(ORM.SystemUser.Where(a=>a.UserName.Contains(searchquery)).ToList<SystemUser>());

            }
            else
            {
                return View(ORM.SystemUser.ToList<SystemUser>());
            }

        }
        public async Task<IActionResult> Detail(int id)
        {
            SystemUser user=await ORM.SystemUser.FindAsync(id);
            if(user==null)
            {
                return NotFound();
            }
            else
            {
                return View(user);
            }    
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SystemUser user = await ORM.SystemUser.FindAsync(id);
            if(user!=null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(SystemUser user)
        {
            
            if(user!=null)
            {

                ORM.Update(user);
               await ORM.SaveChangesAsync();
                return RedirectToAction(nameof(Register));
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            SystemUser s = await ORM.SystemUser.FindAsync(id);
            if (s != null)
            {
                ORM.SystemUser.Remove(s);
                await ORM.SaveChangesAsync();
                return RedirectToAction(nameof(ShowAll));
            }
            else
            {
                return NotFound();
            }

        }
    }
}