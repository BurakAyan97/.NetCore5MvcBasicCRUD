using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CoreProject.Controllers
{
    //username : burak , şifre : 123123  (Personeller listesine bakmak isterseniz)
    public class LoginController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        public async Task<IActionResult> GirisYap(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);
            if (bilgiler != null) //kullanıcı değerleri eşleşiyorsa yapılacaklar
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Kullanici)
                };
                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Personeller");
            }
            return View();
        }
    }
}
