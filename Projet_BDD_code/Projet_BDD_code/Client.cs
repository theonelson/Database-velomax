using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projet_BDD_code
{
    class Client
    {
        int id_client;
        int id_fidelio; 
        string nom_client;
        int id_adresse;
        int id_type;
        float reduction;


        //Constructeur 1  : création d'un client 
        public Client( string nom_client, int id_adresse, int id_type, int fidelio)
        {
            this.id_client = MaxIdClient()+1;
            this.nom_client = nom_client;
            this.id_fidelio = fidelio;
            this.id_type = id_type;
            this.id_adresse = id_adresse;

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
            command3.CommandText = "select * from client natural join prog_fidelite where id_client=" + id_client + ";";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {

                this.reduction = (int)reader3["rabais"];

            }
            command3.Dispose();

        }
        //Constructeur 2 : Accès à un client existant 
        public Client(int idc)
        {
            this.id_client = idc;
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
            command3.CommandText = "select * from client natural join prog_fidelite where id_client=" + idc + ";";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                this.id_type = (int)reader3["id_type"];
                this.nom_client = (string)reader3["nom_client"];
                this.id_adresse = (int)reader3["id_adresse"];
                this.id_type = (int)reader3["id_type"];
                if (string.IsNullOrEmpty(reader3["rabais"].ToString()))
                {
                    this.reduction = 0;
                    this.id_fidelio = 5;
                }
                else
                {
                    this.reduction = (float)reader3["rabais"];
                    this.id_fidelio = (int)reader3["num_programme"];
                }

            }
            command3.Dispose();

        }


        public int Idclient
        {
            get { return id_client; }
        }

        public int Fidelio
        {
            get { return id_fidelio; }
            set { id_fidelio = value; }
        }


        public int IdAdresse
        {
            get { return id_adresse; }
            set { id_adresse = value; }
        }
        public int IdType
        {
            get { return id_type; }
           
        }
        public string NomClient
        {
            get { return nom_client; }
        }

        public float Reduction
        {
            get { return reduction; }
            set { reduction = value; }
        }


        public override string ToString()
        {
            return  " ID client : " + this.id_client +  " | Nom : " + this.nom_client +  " | ID adresse : " + this.id_adresse + " | Fidélio : " + this.id_fidelio + '\n';
        }


        //Récupère le dernier ID de client créé
        public int MaxIdClient()
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

            #region Selection Max ID client
            string requete = "SELECT max(id_client) FROM client;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();
            int max = 0;
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                max = (int)reader["max(id_client)"];
            }
            return max;
            #endregion
        }

        //Creation d'un client 
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

            #region Insertion du client
            int Id = MaxIdClient() + 1;
            this.id_client = Id;
            string insertTable = "";
            if (id_fidelio==5)
            {
                insertTable = "insert into client values (" + Id + ",'" + NomClient + "'," + IdAdresse + ",'" + IdType + "',null);";
            }
            else
            {
                insertTable = "insert into client values (" + Id + ",'" + NomClient + "'," + IdAdresse + ",'" + IdType + "'," + Fidelio + ");";
                Console.WriteLine(insertTable);
            }
            
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

        public void MajBDD()
        {
            

            string requete1 = "";

            Console.WriteLine("     Saisir l'information à modifier : " );
            Console.WriteLine("     1. Adresse ");
            Console.WriteLine("     2. Programme Fidélio");
            int choix = Convert.ToInt32(Console.ReadLine());
            while(choix!=1 && choix!=2 )
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 ou 2)");
                choix = Convert.ToInt32(Console.ReadLine());
            }

            switch(choix)
            {

                case 1:
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
                        int old_id=0;
                        string requete = "select id_adresse from client where id_client=" + id_client + ";";
                        MySqlCommand command1 = maConnexion.CreateCommand();
                        command1.CommandText = requete;
                        MySqlDataReader reader = command1.ExecuteReader();
                        while (reader.Read())
                        {
                            old_id = (int)reader["id_adresse"];
                        }
                        reader.Close();
                        command1.Dispose();


                        //Création de la nouvelle adresse
                        Adresse ad = null;

                        Console.WriteLine(" Saisir l'adresse : ");
                        string rue = Console.ReadLine();
                        Console.WriteLine(" Saisir le code postal : ");
                        int code_postal = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(" Saisir la ville : ");
                        string ville = Console.ReadLine();
                        Console.WriteLine(" Saisir la Région : ");
                        string region = Console.ReadLine();
                        Console.WriteLine(" " + rue + ", " + code_postal + " " + ville + ", " + region + ".");
                        Console.WriteLine(" L'adresse est-elle correcte ?");
                        Console.WriteLine(" 1. Oui");
                        Console.WriteLine(" 2. Non\n\n");

                        int rep = Convert.ToInt32(Console.ReadLine());
                        if (rep != 1)
                        {
                            MajBDD();
                        }
                        else
                        {
                            ad = new Adresse(rue, ville, code_postal, region);
                            ad.InsertionBDD();
                        }
                        //Création de la requete maj
                        requete1 = "update client set id_adresse='" + ad.IdAdresse + "' where id_client = " + id_client + ";";

                        #region Mise à jour de la table client
                        MySqlCommand command2 = maConnexion.CreateCommand();
                        command2.CommandText = requete1;
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


                        //suppression de l'ancienne adresse
                        Adresse old_adresse = new Adresse(old_id);
                        old_adresse.SuppressionBDD();
                        
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Saisir le nouveau programme : ");
                        Console.WriteLine(" 1. Fidélio");
                        Console.WriteLine(" 2. Fidélio Or");
                        Console.WriteLine(" 3. Fidélio Platine");
                        Console.WriteLine(" 4. Fidélio Max");
                        Console.WriteLine(" 5. Aucun");
                        int repF = Convert.ToInt32(Console.ReadLine());
                        while (repF != 1 && repF != 2 && repF != 3 && repF != 4 && repF != 5)
                        {
                            Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 à 5)");
                            repF = Convert.ToInt32(Console.ReadLine());
                        }
                        if(repF!=5)
                        {
                            requete1 = "update client set id_programme=" + repF + " where id_client = " + id_client + ";";
                        }
                        else
                        {
                            requete1 = "update client set id_programme=null where id_client = " + id_client + ";";
                        }
                        break;
                    }

            }


          
        }
        //Supprime le client et tous ses liens dans la base 
        public void SuppressionBDD()
        {

            //Suppression des commandes du client

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
            string insertTable1 = "DELETE FROM commande WHERE id_client=" + Idclient + ";";

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
            requete2 = "delete from vente_modele where id_client=" + Idclient + ";";
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
            requete3 = "delete from vente_piece where id_client=" + Idclient + ";";
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






            //Suppression du client 
            #region Ouverture de connexion

            MySqlConnection maConnexion4 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax;" + "UID=root;PASSWORD=root";
                maConnexion4 = new MySqlConnection(connexionString);
                maConnexion4.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("ErreurConnexion : " + e.ToString());
                return;
            }
            #endregion

            string requete = "delete from client where id_client = " + id_client + ";";

            #region Mise à jour
            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = requete;
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


            //Suppression de l'adresse du client
            Adresse ad = new Adresse(IdAdresse);
            ad.SuppressionBDD();
        }
    }
}
