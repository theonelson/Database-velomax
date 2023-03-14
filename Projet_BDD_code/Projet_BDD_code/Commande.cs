using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Commande
    {
        int num_commande;
        DateTime dateCommande;
        DateTime dateLivraison;
        Client acheteur;
        Velo velo;
        Piece piece;
        double totalCommande;

        //Constructeur 1 : cas commande de velo
        public Commande(DateTime dateCommande, DateTime dateLivraison, Client client,Velo lvelo)
        {
            this.num_commande = MaxIdCommande() + 1;
            this.dateCommande = dateCommande;
            this.dateLivraison = dateLivraison;
            this.acheteur = client;
            this.velo = lvelo;
            this.piece = null;
            this.totalCommande = CalculMontant();

        }

        //Constructeur 2 : cas commande de pièces
        public Commande(DateTime dateCommande, DateTime dateLivraison, Client client, Piece piece)
        {
            this.num_commande = MaxIdCommande() + 1;
            this.dateCommande = dateCommande;
            this.dateLivraison = dateLivraison;
            this.acheteur = client;
            this.piece = piece;
            this.velo = null;
            this.totalCommande = CalculMontant();
        }

        //Constructeur 3 : Accès aux informations d'une commande existante
        public Commande(int idc)
        {
            this.num_commande = idc;
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

            string requete = "SELECT * FROM commande where num_commande=" + num_commande + ";";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            string[] valueString = new string[reader.FieldCount];

            while (reader.Read())
            {
                this.acheteur = new Client((int)reader["id_client"]);
                DateTime dateCommande = (DateTime)reader["date_commande"];
                DateTime dateLivraison = (DateTime)reader["date_livraison"];
                this.totalCommande = (double)reader["montant"];


            }
            reader.Close();
            command1.Dispose();
        }


        public int NumCommande
        {
            get { return num_commande; }
        }

        public DateTime DateCommande
        {
            get { return dateCommande; }
        }


        public DateTime DateLivraison
        {
            get { return dateLivraison; }
            set { dateLivraison = value; }
        }

        public Client Acheteur
        {
            get { return acheteur; }
            set { acheteur = value; }
        }

        public Velo VeloCommande
        {
            get { return velo; }
            set { velo = value; }
        }

        public Piece PieceCommande
        {
            get { return piece; }
            set { piece = value; }
        }

        public double TotalCommande
        {
            get { return totalCommande; }
            set { totalCommande = value; }
        }


        //Calcule le montant total d'une commande
        public double CalculMontant()
        {
            
            double mont = 0;

            //cas pièce
            if (velo == null)
            {
                
                if(acheteur.Fidelio==5)
                {
                    mont = velo.Quantite;
                }
                else
                {
                    mont = (double)(piece.Quantite * piece.PrixPiece * (1 - acheteur.Reduction * 0.01));
                }
            }
            //cas vélo
            if (piece == null)
            {
                if (acheteur.Fidelio == 5)
                {
                    mont = velo.Quantite;
                }
                else
                {
                    mont = (double)(velo.Quantite * velo.PrixUnitaire * (1 - acheteur.Reduction * 0.01));
                }
            }


            
            return mont;
        }



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
            string requete = "SELECT max(num_commande) FROM velomax.commande;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();
            int max = 0;
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                max = (int)reader["max(num_commande)"];
            }
            return max;
            #endregion
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

            #region Insertion dans la table commande 
            string requete;
            int num_commande = MaxIdCommande() + 1;

            requete = "insert into commande values (" + NumCommande + ",'" + dateCommande.ToString("yyyy-MM-dd") + "','" + dateLivraison.ToString("yyyy-MM-dd") + "'," + Acheteur.Idclient + ", " + TotalCommande + ");";

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

            #region Insertion dans les tables vente_piece et vente_modele
            string requete2 = "";

            //cas pièce
            if (this.velo == null)
            {

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
                requete2 = "insert into velomax.vente_piece values (" + num_commande + ",'" + piece.IdPiece + "'," + piece.Quantite + "," + acheteur.Idclient + ");";
                #region requete
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete2;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(" ErreurConnexion : " + e.ToString());
                    Console.ReadLine();
                    return;
                }
                command.Dispose();
                #endregion

            }
            //cas vélo
            else
            {

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

                requete2 = "insert into velomax.vente_modele values (" + num_commande + "," + velo.IdVelo + ", " + velo.Quantite + "," + acheteur.Idclient+");";
                #region requete
                MySqlCommand command3 = maConnexion.CreateCommand();
                command3.CommandText = requete2;
                try
                {
                    command3.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(" ErreurConnexion : " + e.ToString());
                    Console.ReadLine();
                    return;
                }
                command3.Dispose();
                #endregion

            }
            #endregion



        }

        
        public void MajSock()
        {
            if (velo != null)
            {
                Velo velobase = new Velo(velo.IdVelo);
                velobase.MajStock(-(velo.Quantite)) ;
            }

            if (piece != null)
            {
                Piece piecebase = new Piece(piece.IdPiece);
                piecebase.MajStock(-(piece.Quantite));

            }

        }

        public void SuppressionBDD()
        {
            #region Connexion BDD
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

            #region Suppression dans la table Commande
            string insertTable1 = "DELETE FROM commande WHERE num_commande=" + NumCommande + ";";

            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = insertTable1;
            try
            {
                command1.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command1.Dispose();


            #endregion

            #region Suppression dans les tables vente_piece et vente_modele
            string requete2 = "";
            string requete3 = "";

            //cas vélo
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
            requete2 = "delete from vente_modele where num_commande=" + NumCommande + ";";
            #region requete
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = requete2;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
            #endregion
            //cas pièce
            #region Ouverture de connexion

            MySqlConnection maConnexion3 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax;" + "UID=root;PASSWORD=root";
                maConnexion3 = new MySqlConnection(connexionString);
                maConnexion3.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ErreurConnexion : " + e.ToString());
                return;
            }
            #endregion
            requete3 = "delete from vente_piece where num_commande=" + NumCommande + ";";
            #region requete
            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete3;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
            #endregion



            #endregion
        }



    }
}

