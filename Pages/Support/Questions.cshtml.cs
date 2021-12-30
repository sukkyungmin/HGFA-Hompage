using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class QuestionsModel : PageModel
    {
        private readonly HangilFADBContext _context;

        public QuestionsModel(HangilFADBContext context)
        {
            _context = context;
        }


        [BindProperty]
        public PaginatedList<QuestionsViewModel> Students { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            IQueryable<QuestionsViewModel> QuestionsViewModelIQ = (from questions in _context.SupporQuestions
                                                               select new QuestionsViewModel()
                                                               {
                                                                   Id = questions.Id,
                                                                   Title = questions.Title,
                                                                   CreateUserId = questions.CreateUserId,
                                                                   CreateUser = questions.CreateUser,
                                                                   CreateFullname = questions.CreateFullname,
                                                                   CreateTime = questions.CreateTime,
                                                                   AnswerCount =questions.AnswerCount,
                                                                   ContentTimeCheck = (DateTime.Compare(questions.CreateTime,DateTime.Now.AddHours(-24)) > 0),                                           
                                                               });

            QuestionsViewModelIQ = QuestionsViewModelIQ.OrderByDescending(s => s.Id);


            int pageSize = 7;
            Students = await PaginatedList<QuestionsViewModel>.CreateAsync(QuestionsViewModelIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
