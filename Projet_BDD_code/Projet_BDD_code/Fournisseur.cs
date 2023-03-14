using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Fournisseur
    {
        int numSiret;
        string nom_fournisseur;
        int id_adresse;
        string libelle;
        string mail;

        //Constructeur 1 : création d'un nouveau fournisseur
        public Fournisseur(int numSiret, string nomFourni, int adresse, string libelle, string mail)
        {
            this.numSiret = numSiret;
            this.nom_fournisseur = nomFourni;
            this.id_adresse = adresse;
            this.libelle = libelle;
            this.mail = mail;
        }
        //Constructeur 2 : accès à un fournisseur existant depuis la base
        public Fournisseur(string nomf)
        {
            this.nom_fournisseur = nomf;
            #region Ouverture de connexion

            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax;" + "UID=root;PASSWORD=root";
                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ErreurConnexion : " + e.ToString());
                return;
            }
            #endregion

            #region Requete
            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM fournisseur where nom_fournisseur='" + nomf + "';";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                this.numSiret = (int)reader3["siret"];
                
                this.mail = (string)reader3["contact"];
                this.id_adresse = (int)reader3["id_adresse"];
                this.libelle = (string)reader3["statut"];
            }
            command3.Dispose();
            #endregion
        }

        public int NumSiret
        {
            get { return numSiret; }
        }

        public string NomFournisseur
        {
            get { return nom_fournisseur; }
            set { nom_fournisseur = value; }
        }


        public int IdAdresse
        {
            get { return id_adresse; }
            set { id_adresse = value; }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string ToString()
        {
            return (" SIRET : " + NumSiret.ToString() + "\n Nom : " + NomFournisseur.ToString() + "\n Libelle : " + Libelle.ToString() + "\n ID adresse : " + IdAdresse.ToString() + "\n Contact : " + Mail.ToString());
        }

        public void InsertionBDD()
        {
            #region Ouverture de connexion

            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax;" + "UID=root;PASSWORD=root";
                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ErreurConnexion : " + e.ToString());
                return;
            }
            #endregion

            #region Insertion
            string insertTable = "insert into velomax.fournisseur values (" + NumSiret + ",'" + NomFournisseur + "','" + Mail + "','" + Libelle +  "'," + IdAdresse + ")";
            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = insertTable;
            try
            {
                command2.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command2.Dispose();
            #endregion
        }
    }
}
