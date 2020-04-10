using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BangBangFuli.Common
{
    public class BigFileProcess
    {
        /// <summary>
        /// 大文件
        /// </summary>
        /// <param name="filename"></param>
        public void Process(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                int length = 5;
                int result = 0;
                do
                {
                    byte[] bytes = new byte[length];
                    result = stream.Read(bytes, 0, 5);
                    for (int i = 0; i < result; i++)
                    {
                        Console.WriteLine(bytes[i].ToString());
                    }
                }
                while (length == result);
            }
        }

        public static void BinarySerialize()
        {
            string fileName = Path.Combine("", "");
            using (Stream fStream = new FileStream(fileName, FileMode.Create,
                FileAccess.ReadWrite))
            {

            }
        }


        /// <summary>
        /// 大图片压缩
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        public static void CompressPercent(string oldPath, string newPath, int maxWidth, int maxHeight)
        {

        }

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        public static void ImageChangeBySize(string oldPath, string newPath, int maxWidth, int maxHeight)
        {

        }



    }
}
