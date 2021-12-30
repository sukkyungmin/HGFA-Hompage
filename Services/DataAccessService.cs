using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace HangilFA.Services
{
    public class DataAccessService : IDataAccessService
    {
		private readonly IMemoryCache _cache;
		private readonly HangilFADBContext _context;

		public DataAccessService(HangilFADBContext context, IMemoryCache cache)
		{
			_cache = cache;
			_context = context;
		}

		public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
		{
			var isAuthenticated = principal.Identity.IsAuthenticated;
			if (!isAuthenticated)
			{
				return new List<NavigationMenuViewModel>();
			}

			var roleIds = await GetUserRoleIds(principal);

			var permissions = await _cache.GetOrCreateAsync("Permissions",
				async x => await (from menu in _context.NavigationMenu select menu).ToListAsync());

			var rolePermissions = await _cache.GetOrCreateAsync("RolePermissions",
				async x => await (from menu in _context.RoleMenuPermission select menu).Include(x => x.NavigationMenu).ToListAsync());

			var data = (from menu in rolePermissions
						join p in permissions on menu.NavigationMenuId equals p.Id
						where roleIds.Contains(menu.RoleId)
						select p)
							  .Select(m => new NavigationMenuViewModel()
							  {
								  Id = m.Id,
								  Name = m.Name,
								  Area = m.Area,
								  Visible = m.Visible,
								  IsExternal = m.IsExternal,
								  ActionName = m.ActionName,
								  ExternalUrl = m.ExternalUrl,
								  DisplayOrder = m.DisplayOrder,
								  ParentMenuId = m.ParentMenuId,
								  ControllerName = m.ControllerName,
							  }).Distinct().ToList();

			return data;
		}

		public async Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act)
		{
			var result = false;
			var roleIds = await GetUserRoleIds(ctx);
			var data = await (from menu in _context.RoleMenuPermission
							  where roleIds.Contains(menu.RoleId)
							  select menu)
							  .Select(m => m.NavigationMenu)
							  .Distinct()
							  .ToListAsync();

			foreach (var item in data)
			{
				result = (item.ControllerName == ctrl && item.ActionName == act);
				if (result)
				{
					break;
				}
			}

			return result;
		}

		public async Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id, int orders)
		{
			var items = await (from m in _context.NavigationMenu
							   join rm in _context.RoleMenuPermission
								on new { X1 = m.Id, X2 = id } equals new { X1 = rm.NavigationMenuId, X2 = rm.RoleId }
								into rmp
							   from rm in rmp.DefaultIfEmpty()
							   where m.DisplayOrder < orders
							   select new NavigationMenuViewModel()
							   {
								   Id = m.Id,
								   Name = m.Name,
								   Area = m.Area,
								   ActionName = m.ActionName,
								   ControllerName = m.ControllerName,
								   IsExternal = m.IsExternal,
								   ExternalUrl = m.ExternalUrl,
								   DisplayOrder = m.DisplayOrder,
								   ParentMenuId = m.ParentMenuId,
								   Visible = m.Visible,
								   Permitted = rm.RoleId == id
							   })
							   .AsNoTracking()
							   .ToListAsync();

			return items;
		}

		public async Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds)
		{
			var existing = await _context.RoleMenuPermission.Where(x => x.RoleId == id).ToListAsync();
			_context.RemoveRange(existing);

			foreach (var item in permissionIds)
			{
				await _context.RoleMenuPermission.AddAsync(new RoleMenuPermission()
				{
					RoleId = id,
					NavigationMenuId = item,
				});
			}

			var result = await _context.SaveChangesAsync();

			// Remove existing permissions to roles so it can re evaluate and take effect
			_cache.Remove("RolePermissions");

			return result > 0;
		}

		private async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
		{
			var userId = GetUserId(ctx);
			var data = await (from role in _context.UserRoles
							  where role.UserId == userId
							  select role.RoleId).ToListAsync();

			return data;
		}

		private string GetUserId(ClaimsPrincipal user)
		{
			return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}

		public async Task<bool> GetRoleCheck(string id, string checkitem)
		{
			var result = false;
			var data = await (from role in _context.UserRoles
							  where role.UserId == id
							  select role.RoleId).ToListAsync();

			if(data.Count > 0)
            {
				var itemlist = await GetPermissionsByRoleIdAsync(data[0].ToString(), 10);

				foreach (var item in itemlist)
				{
					if(item.Name == checkitem && item.Permitted == true)
                    {
						result = true;
					}
				}
			}

			return result;
		}


		#region Notices Service

		public async Task<NoticesViewModel> GetNoticesAsync(Guid id)
		{
			// var items = await (from notices in _context.SupporNews
			//                    select new NoticesViewModel()
			//                    {
			//                        Id = notices.Id,
			//                        Title = notices.Title,
			//                        Content = HttpUtility.HtmlDecode(notices.Content),
			//RichContent = HttpUtility.HtmlDecode(notices.RichContent),
			//CreateUser = notices.CreateUser,
			//                        ModifyUser = notices.ModifyUser,
			//                        CreateFullname = notices.CreateFullname,
			//                        ModifyFullname = notices.ModifyFullname,
			//                        CreateTime = notices.CreateTime,
			//                        ModifyTime = notices.ModifyTime,
			//                        ModifyCount = notices.ModifyCount
			//                    })
			//                    .AsNoTracking()
			//                    .ToListAsync();
			var users = new NoticesViewModel();

			var items = await _context.SupporNotices.FindAsync(id);

			users.Id = items.Id;
			users.Title = items.Title;
			users.RichContent = HttpUtility.HtmlDecode(items.RichContent);
			users.CreateUser = items.CreateUser;
			users.CreateFullname = items.CreateFullname;
			users.ModifyUser = items.ModifyUser;
			users.ModifyFullname = items.ModifyFullname;
			users.CreateTime = items.CreateTime;
			users.ModifyTime = items.ModifyTime;
			users.ModifyCount = items.ModifyCount;

            return users;
		}

        public async Task<bool> SetNoticesAsync(NoticesViewModel model)
        {

            await _context.SupporNotices.AddAsync(new SupporNotices()
            {
                Title = model.Title,
                Content = model.Content,
                RichContent = HttpUtility.HtmlEncode(model.RichContent),
                CreateUser = model.CreateUser,
                CreateFullname = model.CreateFullname,
                ModifyUser = "Not User",
                ModifyFullname = "Not User",
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now,
                ModifyCount = 0,
            });

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> SetUpdateNoticesAsync(NoticesViewModel model)
        {
			var result = 0;
			var items = await _context.SupporNotices.FindAsync(model.Id);

            if (items != null)
            {
				items.Title = model.Title;
                items.RichContent = HttpUtility.HtmlEncode(model.RichContent);
                items.ModifyUser = model.ModifyUser;
                items.ModifyFullname = model.ModifyFullname;
                items.ModifyTime = DateTime.Now;
                items.ModifyCount = model.ModifyCount + 1;

                result = await _context.SaveChangesAsync();
			}

			return result > 0;
        }

		#endregion

		#region Questions Service
		public async Task<QuestionsViewModel> GetQuestionsAsync(Guid id)
		{
			var users = new QuestionsViewModel();

            var items = await _context.SupporQuestions.FindAsync(id);

            users.Id = items.Id;
			users.Title = items.Title;
			users.Content = HttpUtility.HtmlDecode(items.Content);
			users.CreateUserId = items.CreateUserId;
			users.CreateUser = items.CreateUser;
			users.CreateFullname = items.CreateFullname;
			users.CreateTime = items.CreateTime;

			return users;
		}

		public async Task<int> GetQuestionsCountAsync(string id)
		{
			int count;

			var item = await (from p in _context.SupporQuestions
							  where p.CreateUserId == id && p.CreateTime >= DateTime.Now.AddDays(-1)
							  select p).ToListAsync();

			count = item.Count();

			return count;
		}

		public async Task<bool> SetQuestionsAsync(QuestionsViewModel model)
		{

			await _context.SupporQuestions.AddAsync(new SupporQuestions()
			{
				Id = model.Id,
				Title = model.Title,
				Content = HttpUtility.HtmlEncode(model.Content),
				CreateUserId = model.CreateUserId,
				CreateUser = model.CreateUser,
				CreateFullname = model.CreateFullname,
				CreateTime = DateTime.Now,
				AnswerCount = 0,
			});

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<bool> SetUpdateQuestionsAsync(QuestionsViewModel model)
		{
			var result = 0;
			var items = await _context.SupporQuestions.FindAsync(model.Id);

			if (items != null)
			{
				items.Title = model.Title;
				items.Content = HttpUtility.HtmlEncode(model.Content);

				result = await _context.SaveChangesAsync();
			}

			return result > 0;
		}

		public async Task<bool> SetUpdateQuestionsCountAsync(Guid id, bool type)
        {
			var result = 0;
			var items = await _context.SupporQuestions.FindAsync(id);

			if (items != null)
			{
				if(type)
                {
					items.AnswerCount += 1;

					result = await _context.SaveChangesAsync();
				}
				else
                {
					items.AnswerCount -= 1;

					result = await _context.SaveChangesAsync();
				}

			}

			return result > 0;
		}

		public async Task<List<QuestionsAnswerViewModel>> GetQuestionsAnswerAsync(Guid id)
		{

			var items = await (from p in _context.SupporQuestionsAnswer
							   where p.SupporQuestionsId == id
							   orderby p.Id ascending
							   select new QuestionsAnswerViewModel()
							   {
								   Id = p.Id,
								   Content = p.Content,
								   AnswerUserId = p.AnswerUserId,
								   AnswerUser = p.AnswerUser,
								   CreateTime = p.CreateTime
							   })
							   .AsNoTracking()
							   .ToListAsync();

			return items;
		}

		public async Task<bool> SetQuestionsAnswerAsync(QuestionsAnswerViewModel model)
		{
            await _context.SupporQuestionsAnswer.AddAsync(new SupporQuestionsAnswer()
			{
				SupporQuestionsId = model.SupporQuestionsId,
				Content = HttpUtility.HtmlEncode(model.Content),
				AnswerUserId = model.AnswerUserId,
				AnswerUser = model.AnswerUser,
				AnswerUserFullname = model.AnswerUserFullname,
				CreateTime = DateTime.Now
			});

            var result = await _context.SaveChangesAsync();

            return result > 0;
		}

		public async Task<bool> SetUpdateQuestionsAnswerAsync(QuestionsAnswerViewModel model)
		{
			var result = 0;
			var items = await _context.SupporQuestionsAnswer.FindAsync(model.Id);

			if (items != null)
			{
				items.Content = (model.Content == "" || model.Content == null) ? "댓글 미 입력." : model.Content;

				result = await _context.SaveChangesAsync();
			}

			return result > 0;
		}

		#endregion


		#region File Service
		public async Task<bool> GetFileCheckAsync(int filemasterid, string filename)
		{

			var item = await (from p in _context.FileGroupList
							  where p.FileMasterId == filemasterid && p.FileName == filename
							  select p).ToListAsync();

			if(item.Count() > 0)
            {
				return false;
			}
			else
            {
				return true;
            }

		}

		public async Task<bool> SetFileSaveAsync(FileUploadViewModel model)
		{

			await _context.FileGroupList.AddAsync(new FileGroupList()
			{
			    FileMasterId = model.FileMasterId,
				FileName =model.FileName,
				DirectoryPath =model.DirectoryPath,
				FileSize = model.FileSize,
				CreateTime = DateTime.Now
			});

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		#endregion

	}
}
