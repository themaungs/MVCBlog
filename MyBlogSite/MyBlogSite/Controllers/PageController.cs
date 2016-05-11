using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models;
using MyBlogSite.Domain.Models.DBContext;
using MyBlogSite.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyBlogSite.Controllers
{
    public class PageController : Controller
    {

        private IPageRepository _pageRepo;
        public PageController()
        {
            _pageRepo = new PageRepository(new MyBlogSiteDatabaseContext());
        }
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        // GET: Page
        public ActionResult PagesList()
        {
            var resultList = _pageRepo.GetAll();
            return View(resultList);
        }
        public ActionResult Index(string id)
        {
            var resultList = _pageRepo.GetPageByTitle(id);
            return View(resultList);
        }
        public ActionResult Pages()
        {
            var resultList = _pageRepo.GetAll();
            return PartialView("_Pages", resultList);
        }

        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public ActionResult CreateChange(string id)
        {
            Page newPage = new Page();
            if (id != null)
            {
                newPage = _pageRepo.GetPageByGuid(id);
            }
            else
            {
                newPage.CreatedBy = User.Identity.Name;
                newPage.CreatedDate = DateTime.Now;
            }
            newPage.LastUpdatedBy = User.Identity.Name;
            newPage.LastUpdated = DateTime.Now;
            return View(newPage);
        }

        // POST: Levels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public ActionResult CreateChange(Page page)
        {
            if (ModelState.IsValid)
            {
                page.LastUpdatedBy = User.Identity.Name;
                page.LastUpdated = DateTime.Now;
                if (page.PageId == new Guid())
                {
                    page.PageId = Guid.NewGuid();
                    _pageRepo.Add(page);
                }
                else
                {
                    _pageRepo.Update(page);
                }
                _pageRepo.SaveChanges();
                return RedirectToAction("PagesList");
            }

            return View(page);
        }
        // GET: PageDetails/Delete/5
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            Page page = new Page();
            if (id != null)
            {
                page = _pageRepo.GetPageByGuid(id.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: PageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            _pageRepo.Remove(id);
            _pageRepo.SaveChanges();
            return RedirectToAction("PagesList");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_pageRepo != null)
                {
                    _pageRepo = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}