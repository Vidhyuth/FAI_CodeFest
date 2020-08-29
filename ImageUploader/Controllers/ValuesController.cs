using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageUploader.Controllers
{
    public class ValuesController : ApiController
    {
        BusinessLogic.BusinessLogic businessLogic = new BusinessLogic.BusinessLogic();
        const string ServerLocation = "//snavndrsfint111.fastts.firstam.net/CodeFest5/Enzo Techoholics/enc/";
        [HttpPost]
        [Route("Values/SaveEncyptImg")]
        public async Task<IHttpActionResult> SaveEncyptImg()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var task = Task.Run(() =>
                {
                    file.SaveAs("C:\\Users\\vvvidhyuth\\Desktop\\CodeFest5.0\\ImageUploader\\ImageUploader\\UploadedImagesTemp\\" + file.FileName);
                    var saveFile = businessLogic.EncryptImageN_Save(file.FileName);
                });
                await task;

                // Can be used later to upload more files at once
                //for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                //{
                    
                //}

                return Ok();

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpGet]
        [Route("Values/DownloadImage")]
        public IHttpActionResult DownloadImage(string ImageName)
        {
            try
            {
                if (string.IsNullOrEmpty(ImageName))
                    throw new Exception("Empty Name");

                var fileBase64 = businessLogic.GetN_DecryptImage(ImageName);

                return Ok(fileBase64);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpGet]
        [Route("Values/TestImage")]
        public IHttpActionResult TestImage()
        {
            try
            {
                var saveFile = businessLogic.EncryptImageN_Save("Fig1");
                var fileDownloaded = businessLogic.GetN_DecryptImage("Fig1");

                return Ok();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Old Default Useless code
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}

        #endregion
    }
}
