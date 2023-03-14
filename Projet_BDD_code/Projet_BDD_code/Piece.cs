using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Piece
    {

        string id_piece;
        int quantite;
        string nom_piece;
        string date_intro;
        string date_sortie;
        int prix_piece;

        //Constructeur 1 : Création d'une pièce en entrant toutes ses valeurs
        public Piece(string id_piece, string nom_piece, int prix_piece,  string date_intro, string date_sortie,  int quantite)

        {
            this.id_piece = id_piece;
            this.nom_piece = nom_piece;
            this.prix_piece = prix_piece;
            this.date_intro = date_intro;
            this.date_sortie = date_sortie;
            this.quantite = quantite;
        }
        //Constructeur 2 : création d'une pièce à partir de son id et une quantité demandée (cas achat/vente)
        public Piece(string id_piece, int quant)

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
            #region Requetes

            string requete = "select * from piece_rechange where num_piece='" + id_piece + "';";
            Console.WriteLine(requete);

            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
            MySqlDataReader reader2 = command2.ExecuteReader();
            string[] valueString2 = new string[reader2.FieldCount];
            while (reader2.Read())
            {
                this.id_piece = (string)reader2["num_piece"];
                this.nom_piece = (string)reader2["description_piece"];
                this.prix_piece = (int)reader2["prix_piece"];
                this.date_intro = Convert.ToString((DateTime)reader2["date_intro_piece"]);
                if (string.IsNullOrEmpty(reader2["date_disco_piece"].ToString()))
                {
                    this.date_sortie = "NULL";
                }
                else
                {
                    this.date_sortie = Convert.ToString((DateTime)reader2["date_disco_piece"]).Split(' ')[0];
                }

                this.quantite = quant;

            }
            reader2.Close();
            command2.Dispose();
            #endregion

        }

        //Constructeur 3 : accès aux données d'une pièce à partir de son id (cas vérif stock)
        public Piece(string id_piece)

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
            #region Requetes

            string requete = "select * from piece_rechange where num_piece='" + id_piece + "';";
           
            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
            MySqlDataReader reader2 = command2.ExecuteReader();
            string[] valueString2 = new string[reader2.FieldCount];
            while (reader2.Read())
            {
                this.id_piece = id_piece;
                this.nom_piece = (string)reader2["description_piece"];
                this.prix_piece = (int)reader2["prix_piece"];
                this.date_intro = Convert.ToString((DateTime)reader2["date_intro_piece"]);
                if (string.IsNullOrEmpty(reader2["date_disco_piece"].ToString()))
                {
                    this.date_sortie = "NULL";
                }
                else
                {
                    this.date_sortie = Convert.ToString((DateTime)reader2["date_disco_piece"]).Split(' ')[0];
                }

                this.quantite = (int)reader2["stock_piece"];

            }
            reader2.Close();
            command2.Dispose();
            #endregion

        }

        public string IdPiece
        {
            get { return id_piece; }
        }
        public string NomPiece
        {
            get { return nom_piece; }
        }
        public int PrixPiece
        {
            get { return prix_piece; }
        }
        public string DateIntro
        {
            get { return date_intro; }
        }
        public string DateSortie
        {
            get { return date_sortie; }
        }
        public int Quantite
        {
            get { return quantite; }
            set { quantite= value; }
        }

        public string ToString()
        {
            return(" ID : " + IdPiece.ToString() + "\n Nom : " + NomPiece.ToString() + "\n Prix : " + PrixPiece.ToString() + "\n Quantité : " + Quantite.ToString() + "\n Date Sortie : " + DateIntro.ToString() + "\n Date Fin : " + DateSortie.ToString());
        }

        //Ajout d'une pièce à la base
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

            #region Insertion dans la base
            string requete = "";
            if (date_sortie != null)
            {
                requete = "INSERT INTO piece_rechange VALUES ('" + IdPiece + "'," + Quantite + ",'" + DateIntro + "','" + DateSortie + "'," + PrixPiece + ",'" + NomPiece + "');";
            }
            else
            {
                requete = "INSERT INTO piece_rechange VALUES ('" + IdPiece + "'," + Quantite + ",'" + DateIntro + "',NULL," + PrixPiece + ",'" + NomPiece + "');";
            }

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

        public void MajStock(int q)
        {

            int newStock = Quantite + q;

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
