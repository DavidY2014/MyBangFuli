using System;
using System.Collections.Generic;
using System.Text;

namespace Colipu.BasicData.API.Extension.Configuration.DBConfig
{
    public class BasicDatacfg
    {
        /// <summary>
        /// 默认配送价格
        /// </summary>
        public static int DefaultShipPrice
        {
            get
            {
                string str = "";
                if (string.IsNullOrWhiteSpace(str))
                {
                    return 10;
                }
                return Convert.ToInt32(str);
            }
        }

        /// <summary>
        /// 默认小B仓
        /// </summary>
        public static int DefaultSmallB2BWarehouseId
        {
            get
            {
                //string str = GetConfig("DefaultSmallB2BWarehouseId");
                string str = string.Empty;
                if (string.IsNullOrWhiteSpace(str))
                {
                    return 0;
                }
                return Convert.ToInt32(str);
            }
        }

        /// <summary>
        /// 默认分站编号
        /// </summary>
        public static int DefaultSiteId
        {
            get
            {
                //string str = !ColipuWeb.AliyunACM.ACMConfiguration.Disabled ? AcmConfig.SysConfig.DefaultSiteId : GetConfig("DefaultSiteId");
                string str = "";
                if (string.IsNullOrWhiteSpace(str))
                {
                    return 111;
                }
                return Convert.ToInt32(str);
            }
        }
    }
}
