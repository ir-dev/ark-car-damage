using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;

namespace Ark.CarDamage.Detect.v1.Web.Api
{
    [Route("api/predict")]
    [ApiController]
    public class PredctController : ControllerBase
    {
        [HttpPost]
        [Route("v1/cd")]
        public async Task<dynamic> Index()
        {
            if (Request.Form.Files.Count == 0) return new { error = true, msg = "no file found" };
            try
            {
                var fn = Ark.CarDamage.Detect.v1.ArkYoloManager.unique_file_name() + System.IO.Path.GetExtension(Request.Form.Files[0].FileName);
                var fd = System.IO.Path.Combine(System.Environment.CurrentDirectory, "wwwroot", "detect");
                if (!Directory.Exists(fd)) Directory.CreateDirectory(fd);
                var ffn = System.IO.Path.Combine(fd, fn);
                using (FileStream fss = new FileStream(ffn, FileMode.Create))
                    Request.Form.Files[0].CopyTo(fss);
                var res = await Ark.CarDamage.Detect.v1.ArkYoloManager.Predict(ffn);
                return new
                {
                    data = res,
                    url = $"/detect/segment/{fn}",
                    error = false,
                    msg = "predicted success"
                };
            }
            catch (Exception ex)
            {
                return new { error = true, msg = $"{ex.Message}" };
            }
        }
    }

}