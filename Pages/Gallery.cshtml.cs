using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESM.Racing.Pages
{
    public class GalleryModel : PageModel
    {
        private IHostingEnvironment _env;

        public GalleryModel(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            // load images from gallery
            Gallery = GetImages("images/gallery");
        }

        public string[] Gallery { get; set; }

        private string[] GetImages(string path) {
            path = System.IO.Path.Combine(_env.WebRootPath, path);
            DirectoryInfo dir = new DirectoryInfo(path);

            var thumbnails = (from file in dir.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
            where (file.Name.EndsWith(".jpg") || file.Name.EndsWith(".png"))
            orderby file.CreationTime ascending
            select file.Name);

            return thumbnails.ToArray();
        }
    }
}
