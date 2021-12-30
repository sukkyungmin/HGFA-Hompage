using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class RolesModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<CreateRoleModel> _logger;
		private readonly IDataAccessService _dataAccessService;

		public RolesModel(RoleManager<IdentityRole> roleManager,
			ILogger<CreateRoleModel> logger,
			UserManager<HangilFAUser> userManager,
			IDataAccessService dataAccessServic)
		{
			_roleManager = roleManager;
			_logger = logger;
			_userManager = userManager;
			_dataAccessService = dataAccessServic;
		}

		[BindProperty]
		public IEnumerable<RoleViewModel> roleviewmodel { get; set; }

		public async Task<IActionResult> OnGetAsync()
		{
			if (_userManager.GetUserName(User) == null)
				return RedirectToPage("/Logins/Login");
			if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Roles"))
				return RedirectToPage("Pageauthority");

			var roleViewModel = new List<RoleViewModel>();

			try
			{
				var roles = await _roleManager.Roles.ToListAsync();
				foreach (var item in roles)
				{
					roleViewModel.Add(new RoleViewModel()
					{
						Id = item.Id,
						Name = item.Name,
					});
				}

			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, ex.GetBaseException().Message);
			}

			roleviewmodel = roleViewModel.OrderBy(x => x.Name);

			return Page();
		}
	}
}
