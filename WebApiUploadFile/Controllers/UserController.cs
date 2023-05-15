using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUploadFile.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var result = await WriteFile(file);
            return Ok(result);
        }

        private async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = file.Name + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (Directory.Exists(filePath + fileName))
                {
                    Directory.Delete(filePath + fileName);
                }

                var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }
            catch (Exception ex)
            {

            }
            return Directory.GetCurrentDirectory();
        }
    }
}
