using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webmanga.Models.MesExceptions;
using Webmanga.Models.Metier;
using Webmanga.Models.Persistance;

namespace Webmanga.Models.Dao
{
    public class ServiceScenariste
    {
        /// <summary>
        /// Renvoie tous les scénaristes présent dans la base de donné dans une collection.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetScenariste()
        {
            DataTable Scenariste;
            Serreurs er = new Serreurs("Erreur sur lecture des Scenaristes.", "Scenariste.getScenariste()");
            try
            {
                String mysql = "Select  id_scenariste, nom_scenariste ";
                mysql += " from scenariste ";

                Scenariste = DBInterface.Lecture(mysql, er);

                return Scenariste;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static void AddScenariste(Scenariste s)
        {
            Serreurs er = new Serreurs("Erreur sur l'ajout d'un scenariste.", "ServiceScenariste.AddScenariste()");
            String requete = "INSERT INTO scenariste VALUES ( " + s.Nom_scenariste +" , " + s.Prenom_scenariste;
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }
    }
}