using System;
using System.Collections.Generic;
using JDCloudSDK.Sms.Apis;
using JDCloudSDK.Sms.Client;
using Newtonsoft.Json;

namespace BangBangFuli.TextMessage.Utlity
{
    /// <summary>
    /// 京东智联云短信发送功能
    /// 参考https://docs.jdcloud.com/cn/text-message/dotnet
    /// </summary>
    public class JDClouldTextMsgHelper
    {

        /*
        * 发送短信
        */
        public void testBatchSend(SmsClient smsClient)
        {
            //3. 设置请求参数
            BatchSendRequest request = new BatchSendRequest();
            request.RegionId = "cn-north-1";
            // 设置模板ID 应用管理-文本短信-短信模板 页面可以查看模板ID
            request.TemplateId = "{{TemplateId}}";
            // 设置签名ID 应用管理-文本短信-短信签名 页面可以查看签名ID
            request.SignId = "qm_0571ace54ebf4b1dbdb2000b4d16dd4a";
            // 设置下发手机号list
            List<string> phoneList = new List<string>(){
                "13800138000"
                // ,
                // "phone number"
            };
            request.PhoneList = phoneList;
            // 设置模板参数，非必传，如果模板中包含变量请填写对应参数，否则变量信息将不做替换
            List<string> param = new List<string>(){
                "123456"
            };
            request.Params = param;

            //4. 执行请求
            var response = smsClient.BatchSend(request).Result;
            Console.WriteLine(JsonConvert.SerializeObject(response));
            Console.ReadLine();
        }

        /*
         * 获取状态报告
         */
        public void testStatusReport(SmsClient smsClient)
        {
            //3. 设置请求参数
            StatusReportRequest request = new StatusReportRequest();
            request.RegionId = "cn-north-1";

            // 设置要查询的手机号列表
            List<string> phoneList = new List<string>(){
                "13800013800"
                // ,
                // "phone number"
            };
            request.PhoneList = phoneList;
            // 设置序列号
            // 序列号从下发接口response中获取。response.getResult().getData().getSequenceNumber();
            request.SequenceNumber = "{{SequenceNumber}}";

            //4. 执行请求
            var response = smsClient.StatusReport(request).Result;
            Console.WriteLine(JsonConvert.SerializeObject(response));
            Console.ReadLine();
            // Result.Data.Status = 0, 成功
        }

        /*
         * 获取回复
         */
        public void testReply(SmsClient smsClient)
        {
            //3. 设置请求参数
            ReplyRequest request = new ReplyRequest();
            request.RegionId = "cn-north-1";
            // 设置应用ID 应用管理-文本短信-概览 页面可以查看应用ID
            request.AppId = "{{AppId}}";
            // 设置查询日期 时间格式：2019-09-01
            request.DataDate = "{{DataDate}}";
            // 设置要查询的手机号列表 非必传
            List<string> phoneList = new List<string>(){
                "13800013800"
                // ,
                // "phone number"
            };
            request.PhoneList = phoneList;

            //4. 执行请求
            var response = smsClient.Reply(request).Result;
            Console.WriteLine(JsonConvert.SerializeObject(response));
            Console.ReadLine();
        }


    }
}
