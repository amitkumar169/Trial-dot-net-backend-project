using System;                                       
using trial2.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;


namespace trial2.Controllers
{
    public class HomeController : Controller
    {
        amitEntities db = new amitEntities();
        ObjectParameter msg = new ObjectParameter("msg", typeof(string));

        public ActionResult Index(Index model)
        {
            model.productlist = db.Tbl_product.Take(3).ToList();
            model.Offerlist = db.Tbl_offer.Take(3).ToList();
            model.Bloglist = db.Tbl_blog.Take(3).ToList();
            model.Sliderlist = db.Tbl_slider.ToList();
            return View(model);
        }
        public ActionResult Products(Home_product model)
        {
            model.Productlist = db.Tbl_product.ToList();
            return View(model);
        }
        public ActionResult aboutus()
        {
            return View();
        }

        public ActionResult Contact(decimal? id) {
            Contact model = new Contact();
            var data = db.Tbl_contact.Where(x => x.id == id).FirstOrDefault();
            if (data != null)
            {
                model.Id = data.id;
                model.name = data.username;
                model.email = data.email;
                model.subject = data.subject;
                model.message = data.message;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if (model.Id > 0)
            {
                db.sp_update_contact(model.Id, model.name, model.email, model.subject, model.message, msg);
                string output = msg.Value.ToString();
                if (output == "Record Updated Successfully")
                {
                    TempData["Msg"] = "Data Updated Successfully!";
                    return RedirectToAction("Contact", "Home");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }

            else
            {
                db.Sp_Ins_Contact(model.name, model.email, model.subject, model.message, msg);
                string output = msg.Value.ToString();
                if (output == "Record Saved Successfully")
                {
                    TempData["Msg"] = "Data saved Successfully!";
                    return RedirectToAction("Contact", "Home");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }

            }

        }

        public ActionResult Blog(Home_blog model) {
            model.Bloglist = db.Tbl_blog.ToList();
            return View(model);
        }

        public ActionResult Offer(Home model)
        {
            model.Offerlist = db.Tbl_offer.ToList();
            return View(model);
        }
        public ActionResult Login()
        {
            Session["AdminId"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(login_1 model)

        {
            //here are everything mention using table like table name and column name and login_1 is model class of login
            var data=db.Tbl_login.Where(x => x.username == model.UserName && x.password == model.Password).FirstOrDefault();
            if (data != null)
            {
                Session["AdminId"] = data.id;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["ErrMsg"] = "Invalid username or password";
                return View(model);
            }
        }
    }
}