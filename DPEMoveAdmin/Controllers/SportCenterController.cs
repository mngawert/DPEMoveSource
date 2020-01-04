using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DPEMoveDAL.Models;
using DPEMoveAdmin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace DPEMoveAdmin.Controllers
{
    public class SportCenterController : Controller
    {
        private AppDbContext context;

        public SportCenterController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var q = context.Department.OrderBy(a => a.DepartmentId).ToList();

            return View(q);
        }

        

        public IActionResult CreateDep()
        {
            ViewBag.ProvinceList = new SelectList(GetProvinceList(), "ProvinceId", "ProvinceName");
            return View();
        }

        
        public ActionResult GetAmphur(int pid)
        {
            List<Amphur> amphurList = context.Amphur.Where(x => x.ProvinceId == pid).ToList();
            ViewBag.AmphurList = new SelectList(amphurList, "AmphurId", "AmphurName");
            return PartialView("DisplayAmphur");
        }

        public List<Amphur> GetAmphur(string acode)
        {

            List<Amphur> amphur = context.Amphur.Where(x => x.AmphurCode == acode).ToList();
            return amphur;
        }

        public ActionResult GetTambon(int aid)
        {
            List<Tambon> tambonList = context.Tambon.Where(x => x.AmphurId == aid).ToList();
            ViewBag.TambonList = new SelectList(tambonList, "TambonId", "TambonName");
            return PartialView("DisplayTambon");
        }

        public List<Tambon> GetTambon(string tcode)
        {

            List<Tambon> tambon = context.Tambon.Where(x => x.TambonCode == tcode).ToList();
            return tambon;
        }

        public List<Province> GetProvinceList()
        {
            List<Province> province = context.Province.ToList();
            return province;
        }

        public List<Province> GetProvinceList(string pcode)
        {
            List<Province> province = context.Province.Where(a => a.ProvinceCode == pcode).ToList();
            return province;
        }

        [HttpPost]
        // public async Task<IActionResult> Create(string productId, string productName, string productDesc)
        public  async Task<IActionResult> CreateDep(Department p, AddressDropdownListClass ddl)
        {

            var provCode = context.Province.Where(a => a.ProvinceId == ddl.ProvinceId).FirstOrDefault();
            var amphurCode = context.Amphur.Where(a => a.AmphurId == ddl.AmphurId).FirstOrDefault();
            var tambonCode = context.Tambon.Where(a => a.TambonId == ddl.TambonId).FirstOrDefault();

            var depID = context.Department.Max(a => a.DepartmentId);

            if (ModelState.IsValid)
            {
                var adr = new Address
                {
                    AmphurCode = amphurCode.AmphurCode,
                    ProvinceCode = provCode.ProvinceCode,
                    TambonCode = tambonCode.TambonCode,
                    BuildingName = ddl.BuildingName,
                    HousePropertyName = ddl.HousePropertyName,
                    Moo = ddl.Moo,
                    No = ddl.No,
                    Road = ddl.Road,
                    Soi = ddl.Soi,
                    Postcode = ddl.Postcode,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0

                };

                await context.AddRangeAsync(adr);
                await context.SaveChangesAsync();

                var adrId = context.Address.Where(a => a.AmphurCode == amphurCode.AmphurCode 
                && a.ProvinceCode == provCode.ProvinceCode 
                && a.TambonCode == tambonCode.TambonCode).FirstOrDefault();

                var q = new Department
                {
                    DepartmentCode = "D001",
                    Department1 = p.Department1,
                    Mission = p.Mission,
                    Vistion = p.Vistion,
                    DepartmentType = "1",
                    Email = ddl.Email,
                    Mobile = ddl.Mobile,
                    AddressId = adrId.AddressId,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0
                };
                await context.AddRangeAsync(q);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();  
        }


        public async Task<IActionResult> DepPersonEdit(int id)
        {
            var q = await context.DepartmentPerson.Where(a => a.DepartmentPersonId == id).FirstOrDefaultAsync();

            List<DepartmentPerson> parentList = context.DepartmentPerson.Where(a => a.DepartmentPersonId != id) .ToList();
            //ViewBag.ParentList = new SelectList(parentList, "DepartmentPersonId", "Firstname");

            var items = new List<SelectListItem>();

            foreach (var p in parentList)
            {
                items.Add(new SelectListItem() 
                {  
                    Text = p.Firstname,
                    Value = p.DepartmentPersonId.ToString()
                });;
            }

            ViewBag.ParentList2 = items;

            return View(q);
        }

        [HttpPost]
        public IActionResult DepPersonEdit(DepartmentPerson dp)
        {
            var q = context.DepartmentPerson.Where(a => a.DepartmentPersonId == dp.DepartmentPersonId).FirstOrDefault();

            if (q != null)
            {
                q.Firstname = dp.Firstname;
                q.Lastname = dp.Lastname;
                q.Email = dp.Email;
                q.Mobile = dp.Mobile;
                q.PositionName = dp.PositionName;
                q.UpdatedDate = DateTime.Now;
                q.UpdatedBy = 1;
                q.ParentPersonId = dp.ParentPersonId;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DepEdit(int id)
        {
            var q = await context.Department.Include(a => a.Address).Include(a => a.DepartmentPerson).Where(a => a.DepartmentId == id).FirstOrDefaultAsync();
            
            ViewBag.ProvinceList = new SelectList(GetProvinceList(q.Address.ProvinceCode), "ProvinceId", "ProvinceName");
            ViewBag.Ammphur = new SelectList(GetAmphur(q.Address.AmphurCode), "AmphurId", "AmphurName");
            ViewBag.Tambon = new SelectList(GetTambon(q.Address.TambonCode), "TambonId", "TambonName");

            if (q != null)
            {
                   return View(q);
            }
            return View();
        }


        [HttpPost]
        public IActionResult DepEdit(Department d, AddressDropdownListClass ddl)
        {
            // String timeStamp = GetTimestamp(DateTime.Now);

            var dep = context.Department.Where(a => a.DepartmentId == d.DepartmentId).FirstOrDefault();
            var adr = context.Address.Where(a => a.AddressId == dep.AddressId).FirstOrDefault();

            if (dep != null && adr != null)
            {
                dep.Department1 = d.Department1;
                dep.Vistion = d.Vistion;
                dep.Mission = d.Mission;
                dep.Email = ddl.Email;
                dep.Mobile = ddl.Mobile;
                dep.UpdatedDate = DateTime.Now;

                adr.BuildingName = ddl.BuildingName;
                adr.HousePropertyName = ddl.HousePropertyName;
                adr.Moo = ddl.Moo;
                adr.No = ddl.No;
                adr.Road = ddl.Road;
                adr.Soi = ddl.Soi;
                adr.Postcode = ddl.Postcode;


                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DepDelete(int Id)
        {
            var q = await context.Department.FindAsync(Id);

            if (q != null)
            {
                return View(q);
            }
            return View();
        }


        [HttpPost]
        public IActionResult DepDelete(Department p)
        {
            if (ModelState.IsValid)
            {
                var q = context.Department.Where(a => a.DepartmentId == p.DepartmentId).FirstOrDefault();

                if (q != null)
                {
                    context.Remove(q);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

    

        //  public ActionResult GetChart()
        //  {
        // List<object> chartData = new List<object>();
        //  chartData.Add(new object[]{
        //     "EmployeeId", "Name", "Designation", "ReportingManager", "PhotoPath"
        //  });

        //   var q  = context.
        //  var result = (from emp EmployeesHierarchies.AsEnumerable()
        //                select new
        //                {
        //                    EmployeeId = emp.EmployeeId,
        //                   Name = emp.Name,
        //                   Designation = emp.Designation,
        //                   ReportingManager = emp.ReportingManager,
        //                   PhotoPath = emp.PhotoPath,
        //               }).Distinct().ToList();

        /*
                  foreach (var c in result)
                  {
                      chartData.Add(new object[]
                          {
                             c.EmployeeId, c.Name,c.Designation, c.ReportingManager,c.PhotoPath
                          });
                  }  */

        //         return new JsonResult
        //        {
        //              Data = new
        //              {
        //                   success = chartData,
        //                  message = "Success",
        //              },
        //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //         };  

        // } 

    }
}