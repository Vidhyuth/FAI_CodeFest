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

        [HttpPost]
        [Route("Values/SaveEncyptImg")]
        public async Task<IHttpActionResult> SaveEncyptImg()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                var task = Task.Run(() =>
                {
                    file.SaveAs("../UploadedImagesTemp");
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
        [Route("Values/Image/{ImageName}")]
        public IHttpActionResult Image(string ImageName)
        {
            try
            {
                if (string.IsNullOrEmpty(ImageName))
                    throw new Exception("Empty Name");

                var fileDownloaded = businessLogic.getN_DecryptImage(ImageName);

                return Ok();
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
                var fileDownloaded = businessLogic.getN_DecryptImage("Fig1");

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
