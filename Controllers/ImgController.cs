using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imagem.Models;

namespace Imagem.Controllers
{
    public class ImgController : Controller
    { 

        private readonly Contexto _context;

        public ImgController(Contexto context)
        {
            _context = context;
        }

        // GET: Img
        public async Task<IActionResult> Index()
        {           
            var imagens = _context.Img;
            if(imagens != null)
            {
                imagens.ToListAsync().Wait();
                foreach (var item in imagens)
                {
                    string imageBase64Data = Convert.ToBase64String(inArray: item.ImagemTeste);
                    string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                    item.testeExibicao = imageDataURL;
                }
                return View(imagens);
            }
            else
            {
                return View();
            }
           
        }

        // GET: Img/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Img == null)
            {
                return NotFound();
            }

            var img = await _context.Img
                .FirstOrDefaultAsync(m => m.ImgId == id);
            if (img == null)
            {
                return NotFound();
            }

            return View(img);
        }

        // GET: Img/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Img/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImgId,DescricaoImg,ImagemTeste")] Img img)
        {           
           /*if (ModelState.IsValid)
            {
                _context.Add(img);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(img);*/
           foreach(var file in Request.Form.Files)
            {
                Img imagem = new Img();
                imagem.DescricaoImg = file.FileName;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                imagem.ImagemTeste = ms.ToArray();

                ms.Close();
                ms.Dispose();

                _context.Add(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(img);
        }

        // GET: Img/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Img == null)
            {
                return NotFound();
            }

            var img = await _context.Img.FindAsync(id);
            if (img == null)
            {
                return NotFound();
            }
            return View(img);
        }

        // POST: Img/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImgId,DescricaoImg,ImagemTeste")] Img img)
        {
            if (id != img.ImgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(img);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImgExists(img.ImgId))
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
            return View(img);
        }

        // GET: Img/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Img == null)
            {
                return NotFound();
            }

            var img = await _context.Img
                .FirstOrDefaultAsync(m => m.ImgId == id);
            if (img == null)
            {
                return NotFound();
            }

            return View(img);
        }

        // POST: Img/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Img == null)
            {
                return Problem("Entity set 'Contexto.Img'  is null.");
            }
            var img = await _context.Img.FindAsync(id);
            if (img != null)
            {
                _context.Img.Remove(img);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImgExists(int id)
        {
          return (_context.Img?.Any(e => e.ImgId == id)).GetValueOrDefault();
        }
    }
}
