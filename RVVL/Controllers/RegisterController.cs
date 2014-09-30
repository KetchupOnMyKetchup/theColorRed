using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RVVL.Models;

namespace RVVL.Controllers
{
    /// <summary>
    /// Controls the HTTP POST and GET
    /// </summary>
    public class RegisterController : Controller
    {
        private VectorLearningEntities db = new VectorLearningEntities();

 
        /// <summary>
        /// GET: /Register/
        /// </summary>
        public ActionResult List()
        {
            return View(db.Clients.ToList());
        }


        /// <summary>
        /// Gets the details of the registration by the id. 
        /// GET: /Register/Details/5
        /// </summary>
        /// <param name="id">id of the registration</param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        /// <summary>
        /// GET: /Register/Create
        /// </summary>
        /// <param name="MarketingCampaignCode">If you enter a code into the following URL 
        /// http://localhost:1903/Register/Create?MarketingCampaignCode=123abc
        /// and remove 123abc, you can utilize the Marketing Campaign Code Query
        /// If it is not used, it will remain null which has been allowed in the database
        /// </param>
        public ActionResult Create(String MarketingCampaignCode = "")
        {
            return View();
        }


        /// <summary>
        /// Creating a new client. 
        /// POST: /Register/Create
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(client);
        }

        /// <summary>
        /// If client does not exist return HttpNotFound error 
        /// GET: /Register/Edit/5
        /// </summary>
        public ActionResult Edit(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        /// <summary>
        /// To edit a client. 
        /// POST: /Register/Edit/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(client);
        }

        /// <summary>
        /// If trying to delete a client that does not exist (null), throw an error. 
        /// GET: /Register/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        /// <summary>
        /// Action of deleting a client that has been entered into the database. 
        /// POST: /Register/Delete/5
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}