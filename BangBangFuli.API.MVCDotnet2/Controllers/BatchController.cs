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
    public class BatchController : Controller
    {
        private readonly IBatchInformationService _batchInformationService;

        public BatchController(IBatchInformationService batchInformationService)
        {
            _batchInformationService = batchInformationService;
        }
        // GET: Batch
        public ActionResult Index()
        {
            List<BatchViewModel> batchViewModels = new List<BatchViewModel>();
            List<BatchInformation> batchInfos = _batchInformationService.GetAll();
            foreach (var batch in batchInfos)
            {
                batchViewModels.Add(new BatchViewModel
                {
                    Id = batch.Id,
                    BatchId = batch.Id,
                    Name = batch.Name,
                    CreateTime = batch.CreateTime
                });
            }
            return View(batchViewModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateSave(BatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                BatchInformation batchInfo = new BatchInformation
                {
                    Id = model.BatchId,
                    Name = model.Name,
                    CreateTime = DateTime.Now,
                };
                _batchInformationService.CreateNew(batchInfo);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            BatchViewModel model = new BatchViewModel();
            if (id != null)
            {
                BatchInformation batchInfo = _batchInformationService.GetBatchInfoById((int)id);
                model = new BatchViewModel
                {
                    Id = batchInfo.Id,
                    Name = batchInfo.Name,
                    BatchId = batchInfo.Id
                };
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditSave(BatchViewModel model)
        {
            BatchInformation batchInfo = new BatchInformation
            {
                Id = model.Id,
                Name = model.Name
            };

            _batchInformationService.UpdateBatchInfo(batchInfo);

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            _batchInformationService.RemoveBatchById((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}