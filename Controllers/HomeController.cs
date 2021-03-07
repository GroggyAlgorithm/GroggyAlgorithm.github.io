using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroggyDesign.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}

		public ActionResult About() {
			ViewBag.Message = "About Me Page";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Contact page.";

			return View();
		}

		public ActionResult Projects() {
			ViewBag.Message = "Project Page";

			return View();
		}

		public ActionResult Resume() {
			ViewBag.Message = "Resume Page.";

			return View();
		}


	}
}