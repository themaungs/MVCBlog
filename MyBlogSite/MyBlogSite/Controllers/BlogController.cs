using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Models.DBContext;
using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Repositories;
using System.Web.Script.Serialization;
using System.Net.Http;
using MyBlogSite.ViewModels;
using MyBlogSite.Helpers;

namespace MyBlogSite.Controllers
{
    public class BlogController : Controller
    {

        private BlogHelper _blogHelper;
        private IBlogRepository _blogRepo;
        private IBlogCategoryRepository _blogCatRepo;
        public BlogController()
        {
            _blogHelper = new BlogHelper();
            _blogRepo = new BlogRepository(new MyBlogSiteDatabaseContext());
            _blogCatRepo = new BlogCategoryRepository(new MyBlogSiteDatabaseContext());
        }
        // GET: Blog
        public async Task<ActionResult> Index()
        {
            var resultList = _blogHelper.GetBlogs(User.Identity.IsAuthenticated);
            return View(resultList);
        }
        public async Task<ActionResult> BlogsList()
        {
            var resultList = _blogHelper.GetBlogs(User.Identity.IsAuthenticated);
            return PartialView("_BlogsList", resultList);
        }

        public async Task<ActionResult> Category(string id)
        {
            var resultList = _blogHelper.GetBlogs(User.Identity.IsAuthenticated).Where(b => b.BlogCategoryId.ToString() == id);
            return View(resultList);
        }

        public async Task<ActionResult> Read(string id)
        {
            var resultList = _blogHelper.GetBlogs(User.Identity.IsAuthenticated).Where(b => b.Title.ToString() == id).FirstOrDefault();
            if (resultList == null)
                return RedirectToAction("Index");
            else
                return View(resultList);
        }

        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public ActionResult CreateChange(string id)
        {
            Blog newBlog = new Blog();
            if (id != null)
            {
                newBlog = _blogRepo.GetBlogByGuid(id);
            }
            else
            {
                newBlog.CreatedBy = User.Identity.Name;
                newBlog.CreatedDate = DateTime.Now;
                newBlog.PublishDate = DateTime.Now.AddDays(1);
            }
            newBlog.LastUpdatedBy = User.Identity.Name;
            newBlog.LastUpdated = DateTime.Now;
            return View(newBlog);
        }

        // POST: Levels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public ActionResult CreateChange(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.LastUpdatedBy = User.Identity.Name;
                blog.LastUpdated = DateTime.Now;
                if (blog.BlogId == new Guid())
                {
                    blog.BlogId = Guid.NewGuid();
                    _blogRepo.Add(blog);
                }
                else
                {
                    _blogRepo.Update(blog);
                }
                _blogRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }
        [HttpGet]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public string GetCategories()
        {
            var categories = _blogCatRepo.GetAll().OrderBy(c=>c.CreatedDate);
            var serializer = new JavaScriptSerializer();
            var JsonData = serializer.Serialize(categories);
            return JsonData;
        }
        [HttpPost]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public string AddCategory(string Category)
        {
            BlogCategory newCategory = _blogCatRepo.GetAll().Where(c => c.Name.ToLower().Trim() == Category.ToLower().Trim()).FirstOrDefault();
            if (newCategory == null)
            {
                newCategory = new BlogCategory();
                newCategory.BlogCategoryId = Guid.NewGuid();
                newCategory.CreatedBy = User.Identity.Name;
                newCategory.CreatedDate = DateTime.Now;
                newCategory.LastUpdatedBy = User.Identity.Name;
                newCategory.LastUpdated = DateTime.Now;
                newCategory.Name = Category;
                _blogCatRepo.Add(newCategory);
                _blogCatRepo.SaveChanges();
            }
            return GetCategories();
        }

        //// POST: Blog/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(Guid id)
        //{
        //    Blog blog = await db.Blogs.FindAsync(id);
        //    db.Blogs.Remove(blog);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_blogRepo != null)
                {
                    _blogRepo = null;
                }
                if (_blogCatRepo != null)
                {
                    _blogCatRepo = null;
                }
                if(_blogHelper != null)
                {
                    _blogHelper = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
