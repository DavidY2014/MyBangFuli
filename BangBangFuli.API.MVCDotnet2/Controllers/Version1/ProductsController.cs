using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangBangFuli.API.MVCDotnet2.Models;
using BangBangFuli.H5.API.Application.Services.BasicDatas;
using BangBangFuli.H5.API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers.Version1
{
    public class ProductsController : BaseController
    {
        private IUserRoleJurisdictionService _userRoleJurisdictionService;
        private IProductInformationService _productInformationService;
        private IBatchInformationService _batchInformationService;
        public ProductsController(IUserRoleJurisdictionService userRoleJurisdictionService, IProductInformationService productInformationService,
            IBatchInformationService batchInformationService)
        {
            _userRoleJurisdictionService = userRoleJurisdictionService;
            _productInformationService = productInformationService;
            _batchInformationService = batchInformationService;
        }




    }
}