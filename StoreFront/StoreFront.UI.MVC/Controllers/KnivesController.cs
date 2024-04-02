using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Utilities;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace StoreFront.UI.MVC.Controllers
{
    public class KnivesController : Controller
    {
        private readonly StorefrontContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public KnivesController(StorefrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Knives
        public async Task<IActionResult> Index()
        {
            var storefrontContext = _context.Knives.Include(k => k.Category).Include(k => k.KnifeBrand).Include(k => k.KnifeStatus);
            return View(await storefrontContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> TiledKnives()
        {
            var knives = _context.Knives.Where(p => !p.IsDiscontinued)
                .Include(p => p.Category)
                .Include(p => p.KnifeBrand)
                .Include(p => p.KnifeStatus);

            return View(await knives.ToListAsync());
        }

        // GET: Knives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives
                .Include(k => k.Category)
                .Include(k => k.KnifeBrand)
                .Include(k => k.KnifeStatus)
                .FirstOrDefaultAsync(m => m.KnifeId == id);
            if (knife == null)
            {
                return NotFound();
            }

            return View(knife);
        }

        // GET: Knives/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["KnifeBrandId"] = new SelectList(_context.KnifeBrands, "BrandId", "BrandName");
            ViewData["KnifeStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusId");
            return View();
        }

        // POST: Knives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KnifeId,KnifeBrandId,KnifeType,KnifePrice,KnifeStatusId,IsDiscontinued,CategoryId,KnifeImage,Image")] Knife knife)
        {
            if (ModelState.IsValid)
            {

                #region File Upload - CREATE

                //Check to see if a file was uploaded
                if (knife.Image != null)
                {
                    //Check the file type
                    //retrieve the extension of the uploaded file
                    string ext = Path.GetExtension(knife.Image.FileName);

                    //Create a list of valid extensions to check against
                    string[] validExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    //Verify the uploaded file has an extension matching one of the extensions in the list above
                    //AND verify the file size will work within our .NET app.
                    if (validExts.Contains(ext.ToLower()) && knife.Image.Length < 4_194_303)//underscores make the int more readable
                    {
                        //Generate a unique filename
                        knife.KnifeImage = Guid.NewGuid() + ext;

                        //Save the file to the web server (wwwroot/images foler)
                        //To access wwwroot, add a property to the controller for the _webHostEnvironment
                        //(see the top of this controller for the example)
                        //Retrieve the path to wwwroot
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        //make a variable for the full image path
                        string fullImagePath = webRootPath + "/images/";

                        //Create a MemoryStream to read the image into the server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await knife.Image.CopyToAsync(memoryStream);//transfer file from the POST action to server memory
                            using (var img = Image.FromStream(memoryStream))//relies on using statement for System.Drawing
                            {
                                //Now we send the Image to ImageUtility for resizing and thumbnail creation
                                //We need 5 parameters for the ImageUtility.Resize():
                                //1) (int) maximum image size
                                //2) (int) maximum thumbnail size
                                //3) (string) full path where the file will be saved
                                //4) (Image) the actual Image file
                                //5) (string) filename
                                int maxImageSize = 500;//size is set in pixels 
                                int maxThumbSize = 100;

                                //Below relies on the using statement for our UI layer .Utilities
                                ImageUtility.ResizeImage(fullImagePath, knife.KnifeImage, img, maxImageSize, maxThumbSize);
                                //myFile.Save("path/to/folder/", "fileName"); - how we save a NON-image
                            }
                        }
                    }
                }
                else
                {
                    //If no image was uploaded, assign a default filename
                    //We also need to add a default image and name it "noimage.png" then copy it to our wwwroot/images folder
                    knife.KnifeImage = "noimage.png";
                }


                #endregion

                _context.Add(knife);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", knife.CategoryId);
            ViewData["KnifeBrandId"] = new SelectList(_context.KnifeBrands, "BrandId", "BrandName", knife.KnifeBrandId);
            ViewData["KnifeStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusId", knife.KnifeStatusId);
            return View(knife);
        }

        // GET: Knives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives.FindAsync(id);
            if (knife == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", knife.CategoryId);
            ViewData["KnifeBrandId"] = new SelectList(_context.KnifeBrands, "BrandId", "BrandName", knife.KnifeBrandId);
            ViewData["KnifeStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusId", knife.KnifeStatusId);
            return View(knife);
        }

        // POST: Knives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KnifeId,KnifeBrandId,KnifeType,KnifePrice,KnifeStatusId,IsDiscontinued,CategoryId,KnifeImage,Image")] Knife knife)
        {
            #region File Upload - EDIT

            //retain the old image file name so we can delete if a new file was uploaded
            string oldImageName = knife.KnifeImage;

            //Check if the user uploaded a file
            if (knife.Image != null)
            {
                //get the file's extension
                string ext = Path.GetExtension(knife.Image.FileName);

                //list valid extensions
                string[] validExts = { ".jpg", ".jpeg", ".gif", ".png" };

                //check the file's extension against the list of valid extensions
                if (validExts.Contains(ext.ToLower()) && knife.Image.Length < 4_194_303)
                {
                    //generate a new unique file name
                    knife.KnifeImage = Guid.NewGuid() + ext;
                    //put together our file path to save the image
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string fullPath = webRootPath + "/images/";

                    //Delete the old image if one existed
                    if (oldImageName != "noimage.png")
                    {
                        ImageUtility.Delete(fullPath, oldImageName);
                    }

                    //Save the new image to our server (wwwroot/images/)
                    using (var memoryStream = new MemoryStream())
                    {
                        await knife.Image.CopyToAsync(memoryStream);
                        using (var img = Image.FromStream(memoryStream))
                        {
                            int maxImageSize = 500;
                            int maxThumbSize = 100;
                            ImageUtility.ResizeImage(fullPath, knife.KnifeImage, img, maxImageSize, maxThumbSize);
                        }
                    }
                }
            }

            #endregion


            if (id != knife.KnifeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knife);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnifeExists(knife.KnifeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", knife.CategoryId);
            ViewData["KnifeBrandId"] = new SelectList(_context.KnifeBrands, "BrandId", "BrandName", knife.KnifeBrandId);
            ViewData["KnifeStatusId"] = new SelectList(_context.ProductStatuses, "ProductStatusId", "ProductStatusId", knife.KnifeStatusId);
            return View(knife);
        }

        // GET: Knives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Knives == null)
            {
                return NotFound();
            }

            var knife = await _context.Knives
                .Include(k => k.Category)
                .Include(k => k.KnifeBrand)
                .Include(k => k.KnifeStatus)
                .FirstOrDefaultAsync(m => m.KnifeId == id);
            if (knife == null)
            {
                return NotFound();
            }

            return View(knife);
        }

        // POST: Knives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Knives == null)
            {
                return Problem("Entity set 'StorefrontContext.Knives'  is null.");
            }
            var knife = await _context.Knives.FindAsync(id);
            if (knife != null)
            {
                _context.Knives.Remove(knife);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnifeExists(int id)
        {
          return (_context.Knives?.Any(e => e.KnifeId == id)).GetValueOrDefault();
        }
    }
}
