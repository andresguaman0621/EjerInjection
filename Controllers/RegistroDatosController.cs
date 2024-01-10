using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EjerInjection.Data;
using EjerInjection.Models;
using EjerInjection.Services;

namespace EjerInjection.Controllers
{
    public class RegistroDatosController : Controller
    {
        private readonly EjerInjectionContext _context;
        private readonly IRegistroDatosService _registroDatosService;

        public RegistroDatosController(EjerInjectionContext context)
        {
            _context = context;
        }

        // GET: RegistroDatos
        public async Task<IActionResult> Index()
        {
              return _context.RegistroDatos != null ? 
                          View(await _context.RegistroDatos.ToListAsync()) :
                          Problem("Entity set 'EjerInjectionContext.RegistroDatos'  is null.");
        }

        // GET: RegistroDatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroDatos == null)
            {
                return NotFound();
            }

            var registroDatos = await _context.RegistroDatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroDatos == null)
            {
                return NotFound();
            }

            return View(registroDatos);
        }

        // GET: RegistroDatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistroDatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email")] RegistroDatos registroDatos, RegistroDatos datos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroDatos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


                _registroDatosService.RegistrarDatos(datos);
                return RedirectToAction("Index", "Home"); // Redirigir a la página principal u otra página después del registro
            }
            return View(registroDatos);
        }

        // GET: RegistroDatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroDatos == null)
            {
                return NotFound();
            }

            var registroDatos = await _context.RegistroDatos.FindAsync(id);
            if (registroDatos == null)
            {
                return NotFound();
            }
            return View(registroDatos);
        }

        // POST: RegistroDatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email")] RegistroDatos registroDatos)
        {
            if (id != registroDatos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroDatos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroDatosExists(registroDatos.Id))
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
            return View(registroDatos);
        }

        // GET: RegistroDatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroDatos == null)
            {
                return NotFound();
            }

            var registroDatos = await _context.RegistroDatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroDatos == null)
            {
                return NotFound();
            }

            return View(registroDatos);
        }

        // POST: RegistroDatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroDatos == null)
            {
                return Problem("Entity set 'EjerInjectionContext.RegistroDatos'  is null.");
            }
            var registroDatos = await _context.RegistroDatos.FindAsync(id);
            if (registroDatos != null)
            {
                _context.RegistroDatos.Remove(registroDatos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroDatosExists(int id)
        {
          return (_context.RegistroDatos?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [HttpPost]
        public IActionResult Registrar(RegistroDatos datos)
        {
            _registroDatosService.RegistrarDatos(datos);
            //return RedirectToAction("Index", "Home"); // Redirigir a la página principal u otra página después del registro
        }
    }
}
