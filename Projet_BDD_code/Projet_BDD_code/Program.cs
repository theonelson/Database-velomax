using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using Newtonsoft.Json;



/// <summary>
/// 
/// 
/// 18/05/2022
/// 
/// 
/// Projet base de données - Boutique Vélomax 
/// Fichier SQL : VeloMax/ admin:root, password:root
/// 
/// </summary>



namespace Projet_BDD_code
{
    class Program
    {
        //Méthode qui appelle l'interface utilisateur
        static void Interface()
        {
            bool marche = true;
            while (marche == true)
            {
                Console.Clear();
                Console.WriteLine("------------------------ ° Boutique VéloMax ° ------------------------");
                Console.WriteLine("\n\n");

                Console.WriteLine("     A quel menu souhaitez-vous accéder ? \n");
                Console.WriteLine("     1. Commandes");
                Console.WriteLine("     2. Matériel");
                Console.WriteLine("     3. Clients");
                Console.WriteLine("     4. Approvisionnement");
                Console.WriteLine("     5. Statistiques");

                Console.WriteLine("\n     6. /Demo/");

                int choix = Convert.ToInt32(Console.ReadLine());
                while (choix != 1 && choix != 2 && choix != 3 && choix != 4 && choix != 5 && choix != 6)
                {
                    Console.WriteLine("Entrer un nombre ci-dessus (1 à 6)");
                    choix = Convert.ToInt32(Console.ReadLine());
                }

                if (choix == 1)
                {
                    Console.Clear();
                    Console.WriteLine("----------------- ° Menu Ventes ° ----------------\n\n");
                    Console.WriteLine("     Saisir le numéro de l'action à effectuer : ");
                    Console.WriteLine("     1. Afficher les commandes");
                    Console.WriteLine("     2. Nouvelle commande");
                    Console.WriteLine("     3. Supprimer une commande");

                    int choix_commande = Convert.ToInt32(Console.ReadLine());
                    while (choix_commande != 1 && choix_commande != 2 && choix_commande != 3)
                    {
                        Console.WriteLine("Entrer un nombre ci-dessus (1 à 3)");
                        choix_commande = Convert.ToInt32(Console.ReadLine());
                    }

                    switch (choix_commande)
                    {
                        case 1:
                            {
                                AfficherCommande();
                                Console.ReadKey();
                                break;
                            }


                        case 2:
                            {
                                Console.WriteLine("     Le client est-il un ancien ou un nouveau client ?");
                                Console.WriteLine("     1. Nouveau client");
                                Console.WriteLine("     2. Client existant");
                                int rep = Convert.ToInt32(Console.ReadLine());

                                Client cli = null;

                                switch (rep)
                                {
                                    case 1:
                                        {
                                            cli = CreationClient();
                                            break;
                                        }
                                    case 2:
                                        {
                                            AfficherClient();
                                            Console.WriteLine("Saisir l'id du client : ");
                                            int id = Convert.ToInt32(Console.ReadLine());

                                            cli = new Client(id);
                                            break;
                                        }
                                }



                                CreationCommande(cli);

                                Console.WriteLine("\nCommande effectuée.");
                                Console.ReadKey();
                                break;
                            }
                            
                            case 3:
                                {
                                    SupprimerCommande();
                                    
                                    Console.ReadKey();
                                    break;
                                }
                            

                    }
                }

                if (choix == 2)
                {
                    Console.Clear();
                    Console.WriteLine("------------------------ ° Menu Produits ° ------------------------\n\n");
                    Console.WriteLine("     Quelle action souhaitez-vous effectuer ? \n");
                    Console.WriteLine("     1. Affichage du stock");
                    Console.WriteLine("     2. Montage de vélo(s)");
                    Console.WriteLine("     3. Démontage de vélo(s)");
                    Console.WriteLine("     4. Réapprovisionner");

                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    while (choix2 != 1 && choix2 != 2 && choix2 != 3 && choix2 != 4)
                    {
                        Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 à 4)");
                        choix2 = Convert.ToInt32(Console.ReadLine());
                    }

                    switch (choix2)
                    {
                        //Affichage du stock
                        case 1:
                            {
                                AfficherStock();
                                Console.ReadKey();
                                break;
                            }
                        //Montage de vélos
                        case 2:
                            {
                                AfficherVelo();
                                Console.Write("\n ==> Saisir l'ID du vélo à monter : ");
                                int idv = Convert.ToInt32(Console.ReadLine());
                                MontageVelo(idv);
                                break;
                            }
                        //Démontage de vélos
                        case 3:
                            {
                                AfficherVelo();
                                Console.Write("\n ==> Saisir l'ID du vélo à monter : ");
                                int idv = Convert.ToInt32(Console.ReadLine());
                                DemontageVelo(idv);

                                break;
                            }
                        //Achat de pièces auprès d'un constructeur
                        case 4:
                            {
                                bool continuer = true;
                                while (continuer == true)
                                {
                                    Approvisionnement();
                                    Console.WriteLine("Commande de pièce(s) effectuée.\n");
                                    Console.WriteLine("Continuer les achats ?");
                                    Console.WriteLine(" 1. Oui");
                                    Console.WriteLine(" 2. Non");
                                    int rep = Convert.ToInt32(Console.ReadLine());
                                    if (rep == 1)
                                    {
                                        continuer = true;
                                    }
                                    else
                                    {
                                        continuer = false;
                                    }
                                }
                                Console.WriteLine("Fin des achats.\n");

                                Console.ReadLine();
                                break;
                            }

                    }
                }


                if (choix == 3)
                {
                    Console.Clear();
                    Console.WriteLine("------------------------ ° Menu Clients ° ------------------------\n");
                    Console.WriteLine("     Saisir l'action à effectuer : \n");
                    Console.WriteLine("     1. Affichage les clients");
                    Console.WriteLine("     2. Création d'un client");
                    Console.WriteLine("     3. Mise à jour d'un client");
                    Console.WriteLine("     4. Suppression des données d'un client");

                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    while (choix2 != 1 && choix2 != 2 && choix2 != 3 && choix2 != 4)
                    {
                        Console.WriteLine("Entrer l'un des numéros ci-dessus");
                        choix2 = Convert.ToInt32(Console.ReadLine());
                    }

                    switch (choix2)
                    {
                        case 1:
                            {
                                AfficherClient();
                                break;
                            }

                        case 2:
                            {
                                Console.Clear();
                                Client cli = CreationClient();

                                break;
                            }
                            //Mise à jour d'un client
                        case 3:
                            {
                                Console.Clear();
                                AfficherClient();

                                Console.WriteLine("\n ==> Saisir l'ID du client à modifier : ");
                                int id_client = Convert.ToInt32(Console.ReadLine());


                                Client c = new Client(id_client);
                                c.MajBDD();
                                Console.WriteLine(" Informations du client mises à jour.");

                                Console.ReadKey();
                                break;
                            }
                            //Suppression d'un client
                        case 4:
                            {
                                Console.Clear();
                                AfficherClient();
                                Console.Write("\n ==> Saisir l'ID du client à supprimer : ");
                                int idc = Convert.ToInt32(Console.ReadLine());
                                Client c = new Client(idc);
                                c.SuppressionBDD();
                                Console.WriteLine("\n Le client '" + c.NomClient + "' a été supprimé.");
                                break;
                            }
                            



                    }
                }

                if (choix == 4)
                {
                    Console.Clear();
                    Console.WriteLine("------------------------ ° Menu Fournisseurs ° ------------------------\n\n");
                    Console.WriteLine("     Saisir le numéro de l'action à effectuer : \n");
                    Console.WriteLine("     1. Afficher les fournisseurs");
                    Console.WriteLine("     2. Afficher les commandes d'approvisionnement");
                    Console.WriteLine("     3. Ajouter un fournisseur");
                    Console.WriteLine("     4. Supprimer un fournisseur");
                    int choix2 = Convert.ToInt32(Console.ReadLine());
                    while (choix2 != 1 && choix2 != 2 && choix2 != 3 && choix2 != 4)
                    {
                        Console.WriteLine("Saisir l'un des numéros ci-dessus (entier de 1 à 5)");
                        choix2 = Convert.ToInt32(Console.ReadLine());
                    }

                    switch (choix2)
                    {
                        case 1:
                            {
                                AfficherFournisseur();
                                break;
                            }
                        
                    case 2:
                        {
                            AfficherCommandeFournisseur();
                            break;
                        }
                        
                        case 3:
                            {
                                CreationFournisseur();
                                Console.WriteLine("Fournisseur créé.");
                                break;
                            }
                            

                            
                        case 4:
                            {
                                AfficherFournisseur();
                                Console.Write("\n ==> Saisir le nom du fournisseur à supprimer : ");
                                string n =Console.ReadLine();
                                Fournisseur f = new Fournisseur(n);
                                Console.WriteLine(" \n Fournisseur supprimé. ");
                                break;
                            }
                            
                    }

                }
                
                if (choix == 5)
                {
                    InterfaceStats();
                }

                if (choix==6)
                {
                    InterfaceDemo();
                }
                



                #region Condition fin de programme

                Console.WriteLine("\n\n Voulez-vous continuer ?");
                Console.WriteLine(" 1- Oui  /  2- Non");
                int choixFin = Convert.ToInt32(Console.ReadLine());
                while (choixFin != 1 && choixFin != 2)
                {
                    Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 ou 2)");
                    choixFin = Convert.ToInt32(Console.ReadLine());
                }
                if (choixFin == 1)
                {
                    marche = true;
                }
                else
                {
                    marche = false;
                }
                #endregion
            }

        }



        // -----------------
        //   Méthodes 
        // -----------------


        // #### Méthodes d'affichage ####

        static void AfficherClient()
        {
            #region Ouverture de connexion

            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "select * from client natural join adresse;";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            string id_client;
            string type;
            string nom_client;
            int id_adresse;
            string id_fidelio;
            string total_achat;
            Console.WriteLine("    ID    |     Nom    |    Type   | N° programme |  Adresse ");
            Console.WriteLine("-------------------------------------------------------------");
            while (reader3.Read())
            {
                id_client =Convert.ToString((int)reader3["id_client"]);
                if ((int)reader3["id_type"] ==1)
                {
                    type = "particulier";
                }
                else
                {
                    type = "entreprise";
                }
                nom_client = (string)reader3["nom_client"];
                id_adresse = (int)reader3["id_adresse"];
                string adresse = (string)reader3["rue"] + ", " + Convert.ToString((int)reader3["code_postal"]) + " " + (string)reader3["ville"];
                if (reader3.IsDBNull(4))
                {
                    id_fidelio = "Null";
                }
                else
                {
                    id_fidelio = Convert.ToString((int)reader3["num_programme"]);
                }
                
                total_achat = "0";

                string[] tab_donnees = new string[5];
                tab_donnees[0] = id_client;
                tab_donnees[1] = nom_client;
                tab_donnees[2] = type;
                tab_donnees[3] = id_fidelio;
                tab_donnees[4] = adresse;

                for (int i = 0; i < tab_donnees.Length - 1; i++)
                {

                    string vide = " ";
                    for (int j = 0; j < 9 - tab_donnees[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    
                    
                    Console.Write(" " + tab_donnees[i] + vide + "|");

                }
                Console.Write(" " + tab_donnees[4]);
                Console.WriteLine();
                //Console.WriteLine("Nom: " + nomclient + "  |  Prenom: " + prenomclient + "  |  NumTel: " + numTelc + "  |  Email: " + email + "  |  Adresse: " + adresse + "  |  Total Achat: " + totalAchatP);
            }
            command3.Dispose();
        }
        static void AfficherFournisseur()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM fournisseur natural join adresse ;";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            Console.WriteLine("      Nom      | SIRET |       Contact      |   Statut     | Adresse    ");
            Console.WriteLine("-----------------------------------------------------------------------------");
            while (reader3.Read())
            {
                string siret = Convert.ToString((int)reader3["siret"]);
                string nom_fournisseur = (string)reader3["nom_fournisseur"];
                string contact = (string)reader3["contact"];
                string id_adresse = Convert.ToString((int)reader3["id_adresse"]);
                string adresse = (string)reader3["rue"] + ", " + Convert.ToString((int)reader3["code_postal"]) + " " + (string)reader3["ville"];
                string statut = (string)reader3["statut"];

                string[] tab_donnees = new string[5];
                tab_donnees[0] = nom_fournisseur;
                tab_donnees[1] = siret;
                tab_donnees[2] = contact;
                tab_donnees[3] = statut;
                tab_donnees[4] = adresse;


                for (int i = 0; i < tab_donnees.Length - 1; i++)
                {

                    string vide = " ";
                    for (int j = 0; j < 14 - tab_donnees[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    if (i==1)
                    {
                        vide = " ";
                    }
                            



                    Console.Write(tab_donnees[i] + vide + "| ");

                }
                Console.Write(" " + tab_donnees[4]);
                Console.WriteLine();

                //Console.WriteLine(" Nom du fournisseur: " + tab_donnees[1] + "  |  SIRET: " + tab_donnees[0] + "  |  Contact: " + contact + "  |  Statut: " + statut + "  |  Adresse: " + adresse);
            }
            command3.Dispose();
        }
        static void AfficherCommande()
        {

            Console.WriteLine("     De qui souhaitez-vous afficher les commandes ? : \n");
            Console.WriteLine("     1. Particuliers");
            Console.WriteLine("     2. Entreprises");
            Console.WriteLine("     3. Total des commandes");
            int choix = Convert.ToInt32(Console.ReadLine());
            while (choix != 1 && choix != 2 && choix != 3)
            {
                Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 à 3)");
                choix = Convert.ToInt32(Console.ReadLine());
            }

            switch (choix)
            {
                case 1:
                    {

                        #region Ouverture de connexion

                        MySqlConnection maConnexion = ouvrir_connexion();
                        #endregion

                        string requete = "SELECT * FROM commande natural join client where id_type=1;";
                        MySqlCommand command1 = maConnexion.CreateCommand();
                        command1.CommandText = requete;

                        MySqlDataReader reader = command1.ExecuteReader();

                        string[] valueString = new string[reader.FieldCount];
                        Console.WriteLine("IdClient|nomClient|IdCommande|  Montant |  DateCommande  |  DateLivraison ");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id_client = (int)reader["id_client"];
                            int id_commande = (int)reader["num_commande"];
                            DateTime dateCommande = (DateTime)reader["date_commande"];
                            DateTime dateLivraison = (DateTime)reader["date_livraison"];
                            string nom_client = (string)reader["nom_client"];
                            int id_adresse = (int)reader["id_adresse"];
                            int id_type = (int)reader["id_type"];
                            double montant = (double)reader["montant"];

                            
                            Console.WriteLine(" " + id_client + "   |   " + nom_client + "   |   " + id_commande + "    | " + montant+"  |  " + dateCommande + "  |  " + dateLivraison );

                        }
                        reader.Close();
                        command1.Dispose();
               

                        break;
                    }
                    
                case 2:
                    {
                        #region Ouverture de connexion
                        MySqlConnection maConnexion = ouvrir_connexion();
                        #endregion

                        string requete = "SELECT * FROM commande natural join client where id_type=2;";
                        MySqlCommand command1 = maConnexion.CreateCommand();
                        command1.CommandText = requete;

                        MySqlDataReader reader = command1.ExecuteReader();

                        string[] valueString = new string[reader.FieldCount];
                        Console.WriteLine("IdClient|nomClient|IdCommande|  Montant |  DateCommande  |  DateLivraison ");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id_client = (int)reader["id_client"];
                            int id_commande = (int)reader["num_commande"];
                            DateTime dateCommande = (DateTime)reader["date_commande"];
                            DateTime dateLivraison = (DateTime)reader["date_livraison"];
                            string nom_client = (string)reader["nom_client"];
                            int id_adresse = (int)reader["id_adresse"];
                            int id_type = (int)reader["id_type"];
                            double montant = (double)reader["montant"];


                            Console.WriteLine(" " + id_client + "   |   " + nom_client + "   |   " + id_commande + "    | " + montant + "  |  " + dateCommande + "  |  " + dateLivraison);

                        }
                        reader.Close();
                        command1.Dispose();


                        break;
                    }
                case 3:
                    {
                        #region Ouverture de connexion
                        MySqlConnection maConnexion = ouvrir_connexion();
                        #endregion

                        string requete = "SELECT * FROM commande natural join client;";
                        MySqlCommand command1 = maConnexion.CreateCommand();
                        command1.CommandText = requete;

                        MySqlDataReader reader = command1.ExecuteReader();

                        string[] valueString = new string[reader.FieldCount];
                        Console.WriteLine("IdClient|nomClient|IdCommande|  Montant |  DateCommande  |  DateLivraison ");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            int id_client = (int)reader["id_client"];
                            int id_commande = (int)reader["num_commande"];
                            DateTime dateCommande = (DateTime)reader["date_commande"];
                            DateTime dateLivraison = (DateTime)reader["date_livraison"];
                            string nom_client = (string)reader["nom_client"];
                            int id_adresse = (int)reader["id_adresse"];
                            int id_type = (int)reader["id_type"];
                            double montant = (double)reader["montant"];


                            Console.WriteLine(" " + id_client + "   |   " + nom_client + "   |   " + id_commande + "    | " + montant + "  |  " + dateCommande + "  |  " + dateLivraison);

                        }
                        reader.Close();
                        command1.Dispose();


                        break;
                    }

            }

        }
        static void AfficherCommandeFournisseur()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM approvisionner ;";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            Console.WriteLine(" ID commande| ID pièce |  SIRET  |    Date     | Quantité    ");
            Console.WriteLine("---------------------------------------------------------------------");
            string date_appro = "";
            while (reader3.Read())
            {
                string siret = Convert.ToString((int)reader3["siret"]);
                string id_appro= Convert.ToString((int)reader3["id_appro"]);
                string id_piece = (string)reader3["num_piece"];
                string quantite = Convert.ToString((int)reader3["quantite"]);
                if (string.IsNullOrEmpty(reader3["date_appro"].ToString()))
                    date_appro = "NULL";
                else
                {
                    date_appro = Convert.ToString((DateTime)reader3["date_appro"]).Split(' ')[0];
                }

                string[] tab_donnees = new string[5];
                tab_donnees[0] = id_appro;
                tab_donnees[1] = id_piece;
                tab_donnees[2] = siret;
                tab_donnees[3] = date_appro;
                tab_donnees[4] = quantite;

                
                for (int i = 0; i < tab_donnees.Length - 1; i++)
                {
                    string vide = " ";
                    for (int j = 0; j < 7 - tab_donnees[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    Console.Write("  " + tab_donnees[i] + vide + "| ");
                }
                Console.Write(" " + tab_donnees[4]);
                Console.WriteLine();
            }
            command3.Dispose();
        }
        static void AfficherStock()
        {
            Console.WriteLine("Saisir le numéro du stock à afficher :");
            Console.WriteLine(" 1. Vélos");
            Console.WriteLine(" 2. Pièces");
            int choix1 = Convert.ToInt32(Console.ReadLine());
            while (choix1 != 1 && choix1 != 2)
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 ou 2)");
                choix1 = Convert.ToInt32(Console.ReadLine());
            }
            if (choix1 == 1)
            {
                Console.WriteLine("Affichage des vélos triés par... ");
                Console.WriteLine(" 1. ID");
                Console.WriteLine(" 2. type");
                Console.WriteLine(" 3. taille");
                Console.WriteLine(" 4. ordre alphabétique");
                Console.WriteLine(" 5. quantités en stock");

                int choix2 = Convert.ToInt32(Console.ReadLine());
                while (choix2 != 1 && choix2 != 2 && choix2 != 3 && choix2 != 4 )
                {
                    Console.WriteLine("Saisir l'un des numéros ci-dessus (1 à 4)");
                    choix2 = Convert.ToInt32(Console.ReadLine());
                }

                switch (choix2)
                {

                    case 1:
                        {                 
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            string requete = "SELECT * FROM modele order by num_modele;";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;
                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            string date_sortie; 
                            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
                            while (reader.Read())
                            {
                                int id_modele = (int)reader["num_modele"];
                                string nom = (string)reader["nom"];
                                string grandeur = (string)reader["grandeur"];
                                int prix_modele = (int)reader["prix_unitaire"];
                                string type = (string)reader["ligne_produit"];
                                DateTime date_intro = (DateTime)reader["date_intro"];
                                if (reader.IsDBNull(6))
                                {
                                    date_sortie = "/";
                                }
                                else
                                {
                                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                                }
                                int stock = (int)reader["stock_velo"];

                                string[] tab = new string[8];
                                tab[0] = Convert.ToString(id_modele);
                                tab[1] = nom;
                                tab[2] = grandeur;
                                tab[3] = Convert.ToString(prix_modele);
                                tab[4] = type;
                                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                                tab[6] = date_sortie;
                                tab[7] = Convert.ToString(stock);
                                Console.Write(" " + tab[0] + " | ");
                                for (int i = 1; i < tab.Length-1; i++)
                                {
                                    
                                    string vide = " ";
                                    for (int j = 0; j < 15 - tab[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if(i==3)
                                    {
                                        vide = " ";
                                    }
                                    Console.Write(tab[i] + vide + " | ");
                                    
                                }
                                Console.Write(" " + tab[7]);
                                Console.WriteLine();

                            }
                            reader.Close();
                            command1.Dispose();
                            break;
                        }
                    case 2:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            string requete = "SELECT * FROM modele order by ligne_produit;";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;
                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            string date_sortie;
                            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
                            while (reader.Read())
                            {
                                int id_modele = (int)reader["num_modele"];
                                string nom = (string)reader["nom"];
                                string grandeur = (string)reader["grandeur"];
                                int prix_modele = (int)reader["prix_unitaire"];
                                string type = (string)reader["ligne_produit"];
                                DateTime date_intro = (DateTime)reader["date_intro"];
                                if (reader.IsDBNull(6))
                                {
                                    date_sortie = "/";
                                }
                                else
                                {
                                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                                }
                                int stock = (int)reader["stock_velo"];

                                string[] tab = new string[8];
                                tab[0] = Convert.ToString(id_modele);
                                tab[1] = nom;
                                tab[2] = grandeur;
                                tab[3] = Convert.ToString(prix_modele);
                                tab[4] = type;
                                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                                tab[6] = date_sortie;
                                tab[7] = Convert.ToString(stock);
                                Console.Write(" " + tab[0] + " | ");
                                for (int i = 1; i < tab.Length - 1; i++)
                                {

                                    string vide = " ";
                                    for (int j = 0; j < 15 - tab[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i == 3)
                                    {
                                        vide = " ";
                                    }
                                    Console.Write(tab[i] + vide + " | ");

                                }
                                Console.Write(" " + tab[7]);
                                Console.WriteLine();

                            }
                            reader.Close();
                            command1.Dispose();
                            break;
                        }
                    case 3:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            string requete = "SELECT * FROM modele order by grandeur;";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;
                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            string date_sortie;
                            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
                            while (reader.Read())
                            {
                                int id_modele = (int)reader["num_modele"];
                                string nom = (string)reader["nom"];
                                string grandeur = (string)reader["grandeur"];
                                int prix_modele = (int)reader["prix_unitaire"];
                                string type = (string)reader["ligne_produit"];
                                DateTime date_intro = (DateTime)reader["date_intro"];
                                if (reader.IsDBNull(6))
                                {
                                    date_sortie = "/";
                                }
                                else
                                {
                                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                                }
                                int stock = (int)reader["stock_velo"];

                                string[] tab = new string[8];
                                tab[0] = Convert.ToString(id_modele);
                                tab[1] = nom;
                                tab[2] = grandeur;
                                tab[3] = Convert.ToString(prix_modele);
                                tab[4] = type;
                                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                                tab[6] = date_sortie;
                                tab[7] = Convert.ToString(stock);
                                Console.Write(" " + tab[0] + " | ");
                                for (int i = 1; i < tab.Length - 1; i++)
                                {

                                    string vide = " ";
                                    for (int j = 0; j < 15 - tab[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i == 3)
                                    {
                                        vide = " ";
                                    }
                                    Console.Write(tab[i] + vide + " | ");

                                }
                                Console.Write(" " + tab[7]);
                                Console.WriteLine();

                            }
                            reader.Close();
                            command1.Dispose();
                            break;
                        }
                    case 4:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            string requete = "SELECT * FROM modele order by nom;";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;
                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            string date_sortie;
                            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
                            while (reader.Read())
                            {
                                int id_modele = (int)reader["num_modele"];
                                string nom = (string)reader["nom"];
                                string grandeur = (string)reader["grandeur"];
                                int prix_modele = (int)reader["prix_unitaire"];
                                string type = (string)reader["ligne_produit"];
                                DateTime date_intro = (DateTime)reader["date_intro"];
                                if (reader.IsDBNull(6))
                                {
                                    date_sortie = "/";
                                }
                                else
                                {
                                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                                }
                                int stock = (int)reader["stock_velo"];

                                string[] tab = new string[8];
                                tab[0] = Convert.ToString(id_modele);
                                tab[1] = nom;
                                tab[2] = grandeur;
                                tab[3] = Convert.ToString(prix_modele);
                                tab[4] = type;
                                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                                tab[6] = date_sortie;
                                tab[7] = Convert.ToString(stock);
                                Console.Write(" " + tab[0] + " | ");
                                for (int i = 1; i < tab.Length - 1; i++)
                                {

                                    string vide = " ";
                                    for (int j = 0; j < 15 - tab[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i == 3)
                                    {
                                        vide = " ";
                                    }
                                    Console.Write(tab[i] + vide + " | ");

                                }
                                Console.Write(" " + tab[7]);
                                Console.WriteLine();

                            }
                            reader.Close();
                            command1.Dispose();
                            break;
                        }
                    case 5:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            string requete = "SELECT * FROM modele order by stock_velo;";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;
                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            string date_sortie;
                            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
                            while (reader.Read())
                            {
                                int id_modele = (int)reader["num_modele"];
                                string nom = (string)reader["nom"];
                                string grandeur = (string)reader["grandeur"];
                                int prix_modele = (int)reader["prix_unitaire"];
                                string type = (string)reader["ligne_produit"];
                                DateTime date_intro = (DateTime)reader["date_intro"];
                                if (reader.IsDBNull(6))
                                {
                                    date_sortie = "/";
                                }
                                else
                                {
                                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                                }
                                int stock = (int)reader["stock_velo"];

                                string[] tab = new string[8];
                                tab[0] = Convert.ToString(id_modele);
                                tab[1] = nom;
                                tab[2] = grandeur;
                                tab[3] = Convert.ToString(prix_modele);
                                tab[4] = type;
                                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                                tab[6] = date_sortie;
                                tab[7] = Convert.ToString(stock);
                                Console.Write(" " + tab[0] + " | ");
                                for (int i = 1; i < tab.Length - 1; i++)
                                {

                                    string vide = " ";
                                    for (int j = 0; j < 15 - tab[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i == 3)
                                    {
                                        vide = " ";
                                    }
                                    Console.Write(tab[i] + vide + " | ");

                                }
                                Console.Write(" " + tab[7]);
                                Console.WriteLine();

                            }
                            reader.Close();
                            command1.Dispose();
                            break;
                        }

                        
                     
                }
            }
            //Cas pièce
            else
            {
                Console.WriteLine("Affichage des pièces par... \n\n");
                Console.WriteLine(" 1. ID\n");
                Console.WriteLine(" 2. type\n");
                Console.WriteLine(" 3. nouveauté\n");
                Console.WriteLine(" 4. quantités en stock\n");
                int choix4 = Convert.ToInt32(Console.ReadLine());
                while (choix4 != 1 && choix4 != 2 && choix4 != 3 && choix4 != 4 )
                {
                    Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 à 4)");
                    choix4 = Convert.ToInt32(Console.ReadLine());
                }
                switch (choix4)
                {
                    case 1:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            #region Selection
                            string requete = "select * from piece_rechange order by num_piece";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;

                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
                            Console.WriteLine("--------------------------------------------------------------------");
                            while (reader.Read())
                            {
                                string id_piece = (string)reader["num_piece"];
                                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                                string nom_piece = (string)reader["description_piece"];
                                string stock = Convert.ToString((int)reader["stock_piece"]);
                                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                                string date_sortie;
                                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                                    date_sortie = "NULL";
                                else
                                {
                                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                                }

                                //Affichage de la table 
                                string[] tab_donnee = new string[6];
                                tab_donnee[0] = id_piece;
                                tab_donnee[1] = nom_piece;
                                tab_donnee[2] = prix_piece;
                                tab_donnee[3] = stock;
                                tab_donnee[4] = date_intro;
                                tab_donnee[5] = date_sortie;
                                for (int i=0; i<tab_donnee.Length; i++)
                                {
                                    string vide = " ";
                                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i != 1)
                                    {
                                        Console.Write(" " + tab_donnee[i] + vide + " |");
                                    }
                                    
                                }
                                Console.WriteLine(" " + tab_donnee[1] );



                            }
                            reader.Close();
                            command1.Dispose();
                            #endregion
                            break;
                        }
                    case 2:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            #region Selection
                            string requete = "select * from piece_rechange order by description_piece";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;

                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
                            Console.WriteLine("--------------------------------------------------------------------");
                            while (reader.Read())
                            {
                                string id_piece = (string)reader["num_piece"];
                                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                                string nom_piece = (string)reader["description_piece"];
                                string stock = Convert.ToString((int)reader["stock_piece"]);
                                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                                string date_sortie;
                                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                                    date_sortie = "NULL";
                                else
                                {
                                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                                }

                                //Affichage de la table 
                                string[] tab_donnee = new string[6];
                                tab_donnee[0] = id_piece;
                                tab_donnee[1] = nom_piece;
                                tab_donnee[2] = prix_piece;
                                tab_donnee[3] = stock;
                                tab_donnee[4] = date_intro;
                                tab_donnee[5] = date_sortie;
                                for (int i = 0; i < tab_donnee.Length; i++)
                                {
                                    string vide = " ";
                                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i != 1)
                                    {
                                        Console.Write(" " + tab_donnee[i] + vide + " |");
                                    }

                                }
                                Console.WriteLine(" " + tab_donnee[1]);



                            }
                            reader.Close();
                            command1.Dispose();
                            #endregion
                            break;
                        }
                
                    case 3:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            #region Selection
                            string requete = "select * from piece_rechange order by date_intro_piece";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;

                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
                            Console.WriteLine("--------------------------------------------------------------------");
                            while (reader.Read())
                            {
                                string id_piece = (string)reader["num_piece"];
                                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                                string nom_piece = (string)reader["description_piece"];
                                string stock = Convert.ToString((int)reader["stock_piece"]);
                                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                                string date_sortie;
                                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                                    date_sortie = "NULL";
                                else
                                {
                                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                                }

                                //Affichage de la table 
                                string[] tab_donnee = new string[6];
                                tab_donnee[0] = id_piece;
                                tab_donnee[1] = nom_piece;
                                tab_donnee[2] = prix_piece;
                                tab_donnee[3] = stock;
                                tab_donnee[4] = date_intro;
                                tab_donnee[5] = date_sortie;
                                for (int i = 0; i < tab_donnee.Length; i++)
                                {
                                    string vide = " ";
                                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i != 1)
                                    {
                                        Console.Write(" " + tab_donnee[i] + vide + " |");
                                    }

                                }
                                Console.WriteLine(" " + tab_donnee[1]);



                            }
                            reader.Close();
                            command1.Dispose();
                            #endregion
                            break;
                        }
                    case 4:
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            #region Selection
                            string requete = "select * from piece_rechange order by stock_piece";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;

                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
                            Console.WriteLine("--------------------------------------------------------------------");
                            while (reader.Read())
                            {
                                string id_piece = (string)reader["num_piece"];
                                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                                string nom_piece = (string)reader["description_piece"];
                                string stock = Convert.ToString((int)reader["stock_piece"]);
                                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                                string date_sortie;
                                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                                    date_sortie = "NULL";
                                else
                                {
                                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                                }

                                //Affichage de la table 
                                string[] tab_donnee = new string[6];
                                tab_donnee[0] = id_piece;
                                tab_donnee[1] = nom_piece;
                                tab_donnee[2] = prix_piece;
                                tab_donnee[3] = stock;
                                tab_donnee[4] = date_intro;
                                tab_donnee[5] = date_sortie;
                                for (int i = 0; i < tab_donnee.Length; i++)
                                {
                                    string vide = " ";
                                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i != 1)
                                    {
                                        Console.Write(" " + tab_donnee[i] + vide + " |");
                                    }

                                }
                                Console.WriteLine(" " + tab_donnee[1]);



                            }
                            reader.Close();
                            command1.Dispose();
                            #endregion
                            break;
                        }
                 
                }

            }
        }

        static void AfficherPiece()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            #region Selection
            string requete = "select * from piece_rechange order by stock_piece";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            string[] valueString = new string[reader.FieldCount];
            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
            Console.WriteLine("--------------------------------------------------------------------");
            while (reader.Read())
            {
                string id_piece = (string)reader["num_piece"];
                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                string nom_piece = (string)reader["description_piece"];
                string stock = Convert.ToString((int)reader["stock_piece"]);
                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                string date_sortie;
                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                    date_sortie = "NULL";
                else
                {
                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                }

                //Affichage de la table 
                string[] tab_donnee = new string[6];
                tab_donnee[0] = id_piece;
                tab_donnee[1] = nom_piece;
                tab_donnee[2] = prix_piece;
                tab_donnee[3] = stock;
                tab_donnee[4] = date_intro;
                tab_donnee[5] = date_sortie;
                for (int i = 0; i < tab_donnee.Length; i++)
                {
                    string vide = " ";
                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    if (i != 1)
                    {
                        Console.Write(" " + tab_donnee[i] + vide + " |");
                    }

                }
                Console.WriteLine(" " + tab_donnee[1]);



            }
            reader.Close();
            command1.Dispose();
            #endregion
        }

        static void AfficherVelo()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            string requete = "SELECT * FROM modele order by num_modele;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;
            MySqlDataReader reader = command1.ExecuteReader();

            string[] valueString = new string[reader.FieldCount];
            string date_sortie;
            Console.WriteLine(" ID |      Nom         |    Grandeur   | Prix |    Ligne     |  Date entrée  |  Date sortie  | stock ");
            while (reader.Read())
            {
                int id_modele = (int)reader["num_modele"];
                string nom = (string)reader["nom"];
                string grandeur = (string)reader["grandeur"];
                int prix_modele = (int)reader["prix_unitaire"];
                string type = (string)reader["ligne_produit"];
                DateTime date_intro = (DateTime)reader["date_intro"];
                if (reader.IsDBNull(6))
                {
                    date_sortie = "/";
                }
                else
                {
                    date_sortie = (Convert.ToString((DateTime)reader["date_disco"])).Split(' ')[0];
                }
                int stock = (int)reader["stock_velo"];

                string[] tab = new string[8];
                tab[0] = Convert.ToString(id_modele);
                tab[1] = nom;
                tab[2] = grandeur;
                tab[3] = Convert.ToString(prix_modele);
                tab[4] = type;
                tab[5] = (Convert.ToString(date_intro)).Split(' ')[0];
                tab[6] = date_sortie;
                tab[7] = Convert.ToString(stock);
                Console.Write(" " + tab[0] + " | ");
                for (int i = 1; i < tab.Length - 1; i++)
                {

                    string vide = " ";
                    for (int j = 0; j < 15 - tab[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    if (i == 3)
                    {
                        vide = " ";
                    }
                    Console.Write(tab[i] + vide + " | ");

                }
                Console.Write(" " + tab[7]);
                Console.WriteLine();

            }
            reader.Close();
            command1.Dispose();
        }
        
        static void AfficherProgramme()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM prog_fidelite ;";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            Console.WriteLine("Numéro |     Nom      |  Prix  |  Durée  |   Remise ");
            Console.WriteLine("-------------------------------------------------------------");
            while (reader3.Read())
            {
                string nump = Convert.ToString((int)reader3["num_programme"]);
                string nom_prog = (string)reader3["description_prog"];
                string prix = Convert.ToString((float)reader3["cout_programme"] + " e");
                string duree = Convert.ToString((int)reader3["duree"]) + " an(s)";
                string remise = Convert.ToString((float)reader3["rabais"]) + " %";


                string[] tab_donnees = new string[5];
                tab_donnees[0] = nump;
                tab_donnees[1] = nom_prog;
                tab_donnees[2] = prix;
                tab_donnees[3] = duree;
                tab_donnees[4] = remise;            

                for (int i = 0; i < tab_donnees.Length - 1; i++)
                {

                    string vide = " ";
                    for (int j = 0; j < 5 - tab_donnees[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    if (i == 1)
                    {
                        vide = " ";
                        for (int j = 0; j < 15 - tab_donnees[1].Length; j++)
                        {
                            vide = vide + " ";
                        }
                    }
                    Console.Write(" " + tab_donnees[i] + vide + "|");

                }
                Console.Write(" " + tab_donnees[4]);
                Console.WriteLine();

                //Console.WriteLine(" Nom du fournisseur: " + tab_donnees[1] + "  |  SIRET: " + tab_donnees[0] + "  |  Contact: " + contact + "  |  Statut: " + statut + "  |  Adresse: " + adresse);
            }
            command3.Dispose();
        }

        static void AfficherAdresse()
        {
            #region Ouverture de connexion
            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            MySqlCommand command3 = maConnexion.CreateCommand();
            command3.CommandText = "SELECT * FROM adresse ;";

            MySqlDataReader reader3;
            reader3 = command3.ExecuteReader();
            Console.WriteLine("ID       |            Rue             |  Ville  |  Code postal  |   Province ");
            Console.WriteLine("-------------------------------------------------------------------------------");
            while (reader3.Read())
            {
                string id_adresse = Convert.ToString((int)reader3["id_adresse"]);
                string rue = (string)reader3["rue"];
                string ville = (string)reader3["ville"];
                string code_postal = Convert.ToString((int)reader3["code_postal"]);
                string province = (string)reader3["province"];

                string[] tab_donnees = new string[5];
                tab_donnees[0] = id_adresse;
                tab_donnees[1] = rue;
                tab_donnees[2] = ville;
                tab_donnees[3] = code_postal;
                tab_donnees[4] = province;

                for (int i = 0; i < tab_donnees.Length - 1; i++)
                {

                    string vide = " ";
                    for (int j = 0; j < 8 - tab_donnees[i].Length; j++)
                    {
                        vide = vide + " ";
                    }
                    if (i == 1)
                    {
                        vide = " ";
                        for (int j = 0; j < 25 - tab_donnees[1].Length; j++)
                        {
                            vide = vide + " ";
                        }
                    }
                    Console.Write(" " + tab_donnees[i] + vide + "|");

                }
                Console.Write(" " + tab_donnees[4]);
                Console.WriteLine();
            }
            command3.Dispose();
        }


        //--- #### Méthodes de création de données ####

        //Demande les informations d'un client puis l'ajoute dans la BDD
        static Client CreationClient()
        {
            Client cli = null;
            Adresse adresse = CreationAdresse();
            int id_adresse = adresse.IdAdresse;

            Console.WriteLine("Saisir Le nom du client : ");
            string nom = Console.ReadLine();
            Console.WriteLine(" Le client est-il un particulier ou une entreprise ? ");
            Console.WriteLine(" 1. Particulier");
            Console.WriteLine(" 2. Entreprise");
            int type = Convert.ToInt32(Console.ReadLine());
            while (type != 1  && type !=2)
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 ou 2)");
                type = Convert.ToInt32(Console.ReadLine());
            }
            Console.Clear();
            AfficherProgramme();
            Console.WriteLine(" Saisir la formule fidélio du client : ");
            Console.WriteLine(" 1. Fidélio");
            Console.WriteLine(" 2. Fidélio Or");
            Console.WriteLine(" 3. Fidélio Platine");
            Console.WriteLine(" 4. Fidélio Max");
            Console.WriteLine(" 5. Aucun");
            int fidelio = Convert.ToInt32(Console.ReadLine());
            while (type != 1 && type != 2 && type != 3 && type != 4 && type != 5)
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 à 5)");
                type = Convert.ToInt32(Console.ReadLine());
            }
            
           
            cli = new Client( nom, id_adresse, type, fidelio);
            cli.ToString();
            Console.WriteLine(" Les informations sont-elles correcetes ? ");
            Console.WriteLine(" 1. Oui ");
            Console.WriteLine(" 2. Recommencer ");
            int choix = Convert.ToInt32(Console.ReadLine());
            if(choix==2)
            {
                CreationClient();
            }
            else
            {
                cli.InsertionBDD();
            }

            return cli;
        }

        //Demande les infromations d'une adresse puis l'ajoute dans la BDD
        static Adresse CreationAdresse()
        {
            Adresse ad = null;

            Console.WriteLine(" Saisir la n° et le nom de la rue : ");
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
            Console.WriteLine(" 2. Non");

            int choix = Convert.ToInt32(Console.ReadLine());
            if(choix !=1 )
            {
                CreationAdresse();
            }
            else
            {
                ad = new Adresse(rue, ville, code_postal, region);
                ad.InsertionBDD();
            }
            return ad;

        }
      
        //Demande les produits à commander puis met à jour la BDD
        static void CreationCommande(Client client)
        {
            Console.WriteLine("Sélectionner les produits vendus : ");
            Console.WriteLine(" 1. Vélo(s)");
            Console.WriteLine(" 2. Pièce(s) ");

            int choix = Convert.ToInt32(Console.ReadLine());
            while (choix != 1 && choix != 2 && choix != 3)
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 ou 2)");
                choix = Convert.ToInt32(Console.ReadLine());
            }

            // Vente vélo
            switch (choix)
            {
                case 1:
                    {                       
                        bool continuer = true;
                        DateTime dateLivrai = DateTime.Now.Date.AddDays(3);
                        while (continuer)
                        {
                            AfficherVelo();

                            Console.Write("\n Saisir l'ID du modèle souhaité : ");
                            int id_mod = Convert.ToInt32(Console.ReadLine());

                            Velo velobase = new Velo(id_mod);

                            Console.Write("\n Saisir le nombre d'exemplaires : ");
                            int quant = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Prix total pour " + quant +  " " + velobase.PrixUnitaire + " : " + velobase.PrixUnitaire * quant+"euros");

                            Velo new_velo = new Velo(id_mod, quant);



                            #region Verif stock et commande fournisseur
                           
                            

                            if (quant > velobase.Quantite)
                            {
                                Console.WriteLine(" La quantité en stock est insuffisante.");

                            }
                            else
                            {
                                
                                Commande com = new Commande(DateTime.Now.Date, dateLivrai, client, new_velo);
                                com.InsertionBDD();
                                //com.MajTotAchatClientBoutique();
                                com.MajSock();
                                Console.WriteLine("Commande validée.");
                            }
                            #endregion

                            Console.WriteLine("\n\nVoulez vous commander un autre vélo ?  1-Oui 2-NON");
                            Console.WriteLine(" 1. Oui");
                            Console.WriteLine(" 2. Non");
                            int choix2 = Convert.ToInt32(Console.ReadLine());
                            while (choix2 != 1 && choix2 != 2)
                            {
                                Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 ou 2)");
                                choix2 = Convert.ToInt32(Console.ReadLine());
                            }
                            if (choix2 == 2)
                            {
                                continuer = false;
                            }


                        }

                        
                        break;
                    }
                    
                    //Vente de pièces 
                case 2:
                    {
                        Console.Clear();
                        double prixAchat = 0;
                        double prixTotAchat = 0;
                        Piece piece = null;
                        bool condition = true;
                        DateTime dateLivrai = DateTime.Now.Date.AddDays(3);
                        while (condition)
                        {
                            #region Ouverture de connexion
                            MySqlConnection maConnexion = ouvrir_connexion();
                            #endregion

                            #region Selection
                            string requete = "select * from piece_rechange order by num_piece";
                            MySqlCommand command1 = maConnexion.CreateCommand();
                            command1.CommandText = requete;

                            MySqlDataReader reader = command1.ExecuteReader();

                            string[] valueString = new string[reader.FieldCount];
                            Console.WriteLine("   ID     |   Prix   |   Stock  | Date entrée |Date sortie | Nom    ");
                            Console.WriteLine("--------------------------------------------------------------------");
                            while (reader.Read())
                            {
                                string id_piece = (string)reader["num_piece"];
                                string prix_piece = Convert.ToString((int)reader["prix_piece"]);
                                string nom_piece = (string)reader["description_piece"];
                                string stock_p = Convert.ToString((int)reader["stock_piece"]);
                                string date_intro = Convert.ToString((DateTime)reader["date_intro_piece"]).Split(' ')[0];
                                string date_sortie;
                                if (string.IsNullOrEmpty(reader["date_disco_piece"].ToString()))
                                    date_sortie = "NULL";
                                else
                                {
                                    date_sortie = Convert.ToString((DateTime)reader["date_disco_piece"]).Split(' ')[0];
                                }

                                //Affichage de la table 
                                string[] tab_donnee = new string[6];
                                tab_donnee[0] = id_piece;
                                tab_donnee[1] = nom_piece;
                                tab_donnee[2] = prix_piece;
                                tab_donnee[3] = stock_p;
                                tab_donnee[4] = date_intro;
                                tab_donnee[5] = date_sortie;
                                for (int i = 0; i < tab_donnee.Length; i++)
                                {
                                    string vide = " ";
                                    for (int j = 0; j < 7 - tab_donnee[i].Length; j++)
                                    {
                                        vide = vide + " ";
                                    }
                                    if (i != 1)
                                    {
                                        Console.Write(" " + tab_donnee[i] + vide + " |");
                                    }

                                }
                                Console.WriteLine(" " + tab_donnee[1]);



                            }
                            reader.Close();
                            command1.Dispose();
                            #endregion

                            Console.WriteLine("Saisir l'ID de la pièce souhaitée : ");
                            string idp = Console.ReadLine();

                            Console.WriteLine("\nVous souhaitez faire une commande de : ");
                            #region Ouverture de connexion

                            MySqlConnection maConnexion2 = ouvrir_connexion();
                            #endregion
                            #region Selection
                            string requete2 = "SELECT * FROM piece_rechange where num_piece='" + idp + "';";
                            MySqlCommand command2 = maConnexion2.CreateCommand();
                            command2.CommandText = requete2;

                            MySqlDataReader reader2 = command2.ExecuteReader();

                            string[] valueString2 = new string[reader2.FieldCount];
                            while (reader2.Read())
                            {
                                string nomPiece = (string)reader2["description_piece"];
                                int stockPiece = (int)reader2["stock_piece"];
                                DateTime intromarche = (DateTime)reader2["date_intro_piece"];
                                prixAchat = prixAchat + (int)reader2["prix_piece"];

                                Console.WriteLine(" "  + idp + " Pièce: " + nomPiece + "     Sortie: " + intromarche);

                            }
                            reader2.Close();
                            command2.Dispose();
                            #endregion

                            

                            Console.WriteLine("\n\nSaisir la quantité");
                            int exemplaire = Convert.ToInt32(Console.ReadLine());
                            while (exemplaire < 1)
                            {
                                Console.WriteLine("Saisir une valeure positive");
                                exemplaire = Convert.ToInt32(Console.ReadLine());
                            }
                            prixTotAchat = prixAchat * exemplaire;
                       
                            piece = new Piece(idp, exemplaire);



                            #region Verif stock et commande fournisseur
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
                            #region Selection
                            string requete3 = "SELECT stock_piece FROM velomax.piece_rechange where num_piece='" + idp + "';";
                            MySqlCommand command3 = maConnexion3.CreateCommand();
                            command3.CommandText = requete3;

                            MySqlDataReader reader3 = command3.ExecuteReader();

                            string[] valueString3 = new string[reader3.FieldCount];
                            int stock = 0;
                            while (reader3.Read())
                            {
                                stock = (int)reader3["stock_piece"];
                            }
                            reader3.Close();
                            command3.Dispose();
                            #endregion

                            if (exemplaire > stock)
                            {
                                Console.WriteLine(" Stock insuffisant.");
                                
                            }
                            else
                            {
                                Commande com = new Commande(DateTime.Now.Date, dateLivrai, client, piece);
                                com.InsertionBDD();
                                //com.MajTotAchatClientBoutique();
                                com.MajSock();
                                Console.WriteLine(" Commande effectée.");

                            }
                            #endregion

                            Console.WriteLine("\n\nVoulez vous commander une autre pièce ? ");
                            Console.WriteLine(" 1. Oui, continuer");
                            Console.WriteLine(" 2. Non");
                            int choix2 = Convert.ToInt32(Console.ReadLine());
                            while (choix2 != 1 && choix2 != 2)
                            {
                                Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 ou 2)");
                                choix2 = Convert.ToInt32(Console.ReadLine());
                            }
                            if (choix2 == 2)
                            {
                                condition = false;
                            }
                        }

                        
                        break;
                    }
                    
                    
                    
                    
            }
        }

        //Demande les informations d'un fournisseur puis l'ajoute dans la BDD
        static Fournisseur CreationFournisseur()
        {
            Fournisseur f = null;
            Console.WriteLine("Entrez le numéro de Siret du fournisseur: ");
            int nSiret = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrez le Nom du fournisseur: ");
            string nomFourni = Console.ReadLine();

            Console.WriteLine("Creation de l'adresse : \n");
            Adresse adresse = CreationAdresse();



            Console.WriteLine("Entrez le Nom du contact: ");
            string nomContact = Console.ReadLine();
            Console.WriteLine("Entrez le telephone du contact: ");
            string numtel = Console.ReadLine();
            Console.WriteLine("Entrez l'email du contact: ");
            string email = Console.ReadLine();

            Console.WriteLine("Entrez le libellé de l'entreprise : \n");
            Console.WriteLine(" 1. entreprise individuelle");
            Console.WriteLine(" 2. EIRL");
            Console.WriteLine(" 3. SARL");
            Console.WriteLine(" 4. EURL");
            Console.WriteLine(" 5. SAS");
            Console.WriteLine(" 6. SASU");
            Console.WriteLine(" 7. SA");
            Console.WriteLine(" 8. SNC");
            int rep = Convert.ToInt32(Console.ReadLine());
            string libelle = "";
            while (rep != 1 && rep != 2 && rep != 3 && rep != 4 && rep != 5 && rep != 6 && rep != 7 && rep != 8)
            {
                Console.WriteLine("Saisir l'une des valeurs ci-dessus (1 à 8");
                rep = Convert.ToInt32(Console.ReadLine());
            }
            switch (rep)
            {
                case 1:
                    {
                        libelle = "E.Ind";
                        break;
                    }
                case 2:
                    {
                        libelle = "EIRL";
                        break;
                    }
                case 3:
                    {
                        libelle = "SARL";
                        break;
                    }
                case 4:
                    {
                        libelle = "EURL";
                        break;
                    }
                case 5:
                    {
                        libelle = "SAS";
                        break;
                    }
                case 6:
                    {
                        libelle = "SASU";
                        break;
                    }
                case 7:
                    {
                        libelle = "SA";
                        break;
                    }
                case 8:
                    {
                        libelle = "SNC";
                        break;
                    }


            }

            f = new Fournisseur(nSiret, nomFourni, adresse.IdAdresse, libelle, email);
            f.InsertionBDD();
            return f;
        }

        //Affectue une commande de pièces auprès d'un fournissuer
        static void Approvisionnement()
        {
            //Choix de la pièce à commander
            Piece piece = null;
            AfficherPiece();
            
            Console.Write("\n Saisir l'ID de la pièce à commander : ");
            string idpiece = Console.ReadLine();
            Console.Write("\n saisir la quantité à commander : ");
            int quant = Convert.ToInt32(Console.ReadLine());

            piece = new Piece(idpiece, quant);



            Fournisseur f = null;

            Console.WriteLine("\n Saisir le type du fournisseur : ");
            Console.WriteLine(" 1. ancien fournisseur");
            Console.WriteLine(" 2. nouveau fournisseur");
            int rep = Convert.ToInt32(Console.ReadLine());
            switch (rep)
            {
                //Récupération d'un fournisseur précédent 
                case 1:
                    {
                        AfficherFournisseur();
                        Console.WriteLine("\n Saisir le nom du fournisseur : ");
                        string nomf =Console.ReadLine();
                        f = new Fournisseur(nomf);
                        break;
                    }
                //Création d'un nouveau fournisseur
                case 2:
                    {
                        f = CreationFournisseur();
                        break;
                    }
            }
            //Création de la commande
            DateTime datecommande = DateTime.Now.AddDays(3);
            CommandeFournisseur cf = new CommandeFournisseur(f.NumSiret, piece.IdPiece, piece.Quantite, datecommande );
            cf.InsertionBDD();
            cf.MajSock();

        }




        //--- #### Méthodes de suppression de données ####

        static void SupprimerCommande()
        {
            AfficherCommande();
            Console.Write("\n ==> Saisir le n° de la commande à supprimer : ");
            int cid = Convert.ToInt32(Console.ReadLine());

            Commande c = new Commande(cid);
            c.SuppressionBDD();
            Console.WriteLine("\n Commande n° " + cid + " supprimée.");
           
        }


        //--- #### Méthodes export ####
        //Export xml 

        /// <summary>
        /// Permet d'exporter en XML la table clients
        /// </summary>
        /// <param></param>
        static void ExportXML()
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
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT * FROM client ; ";
            MySqlDataReader reader = command.ExecuteReader();
            int idclient;
            string nom;
            int id_adresse;
            string idprogramme;

            Console.WriteLine("\n Export de la table  des client en xml... :\n");
            XmlDocument docXml = new XmlDocument();
            XmlElement racine = docXml.CreateElement("Clients");
            docXml.AppendChild(racine);
            XmlDeclaration xmldecl = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldecl, racine);
            while (reader.Read())
            {

                idclient = (int)reader["id_client"];
                nom = (string)reader["nom_client"];
                id_adresse = (int)reader["id_adresse"];
                if (string.IsNullOrEmpty(reader["num_programme"].ToString()))
                {
                    idprogramme = "NULL";
                }
                else
                {
                    idprogramme = Convert.ToString((int)reader["num_programme"]);
                }
                

                XmlElement IDClt = docXml.CreateElement("IDClient");
                IDClt.InnerText = idclient.ToString();
                racine.AppendChild(IDClt);

                XmlElement NOM = docXml.CreateElement("Nom");
                NOM.InnerText = nom;
                racine.AppendChild(NOM);

                XmlElement adresses = docXml.CreateElement("Adresse");
                adresses.InnerText = id_adresse.ToString();
                racine.AppendChild(adresses);

                XmlElement subs = docXml.CreateElement("Programme");
                subs.InnerText = idprogramme;
                racine.AppendChild(subs);

            }
            reader.Close();
            command.Dispose();

            docXml.Save("Individu.xml");
        }

        //Export Json  
        /// <summary>
        /// Exportation en Json de la table client dont l'abonnement  expire dans moins de 2 mois
        /// </summary>
        static void ExportJson()
        {
            string nomFichier = "AbonnementsClienst.json";

            StreamWriter writer = new StreamWriter(nomFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            jwriter.WriteStartObject();

            jwriter.WritePropertyName("Clients");
            jwriter.WriteStartArray();

            
            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //fermeture de "writer"
            jwriter.Close();
            writer.Close();
            Console.WriteLine("Fichier JSON : " + nomFichier + " créé ");

        }




        // --- ### Méthodes modifications ### 
        //Simule le montage d'un vélo en lui ajoutant des pièces
        static void MontageVelo(int idv)
        {

            Velo velobase = new Velo(idv);
            Console.WriteLine("\n Liste des pièces contenues dans le " + velobase.NomVelo + " :\n");
            velobase.AfficherPiecesDispo();
            #region recupération de la pièce qui va manquer en premier
            List<int> lquant = new List<int>();
            for (int i = 0; i < velobase.ListePieces.Count(); i++)
            {
                lquant.Add(velobase.ListePieces[i].Quantite);
            }
            int min = lquant.Min();
            #endregion
            Console.WriteLine(min);
            Console.Write(" Saisir la quantité à monter (max : " + min +  ") : ");
            int quant = Convert.ToInt32(Console.ReadLine());
            Velo new_velo = new Velo(idv, quant);
            if (min>=quant)
            {
                velobase.MajStock(quant);
                for(int i=0; i<velobase.ListePieces.Count; i++)
                {
                    velobase.ListePieces[i].MajStock(-quant);
                }
                Console.WriteLine("\n " + quant + " vélo(s) '" + velobase.NomVelo + "' monté(s).");
            }
            else
            {
                Console.WriteLine("Quantité de pièces insuffisantes. ");

            }


        }

        //Simule le démontage d'un vélo en retirant ses pièces 
        static void DemontageVelo(int idv)
        {

            Velo velobase = new Velo(idv);
            Console.WriteLine("\n Liste des pièces contenues dans le " + velobase.NomVelo + " :\n");
            velobase.AfficherPiecesDispo();
            Console.WriteLine(" Stock restant du vélo '" + velobase.NomVelo + "' : " + velobase.Quantite);

            Console.Write(" Saisir la quantité dé vélos à démonter (max : " + velobase.Quantite + ") : ");
            int quant = Convert.ToInt32(Console.ReadLine());
            Velo new_velo = new Velo(idv, quant);
            if (velobase.Quantite >= quant)
            {
                velobase.MajStock(-quant);
                for (int i = 0; i < velobase.ListePieces.Count; i++)
                {
                    velobase.ListePieces[i].MajStock(quant);
                }
                Console.WriteLine("\n " + quant + " vélo(s) '" + velobase.NomVelo + "' démonté(s).");
            }
            else
            {
                Console.WriteLine("Quantité de pièces insuffisantes. ");

            }


        }






        //Appel du menu statistiques
        static void InterfaceStats()
        {
            Console.Clear();
            Console.WriteLine("\n ------------------------ ° Menu Statistiques ° ------------------------ \n");
            #region Ouverture de connexion

            MySqlConnection maConnexion = ouvrir_connexion();
            #endregion

            Console.WriteLine("     Saisir les résultats à afficher ? \n\n");
            Console.WriteLine("     1. Quantités vendues\n");
            Console.WriteLine("     2. Stats Fidelio \n");
            Console.WriteLine("     3. Meilleurs clients \n");
            Console.WriteLine("     4. Moyennes \n");
            int choix = Convert.ToInt32(Console.ReadLine());
            while (choix != 1 && choix != 2 && choix != 3 && choix != 4 && choix != 5)
            {
                Console.WriteLine("Saisir l'un des numéros ci-dessus (1 à 5)");
                choix = Convert.ToInt32(Console.ReadLine());
            }

            switch (choix)
            {
                case 1:
                    {

                        Console.WriteLine("\n 1. Classment des vélos les plus vendus\n");
                        Console.WriteLine(" 2. Classement des pièces les plus vendues\n");
                        int choixQ = Convert.ToInt32(Console.ReadLine());

                        if (choixQ == 1)
                        {
                            MySqlCommand command = maConnexion.CreateCommand();
                            command.CommandText = "select nom,num_modele, sum(quantite_modele) from vente_modele natural join modele group by num_modele order by quantite_modele desc; ";

                            MySqlDataReader reader;
                            reader = command.ExecuteReader();
                            int idmodele = 0;
                            int sexemplaire = 0;
                            string nommodele;
                            Console.WriteLine(" Quantité venue | ID vélo |     Nom vélo   " );
                            while (reader.Read())
                            {
                                nommodele = (string)reader.GetString(0);
                                idmodele = (int)reader.GetInt32(1);
                                sexemplaire = (int)reader.GetInt32(2);
                                Console.WriteLine(" " + sexemplaire + "        | " +  idmodele + "   |      " + nommodele );
                            }
                            command.Dispose();
                        }
                        else
                        {
                            MySqlCommand command = maConnexion.CreateCommand();
                            command.CommandText = "select description_piece,num_piece, sum(quantite_modele) from vente_piece natural join piece_rechange group by num_piece order by quantite_piece desc; ";

                            MySqlDataReader reader;
                            reader = command.ExecuteReader();
                            string idpiece = "";
                            int sexemplaire = 0;
                            string nompiece;
                            Console.WriteLine(" Quantité venue | ID pièce |     Nom pièce   ");
                            while (reader.Read())
                            {
                                nompiece = (string)reader.GetString(0);
                                idpiece = (string)reader.GetString(1);
                                sexemplaire = (int)reader.GetInt32(2);
                                Console.WriteLine(" " + sexemplaire + "        | " + idpiece + "   |      " + nompiece);
                            }
                            command.Dispose();
                        }

                        break;
                    }


                case 2:
                    {
                        Console.WriteLine("\n\n  Informations sur les programmes fidélio : \n\n ");

                        Console.WriteLine("   Nom du programme   | Nombre d'adhérents |   bénéfices ");
                        Console.WriteLine("----------------------------------------------------------");
                        MySqlCommand command2 = maConnexion.CreateCommand();
                        command2.CommandText = "select description_prog, count(*), cout_programme* count(*) as benefices from client natural join prog_fidelite group by num_programme order by benefices desc;" ;

                        MySqlDataReader reader2;
                        reader2 = command2.ExecuteReader();
                        string nomprogramme = "";
                        while (reader2.Read())
                        {
                            nomprogramme = reader2.GetString(0);
                            int adherents = reader2.GetInt32(1);
                            int benefices = reader2.GetInt32(2);
                            Console.WriteLine("  " + nomprogramme + "       |   " + adherents + "   |    " + benefices+"euros");
                        }
                        command2.Dispose();

                        break;
                    }

                case 3:
                    {
                        
                        //Sélection du client ayant le plus dépensé
                        MySqlCommand command3 = maConnexion.CreateCommand();
                        command3.CommandText = "select * from client natural join commande group by id_client having montant >= ALL(select montant from client natural join commande group by id_client);";

                        MySqlDataReader reader3;
                        reader3 = command3.ExecuteReader();
                        int idClient;
                        Console.WriteLine(" Client ayant le plus dépensé d'argent : \n");
                        while (reader3.Read())
                        {
                            idClient = Convert.ToInt32(reader3.GetString(0));
                            int depense_totale = Convert.ToInt32(reader3.GetString(8));
                            Client c = new Client(idClient);

                            Console.WriteLine(c.ToString() + "==> Total Achat: " + depense_totale);
                        }
                        command3.Dispose();

                        break;
                    }

                    //Affichage des moyennes
                case 4:
                    {
                        Console.Write("\n Moyenne du montant des commandes : ");
                        MySqlCommand command4 = maConnexion.CreateCommand();
                        command4.CommandText = "select avg(montant) from commande;";
                        MySqlDataReader reader4;
                        reader4 = command4.ExecuteReader();
                        double moyenne;
                        while (reader4.Read())
                        {
                            moyenne = Convert.ToDouble(reader4.GetString(0));
                            Console.WriteLine(moyenne);
                        }
                        command4.Dispose();

                        Console.Write("\n Nombre moyen de vélos commandés : ");
                        MySqlCommand command5 = maConnexion.CreateCommand();
                        command5.CommandText = "select avg(quantite_modele) from vente_modele;";
                        MySqlDataReader reader5;
                        reader5 = command5.ExecuteReader();
                        double m2;
                        while (reader5.Read())
                        {
                            m2 = reader4.GetDouble(0);
                            Console.WriteLine(m2);

                        }
                        command4.Dispose();

                        Console.Write("\n Nombre moyen de pièces commandées : ");
                        MySqlCommand command6 = maConnexion.CreateCommand();
                        command6.CommandText = "select avg(quantite_piece) from vente_piece;";
                        MySqlDataReader reader6;
                        reader6 = command6.ExecuteReader();
                        double m6;
                        while (reader6.Read())
                        {
                            m6 = reader4.GetDouble(0);
                            Console.WriteLine( m6);

                        }
                        command4.Dispose();


                        break;
                    }

            }
        }
        
        //Appel du menu démo 
        static void InterfaceDemo()
        {
            Console.Clear();
            Console.WriteLine("------------------------ ° VéloMax - Démo ° ------------------------\n\n\n\n");



            Console.WriteLine(" > Entrer une touche pour continuer.\n\n");
            Console.ReadKey();

            #region Affichage du nombre de clients
            Console.Write("     Nombre de clients : ");
            #region Ouverture de connexion
            MySqlConnection maConnexion2 = ouvrir_connexion();
            #endregion

            int nbclients = 0;
            string requete2 = "SELECT count(id_client) FROM client;";
            #region Selection nombre de clients
            MySqlCommand command2 = maConnexion2.CreateCommand();
            command2.CommandText = requete2;

            MySqlDataReader reader2 = command2.ExecuteReader();

            string[] valueString2 = new string[reader2.FieldCount];
            while (reader2.Read())
            {            
                nbclients = reader2.GetInt32(0);
                
            }
            reader2.Close();
            command2.Dispose();
            #endregion
            Console.WriteLine(nbclients);

            Console.WriteLine("\n\n > Entrer une touche pour continuer.\n\n");
            Console.ReadKey();
            #endregion

            #region Affichage du nom des clients et du montant
            Console.WriteLine("\n\n Liste des clients en fonction de leur somme dépensée : ");
            Console.WriteLine("\n\n  Montant |  Nom   ");
            Console.WriteLine(" ------------------------");
            #region Ouverture de connexion
            MySqlConnection maConnexion3 = ouvrir_connexion();
            #endregion
           
            string requete3 = "select nom_client,sum(montant) from client natural join commande group by id_client order by montant desc";
            #region Selection nombre de clients
            MySqlCommand command3 = maConnexion3.CreateCommand();
            command3.CommandText = requete3;

            MySqlDataReader reader3 = command3.ExecuteReader();

            string[] valueString3 = new string[reader3.FieldCount];
            string nom_client = "";
            int montant = 0;
            while (reader3.Read())
            {

                nom_client = reader3.GetString(0);
                montant = reader3.GetInt32(1);


                Console.WriteLine("   " + montant + "  |  " + nom_client);

            }
            reader3.Close();
            command3.Dispose();
            #endregion
            

            Console.WriteLine("\n\n > Entrer une touche pour continuer.\n\n");
            Console.ReadKey();
            #endregion

            #region Affichage des produits ayant une quanité en stock<=2

            Console.WriteLine("\n\n Affichage des produits ayant une quantité en stock <=2 : ");
            Console.WriteLine("\n Stock |  Nom vélo   ");
            Console.WriteLine(" ------------------------");
            #region Ouverture de connexion
            MySqlConnection maConnexion4 = ouvrir_connexion();
            #endregion

            string requete4 = "select nom, stock_velo from modele where stock_velo<=2;";
            #region Selection nombre de clients
            MySqlCommand command4 = maConnexion4.CreateCommand();
            command4.CommandText = requete4;

            MySqlDataReader reader4 = command4.ExecuteReader();

            string[] valueString4 = new string[reader4.FieldCount];
            string nom_velo = "";            
            int stockvelo = 0;
            while (reader4.Read())
            {

                nom_velo = reader4.GetString(0);
                stockvelo = reader4.GetInt32(1);


                Console.WriteLine("   " + stockvelo + "  |  " + nom_velo);

            }
            reader4.Close();
            command4.Dispose();
            #endregion

            Console.WriteLine("\n\n Stock |  ID pièce   ");
            Console.WriteLine(" ------------------------");
            #region Ouverture de connexion
            MySqlConnection maConnexion5 = ouvrir_connexion();
            #endregion

            string requete5 = "select num_piece, stock_piece from piece_rechange where stock_piece<=2;";
            #region Selection nombre de clients
            MySqlCommand command5 = maConnexion5.CreateCommand();
            command5.CommandText = requete5;

            MySqlDataReader reader5 = command5.ExecuteReader();

            string[] valueString5 = new string[reader4.FieldCount];
            string idpiece = "";
            int stock_piece = 0;
            while (reader5.Read())
            {

                idpiece = reader5.GetString(0);
                stockvelo = reader5.GetInt32(1);


                Console.WriteLine("   " + stock_piece + "  |  " + idpiece);

            }
            reader5.Close();
            command5.Dispose();
            #endregion

            Console.WriteLine("\n\n > Entrer une touche pour continuer.\n\n");
            Console.ReadKey();
            #endregion

            #region Nombres de pièces et/ou vélos fournis par fournisseur.

            Console.WriteLine("\n Nombres de pièces et/ou vélos fournis par fournisseur : ");

            #region Ouverture de connexion
            MySqlConnection maConnexion6 = ouvrir_connexion();
            #endregion

            string requete6 = "select nom_fournisseur, sum(quantite) from fournisseur natural join approvisionner group by siret order by quantite;";

            #region Selection fournisseurs
            MySqlCommand command6 = maConnexion4.CreateCommand();
            command6.CommandText = requete6;

            MySqlDataReader reader6 = command6.ExecuteReader();

            string[] valueString6 = new string[reader6.FieldCount];
            string nom_f = "";
            int quant = 0;
            Console.WriteLine("\n Quantité |  Nom Fournisseur   ");
            Console.WriteLine(" --------------------------------");
            while (reader6.Read())
            {

                nom_f = reader6.GetString(0);
                quant = reader6.GetInt32(1);


                Console.WriteLine("   " + quant + "  |  " + nom_f);

            }
            reader6.Close();
            command6.Dispose();
            #endregion
            Console.WriteLine("\n\n > Entrer une touche pour continuer.\n\n");
            Console.ReadKey();
            #endregion

            #region Export XML/Json
            ExportXML();
            #endregion



        }

        //Ouvre une connection à la BDD
        static MySqlConnection ouvrir_connexion()
        {
            #region Ouverture de connexion

            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=VeloMax;" +
                                         "UID=root;PASSWORD=root";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                //return ;
            }
            #endregion
            return maConnexion;
        }
        



        static void Main(string[] args)
        {


            Interface();
           
            Console.ReadLine();


           
        }
    }
}