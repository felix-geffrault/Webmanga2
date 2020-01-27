using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  Webmanga.Models.Utilitaires;
using Webmanga.Models.Dao;
using Webmanga.Models.Metier;
using Webmanga.Models.MesExceptions;

namespace Webmanga.Controllers
{
    public class ConnexionController : Controller
    {
        // GET: Connexion
        public ActionResult Index()
        {
            return View("Connexion");
        }

        [HttpPost]
        public ActionResult Controle()
        {
            try
            {
                // on récupère les données du formulaire
                
                String login = Request["login"];
                String mdp = Request["pwd"];
                try
                {
                    ServiceUtilisateur unService = new ServiceUtilisateur();
                    Utilisateur unUtilisateur = unService.getUtilisateur(login);
                    
                    if (unUtilisateur != null)
                    {
                        try
                        {

                            String sel = unUtilisateur.Salt;
                            // on récupère le sel 
                            Byte[] salt = MonMotPassHash.transformeEnBytes(unUtilisateur.Salt);
                            // on génère le mot de passe 
                            Byte[] tempo = MonMotPassHash.PasswordHashe(mdp, salt);
                            if (MonMotPassHash.VerifyPassword(salt, mdp, tempo)) 
                            { 
                                Session["id"] = unUtilisateur.NumUtil;
                                Session["role"] = unUtilisateur.Role;
                                Session["Prenom"] = unUtilisateur.PrenomUtil;
                            }
                            else
                            {
                                ModelState.AddModelError("Erreur", "Erreur lors du contrôle  du mot de passe pour : " + login);
                                return RedirectToAction("Index", "Connexion");
                            }
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("Erreur", "Erreur lors du contrôle : " + e.Message);
                            return RedirectToAction("Index", "Connexion");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", "Erreur  login erroné : " + login);
                        return RedirectToAction("Index", "Connexion");
                    }
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                    return RedirectToAction("Index", "Connexion");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de l'authentification : " + e.Message);
                return RedirectToAction("Index", "Connexion");
            }
        }
        public ActionResult Deconnexion()
        {
            Session["id"] = null;
            Session["Prenom"] = null;
            Session["role"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}