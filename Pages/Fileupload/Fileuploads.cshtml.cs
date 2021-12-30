using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

using HangilFA.Areas.Identity.Data;
using HangilFA.Services;
using HangilFA.Utility;
using HangilFA.Model;

namespace HangilFA.Pages.Fileupload
{
    public class FileuploadsModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".txt", ".png" };
        private readonly string _targetFilePath;

        public FileuploadsModel(IConfiguration config, UserManager<HangilFAUser> userManager,
             IDataAccessService dataAccessServic)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            // To save physical files to the temporary files folder, use:
            //_targetFilePath = Path.GetTempPath();

            _userManager = userManager;
            _dataAccessService = dataAccessServic;

            Radiobuttonitem = new List<RadioButtonItem>
            {
                new RadioButtonItem { Text = "PLC", Values = "PLC", Checks = true },
                new RadioButtonItem { Text = "HMI", Values = "HMI", Checks = false },
                new RadioButtonItem { Text = "Motion", Values = "Motion", Checks = false }
            };
        }

        [BindProperty]
        public BufferedMultipleFileUploadPhysical FileUpload { get; set; }
        [BindProperty]
        public FileUploadViewModel fileuploadviewmodel { get; set; }

        public List<RadioButtonItem> Radiobuttonitem { get; set; }

        public string Result { get; private set; }

        public string Listgroup { get; private set; }


        //public async Task<IActionResult> OnGetAsync()
        //{
        //    if (_userManager.GetUserName(User) != null)
        //    {
        //        var user = await _context.SupporQuestions.FindAsync(id);

        //        if (user.CreateUserId == _userManager.GetUserId(User) || _userManager.GetUserName(User) == "Admin")
        //        {

        //            Questionsviewmodel = new QuestionsViewModel();

        //            var permissions = await _dataAccessService.GetQuestionsAsync(id);

        //            Questionsviewmodel = permissions;

        //            return Page();
        //        }
        //        return RedirectToPage("/Admin/Pageauthority");

        //    }

        //    return RedirectToPage("/Logins/Login");
        //}


        public async Task<IActionResult> OnPostUploadAsync()
        {
            if (!ModelState.IsValid)
            {
                Result = "Please correct the form.";

                return Page();
            }


            foreach(var items in Radiobuttonitem)
            {
                if(items.Checks)
                {
                    Listgroup = items.Text;
                }
            }

            //fileuploadviewmodel.FileName = FileUpload.FormFiles.filename


            foreach (var formFile in FileUpload.FormFiles)
            {
                var formFileContent =
                    await FileHelpers
                        .ProcessFormFile<BufferedMultipleFileUploadPhysical>(
                            formFile, ModelState, _permittedExtensions,
                            _fileSizeLimit);

                if (!ModelState.IsValid)
                {
                    Result = "Please correct the form.";

                    return Page();
                }

                fileuploadviewmodel.FileMasterId = 1;
                fileuploadviewmodel.DirectoryPath = string.Format("~/images/{0}/{1}", Listgroup, formFile.FileName);
                fileuploadviewmodel.FileName = formFile.FileName;
                fileuploadviewmodel.FileSize = formFile.Length.ToString();


                if (!await _dataAccessService.GetFileCheckAsync(fileuploadviewmodel.FileMasterId, fileuploadviewmodel.FileName))
                {
                    Result = "Please Overlap File Name Check.";

                    return Page();
                }

                // For the file name of the uploaded file stored
                // server-side, use Path.GetRandomFileName to generate a safe
                // random file name.
                //var trustedFileNameForFileStorage = Path.GetRandomFileName();

                var trustedFileNameForFileStorage = formFile.FileName;
                //var filePath = Path.Combine(
                //    _targetFilePath, trustedFileNameForFileStorage);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), string.Format("wwwroot\\images\\productlist\\{0}\\", Listgroup)) + trustedFileNameForFileStorage;
                // **WARNING!**
                // In the following example, the file is saved without
                // scanning the file's contents. In most production
                // scenarios, an anti-virus/anti-malware scanner API
                // is used on the file before making the file available
                // for download or for use by other systems. 
                // For more information, see the topic that accompanies 
                // this sample.

                using var fileStream = System.IO.File.Create(filePath);
                await fileStream.WriteAsync(formFileContent);

                await _dataAccessService.SetFileSaveAsync(fileuploadviewmodel);
                // To work directly with the FormFiles, use the following
                // instead:
                //await formFile.CopyToAsync(fileStream);
            }

            return RedirectToPage("./Fileuploads");
        }
    }

    public class BufferedMultipleFileUploadPhysical
    {
        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }

    public class RadioButtonItem
    {
        public string Text { get; set; }

        public string Values { get; set; }

        public bool Checks { get; set; }
    }
}
