using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using WebApi.Html5.Upload.Models;
using WebApi.Html5.Upload.Infrastructure;
using System.Collections.Specialized;
using System.Threading;
using System.Collections.ObjectModel;

namespace WebApi.Html5.Upload.Controllers
{
    //http://www.cnblogs.com/Kummy/p/3553799.html
    //http://www.cnblogs.com/ang/archive/2012/10/24/2634176.html
    //http://www.strathweb.com/2012/09/dealing-with-large-files-in-asp-net-web-api/
    //http://www.cnblogs.com/jacksonwj/p/3525247.html
    public class UploadingController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {
            string folderName = "uploads";
            string PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                //IEnumerable<FileDesc> fileInfos = null;
                await Request.Content.ReadAsMultipartAsync(streamProvider);
                var fileInfo = streamProvider.FileData.Select(i =>
                   {
                       var info = new FileInfo(i.LocalFileName);
                       return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
                   });

                return Request.CreateResponse(HttpStatusCode.OK, fileInfo);
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }
    }
}
