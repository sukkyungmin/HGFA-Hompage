using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class NoticeModel : PageModel
    {
        private readonly HangilFADBContext _context;

        public NoticeModel(HangilFADBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaginatedList<NoticesViewModel> Students { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            IQueryable<NoticesViewModel> NoticesViewModelIQ = (from notices in _context.SupporNotices
                                                               select new NoticesViewModel()
                                                               {
                                                                   Id = notices.Id,
                                                                   Title = notices.Title,
                                                                   CreateUser = notices.CreateUser,
                                                                   CreateFullname = notices.CreateFullname,
                                                                   CreateTime = notices.CreateTime,
                                                               });

            NoticesViewModelIQ = NoticesViewModelIQ.OrderByDescending(s => s.Id);


            int pageSize = 7;
            Students = await PaginatedList<NoticesViewModel>.CreateAsync(NoticesViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
