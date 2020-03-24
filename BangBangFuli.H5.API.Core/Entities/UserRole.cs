using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BangBangFuli.H5.API.Core.Entities.Enumes;

namespace BangBangFuli.H5.API.Core.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class UserRole
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(32)]
        public string RoleName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StateEnum State { get; set; }
    }
}
