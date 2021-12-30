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
    public class DeleteRoleModel : PageModel
    {
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly ILogger<CreateRoleModel> _logger;
		private readonly HangilFADBContext _context;

		public DeleteRoleModel(RoleManager<IdentityRole> roleManager,
			ILogger<CreateRoleModel> logger,
			HangilFADBContext context,
			UserManager<HangilFAUser> userManager)
		{
			_roleManager = roleManager;
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		[BindProperty]
		public RoleViewModel roleviewmodel { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (_userManager.GetUserName(User) == null || _userManager.GetUserName(User) != "Admin")
				return RedirectToPage("Pageauthority");

			roleviewmodel = new RoleViewModel();
			if (!string.IsNullOrWhiteSpace(id))
			{
				var user = await _roleManager.FindByIdAsync(id);

                roleviewmodel.Id = user?.Id;
				roleviewmodel.Name = user?.Name;
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

				var viewmodel = await _context.Roles.FindAsync(id);

				if (viewmodel == null)
				{
					return NotFound();
				}

				try
				{
					_context.Roles.Remove(viewmodel);
					await _context.SaveChangesAsync();
					return RedirectToPage("./Roles");
				}
				catch (DbUpdateException)
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
