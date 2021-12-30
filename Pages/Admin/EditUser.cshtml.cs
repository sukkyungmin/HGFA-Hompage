using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class EditUserModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<CreateRoleModel> _logger;

		public EditUserModel(RoleManager<IdentityRole> roleManager,
			ILogger<CreateRoleModel> logger,
			UserManager<HangilFAUser> userManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}

		[BindProperty]
		public UserViewModel userviewmodel { get; set; }

		public bool error { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (_userManager.GetUserName(User) == null || _userManager.GetUserName(User) != "Admin")
				return RedirectToPage("Pageauthority");

			error = false;
			userviewmodel = new UserViewModel();
			if (!string.IsNullOrWhiteSpace(id))
			{
				var user = await _userManager.FindByIdAsync(id);
				var userRoles = await _userManager.GetRolesAsync(user);

				userviewmodel.Email = user?.Email;
				userviewmodel.UserName = user?.UserName;
				userviewmodel.Id = user?.Id;
				userviewmodel.FullName = user?.FullName;

				var allRoles = await _roleManager.Roles.ToListAsync();
				userviewmodel.Roles = allRoles.Select(x => new RoleViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Selected = userRoles.Contains(x.Name)
				}).ToArray();

			}


			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(userviewmodel.Id);
				var userRoles = await _userManager.GetRolesAsync(user);

                if (userviewmodel.Roles.Where(x => x.Selected).Select(x => x.Name).Count() > 1 || userviewmodel.Roles.Where(x => x.Selected).Select(x => x.Name).Count() < 1)
                {
					error = true;
					return Page();
                }

                await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRolesAsync(user, userviewmodel.Roles.Where(x => x.Selected).Select(x => x.Name));

				return RedirectToPage("Users");
			}

			return Page();
		}
	}
}
