using CartAPI.Algolia;
using CartAPI.Models;
using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CartAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Items")]
    public class ItemsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            // Instantiates a client
            //var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            //var image = Image.FromFile(@"C:\Users\kumar\OneDrive\Desktop\pasta.jpg");
            // Performs label detection on the image file
            //var response = client.DetectWebInformation(image);

            AlgSearch algSearch = new AlgSearch();

            //var result = algSearch.Search(response.WebEntities.Select(t=>t.Description).Take(10).ToList());

            var result = algSearch.Search("soda");

            Random random = new Random();

            result.ForEach(t => {
                t.ItemName = t.ItemName.Substring(0, Math.Min(t.ItemName.Length, 20));

                t.Price = random.Next(1, 20);
                int temp = random.Next(4);
                t.OnSale = temp == 1 || temp == 0 ? true : false;
            });

            return result.AsEnumerable<Item>();
        }

        [HttpGet]
        [Route("RandomItems")]
        public IEnumerable<Item> GetRandomItems()
        {

            // Instantiates a client
            //var client = ImageAnnotatorClient.Create();
            // Load the image file into memory
            //var image = Image.FromFile(@"C:\Users\kumar\OneDrive\Desktop\pasta.jpg");
            // Performs label detection on the image file
            //var response = client.DetectWebInformation(image);

            AlgSearch algSearch = new AlgSearch();

            var result = algSearch.RandomItems();
            //var result = algSearch.Search(response.WebEntities.Select(t=>t.Description).Take(10).ToList());

            //var result = algSearch.Search("pasta");

            Random random = new Random();

            result.ForEach(t => {
                t.ItemName = t.ItemName.Substring(0, Math.Min(t.ItemName.Length, 20));

                t.Price = random.Next(1,20);
                int temp = random.Next(4);
                t.OnSale = temp == 1 || temp==0 ? true : false;
            });

            return result.AsEnumerable<Item>();
        }

        [HttpPost]
        [Route("Test")]
        public HttpResponseMessage Post([FromBody]string raw)
        {
            //var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            //var binData = Convert.FromBase64String(base64Data);

            //using (var stream = new MemoryStream(binData))
            //{
            //    var pictureBox = new PictureBox
            //    {
            //        Image = new Bitmap(stream),
            //    };

            //    var form = new Form { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
            //    form.Controls.Add(pictureBox);
            //    Application.Run(form);
            //}

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
