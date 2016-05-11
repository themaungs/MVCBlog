using MyBlogSite.Domain.Interfaces;
using MyBlogSite.Domain.Models.DBContext;
using MyBlogSite.Domain.Repositories;
using MyBlogSite.Helpers;
using MyBlogSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogSite.Controllers
{
    public class HomeController : Controller
    {
        private BlogHelper _blogHelper;
        public HomeController()
        {
            _blogHelper = new BlogHelper();
        }
        public ActionResult Index()
        {
            //var configuration = new MyBlogSite.Domain.Migrations.Configuration();
            //var migrator = new DbMigrator(configuration);
            //migrator.Update();

            var resultList = _blogHelper.GetBlogs(User.Identity.IsAuthenticated);
            return View(resultList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}