using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webmanga.Models.MesExceptions;
using Webmanga.Models.Metier;
using Webmanga.Models.Dao;

namespace Webmanga.Controllers
{
    public class MangaController : Controller
    {
        // GET: Manga
        // GET: Client
        public ActionResult Index()
        {
            System.Data.DataTable mesMangas = null;

            try
            {
                mesMangas= ServiceManga.GetManga();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
            }

            return View(mesMangas);
        }

        // GET: Commande/Edit/5
        public ActionResult Modifier(string id)
        {
            Manga unManga = null;
            try
            {
                unManga = ServiceManga.GetunManga(id);
                return View(unManga);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Manga unM)
        {
            try
            {
                ServiceManga.UpdateManga(unM);
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Supprimer(String id)
        {
            try
            {
                ServiceManga.DelManga(id);
                return RedirectToAction("Index", "Manga");

            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Ajouter()
        {
            try
            {
                System.Data.DataTable genres = ServiceGenre.GetGenre();
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Ajouter(Manga unM){
        
        }

    }
}