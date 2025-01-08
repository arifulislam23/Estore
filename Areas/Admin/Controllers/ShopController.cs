using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estore.Areas.Admin.Models;
using Estore.Data;
using Estore.Areas.Admin.ViewModel;

namespace Estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[Action]")]
    [Route("Admin/[Controller]")]
    public class ShopController : Controller
    {
        private readonly EstoreContext _context;

        public ShopController(EstoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Shop
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Admin/Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(ProductVM product)
        {

            //file upload start
            string uniqueFileName = null;
            if(product.UploadImage != null)
            { 
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload");
                uniqueFileName=Guid.NewGuid().ToString()+"_"+product.UploadImage.FileName;

                string filepath = Path.Combine(uploadsFolder, uniqueFileName);

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);

                }
                using (var filestrem = new FileStream(filepath,FileMode.Create))
                {
                    await product.UploadImage.CopyToAsync(filestrem);
                }

            }
            //file upload End

            var RealDataModel = new Product();

            RealDataModel.Name = product.Name;
            RealDataModel.Price = product.Price;
            RealDataModel.Description = product.Description;
            RealDataModel.Category = product.Category;
            RealDataModel.Image = uniqueFileName;


            _context.Product.Add(RealDataModel);
            _context.SaveChanges();

            return View(product);
        }

        // GET: Admin/Shop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Image,Price,LableId,IsActive,Category")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Admin/Shop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Labels()
        {

            var LabelList = _context.Label.ToList();

            return View(LabelList);
        }

        public IActionResult LabelCreate(Label datamodel)
        {
            _context.Label.Add(datamodel);

            try
            {
                _context.SaveChanges();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }

        }


        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
