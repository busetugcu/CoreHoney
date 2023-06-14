using CoreHoney.Business.Abstract;
using CoreHoney.Entities;
using CoreHoney.WEBUI.Identity;
using CoreHoney.WEBUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data;

namespace CoreHoney.WEBUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IHoneyService _honeyService;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        public AdminController(IHoneyService honeyService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
                _honeyService = honeyService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult HoneyList()
        {
            return View(new HoneyListModel()
            {
                Honeys = _honeyService.GetAll()
            });
        }
        public IActionResult CreateHoney()
        {
            return View();
        }
        [HttpPost]  
        public IActionResult CreateHoney(HoneyModel model, IFormFile Image)
        {

            var entity = new Honey()
            {
                Name = model.Name,
                Price = model.Price,
                Decription = model.Decription,
                Image = Image.FileName
            };

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Image.FileName);
            _honeyService.Create(entity);
              

            return RedirectToAction("HoneyList");
        }


        [HttpGet]
        public IActionResult EditHoney(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _honeyService.GetHoneyDetails(id.Value);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new HoneyModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Decription = entity.Decription,
                Image = entity.Image
            };



            return View(model);
        }

        [HttpPost]
        public IActionResult EditHoney(HoneyModel model, IFormFile Image)
        {
            var entity = new Honey()
            {
                Name = model.Name,
            Price = model.Price,
            Decription = model.Decription,
            Image = model.Image,

        };

          
            
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
            _honeyService.Update(entity);
            return RedirectToAction("HoneyList");


        }
        public IActionResult DeleteHoney(int honeyid)
        {
            var entity = _honeyService.GetById(honeyid);

            if (entity != null)
            {
                _honeyService.Delete(entity);
            }

            return RedirectToAction("HoneyList");
        }

        public async Task<IActionResult> UserList()
        {
            List<ApplicationUser> userList = _userManager.Users.ToList();
            List<AdminUserModel> model = new List<AdminUserModel>();
            foreach (ApplicationUser item in userList)
            {
                AdminUserModel user = new AdminUserModel();
                user.FullName = item.FullName;
                user.EmailConfirmed = item.EmailConfirmed;
                user.Username = item.UserName;
                user.Email = item.Email;
                user.IsAdmin = await _userManager.IsInRoleAsync(item, "admin");

                model.Add(user);
            }

            return View(model);
        }

        public async Task<IActionResult> UserEdit(string Email)
        {

            ApplicationUser entity = await _userManager.FindByEmailAsync(Email);
            if (entity == null)
            {
                ModelState.AddModelError("", "bu kullanıcı ile daha önce hesap oluşturulmamıştır.");
                return View(entity);
            }

            AdminUserModel user = new AdminUserModel();
            user.FullName = entity.FullName;
            user.EmailConfirmed = entity.EmailConfirmed;
            user.Username = entity.UserName;
            user.Email = entity.Email;
            user.IsAdmin = await _userManager.IsInRoleAsync(entity, "admin");

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(AdminUserModel model)
        {
            ApplicationUser entity = await _userManager.FindByEmailAsync(model.Email);
            if (entity == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı ile daha önce hesap oluşturulmamıştır.");
                return View(model);
            }

            entity.FullName = model.FullName;
            entity.Email = model.Email;
            entity.EmailConfirmed = model.EmailConfirmed;
            entity.UserName = model.Username;
            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(entity, "admin");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(entity, "admin");
            }

            await _userManager.UpdateAsync(entity);
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete(string Email)
        {
            ApplicationUser entity = await _userManager.FindByEmailAsync(Email);
            if (entity != null)
            {
                await _userManager.DeleteAsync(entity);
                return RedirectToAction("UserList");
            }
            ModelState.AddModelError("", "Silme işlemi başarısız");
            return View(entity);
        }

    }
}

