using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BangBangFuli.H5.API.Core.Entities.Enumes;

namespace BangBangFuli.H5.API.Core.Entities
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(32)]
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        public string TelPhone { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public StateEnum State { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 月到账金额目标
        /// </summary>
        public double TargetAmt { get; set; }
    }

}
