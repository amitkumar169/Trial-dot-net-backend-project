using trial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;
using System.IO;

namespace trial2.Controllers
{
    public class AdminController : Controller
    {
        string strdate11 = System.DateTime.UtcNow.Day.ToString("00") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Year.ToString("0000");
        string strtime11 = System.DateTime.UtcNow.Hour.ToString("00") + System.DateTime.UtcNow.Minute.ToString("00") + System.DateTime.UtcNow.Second.ToString("00");
        string strdate12 = System.DateTime.Now.Day.ToString("00") + "/" + System.DateTime.Now.Month.ToString("00") + "/" + System.DateTime.Now.Year.ToString("0000");

        ObjectParameter Msg = new ObjectParameter("msg", typeof(string));
        amitEntities db = new amitEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult View_slider(Slider model)
        {
            
            if (Session["AdminId"] != null)
            {
                model.View_slider = db.Tbl_slider.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Deleteslider(decimal? id)
        {
            if (id != null)
            {
                db.sp_del_slider(id);
                return RedirectToAction("View_slider", "Admin");
            }
            return RedirectToAction("View_slider", "Admin");
        }
        public ActionResult View_product(Product model)
        {
            model.View_product = db.Tbl_product.ToList();
            return View(model);
        }
        public ActionResult Deleteproduct(decimal? id)
        {
            if (id != null)
            {
                db.sp_del_product(id);
                return RedirectToAction("View_product", "Admin");
            }
            return RedirectToAction("View_product", "Admin");
        }
        public ActionResult View_offer(Offer model)
        {
            model.View_offer = db.Tbl_offer.ToList();
            return View(model);
        }
        public ActionResult Deleteoffer(decimal? id)
        {
            if (id != null)
            {
                db.sp_del_offer(id);
                return RedirectToAction("View_offer", "Admin");
            }
            return RedirectToAction("View_offer", "Admin");
        }
        public ActionResult View_blog(Blog model)
        {
            model.View_blog = db.Tbl_blog.ToList();
            return View(model);
        }
        public ActionResult Deleteblog(decimal? id)
        {
            if (id != null)
            {
                db.sp_del_blog(id);
                return RedirectToAction("View_blog", "Admin");
            }
            return RedirectToAction("View_blog", "Admin");
        }
        public ActionResult Add_offer(decimal? id)
        {
            Offer model = new Offer();
            var data = db.Tbl_offer.Where(x => x.Product_id == id).FirstOrDefault();
            if (data != null)
            {
                model.Id = data.Product_id;
                model.title = data.Product_title;
                model.desc = data.Product_desc;
                model.image = data.Product_image;
                
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Add_offer(Offer model)
        {

            string photo = "";
            if (model.photo != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg", ".pdf" };
                var fileName = Path.GetFileName(model.photo.FileName);
                var ext = Path.GetExtension(model.photo.FileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = "Image_" + strdate11 + strtime11 + ".png";
                var path = Path.Combine(HttpContext.Server.MapPath("~/Data/"), myfile);
                model.photo.SaveAs(path);
                photo = myfile;
            }
            else
            {
                photo = model.image;
            }

            if (model.Id > 0)
            {
                db.sp_update_offer(model.Id, model.title, model.desc,photo , Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Updated Successfully")
                {
                    TempData["Msg"] = "Data Updated Successfully!";
                    return RedirectToAction("Add_offer", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
            else
            {
                db.Sp_Ins_Offer(model.title, model.desc, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Saved Successfully")
                {
                    TempData["Msg"] = "Data saved Successfully!";
                    return RedirectToAction("Add_offer", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
        }
        //public ActionResult Contact(decimal? id)
        //{
        //    Contact model = new Contact();
        //    var data = db.Tbl_contact.Where(x => x.id == id).FirstOrDefault();
        //    if (data != null)
        //    {
        //        model.Id = data.id;
        //        model.name = data.username;
        //        model.email = data.email;
        //        model.subject = data.subject;
        //        model.message = data.message;
        //}

        //    return View(model);
        //}

        public ActionResult ContactList(Contact model)
        {
            model.ContactList = db.Tbl_contact.ToList();
            return View(model);
        }

        public ActionResult DeleteContactList(decimal? id)
        {
            if (id != null)
            {
                db.sp_del_contact(id);
                return RedirectToAction("ContactList", "Admin");
            }
            return RedirectToAction("ContactList", "Admin");
        }

        public ActionResult Profile(login_1 model)
        {
            int Id = Convert.ToInt32(Session["AdminId"]);
            var data = db.Tbl_login.Where(x => x.id == Id).FirstOrDefault();
            if (data != null)
            {
                model.Id = data.id;
                model.UserName = data.username;
                model.Password = data.password;
                return View(model);
            }
            return View();
        }
        public ActionResult Add_product(decimal? id)   
        {
            Product model = new Product();
            var data = db.Tbl_product.Where(x => x.product_id == id).FirstOrDefault();
            if(data != null)
            {
                model.Id = data.product_id;
                model.title = data.product_title;
                model.desc = data.product_desc;
                model.price = (int)data.product_price;
                model.image = data.product_image;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Add_product(Product model)
        {
            string photo = "";
            if (model.photo != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg", ".pdf" };
                var fileName = Path.GetFileName(model.photo.FileName);
                var ext = Path.GetExtension(model.photo.FileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = "Image_" + strdate11 + strtime11 + ".png";
                var path = Path.Combine(HttpContext.Server.MapPath("~/Data/"), myfile);
                model.photo.SaveAs(path);
                photo = myfile;
            }
            else
            {
                photo = model.image;
            }
            if (model.Id > 0)
            {
                db.sp_update_Product(model.Id, model.title, model.desc, model.price, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Updated Successfully")
                {
                    TempData["Msg"] = "Data Updated Successfully!";
                    return RedirectToAction("Add_product", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
            else
            {
                db.Sp_Ins_Product(model.title, model.desc, model.price, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Saved Successfully")
                {
                    TempData["Msg"] = "Data saved Successfully!";
                    return RedirectToAction("Add_product", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
        }
        public ActionResult Add_blog(decimal? id)
        {
            Blog model = new Blog();
            var data = db.Tbl_blog.Where(x => x.blog_id == id).FirstOrDefault();
            if(data != null)
            {
                model.Id = data.blog_id;
                model.title = data.blog_title;
                model.desc = data.blog_desc;
                model.image = data.blog_image;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Add_blog(Blog model)
        {
            string photo = "";
            if (model.photo != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg", ".pdf" };
                var fileName = Path.GetFileName(model.photo.FileName);
                var ext = Path.GetExtension(model.photo.FileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = "Image_" + strdate11 + strtime11 + ".png";
                var path = Path.Combine(HttpContext.Server.MapPath("~/Data/"), myfile);
                model.photo.SaveAs(path);
                photo = myfile;
            }
            else
            {
                photo = model.image;
            }
            if (model.Id > 0)
            {
                db.sp_update_blog(model.Id, model.title, model.desc, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Updated Successfully")
                {
                    TempData["Msg"] = "Data Updated Successfully!";
                    return RedirectToAction("Add_blog", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
            else
            {
                db.Sp_Ins_Blog(model.title, model.desc, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Saved Successfully")
                {
                    TempData["Msg"] = "Data saved Successfully!";
                    return RedirectToAction("Add_blog", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
        }
        public ActionResult Slider()
        {
            return View();
        }

            [HttpPost]
        public ActionResult Slider(Slider model)
        {
            string photo = "";
            if (model.photo != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", ".jpeg", ".pdf" };
                var fileName = Path.GetFileName(model.photo.FileName);
                var ext = Path.GetExtension(model.photo.FileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string myfile = "Image_" + strdate11 + strtime11 + ".png";
                var path = Path.Combine(HttpContext.Server.MapPath("~/Data/"), myfile);
                model.photo.SaveAs(path);
                photo = myfile;
            }
            else
            {
                photo = model.image;
            }
            if (model.Id > 0)
            {
                db.sp_update_Slider(model.Id, photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Updated Successfully")
                {
                    TempData["Msg"] = "Data Updated Successfully!";
                    return RedirectToAction("Slider", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
            else
            {
                db.Sp_Ins_Slider(photo, Msg);
                string output = Msg.Value.ToString();
                if (output == "Record Saved Successfully")
                {
                    TempData["Msg"] = "Data saved Successfully!";
                    return RedirectToAction("Slider", "Admin");
                }
                else
                {
                    TempData["ErrMsg"] = "Something Went Wrong!";
                    return View(model);
                }
            }
            
        }
    }
}