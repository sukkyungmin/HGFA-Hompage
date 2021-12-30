using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class DeleteUserModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<CreateRoleModel> _logger;
		private readonly HangilFADBContext _context;

		public DeleteUserModel(RoleManager<IdentityRole> roleManager,
			ILogger<CreateRoleModel> logger,
			UserManager<HangilFAUser> userManager,
			HangilFADBContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_context = context;
		}

		[BindProperty]
		public UserViewModel userviewmodel { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (_userManager.GetUserName(User) == null || _userManager.GetUserName(User) != "Admin")
				return RedirectToPage("Pageauthority");

			userviewmodel = new UserViewModel();
			if (!string.IsNullOrWhiteSpace(id))
			{
				var user = await _userManager.FindByIdAsync(id);

				userviewmodel.Email = user?.Email;
				userviewmodel.UserName = user?.UserName;
				userviewmodel.Id = user?.Id;
				userviewmodel.FullName = user?.FullName;
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string id)
		{
			if (ModelState.IsValid)
			{
				if (id == null)
				{
					return NotFound();
				}

				var viewmodel = await _context.Users.FindAsync(id);

				if(viewmodel == null)
                {
					return NotFound();
				}

                try
                {
					_context.Users.Remove(viewmodel);
					await _context.SaveChangesAsync();
					return RedirectToPage("./Users");
				}
				catch(DbUpdateException)
                {
					//return RedirectToAction("./Delete",new { id,savechangeserror = })
                }
					//_context.ContextId(id).r
					//await _context.SaveChangesAsync();
			}

			return Page();
		}
	}
}
