﻿using System;
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
using DPEMoveDAL.Helper;
using Microsoft.AspNetCore.Authorization;

namespace DPEMoveAdmin.Controllers
{
    [Authorize(Roles = "ADMIN_VIEW")]
    public class SportCenterController : Controller
    {
        private AppDbContext context;

        public SportCenterController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int? pageNumber)
        {
            var q = context.Department.Include(a => a.DepartmentPerson).OrderBy(a => a.DepartmentId).ToList();

            int pageSize = 10;
            var qq = PaginatedList<Department>.Create(q, pageNumber ?? 1, pageSize);

            return View(qq);
        }

        [HttpPost]
        public IActionResult Index(Department model)
        {
            var q = context.Department.Include(a => a.DepartmentPerson).OrderBy(a => a.DepartmentId).ToList();

            q = q.Where(a => a.Status == model.Status).ToList();

            if (!string.IsNullOrEmpty(model.Department1))
            {
                q = q.Where(a => !string.IsNullOrEmpty(a.Department1)).ToList();
                q = q.Where(a => a.Department1.Contains(model.Department1)).ToList();
            }
            if (!string.IsNullOrEmpty(model.Vistion))
            {
                q = q.Where(a => !string.IsNullOrEmpty(a.Vistion)).ToList();
                q = q.Where(a => a.Vistion.Contains(model.Vistion)).ToList();
            }

            int pageSize = 10;
            var qq = PaginatedList<Department>.Create(q, 1, pageSize);

            return View(qq);
        }


        public List<Province> GetProvinceList()
        {
            List<Province> province = context.Province.ToList();
            return province;
        }


        public ActionResult GetAmphur(int pid)
        {
            List<Amphur> amphurList = context.Amphur.Where(x => x.ProvinceId == pid).ToList();
            ViewBag.AmphurList = new SelectList(amphurList, "AmphurId", "AmphurName");
            return PartialView("DisplayAmphur");
        }

        public List<Amphur> GetAmphur2(string acode)
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

        public List<Tambon> GetTambon2(string tcode)
        {

            List<Tambon> tambon = context.Tambon.Where(x => x.TambonCode == tcode).ToList();
            return tambon;
        }


       public List<Province> GetProvinceList2(string pcode)
        {
            List<Province> province = context.Province.Where(a => a.ProvinceCode == pcode).ToList();
            return province;
        }

        public IActionResult CreateDep()
        {
            return View();
        }


        [HttpPost]
        // public async Task<IActionResult> Create(string productId, string productName, string productDesc)
        public  IActionResult CreateDep(Department p, AddressDropdownListClass ddl)
        {
            if (p != null && ddl != null)
            {
                var adr = new Address
                {
                    AmphurCode = ddl.AmphurCode,
                    ProvinceCode = ddl.ProvinceCode,
                    TambonCode = ddl.TambonCode,
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

                context.AddRangeAsync(adr);
                context.SaveChangesAsync();

                var q = new Department
                {
                    DepartmentCode = "D001",
                    Department1 = p.Department1,
                    Mission = p.Mission,
                    Vistion = p.Vistion,
                    DepartmentType = "1",
                    Email = ddl.Email,
                    Mobile = ddl.Mobile,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 0
                };

                Address adrId = null;

                adrId = context.Address.Where(a => a.AmphurCode == ddl.AmphurCode
                    && a.ProvinceCode == ddl.ProvinceCode
                    && a.TambonCode == ddl.TambonCode
                    && a.BuildingName == ddl.BuildingName
                    && a.No == ddl.No).FirstOrDefault();

                q.AddressId = adrId.AddressId;

                context.AddRangeAsync(q);
                context.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();  
        }


        public async Task<IActionResult> DepPersonCreate(int id)
        {
            List<DepartmentPerson> parentList = await context.DepartmentPerson.Where(a => a.DepartmentId == id).ToListAsync();
            
            var items = new List<SelectListItem>();

            foreach (var p in parentList)
            {
                items.Add(new SelectListItem()
                {
                    Text =  p.Firstname,
                    Value = p.DepartmentPersonId.ToString()
                }); ;
            }

            ViewBag.ParentList = items;
            //ViewBag.Departmentid = 
            return View(id);

        }

        [HttpPost]
        public IActionResult DepPersonCreate(DepartmentPerson dp)
        {

          
            int code = 2000;
            code = code + 1;
            var pcode = "P" + '_' + code;

            var depp = new DepartmentPerson
            {
                DepartmentPersonCode = pcode,
                DepartmentId = dp.DepartmentId,
                TitleCode = "1",
                Firstname = dp.Firstname,
                Lastname = dp.Lastname,
                ParentPersonId = dp.ParentPersonId,
                PositionName = dp.PositionName,
                Email = dp.Email,
                Mobile = dp.Mobile,
                Status = 1,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            context.AddRangeAsync(depp);
            context.SaveChangesAsync();
           
            return RedirectToAction("DepEdit", new { id = dp.DepartmentId });

        }


        public async Task<IActionResult> DepPersonEdit(int id)
        {
            var q = await context.DepartmentPerson.Where(a => a.DepartmentPersonId == id).FirstOrDefaultAsync();

            List<DepartmentPerson> parentList = context.DepartmentPerson.Where(a => a.DepartmentPersonId != id).Where(a => a.DepartmentId == q.DepartmentId).ToList();

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
            ViewBag.DepId = q.DepartmentId;

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
                return RedirectToAction("DepEdit", new { id = dp.DepartmentId });
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DepEdit(int id)
        {
            var q = await context.Department.Include(a => a.Address).Include(a => a.DepartmentPerson).Where(a => a.DepartmentId == id).FirstOrDefaultAsync();

            var adr = await context.Address.Where(a => a.AddressId == q.AddressId).FirstOrDefaultAsync();

            if(q.Address == null)
            {
                q.Address = new Address();
            }

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
                adr.AmphurCode = ddl.AmphurCode;
                adr.ProvinceCode = ddl.ProvinceCode;
                adr.TambonCode = ddl.TambonCode;


                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DepDelete(int Id)
        {
            var q = await context.Department.Include(a => a.Address).Include(a => a.DepartmentPerson).Where(a => a.DepartmentId == Id).FirstOrDefaultAsync();

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
                var dep = context.Department.Where(a => a.DepartmentId == p.DepartmentId).FirstOrDefault();
                var depperson = context.DepartmentPerson.Where(a => a.DepartmentId == p.DepartmentId); //.FirstOrDefault();
                var addr = context.Address.Where(a => a.AddressId == p.AddressId).FirstOrDefault();

                if (dep != null)
                {
                    if(depperson != null)
                    {
                        context.RemoveRange(depperson);
                    }
                    
                    if(addr != null)
                    {
                        context.Remove(addr);
                    }
                    
                    context.Remove(dep);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public async Task<IActionResult> DepPersonDelete(int Id)
        {
            var q = await context.DepartmentPerson.Where(a => a.DepartmentPersonId == Id).FirstOrDefaultAsync();

            if (q != null)
            {
                return View(q);
            }
            return View();
        }


        [HttpPost]
        public IActionResult DepPersonDelete(DepartmentPerson p)
        {
            if (ModelState.IsValid)
            {
                var dep = context.DepartmentPerson.Where(a => a.DepartmentPersonId == p.DepartmentPersonId).FirstOrDefault();

                if (dep != null)
                {
                    context.Remove(dep);
                    context.SaveChanges();

                    return RedirectToAction("DepEdit", new { id = dep.DepartmentId });
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