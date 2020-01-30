using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webmanga.Models.MesExceptions;
using Webmanga.Models.Metier;
using Webmanga.Models.Dao;
using System.Globalization;

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
                mesMangas = ServiceManga.GetManga();
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

        public ActionResult Ajouter()
        {
            System.Data.DataTable genres = null;
            //System.Data.DataTable scenaristes = null;
            //System.Data.DataTable dessinateurs = null;
            try
            {
                genres = ServiceGenre.GetGenre();
                // dessinateurs = ServiceDessinateur.GetDessinateur();
                // scenaristes = ServiceScenariste.GetScenariste();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }

            return View(genres);
        }

        [HttpPost]
        public ActionResult Ajouter(FormCollection manga)
        {
            var nom_dessinateur = manga["Nom_dessinateur"].ToUpper();
            var nom_scenariste = manga["Nom_scenariste"].ToUpper();
            try
            {
                Scenariste s = ServiceScenariste.GetScenaristeByName(nom_scenariste);
                if (s.Id_scenariste == -1) //On teste si le nom du scénariste est dans la base de donné
                {
                    s.Nom_scenariste = nom_scenariste;
                    ServiceScenariste.AddScenariste(s);
                    s = ServiceScenariste.GetScenaristeByName(nom_scenariste);
                }

                Dessinateur d = ServiceDessinateur.GetDessinateurByName(nom_dessinateur); //Même chose pour le dessinateur
                if (d.Id_dessinateur == -1)
                {
                    d.Nom_dessinateur = nom_dessinateur;
                    ServiceDessinateur.AddDessinateur(d);
                    d = ServiceDessinateur.GetDessinateurByName(nom_dessinateur);
                }
                Manga unM = new Manga();
                unM.Id_dessinateur = d.Id_dessinateur;
                unM.Id_scenariste = s.Id_scenariste;
                String prix = manga["Prix"];
                prix = prix.Replace(".", ",");
                unM.Prix = Double.Parse(prix);
                unM.Titre = manga["Titre_manga"];
                unM.Couverture = manga["Couverture"];
                unM.Id_genre = int.Parse(manga["Id_genre"]);
                unM.DateParution = DateTime.Parse(manga["DateParution"]);
                ServiceManga.AddManga(unM);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Manga");
        }

        public ActionResult Augmenter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Augmenter(String ratio)
        {
            Double augm = (int) Double.Parse(ratio);
            augm = 1 + augm/100;
            ServiceManga.IncreasePrix(augm);
            return RedirectToAction("Index", "Manga");
        }
    }
}