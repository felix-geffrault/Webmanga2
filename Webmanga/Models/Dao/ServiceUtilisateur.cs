using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Webmanga.Models.MesExceptions;
using Webmanga.Models.Metier;
using Webmanga.Models.Persistance;

namespace Webmanga.Models.Dao
{
    public class ServiceUtilisateur
    {

        public Utilisateur getUtilisateur(String nom)
        {
            DataTable dt;
            Utilisateur unUti = null;
            String mysql = "SELECT numUtil, nomUtil, PrenomUtil, MotPasse, Salt, role FROM utilisateur ";
            mysql += "where nomUtil = " + "'" + nom + "'";
            Serreurs er = new Serreurs("Erreur sur recherche d'un utilisateur.", "Service.getUtilisateur");
            try
            {
                dt = DBInterface.Lecture(mysql, er);
                if(dt.IsInitialized && dt.Rows.Count > 0)
                {
                    unUti = new Utilisateur();
                    DataRow dataRow = dt.Rows[0];
                    unUti.NumUtil = Int16.Parse(dataRow[0].ToString());
                    unUti.NomUtil = dataRow[1].ToString();
                    unUti.PrenomUtil = dataRow[2].ToString();
                    unUti.MotPasse = dataRow[3].ToString();
                    unUti.Salt = dataRow[4].ToString();
                    unUti.Role = dataRow[5].ToString();
                }
                return unUti;
            }
            catch(MonException e)
            {
                throw e;
            }
            catch (Exception exc)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), exc.Message);
            }
        }
    }
}