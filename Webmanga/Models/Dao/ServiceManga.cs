using System;
using System.Data;
using Webmanga.Models.MesExceptions;
using Webmanga.Models.Persistance;
using Webmanga.Models.Metier;
using Webmanga.Models.Utilitaires;

namespace Webmanga.Models.Dao
{
    public class ServiceManga
    {
        /// <summary>
        /// Fonction qui retourne une collection de données n'appartenant
        /// pas à la même table
        /// </summary>
        /// <returns></returns>
        public static DataTable GetManga()
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas.", "Manga.getManags()");
            try
            {
                String mysql = "Select  id_manga,lib_genre,nom_dessinateur,nom_scenariste,dateParution,prix,couverture ";
                mysql += " from Manga join genre on   manga.id_genre  = genre.id_genre ";
                mysql += " join   dessinateur  on  manga.id_dessinateur  =  dessinateur.id_dessinateur";
                mysql += " join   scenariste   on  manga.id_scenariste   = scenariste.id_scenariste ";

                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        /// <summary>
        /// Fonction qui retourne un manga
        /// </summary>
        /// <returns></returns>
        public static Manga GetunManga(String id)
        {
            DataTable dt;
            Manga unManga = null;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga.getunManag()");
            try
            {
                String mysql = "Select  id_manga,id_genre,id_dessinateur,id_scenariste,titre,dateParution,prix,couverture ";
                mysql += " from Manga ";
                mysql += " where id_manga = " + id;
                dt = DBInterface.Lecture(mysql, er);
                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    unManga = new Manga();
                    DataRow dataRow = dt.Rows[0];
                    unManga.Id_manga = int.Parse(dataRow[0].ToString());
                    unManga.Id_genre = int.Parse(dataRow[1].ToString());
                    unManga.Id_dessinateur = int.Parse(dataRow[2].ToString());
                    unManga.Id_scenariste = int.Parse(dataRow[3].ToString());
                    unManga.Titre = dataRow[4].ToString();
                    unManga.DateParution = DateTime.Parse(dataRow[5].ToString());
                    unManga.Prix = Double.Parse(dataRow[6].ToString());
                    unManga.Couverture = dataRow[7].ToString();
                    return unManga;
                }
                else
                    return null;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
        /// <summary>
        /// Fonction qui met à jour un manga
        /// </summary>
        /// <param name="unM"></param>
        public static void UpdateManga(Manga unM)
        {

            Serreurs er = new Serreurs("Erreur sur l'écriture d'un manga.", "ServiceManga.update()");
            String requete = "UPDATE Manga SET " +
                                  "id_scenariste = " + unM.Id_scenariste +
                                  ", id_dessinateur = " + unM.Id_dessinateur + "" +
                                  ", id_genre = " + +unM.Id_genre +
                                  ", titre = '" + unM.Titre + "'" +
                                   ", Prix = " + unM.Prix +
                                     ", dateParution = '" + FonctionsUtiles.DateToString(unM.DateParution) + "'" +
                                   ",couverture = '" + unM.Couverture + "'" +
                                   " WHERE id_manga =" + unM.Id_manga;
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }
        
        public static void DelManga(String id)
        {
            Serreurs er = new Serreurs("Erreur sur la suppression d'un manga.", "ServiceManga.DelManga()");
            String requete = "DELETE FROM Manga WHERE id_manga = " + id;
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }
        public static void AddManga(Manga unM)
        {
            Serreurs er = new Serreurs("Erreur sur l'ajout d'un manga.", "ServiceManga.AddManga()");
            String prix = unM.Prix.ToString().Replace(",", ".");
            String requete = "INSERT INTO manga (id_scenariste, id_dessinateur, id_genre, titre, Prix, dateParution, couverture) VALUES ( " +
                                  unM.Id_scenariste +
                                  ", " + unM.Id_dessinateur +
                                  ", '" + +unM.Id_genre + "'" +
                                  ", '" + unM.Titre + "'" +
                                  ", " + prix +
                                  ", '" + FonctionsUtiles.DateToString(unM.DateParution) + "'" +
                                  ", '" + unM.Couverture + "' )";
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }

        public static void IncreasePrix(Double augm)
        {
            String augmenter = augm.ToString().Replace(",", ".");
            try
            {
                DBInterface.Appele_AugmentationPrix(augm);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }
        public static DataTable SearchManga(string table_name, string critere, string recherche)
        {
            DataTable mesMangas;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas.", "Manga.getManags()");
            try
            {
                String mysql = "Select  id_manga,lib_genre,nom_dessinateur,nom_scenariste,dateParution,prix,couverture ";
                mysql += " from Manga join genre on   manga.id_genre  = genre.id_genre ";
                mysql += " join   dessinateur  on  manga.id_dessinateur  =  dessinateur.id_dessinateur";
                mysql += " join   scenariste   on  manga.id_scenariste   = scenariste.id_scenariste ";
                mysql += " where " + table_name + "." + critere + " = " + "'" + recherche + "'";
                mesMangas = DBInterface.Lecture(mysql, er);

                return mesMangas;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
    }
}