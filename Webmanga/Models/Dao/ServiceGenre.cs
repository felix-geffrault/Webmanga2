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
    public class ServiceGenre
    {
        /// <summary>
        /// Renvoie tous les genre présent dans la base de donné dans une collection.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGenre()
        {
            DataTable Genre;
            Serreurs er = new Serreurs("Erreur sur lecture des Genres.", "ServiceGenre.getGenre()");
            try
            {
                String mysql = "Select  id_genre, lib_genre ";
                mysql += " from genre ";

                Genre = DBInterface.Lecture(mysql, er);

                return Genre;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
    }
}