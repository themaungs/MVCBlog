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

namespace MyBlogSite.Controllers
{
    public class PageDetailsController : Controller
    {
        private MyBlogSiteDatabaseContext db = new MyBlogSiteDatabaseContext();

        private IPageRepository _pageRepo;
        private IPageDetailRepository _pageDetailRepo;
        public PageDetailsController()
        {
            _pageRepo = new PageRepository(new MyBlogSiteDatabaseContext());
            _pageDetailRepo = new PageDetailRepository(new MyBlogSiteDatabaseContext());
        }

        // GET: PageDetails
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public async Task<ActionResult> PageDetailsList(Guid id)
        {
            var pageDetails = db.PageDetails.Include(p => p.Page).Where(p => p.PageId == id);
            ViewBag.Title = _pageRepo.GetPageByGuid(id.ToString()).Title;
            ViewBag.PageId = id;
            return View(await pageDetails.ToListAsync());
        }
        public ActionResult DetailListPartial(Guid id)
        {
            var pageDetails = db.PageDetails.Include(p => p.Page).Where(p => p.PageId == id);
            ViewBag.Title = _pageRepo.GetPageByGuid(id.ToString()).Title;
            ViewBag.PageId = id;
            return PartialView( "DetailListPartial",pageDetails.ToList());
        }


        [Authorize(Roles = "Super-User,Editor,Blogger")]
        // GET: PageDetails/Create
        public ActionResult CreateChange(string pageId, Guid? pageDetailId)
        {
            PageDetail newPageDetail = new PageDetail();
            if (pageDetailId != null)
            {
                newPageDetail = _pageDetailRepo.GetPageDetailByGuid(pageDetailId.ToString());
            }
            else
            {
                newPageDetail.PageId = new Guid(pageId);
                newPageDetail.CreatedBy = User.Identity.Name;
                newPageDetail.CreatedDate = DateTime.Now;
            }
            newPageDetail.LastUpdatedBy = User.Identity.Name;
            newPageDetail.LastUpdated = DateTime.Now;
            return View(newPageDetail);
        }

        // POST: Levels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public ActionResult CreateChange(PageDetail pageDetail)
        {
            if (ModelState.IsValid)
            {
                pageDetail.LastUpdatedBy = User.Identity.Name;
                pageDetail.LastUpdated = DateTime.Now;
                if (pageDetail.PageDetailId == new Guid())
                {
                    pageDetail.PageDetailId = Guid.NewGuid();
                    _pageDetailRepo.Add(pageDetail);
                }
                else
                {
                    _pageDetailRepo.Update(pageDetail);
                }
                _pageDetailRepo.SaveChanges();
                return RedirectToAction("PageDetailsList", new { Id = pageDetail.PageId });
            }

            return View(pageDetail);
        }


        // GET: PageDetails/Delete/5
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageDetail pageDetail = _pageDetailRepo.GetPageDetailByGuid(id.ToString());
            if (pageDetail == null)
            {
                return HttpNotFound();
            }
            return View(pageDetail);
        }

        // POST: PageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-User,Editor,Blogger")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            PageDetail pageDetail = _pageDetailRepo.GetPageDetailByGuid(id.ToString());
            _pageDetailRepo.Remove(id);
            _pageDetailRepo.SaveChanges();
            return RedirectToAction("PageDetailsList", new { Id = pageDetail.PageId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
