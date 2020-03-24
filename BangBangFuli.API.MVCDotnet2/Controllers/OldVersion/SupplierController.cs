using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// 供应商列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<SupplierViewModel> supplierViewModels = new List<SupplierViewModel>();
            List<Supplier> suppliers = _supplierService.GetAll();
            foreach (var supplier in suppliers)
            {
                supplierViewModels.Add(new SupplierViewModel
                {
                    SupplierCode = supplier.Code,
                    SupplierName = supplier.SupplierName,
                    CreateTime = supplier.CreateTime
                });
            }

            return View(supplierViewModels);
        }


        /// <summary>
        /// 新建页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSave(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = new Supplier
                {
                    Code = model.SupplierCode,
                    SupplierName = model.SupplierName,
                    CreateTime = DateTime.Now
                };
                _supplierService.CreateNew(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public ActionResult Details()
        {
            return View();
        }


    }
}