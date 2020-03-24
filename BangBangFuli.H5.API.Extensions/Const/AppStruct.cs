using System;
using System.Collections.Generic;
using System.Text;

namespace Colipu.BasicData.API.Extension.Const
{
    public class AppStruct
    {
        /// <summary>
        /// 特殊商品类型		
        /// </summary>
        public struct SpecialType
        {
            /// <summary>
            /// 后台商品		
            /// </summary>
            public static readonly string Back = "B";
            /// <summary>
            /// 前后台商品		
            /// </summary>
            public static readonly string FrontAndBack = "F";
        }

        public struct ShowType
        {
            /// <summary>
            /// 不显示		
            /// </summary>
            public static readonly int NotShow = 2;
            /// <summary>
            /// 全部显示		
            /// </summary>
            public static readonly int AllShow = 1;
            /// <summary>
            /// 显示协议价商品		
            /// </summary>
            public static readonly int ContractItemShow = 0;
        }

        /// <summary>
        /// 协议优先级
        /// </summary>
        public struct PricePriority
        {
            /// <summary>
            /// 折扣价格优先		
            /// </summary>
            public static readonly string DiscountPrice = "D";
            /// <summary>
            /// 取最低价格		
            /// </summary>
            public static readonly string LowestPrice = "L";
            /// <summary>
            /// 专柜价格优先		
            /// </summary>
            public static readonly string ContractPrice = "P";
        }

        public struct StatusType
        {
            /// <summary>
            /// 有效		
            /// </summary>
            public static readonly string Active = "A";
            /// <summary>
            /// 无效		
            /// </summary>
            public static readonly string Passive = "P";
            /// <summary>
            /// 删除		
            /// </summary>
            public static readonly string Delete = "X";
        }

        public struct YNStatus
        {
            /// <summary>
            /// 否		
            /// </summary>
            public static readonly string No = "N";
            /// <summary>
            /// 是		
            /// </summary>
            public static readonly string Yes = "Y";
        }

        /// <summary>
        /// 价格协议明细类型		
        /// </summary>
        public struct ContractDetailType
        {
            /// <summary>
            /// 物料分类		
            /// </summary>
            public static readonly string Category = "C";
            /// <summary>
            /// 物料SKU		
            /// </summary>
            public static readonly string ProductSku = "P";
        }

        /// <summary>
        /// 状态		
        /// </summary>
        public struct ContractStatus
        {
            /// <summary>
            /// 已结束		
            /// </summary>
            public static readonly string Completed = "C";
            /// <summary>
            /// 已中断		
            /// </summary>
            public static readonly string Interrupted = "I";
            /// <summary>
            /// 已审核		
            /// </summary>
            public static readonly string Passed = "P";
            /// <summary>
            /// 运行中		
            /// </summary>
            public static readonly string Running = "R";
        }

        /// <summary>
        /// 价格协议策略		
        /// </summary>
        public struct ContractStrategy
        {
            /// <summary>
            /// 专柜价格优先		
            /// </summary>
            public static readonly string CounterPrice = "C";
            /// <summary>
            /// 取最低价格		
            /// </summary>
            public static readonly string MinPrice = "M";
        }
        /// <summary>
        /// 价格协议类型		
        /// </summary>
        public struct ContractType
        {
            /// <summary>
            /// 客户价套		
            /// </summary>
            public static readonly string Customer = "C";
            /// <summary>
            /// 集团价套		
            /// </summary>
            public static readonly string CustomerGroup = "G";
        }

        /// <summary>
        /// 默认分站编号
        /// </summary>
        public struct DefaultSiteId
        {
            /// <summary>
            /// 客户价套		
            /// </summary>
            public static readonly int B2BSiteId = 211;
            /// <summary>
            /// 集团价套		
            /// </summary>
            public static readonly int B2CSiteId = 111;
        }

        public struct AdType
        {
            /// <summary>
            /// 文字		
            /// </summary>
            public static readonly string Text = "T";
            /// <summary>
            /// 全部显示		
            /// </summary>
            public static readonly string Picture = "P";
            /// <summary>
            /// 显示协议价商品		
            /// </summary>
            public static readonly string Item = "I";
        }

        /// <summary>
        /// 支付类型		
        /// </summary>
        public struct PayClassStruct
        {
            /// <summary>
            /// 账期支付		
            /// </summary>
            public static readonly string AccountPeriodPayment = "A";
            /// <summary>
            /// 到付		
            /// </summary>
            public static readonly string CashOnDelivery = "C";
            /// <summary>
            /// 其它		
            /// </summary>
            public static readonly string Else = "E";
            /// <summary>
            /// 网上支付		
            /// </summary>
            public static readonly string OnlinePayment = "O";
        }

        public struct YNStruct
        {
            public static readonly string Yes = "Y";
            public static readonly string No = "N";
        }


    }
}
