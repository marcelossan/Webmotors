using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using back_end_Webmotors.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace back_end_Webmotors.Controllers
{
    public class AnuncioWebmotorsController : Controller
    {
        private readonly AnuncioswebmotorsContext _context;

        public AnuncioWebmotorsController(AnuncioswebmotorsContext context)
        {
            _context = context;
        }

        // GET: AnuncioWebmotors
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnuncioWebmotors.ToListAsync());
        }

        // GET: AnuncioWebmotors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncioWebmotors = await _context.AnuncioWebmotors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncioWebmotors == null)
            {
                return NotFound();
            }

            return View(anuncioWebmotors);
        }

        // GET: AnuncioWebmotors/Create
        public IActionResult Create()
        {
            List<MarcaCarro> listMarcasCarros = new List<MarcaCarro>();

            var requisicaoWeb = WebRequest.CreateHttp("http://desafioonline.webmotors.com.br/api/OnlineChallenge/Make");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                listMarcasCarros = JsonConvert.DeserializeObject<List<MarcaCarro>>(objResponse.ToString());
                //Console.WriteLine(marcaCarro.ID + " " + marcaCarro.Name);
                //Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }

            ViewBag.ListaMarcas = listMarcasCarros;
            return View();
        }

        [HttpGet]
        public List<MarcaCarro> GetMarcas() {
            List<MarcaCarro> listMarcasCarros = new List<MarcaCarro>();

            var requisicaoWeb = WebRequest.CreateHttp("http://desafioonline.webmotors.com.br/api/OnlineChallenge/Make");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                listMarcasCarros = JsonConvert.DeserializeObject<List<MarcaCarro>>(objResponse.ToString());
                //Console.WriteLine(marcaCarro.ID + " " + marcaCarro.Name);
                //Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }

            return listMarcasCarros;
        }

        [HttpGet]
        public string getModelos(int makeId)
        {
            List<ModeloCarro> listModeloCarro = new List<ModeloCarro>();

            var requisicaoWeb = WebRequest.CreateHttp("http://desafioonline.webmotors.com.br/api/OnlineChallenge/Model?MakeID=" + makeId);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                listModeloCarro = JsonConvert.DeserializeObject<List<ModeloCarro>>(objResponse.ToString());
                //Console.WriteLine(marcaCarro.ID + " " + marcaCarro.Name);
                //Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }

            string json = JsonConvert.SerializeObject(listModeloCarro);

            return json;
        }

        [HttpGet]
        public string getVersoes (int ModelId)
        {
            List<VersoesModeloCarro> listVersoesModelo = new List<VersoesModeloCarro>();

            var requisicaoWeb = WebRequest.CreateHttp("http://desafioonline.webmotors.com.br/api/OnlineChallenge/Version?ModelID=" + ModelId);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                listVersoesModelo = JsonConvert.DeserializeObject<List<VersoesModeloCarro>>(objResponse.ToString());
                //Console.WriteLine(marcaCarro.ID + " " + marcaCarro.Name);
                //Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }

            string json = JsonConvert.SerializeObject(listVersoesModelo);

            return json;

        }

        // POST: AnuncioWebmotors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,versao, Observacao, Ano, Quilometragem")] AnuncioWebmotors anuncioWebmotors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncioWebmotors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anuncioWebmotors);
        }

        // GET: AnuncioWebmotors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncioWebmotors = await _context.AnuncioWebmotors.FindAsync(id);
            if (anuncioWebmotors == null)
            {
                return NotFound();
            }
            return View(anuncioWebmotors);
        }

        // POST: AnuncioWebmotors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,versao, Observacao, Ano, Quilometragem")] AnuncioWebmotors anuncioWebmotors)
        {
            if (id != anuncioWebmotors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anuncioWebmotors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioWebmotorsExists(anuncioWebmotors.Id))
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
            return View(anuncioWebmotors);
        }

        // GET: AnuncioWebmotors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncioWebmotors = await _context.AnuncioWebmotors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncioWebmotors == null)
            {
                return NotFound();
            }

            return View(anuncioWebmotors);
        }

        // POST: AnuncioWebmotors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncioWebmotors = await _context.AnuncioWebmotors.FindAsync(id);
            _context.AnuncioWebmotors.Remove(anuncioWebmotors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnuncioWebmotorsExists(int id)
        {
            return _context.AnuncioWebmotors.Any(e => e.Id == id);
        }
    }
}
