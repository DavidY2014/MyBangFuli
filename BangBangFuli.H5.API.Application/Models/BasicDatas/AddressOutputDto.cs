using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangBangFuli.H5.API.Application.Models.BasicDatas
{

    [Serializable]
    public  class AddressOutputDto : BaseOutputDto
    {
        public List<AddressOutputDto> CustomerAddresss { get; set; }

        public List<AddressOutputDto> CustomerAddresses { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        ///客户地址系统编号 
        /// </summary>
        public int SysNo { get; set; }
        /// <summary>
        /// 客户系统编号
        /// </summary>
        public int CustomerSysNo { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        public string Brief { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// 收货人传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 收货邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 收货地址系统编号
        /// </summary>
        public int AreaSysNo { get; set; }
        /// <summary>
        /// 是否默认(1 默认 0 非默认)
        /// </summary>
        public int IsDefault { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 收货人小灵通
        /// </summary>
        public string ReceivePASPhone { get; set; }
        /// <summary>
        /// 收货邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 外部部门编码
        /// </summary>
        public string DistributionCode { get; set; }
        /// <summary>
        /// 帐号系统编号(关联Operator表的SYSNO)
        /// </summary>
        public int OperatorID { get; set; }
        /// <summary>
        /// 状态(0 有效 -1 无效)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 公司系统编号
        /// </summary>
        public int CompanySysNo { get; set; }
        /// <summary>
        /// 仓库系统编号
        /// </summary>
        public int StockSysNo { get; set; }
        /// <summary>
        /// 经度(对应地址)
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 纬度(对应地址)
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 省系统编号
        /// </summary>
        public int ProvinceSysNo { get; set; }
        /// <summary>
        /// 市系统编号
        /// </summary>
        public int CitySysNo { get; set; }

        public string areaname { get; set; }

        /// <summary>
        /// 区系统编号
        /// </summary>
        public int DistrictSysNo { get; set; }

        public override object GetKey()
        {
            return this.SysNo;
        }
    }
}

