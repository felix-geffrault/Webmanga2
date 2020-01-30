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
    public class ServiceDessinateur
    {
        /// <summary>
        /// Renvoie tous les scénaristes présent dans la base de donné dans une collection.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDessinateur()
        {
            DataTable Dessinateur;
            Serreurs er = new Serreurs("Erreur sur lecture des Scenaristes.", "Scenariste.getScenariste()");
            try
            {
                String mysql = "Select  id_dessinateur, nom_dessinateur ";
                mysql += " from scenariste ";

                Dessinateur = DBInterface.Lecture(mysql, er);

                return Dessinateur;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static Dessinateur GetDessinateurByName(String nom_dessinateur)
        {
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture du Dessinateur.", "Dessinateur.GetDessinateurByName()");
            try
            {
                String mysql = "Select  * ";
                mysql += " from dessinateur WHERE nom_dessinateur = '" + nom_dessinateur +"'";
                dt = DBInterface.Lecture(mysql, er);
                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    Dessinateur d = new Dessinateur(int.Parse(dataRow[0].ToString()), dataRow[1].ToString(), dataRow[2].ToString());
                    return d;
                }
                else
                    return new Dessinateur(-1,"","");
               
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static void AddDessinateur(Dessinateur d)
        {
            Serreurs er = new Serreurs("Erreur sur l'ajout d'un dessinateur.", "ServiceDessinateur.AddDessinateur()");
            String requete = "INSERT INTO dessinateur ( nom_dessinateur, prenom_dessinateur ) VALUES ( '" + d.Nom_dessinateur + "' , '" + d.Prenom_dessinateur +"' )";
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