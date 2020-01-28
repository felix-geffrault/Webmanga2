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

        public static Scenariste GetScenaristeByName(String nom_scenariste)
        {
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture des Scenaristes.", "Scenariste.getScenariste()");
            try
            {
                String mysql = "Select  * ";
                mysql += " from scenariste WHERE nom_scenariste = '" + nom_scenariste + "'";
                dt = DBInterface.Lecture(mysql, er);
                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    Scenariste s = new Scenariste(int.Parse(dataRow[0].ToString()), dataRow[1].ToString(), dataRow[2].ToString());
                    return s;
                }
                else
                    return new Scenariste(-1,"",""); //-1 spécifie que le scénariste n'est pas dans la base de donné
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static void AddScenariste(Scenariste s)
        {
            Serreurs er = new Serreurs("Erreur sur l'ajout d'un scenariste.", "ServiceScenariste.AddScenariste()");
            String requete = "INSERT INTO scenariste ( nom_scenariste, prenom_scenariste ) VALUES ( '" + s.Nom_scenariste +"' , '" + s.Prenom_scenariste + "' )";
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