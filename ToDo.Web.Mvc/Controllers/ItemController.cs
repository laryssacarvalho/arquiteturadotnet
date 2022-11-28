using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers
{
    public class ItemController : Controller
    {
        protected IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(createItemModel.Description);
                await repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }  

            return View(createItemModel);
        }

        
        public async Task<IActionResult> Check(Guid id)
        {
            var item = await repository.GetByIdAsync(id);

            if (item is null)
                return NotFound();

            item.Mark(!item.Done);

            await repository.EditAsync(item);
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await repository.GetByIdAsync(id);

            if (item is null)
                return NotFound();

            item.Mark(!item.Done);

            await repository.DeleteByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
