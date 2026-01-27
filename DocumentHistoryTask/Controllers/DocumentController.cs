using DocumentHistoryTask.Data;
using DocumentHistoryTask.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocumentHistoryTask.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        //private readonly IStorageManager _storage;
        //private readonly IDocumentShareService _shareService;
        private readonly UserManager<User> _userManager;
        private readonly DocumentDbContext _context;

        public DocumentController(
            //IStorageManager storage,
            //IDocumentShareService shareService,
            UserManager<User> userManager,
            DocumentDbContext context)
        {
            //_storage = storage;
            //_shareService = shareService;
            _userManager = userManager;
            _context = context;
        }


        /// <summary>
        /// Список документов
        /// </summary>
        /// <param name="all">Отобразить все документы, или только актуальные</param>
        /// <returns></returns>
        public async Task<IActionResult> ListAsync(bool all = false)
        {

            User user = await _userManager.GetUserAsync(User) ?? throw new Exception("Не удалось распознать пользователся");
            List<Document> docs = new List<Document>();
            if (all)
            {
                docs = _context.Documents.Where(d => d.OwnerId == user.Id && !d.IsTemporary).ToList();
            }
            else
                docs = _context.Documents.Where(d => d.OwnerId == user.Id && d.IsDeleted == false && d.IsTemporary == false && d.ParentId == null).ToList();
            return View(docs);
        }
    }
}
