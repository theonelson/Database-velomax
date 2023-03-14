using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Velo
    {
        int id_velo;
        string nom_velo;
        string grandeur_velo;
        int prix_unitaire;
        string ligne_produit;
        string date_intro;
        string date_sortie;
        int quantite;
        List<Piece> liste_pieces;
        
        //Constructeur 1 : création d'un vélo en entrant toutes les informations
        public Velo(int id_velo, string nom_velo, string grandeur_velo, int prix_unitaire, string ligne_produit, string date_intro, string date_sortie, int stock, List<Piece> lp)

        {
            this.id_velo = id_velo;
            this.nom_velo = nom_velo;
            this.grandeur_velo = grandeur_velo;
            this.prix_unitaire = prix_unitaire;
            this.ligne_produit = ligne_produit;
            this.date_intro = date_intro;
            this.date_sortie = date_sortie;
            this.quantite = stock;
            this.liste_pieces = lp;
            this.liste_pieces = TrouverPiecesVelo();
        }
        //Constructeur 2 : Création d'un vélo à partir de son ID 
        //et la quantité à commander (cas achat d'un client)
        public Velo(int id_velo, int quant)

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
            
            string requete = "select * from modele where num_modele=" + id_velo + ";";     
            Console.WriteLine(requete);

            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
            MySqlDataReader reader2 = command2.ExecuteReader();
            string[] valueString2 = new string[reader2.FieldCount];
            while (reader2.Read())
            {
                this.id_velo = (int)reader2["num_modele"];
                this.nom_velo = (string)reader2["nom"];
                this.grandeur_velo = (string)reader2["grandeur"];
                this.ligne_produit = (string)reader2["ligne_produit"];
                this.date_intro = Convert.ToString((DateTime)reader2["date_intro"]);
                if (string.IsNullOrEmpty(reader2["date_disco"].ToString()))
                {
                    this.date_sortie = "NULL";
                }               
                else
                {
                    this.date_sortie = Convert.ToString((DateTime)reader2["date_disco"]).Split(' ')[0];
                }

                this.prix_unitaire = (int)reader2["prix_unitaire"];
                this.quantite = quant;
                this.liste_pieces = null;

            }
            reader2.Close();
            command2.Dispose();
            #endregion
            this.liste_pieces = TrouverPiecesVelo();
        }
        
        //constructeur 3 : Récupération des informations d'un vélo de la base
        //à partir de son ID (cas construction d'un vélo
        public Velo(int id_velo)

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

            string requete = "select * from modele where num_modele=" + id_velo + ";";
            Console.WriteLine(requete);

            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete;
            MySqlDataReader reader2 = command2.ExecuteReader();
            string[] valueString2 = new string[reader2.FieldCount];
            while (reader2.Read())
            {
                this.id_velo = (int)reader2["num_modele"];
                this.nom_velo = (string)reader2["nom"];
                this.grandeur_velo = (string)reader2["grandeur"];
                this.ligne_produit = (string)reader2["ligne_produit"];
                this.date_intro = Convert.ToString((DateTime)reader2["date_intro"]);
                if (string.IsNullOrEmpty(reader2["date_disco"].ToString()))
                {
                    this.date_sortie = "NULL";
                }
                else
                {
                    this.date_sortie = Convert.ToString((DateTime)reader2["date_disco"]).Split(' ')[0];
                }

                this.prix_unitaire = (int)reader2["prix_unitaire"];
                this.quantite = (int)reader2["stock_velo"];
                this.liste_pieces = null;

            }
            reader2.Close();
            command2.Dispose();
            #endregion
            this.liste_pieces = TrouverPiecesVelo();
        }

        public int IdVelo
        {
            get { return id_velo; }
        }
        public string NomVelo
        {
            get { return nom_velo; }
        }
        public string GrandeurVelo
        {
            get { return grandeur_velo; }
        }
        public int PrixUnitaire
        {
            get { return prix_unitaire; }
        }
        public string LigneProduit
        {
            get { return ligne_produit; }
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
            set { quantite = value; }
        }
        public List<Piece> ListePieces
        {
            get { return liste_pieces; }
            set { liste_pieces = value; }
        }


        //Récupère la liste des pièces présentes dans un vélo
        public List<Piece> TrouverPiecesVelo()
        {
            List<Piece> liste_p = new List<Piece>();

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
            #region Selection
            string requete = "select * from affectation where num_modele=" + IdVelo + ";";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            string[] valueString = new string[reader.FieldCount];
            //Récupération des infos de toutes les pièces du vélo
            while (reader.Read())
            {
                string idp = (string)reader["num_piece"];
                Piece ptemp = new Piece(idp);
                liste_p.Add(ptemp);

            }
            reader.Close();
            command1.Dispose();
            #endregion


            return liste_p;
        }
        
        public void AfficherPiecesDispo()
        {
            for(int i=0;i<ListePieces.Count; i++)
            {
                Console.WriteLine(" ID: " + ListePieces[i].IdPiece + " | Stock: " + ListePieces[i].Quantite);
            }
        }

        //Ajout d'un vélo dans la base
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
            if (date_sortie!=null)
            {
                requete = "INSERT INTO modele VALUES (" + IdVelo + ",'" + NomVelo + "','" + GrandeurVelo + "'," + PrixUnitaire + ",'" + LigneProduit + "','" + DateIntro + "','" + DateSortie + "'," + Quantite +");";
            }
            else
            {
                requete = "INSERT INTO modele VALUES (" + IdVelo + ",'" + NomVelo + "','" + GrandeurVelo + "'," + PrixUnitaire + ",'" + LigneProduit + "','" + DateIntro + "', NULL ," + Quantite + ");";
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

     

        //Vérifie si les pièces qui composent le vélo sont en stock 
        public bool EstConstructible()
        {
            bool dispo = true;
            for (int i=0; i<ListePieces.Count(); i++)
            {
                if(ListePieces[i].Quantite <1)
                {
                    dispo = false;
                }
            }
            return dispo;
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
            string insertTable = "update modele set stock_velo=" + newStock + " where num_modele = " + IdVelo + ";";
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
