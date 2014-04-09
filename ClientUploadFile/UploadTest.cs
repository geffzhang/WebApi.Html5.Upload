using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using System.IO;

namespace ClientUploadFile
{
    [TestClass]
    public class UploadTest
    {
        [TestMethod]
        public void TestHttpClientUpload()
        {
            for (int i = 0; i < 100; i++)
            {
                Test();
            }
            //string rets = Upload(files).Result;
            //Console.WriteLine(rets);
        }


        internal void Test()
        {
            string dir = @"E:\temp\近期采集日志\";
            string[] files = Directory.GetFiles(dir);
            foreach (var fileName in files)
            {
                Console.WriteLine(fileName);
                string ret = Upload(fileName).Result;
                Console.WriteLine(ret);
            }
        }

        public static Task<string> Upload(string files)
        {
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    //foreach (var fileName in files)
                    //{
                    FileInfo file = new FileInfo(files);
                    byte[] bytes = File.ReadAllBytes(files);
                        content.Add(new StreamContent(new MemoryStream(bytes)), file.Name, file.Name);
                    //}
                    using (
                       var message = client.PostAsync("http://localhost:3128/api/uploading", content))
                    {
                        var input = message.Result.Content.ReadAsStringAsync();
                        return input;
                    }
                }
            }
        }

        public static Task<string> Upload(string[] files)
        {
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    foreach (var fileName in files)
                    {
                        FileInfo file = new FileInfo(fileName);
                        byte[] bytes = File.ReadAllBytes(fileName);
                        content.Add(new StreamContent(new MemoryStream(bytes)), file.Name, file.Name);
                    }
                    using (
                       var message = client.PostAsync("http://localhost:3128/api/uploading", content))
                    {
                        var input = message.Result.Content.ReadAsStringAsync();
                        return input;
                    }
                }
            }
        }
    }
}
