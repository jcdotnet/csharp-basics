﻿using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class FileController : Controller
    {

        [Route("/files/{filename:minlength(3)}.{extension = txt}")]
        public IActionResult DownloadFile()
        {
            string? fileName = Convert.ToString(Request.RouteValues["filename"]);
            string? extension = Convert.ToString(Request.RouteValues["extension"]);
            return Content($"Requesting file: {fileName}.{extension}");
        }
    }
}
