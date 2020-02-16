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

namespace DPEMoveAdmin.Controllers
{
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
            ViewBag.ProvinceList = new SelectList(GetProvinceList(), "ProvinceId", "ProvinceName");
            return View();
        }


        [HttpPost]
        // public async Task<IActionResult> Create(string productId, string productName, string productDesc)
        public  IActionResult CreateDep(Department p, AddressDropdownListClass ddl)
        {
            if (p != null && ddl != null)
            {
                var provCode = context.Province.Where(a => a.ProvinceId == ddl.ProvinceId).FirstOrDefault();
                var amphurCode = context.Amphur.Where(a => a.AmphurId == ddl.AmphurId).FirstOrDefault();
                var tambonCode = context.Tambon.Where(a => a.TambonId == ddl.TambonId).FirstOrDefault();

                string strProvinceCode = (provCode!=null ? provCode.ProvinceCode : "");
                string strAmphurCode = (amphurCode != null ? amphurCode.AmphurCode : "");
                string strTambonCode = (tambonCode != null ? tambonCode.TambonCode : "");

                var adr = new Address
                {
                    AmphurCode = strAmphurCode,
                    ProvinceCode = strProvinceCode,
                    TambonCode = strTambonCode,
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

                var adrId = context.Address.Where(a => a.AmphurCode == amphurCode.AmphurCode 
                && a.ProvinceCode == provCode.ProvinceCode 
                && a.TambonCode == tambonCode.TambonCode
                && a.BuildingName == ddl.BuildingName
                && a.No == ddl.No).FirstOrDefault();

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
                context.AddRangeAsync(q);
                context.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View();  
        }


        public async Task<IActionResult> DepPersonCreate(int id)
        {
            List<DepartmentPerson> parentList = await context.DepartmentPerson.ToListAsync();
            
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

            List<DepartmentPerson> parentList = context.DepartmentPerson.Where(a => a.DepartmentPersonId != id) .ToList();

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

            ViewBag.ProvinceList = new SelectList(GetProvinceList2(adr.ProvinceCode), "ProvinceId", "ProvinceName");
            ViewBag.Ammphur = new SelectList(GetAmphur2(adr.AmphurCode), "AmphurId", "AmphurName");
            ViewBag.Tambon = new SelectList(GetTambon2(adr.TambonCode), "TambonId", "TambonName");

            /*
            List<Province> provinceList = context.Province.ToList();
            var pitems = new List<SelectListItem>();
            foreach (var p in provinceList)
            {
                pitems.Add(new SelectListItem()
                {
                    Text = p.ProvinceName,
                    Value = p.ProvinceId.ToString()
                }); ;
            }

            List<Amphur> amphurList = context.Amphur.ToList();
            var aitems = new List<SelectListItem>();
            foreach (var p in amphurList)
            {
                aitems.Add(new SelectListItem()
                {
                    Text = p.AmphurName,
                    Value = p.AmphurId.ToString()
                }); ;
            }

            List<Tambon> tambonList = context.Tambon.ToList();
            var titems = new List<SelectListItem>();
            foreach (var p in tambonList)
            {
                titems.Add(new SelectListItem()
                {
                    Text = p.TambonName,
                    Value = p.TambonId.ToString()
                }); ;
            }

            ViewBag.ProvinceList = pitems;
            ViewBag.Ammphur = aitems;
            ViewBag.Tambon = titems;
            */


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
                var depperson = context.DepartmentPerson.Where(a => a.DepartmentId == p.DepartmentId).FirstOrDefault();
                var addr = context.Address.Where(a => a.AddressId == p.AddressId).FirstOrDefault();

                if (dep != null)
                {
                    context.Remove(depperson);
                    context.Remove(addr);
                    context.Remove(dep);
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