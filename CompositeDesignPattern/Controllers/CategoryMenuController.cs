using CompositeDesignPattern.Composite;
using CompositeDesignPattern.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Web;

namespace CompositeDesignPattern.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryMenuController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var categories = await _applicationDbContext.Categories
                                                      .Include(x => x.Books)
                                                      .Where(x => x.UserId == userId)
                                                      .OrderBy(x => x.Id)
                                                      .ToListAsync();

            var topCategory = new Category { Id = 0, Name = "Top Category" };
            var topComposite = new BookComposite(0, "Top Menu");
            var menu = GetMenu(categories, topCategory, topComposite);

            ViewBag.menu = menu;

            ViewBag. selectList = menu.Components.SelectMany(x => ((BookComposite)x).GetSelectList());

            return View();
        }


        // category => bookcomposite
        // book => bookcomponent
        public BookComposite GetMenu(List<Category> categories, Category topCategory, BookComposite topBookComposite, BookComposite last = null)
        {
            categories.Where(category => category.ReferenceId == topCategory.Id).ToList().ForEach(category =>
            {
                var bookComposite = new BookComposite(category.Id, category.Name);

                category.Books.ToList().ForEach(book =>
                {
                    bookComposite.Add(new BookComponent(book.Id, book.Name));
                });

                if (last != null)
                    last.Add(bookComposite);
                else
                    topBookComposite.Add(bookComposite);

                GetMenu(categories, category, topBookComposite, bookComposite);
            });

            return topBookComposite;
        }
    }
}
