﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eLections.Models;
using eLections.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace eLections.Controllers
{
    [Authorize(Roles="CanManageUsers")]
    public class AdministrationController : Controller
    {
        private  ApplicationUserManager _userManager;
        private  ApplicationRoleManager _roleManager;
        private readonly ApplicationDbContext _context;

        public AdministrationController()
        {
            _context = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(_context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));

        }
        public AdministrationController(ApplicationRoleManager roleManager,
                                        ApplicationUserManager userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Administration
        public ActionResult Index()
        {
            var users = _userManager.Users.Include(u=> u.Land).ToList();
            var listOfUserViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                listOfUserViewModels.Add(new UserViewModel
                {
                    Id=user.Id,
                    UserName = user.UserName,
                    Land = user.Land,
                    RolesList = null
                });
            }
            return View("UsersList",listOfUserViewModels);
        }
        // GET: Administration/Details/{id}
        public ActionResult Details(string id)
        {
            var user = _userManager.Users
                                    .Include(u => u.Land)
                                    .SingleOrDefault(u => u.Id == id);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            var roles = _roleManager.Roles.ToList()
                .Where(r => user.Roles.ToList().Any(r2 => r2.RoleId == r.Id))
                .ToList();
            var viewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Land = user.Land,
                RolesList = roles

            };
            return View(viewModel);
        }

        public async Task<ActionResult> ManageRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return HttpNotFound();
            }

            var viewModel = new List<ManageRoleViewModel>();
            var roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                viewModel.Add(new ManageRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    RoleDescription = role.Description,
                    IsSelected = await _userManager.IsInRoleAsync(id,role.Name)
                });
            }
            return View(viewModel);

        }

        [HttpPost]
        public async Task<ActionResult> ManageRoles(List<ManageRoleViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var roles = await _userManager.GetRolesAsync(id);
            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(id, role);
            }

            foreach (var managedRole in model)
            {
                if (managedRole.IsSelected)
                {
                    await _userManager.AddToRoleAsync(id, managedRole.RoleName);
                }
            }
            
            return RedirectToAction("Index");

        }
    }
}