using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_With_MongoDb.Models;
using MongoDB;
using MongoDB.Driver.Linq;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace MVC_With_MongoDb.Controllers
{
    public class EmployeController : Controller
    {
        MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["connectionString"]);

        public ActionResult Index(string Message="")
        {
            TempData["Message"] = Message;
            List<EmployesModel> Emp = new List<EmployesModel>();
            MongoDatabase myDB = server.GetDatabase("Employe");
            MongoCollection<EmployesModel> employes = myDB.GetCollection<EmployesModel>("employes");
            foreach (EmployesModel _Emp in employes.FindAll())
            {
                EmployesModel Em = new EmployesModel();
                Em.empname = _Emp.empname;
                Em.email = _Emp.email;
                Em._id = _Emp._id;
                Em.address = _Emp.address;
                Em.phon = _Emp.phon;
                Emp.Add(Em);
            }
            return View(Emp);
        }

        public ActionResult Details(string id)
        {
            MongoDatabase myDB = server.GetDatabase("Employe");
            var collection = myDB.GetCollection<EmployesModel>("employes");
            var query = from e in collection.AsQueryable<EmployesModel>() where e._id == ObjectId.Parse(id) select e;
            var _Emp = new EmployesModel();
            foreach (var Emp in query)
            {
                _Emp._id = Emp._id;
                _Emp.empname = Emp.empname;
                _Emp.email = Emp.email;
                _Emp.address = Emp.address;
                _Emp.phon = Emp.phon;
            }
            return View(_Emp);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployesModel Emp)
        {
            try
            {
                MongoDatabase myDB = server.GetDatabase("Employe");
                MongoCollection<EmployesModel> employes = myDB.GetCollection<EmployesModel>("employes");
                var _Emp = new EmployesModel();
                _Emp.empname = Emp.empname;
                _Emp.email = Emp.email;
                _Emp.address = Emp.address;
                _Emp.phon = Emp.phon;
                employes.Save(_Emp);
                string Message = "Employee Inserted Successfully.";
                return RedirectToAction("Index",new{Message=Message});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            MongoDatabase myDB = server.GetDatabase("Employe");
            var collection = myDB.GetCollection<EmployesModel>("employes");
            var query = from e in collection.AsQueryable<EmployesModel>() where e._id == ObjectId.Parse(id) select e;
            var _Emp = new EmployesModel();
            foreach (var Emp in query)
            {
                _Emp._id = Emp._id;
                _Emp.empname = Emp.empname;
                _Emp.email = Emp.email;
                _Emp.address = Emp.address;
                _Emp.phon = Emp.phon;
            }
            return View(_Emp);
        }

        [HttpPost]
        public ActionResult Edit(EmployesModel Model, string _id)
        {
            try
            {
                MongoDatabase myDB = server.GetDatabase("Employe");
                var collection = myDB.GetCollection<EmployesModel>("employes");
                EmployesModel _Emp = collection.FindOneById(ObjectId.Parse(_id));
                _Emp._id = ObjectId.Parse(_id);
                _Emp.empname = Model.empname;
                _Emp.email = Model.email;
                _Emp.address = Model.address;
                _Emp.phon = Model.phon;
                collection.Save(_Emp);
                string Message = "Employee Updated Successfully.";
                return RedirectToAction("Index",new{Message=Message});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            MongoDatabase myDB = server.GetDatabase("Employe");
            var collection = myDB.GetCollection<EmployesModel>("employes");
            var query = Query<EmployesModel>.EQ(e => e._id,ObjectId.Parse(id));
            collection.Remove(query);
            string Message = "Employee Deleted Successfully.";
            return RedirectToAction("Index",new{Message=Message});
        }

    }
}
