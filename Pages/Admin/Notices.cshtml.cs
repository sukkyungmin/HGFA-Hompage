using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Services;
using HangilFA.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class NoticesModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly IDataAccessService _dataAccessService;
        private readonly HangilFADBContext _context;

        public NoticesModel(UserManager<HangilFAUser> userManager,
			IDataAccessService dataAccessServic,
            HangilFADBContext context)
		{
			_userManager = userManager;
			_dataAccessService = dataAccessServic;
            _context = context;
		}

		[BindProperty]
        //public IEnumerable<NoticesViewModel> noticesViewModel { get; set; }

        public PaginatedList<NoticesViewModel> Students { get; set; }

        //public async Task<IActionResult> OnGetAsync(int? pageIndex)
        //{
        //    if (_userManager.GetUserName(User) != null)
        //    {
        //        if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Notices"))
        //            return RedirectToPage("Pageauthority");

        //        var permissions = new List<NoticesViewModel>();

        //        permissions = await _dataAccessService.GetNoticesAsync();

        //        noticesViewModel = permissions.OrderByDescending(a => a.Id);

        //        return Page();
        //    }

        //    return RedirectToPage("/Logins/Login");
        //}


        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            if (_userManager.GetUserName(User) != null)
            {
                if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Notices"))
                    return RedirectToPage("Pageauthority");

                IQueryable<NoticesViewModel> NoticesViewModelIQ = (from notices in _context.SupporNotices
                                                                   select new NoticesViewModel()
                                                                   {
                                                                       Id = notices.Id,
                                                                       Title = notices.Title,
                                                                       Content = notices.Content,
                                                                       CreateUser = notices.CreateUser,
                                                                       CreateFullname = notices.CreateFullname,
                                                                       CreateTime = notices.CreateTime,
                                                                   }) ;

                NoticesViewModelIQ = NoticesViewModelIQ.OrderByDescending(s => s.CreateTime);


                int pageSize = 7;
                Students = await PaginatedList<NoticesViewModel>.CreateAsync(NoticesViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }

    }
}
