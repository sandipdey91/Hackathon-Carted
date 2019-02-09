using CartAPI.Algolia;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace CartAPI.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Test()
        {

            AlgSearch algSearch = new AlgSearch();

            algSearch.Search(new List<string>(new string[] { "pasta", "penne", "Rigatoni" }));

            //// Instantiates a client
            //var client = ImageAnnotatorClient.Create();
            //    // Load the image file into memory
            //    var image = Image.FromFile(@"C:\Users\kumar\OneDrive\Desktop\eb74754f-d053-46b6-9f94-8f78cb648fa3_1.48cb6d913d610799e169f42beca64ff2.png");
            //    // Performs label detection on the image file
            //    var response = client.DetectLabels(image);


            //foreach (var annotation in response)
            //    {
            //        if (annotation.Description != null)
            //            Console.WriteLine(annotation.Description);
            //    }

            return Ok();
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent())
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    int i = 0;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        String filePath = HostingEnvironment.MapPath("~/Images/");
                        //String[] headerValues = (String[])Request.Headers.GetValues("UniqueId");
                        String fileName = "a"+i++ + ".jpg";
                        String fullPath = Path.Combine(filePath, fileName);
                        image.Save(fullPath);
                    }
                });
                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }


        }
    }
}
