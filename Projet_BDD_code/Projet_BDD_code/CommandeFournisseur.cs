using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class CommandeFournisseur
    {
        int id_commande;
        int siret;
        string id_piece;
        DateTime dateLivraison;
        int quantite;

        public CommandeFournisseur(int numSiret, string numpiece, int quantite, DateTime dateLivraison) 
        {
            this.siret = numSiret;
            this.id_piece = numpiece;
            this.quantite = quantite;
            this.dateLivraison = dateLivraison;
            id_commande = MaxIdCommande() + 1;
        }
        public CommandeFournisseur(int id_commande)
        {

            this.id_commande = id_commande;
        }

        public int IdCommande
        {
            get { return id_commande; }
            set { id_commande = value; }
        }
        public int Siret
        {
            get { return siret; }
            set { siret = value; }
        }

        public string IdPiece
        {
            get { return id_piece; }
            set { id_piece = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set { quantite = value; }
        }

        public DateTime DateLivraison
        {
            get { return dateLivraison; }
            set { dateLivraison = value; }
        }

        //Accès à l'id de la dernière commande au fournisseur
        static int MaxIdCommande()
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

            #region Selection Max ID commande
            string requete = "SELECT max(id_appro) FROM approvisionner;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();
            int max = 0;
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                max = (int)reader["max(id_appro)"];
            }
            return max;
            #endregion
        }

        public string ToString()
        {
            return ("ID commande : " + IdCommande.ToString() + " ID piece : " + IdPiece.ToString() + " SIRET : " + Siret.ToString() + " Date : " + DateLivraison.ToString("yyyy-MM-dd") + " Quantité : " + Quantite);
        }



        //Ajout de la commande dans la base
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
            string requete;
            requete = "insert into approvisionner values (" + IdCommande + ",'" + IdPiece + "'," + Siret + ",'" + DateLivraison.ToString("yyyy-MM-dd") + "'," + Quantite  + ")";
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

        //Mise à jour du stock : addition de la quantité commandée
        public void MajSock()
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
            #region Selection
            string requete3 = "SELECT stock_piece FROM piece_rechange where num_piece='" + IdPiece + "';";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete3;

            MySqlDataReader reader3 = command1.ExecuteReader();

            string[] valueString3 = new string[reader3.FieldCount];
            int stock = 0;
            while (reader3.Read())
            {
                stock = (int)reader3["stock_piece"];
            }
            reader3.Close();
            command1.Dispose();
            #endregion

            int newStock = stock + Quantite;

            #region Ouverture de connexion

            MySqlConnection maConnexion2 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax;" + "UID=root;PASSWORD=root";
                maConnexion2 = new MySqlConnection(connexionString);
                maConnexion2.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ErreurConnexion : " + e.ToString());
                return;
            }
            #endregion

            #region Selection
            string insertTable = "update piece_rechange set stock_piece=" + newStock + " where num_piece = '" + IdPiece + "';";
            MySqlCommand command2 = maConnexion2.CreateCommand();
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
