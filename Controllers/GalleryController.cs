using Microsoft.AspNetCore.Mvc;
using MVC_Portfolio.Models;

namespace MVC_Portfolio.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public GalleryController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            string uploadsFolder =
                Path.Combine(_environment.WebRootPath, "uploads");

            var images = new List<GalleryImage>();

            if (Directory.Exists(uploadsFolder))
            {
                images = Directory.GetFiles(uploadsFolder)
                    .Select(file => new GalleryImage
                    {
                        FileName = Path.GetFileName(file),
                        ImagePath = "/uploads/" + Path.GetFileName(file)
                    })
                    .ToList();
            }

            return View(images);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uploadsFolder =
                    Path.Combine(_environment.WebRootPath, "uploads");

                Directory.CreateDirectory(uploadsFolder);

                string fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(imageFile.FileName);

                string filePath =
                    Path.Combine(uploadsFolder, fileName);

                using var stream =
                    new FileStream(filePath, FileMode.Create);

                await imageFile.CopyToAsync(stream);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}