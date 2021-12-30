using HangilFA.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HangilFA.Services
{
    public interface IDataAccessService
    {
        Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act);
        Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal);
        Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id, int orders);
        Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds);
        Task<bool> GetRoleCheck(string id, string checkitem);
        Task<NoticesViewModel> GetNoticesAsync(Guid id);
        Task<bool> SetNoticesAsync(NoticesViewModel model);
        Task<bool> SetUpdateNoticesAsync(NoticesViewModel model);
        Task<QuestionsViewModel> GetQuestionsAsync(Guid id);
        Task<int> GetQuestionsCountAsync(string id);
        Task<bool> SetQuestionsAsync(QuestionsViewModel model);
        Task<bool> SetUpdateQuestionsAsync(QuestionsViewModel model);
        Task<bool> SetUpdateQuestionsCountAsync(Guid id,bool type);
        Task<List<QuestionsAnswerViewModel>> GetQuestionsAnswerAsync(Guid id);
        Task<bool> SetQuestionsAnswerAsync(QuestionsAnswerViewModel model);
        Task<bool> SetUpdateQuestionsAnswerAsync(QuestionsAnswerViewModel model);
        Task<bool> GetFileCheckAsync(int filemasterid, string filename);
        Task<bool> SetFileSaveAsync(FileUploadViewModel model);
    }
}
