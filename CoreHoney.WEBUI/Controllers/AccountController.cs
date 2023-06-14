using CoreHoney.Business.Abstract;
using CoreHoney.WEBUI.EmailServices;
using CoreHoney.WEBUI.Identity;
using CoreHoney.WEBUI.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreHoney.WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signManager;
        private ICartService _cartService;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICartService cartService)
        {
            _cartService = cartService;
            _userManager = userManager;
            _signManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                // generated token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });
                //send mail
                string siteUrl = "https://localhost:7218";
                string activateUrl = $"{siteUrl}{callbackUrl}";
                string body = $"Merhaba {model.UserName};<br><br>Hesabınızı aktifleştirmek için  <a href='{activateUrl}' target='_blank'> tıklayınız</a>.";
                MailHelper.SendMail(body, model.Email, "CoreHoney Hesap Aktifleştirme");

                //TempData.Put("message", new ResultMessage()
                //{
                //    Title = "Hesap Onayı",
                //    Message = "Email adresinize gelen link ile hesabınızı onaylayınız",
                //    Css = "warning"
                //});

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen hata oluştu lütfen tekrar deneyiniz");
            return View(model); 

           





            //return RedirectToAction("Login", "Account");
        }

        public IActionResult Login(string ReturnUrl = "~/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //ReturnUrl= ReturnUrl ?? "~/";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);


            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı ile daha önce kayıt oluşturulmamış.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen hesabınızı email ile onaylayınız.");
                return View(model);
            }

            var result = await _signManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if(result.Succeeded)
            {
                return Redirect(model.ReturnUrl?? "~/");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya Şifre hatalı!!");


            return View(model);

           





            //return RedirectToAction("Login", "Account");


        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
           

            return Redirect("~/");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData["message"] = "Geçersiz token";
                return View();  

            }
              var user =await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result =await _userManager.ConfirmEmailAsync(user,token);
                if (result.Succeeded)
                {
                    _cartService.InitializeCart(user.Id);

                    TempData["message"] = "Hesabınız Onaylandı.";
                    return View();              
                }
            }


            TempData["message"] = "Hesabınız Onaylanmadı.";
                return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user =await _userManager.FindByEmailAsync(Email);   
            if(user==null)
            {
                return View();  
            }
            var code =await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
               
                token = code
            });
            //send mail
            string siteUrl = "https://localhost:7218";
            string activateUrl = $"{siteUrl}{callbackUrl}";
            string body = $"parolanızı yenilemek için;  <a href='{activateUrl}' target='_blank'> tıklayınız</a>.";
            MailHelper.SendMail(body, Email, "CoreHoney Şifr Resetleme");
            return RedirectToAction("Login", "Account");
           
        }

        public IActionResult ResetPassword(string token)
        {

            if (token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordModel() { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
              
            return View(model);
        }





        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}
