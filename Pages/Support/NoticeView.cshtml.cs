using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class NoticeViewModel : PageModel
    {
        private readonly IDataAccessService _dataAccessService;

        public NoticeViewModel(IDataAccessService dataAccessServic)
        {
            _dataAccessService = dataAccessServic;
        }

        [BindProperty]
        public NoticesViewModel noticesviewmodel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var noticesViewModel = new NoticesViewModel();

            noticesViewModel = await _dataAccessService.GetNoticesAsync(id);

            noticesviewmodel = noticesViewModel;

            return Page();

        }
    }
}
