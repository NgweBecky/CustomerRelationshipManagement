using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomerRelationshipManagement.UI.Models;
using CustomerRelationshipManagement.UI.Models.DBContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRelationshipManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        CustomerRelationshipManagementDBContext context = new CustomerRelationshipManagementDBContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model))
                {
                    bool isAuthenticate = false;
                    var user_role = GetRole(get_role_id).ToString();
                    if (user_role == "Manager" || user_role == "Employee")
                    {
                        isAuthenticate = true;
                    }
                    var claims = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, user_role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    if (isAuthenticate)
                    {
                        var principal = new ClaimsPrincipal(claims);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        //if (Url.IsLocalUrl(returnUrl))
                        //{
                        //    return Redirect(returnUrl);
                        //}
                        //else
                        //{
                        //    return Redirect("/");
                        //}
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Credentials");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/account/login");
        }
        public int get_role_id;
        public bool ValidateUser(LoginModel model)
        {
            var user = context.GetUser(model.Username);
            if (user?.Id > 0)
            {
                if (user.Username == model.Username && user.Password == model.Password)
                {
                    get_role_id = Convert.ToInt32(user.RoleId.ToString());
                    return true;
                }
            }
            return false;
        }
        public string GetRole(int rol)
        {
            var role = context.GetRole(rol);
            if (role.Rolename.ToString() != null)
            {
                return role.Rolename.ToString();
            }
            return "";
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
