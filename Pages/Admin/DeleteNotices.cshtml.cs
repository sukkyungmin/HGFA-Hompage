using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class DeleteNoticesModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;
        private readonly HangilFADBContext _context;

        public DeleteNoticesModel(UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic,
            HangilFADBContext context)
        {
            _userManager = userManager;
            _dataAccessService = dataAccessServic;
            _context = context;

        }

        [BindProperty]
        public NoticesViewModel noticesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (_userManager.GetUserName(User) != null)
            {
                if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Notices"))
                    return RedirectToPage("Pageauthority");

                noticesViewModel = new NoticesViewModel();

                var permissions = await _dataAccessService.GetNoticesAsync(id);

                noticesViewModel = permissions;

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }


        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var viewmodel = await _context.SupporNotices.FindAsync(id);

                if (viewmodel == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.SupporNotices.Remove(viewmodel);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Notices");
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
