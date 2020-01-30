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

        public ActionResult Rechercher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recherche(string choix, string input)
        {
            input = input.ToUpper();
            string table_name = "";
            string critere = "";
            if (choix == "titre") { 
                table_name = "manga";
                critere = "titre";
            }
            if (choix == "dessinateur") { 
                table_name = "dessinateur";
                critere = "nom_dessinateur";
            }
            if (choix == "genre")
            {
                table_name = "genre";
                critere = "lib_genre";
            }
            if (choix == "scenariste")
            {
                table_name = "scenariste";
                critere = "nom_scenariste";
            }
            System.Data.DataTable mesMangas = null;
            try
            {
                mesMangas = ServiceManga.SearchManga(table_name, critere, input);
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
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
        /*public ActionResult Ajouter()
        {
            Manga unManga = null;
            try
            {
                unManga = ServiceManga.Get();
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }*/


    }
}