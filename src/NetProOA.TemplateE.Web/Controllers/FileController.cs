using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetProOA.Framework.Document;
using static System.Net.WebRequestMethods;

namespace NetProOA.TemplateE.Web.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Upload()
        {
            var documents = await _fileService.UploadToSystemDirectoryAsync(Request);
            return Json(new { Succeed = true, FileID = documents.First().FileId, FileName = documents.First().Name, Length = documents.First().Size });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Stream(string id)
        {
            var stream = await _fileService.DownloadStreamAsync(id);
            string encodeFilename = HttpUtility.UrlEncode(id, Encoding.GetEncoding("UTF-8"));
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + encodeFilename);
            return new FileStreamResult(stream, "application/octet-stream");
        }
    }
}