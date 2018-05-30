using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateForum.Context;
using PrivateForum.Entities;
using PrivateForum.Entities.DTO;
using PrivateForum.Entities.Helpers;

namespace PrivateForum.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginDto { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                model.TagsToList();
                ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.InviteToken != null) {
                        Invite invite = _context.Invites.Include(i => i.User).SingleOrDefault(i => i.InviteToken == model.InviteToken);
                        if (invite != null) {
                            user.Parent = invite.User;
                            _context.Invites.Remove(invite);
                            _context.SaveChanges();
                        }
                    }
                    user.InviteCount = 5;
                    foreach (string Tag in model.TagsNames) {
                        Tag tag = _context.Tags.SingleOrDefault(t => t.Name == Tag);
                        if (tag == null)
                        {
                            tag = new Tag { Name = Tag };
                            _context.Tags.Add(tag);
                            _context.SaveChanges();
                        }
                            _context.Add(new ApplicationUserTag { ApplicationUser = user, Tag = tag });
                            _context.SaveChanges();
                    }
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}