using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Adresse
    {
        int id_adresse;
        string rue;
        string ville;
        int code_postal;
        string province;

        //Constructeur 1 : création d'une nouvelle adresse
        public Adresse(string rue, string ville, int code_postal, string province)

        {
            id_adresse = MaxIdAdresse()+1;
            this.rue =rue; 
            this.ville = ville ;
            this.code_postal = code_postal;
            this.province = province;
        }
        //Constructeur 2 : Récupération des données d'une adresse existante dans la base
        public Adresse(int idad)
        {
            this.id_adresse = idad;
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

            }
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM adresse where id_adresse=" + id_adresse + ";";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                this.rue = (string)reader3["rue"];
                this.ville = (string)reader3["ville"];
                this.code_postal = (int)reader3["code_postal"];
                this.province = (string)reader3["province"];

                
            }
            command3.Dispose();
        }


        public int IdAdresse
        {
            get { return id_adresse; }
        }

        public string Rue
        {
            get { return rue; }
           
        }

        public string Ville
        {
            get { return ville; }
        }
        public int CodePostal
        {
            get { return code_postal; }
        }
        public string Province
        {
            get { return province; }
        }



        //Récupère le dernier ID d'adresse créé
        static int MaxIdAdresse()
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

            }
            #endregion

            #region Selection Max ID adresse
            string requete = "SELECT max(id_adresse) FROM velomax.adresse;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();
            int max = 0;
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                max = (int)reader["max(id_adresse)"];
            }
            return max;
            #endregion
        }

        //Création d'une nouvelle adresse
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
            int Id = MaxIdAdresse() + 1;
            this.id_adresse = Id;
            string requete = "";           
            
            requete = "INSERT INTO adresse values (" + Id + ",'" + Rue + "','" + Ville + "'," + CodePostal + ",'" + Province+"');";
            Console.WriteLine(requete);
            

            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
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

 

        //Supprime l'adresse
        public void SuppressionBDD()
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

            string requete = "delete from adresse where id_adresse=" + id_adresse;


            #region Mise à jour
            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
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
