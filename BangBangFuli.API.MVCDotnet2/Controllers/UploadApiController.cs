using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangBangFuli.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BangBangFuli.API.MVCDotnet2.Controllers
{
    public class UploadApiController : Controller
    {
        private IHostingEnvironment hostingEnv;
        public UploadApiController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        [HttpPost]
        public async Task<IActionResult> uploadImage()
        {

            JsonResult<PicMsg> result = new JsonResult<PicMsg>();
            result.code = 1;
            result.msg = "";
            string url = string.Empty;
            // 定义允许上传的文件扩展名 
            const string fileTypes = "gif,jpg,jpeg,png,bmp";
            // 最大文件大小(200KB)
            const int maxSize = 505000;

            // 获取附带POST参数值
            for (var fileId = 0; fileId < Request.Form.Files.Count; fileId++)
            {
                var curFile = Request.Form.Files[fileId];
                if (curFile.Length < 1) { continue; }
                else if (curFile.Length > maxSize)
                {
                    result.msg = "上传文件中有图片大小超出500KB!";
                    return Json(result);
                }
                var fileExt = Path.GetExtension(curFile.FileName);
                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    result.msg = "上传文件中包含不支持文件格式!";
                    return Json(result);
                }
                else
                {
                    // 存储文件名
                    string FileName = DateTime.Now.ToString("yyyyMMddhhmmssff") + CreateRandomCode(8) + fileExt;

                    // 存储路径（绝对路径）
                    string virtualPath = Path.Combine(hostingEnv.WebRootPath, "img");
                    if (!Directory.Exists(virtualPath))
                    {
                        Directory.CreateDirectory(virtualPath);
                    }
                    string filepath = Path.Combine(virtualPath, FileName);
                    try
                    {
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            await curFile.CopyToAsync(fs);
                            fs.Flush();
                        }
                        result.code = 0;
                        result.msg = "OK";
                        result.data = new PicMsg();
                        result.data.src = "/img/" + FileName;
                        result.data.title = FileName;
                    }
                    catch (Exception exception)
                    {
                        result.msg = "上传失败:" + exception.Message;
                    }
                }
            }
            return Json(result);
        }


        #region 其他信息

        class PicMsg
        {
            public string src { get; set; }
            public string title { get; set; }
        }
        /// <summary>
        /// 生成指定长度的随机码。
        /// </summary>
        private string CreateRandomCode(int length)
        {
            string[] codes = new string[36] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StringBuilder randomCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                randomCode.Append(codes[rand.Next(codes.Length)]);
            }
            return randomCode.ToString();
        }


        #endregion


    }
}