drop database if exists VeloMax;
create database if not exists VeloMax;
use VeloMax;


drop table if exists modele;
create table if not exists modele(
num_modele int primary key, 
nom varchar(20),
grandeur varchar(20),
prix_unitaire int,
ligne_produit varchar(20),
date_intro date,
date_disco date,
stock_velo int);


drop table if exists piece_rechange;
create table if not exists piece_rechange(
num_piece varchar(20) primary key, 
stock_piece int,
date_intro_piece date,
date_disco_piece date,
prix_piece int,
description_piece varchar(40));


drop table if exists prog_fidelite;
create table if not exists prog_fidelite(
num_programme int not null primary key,
description_prog varchar(40),
cout_programme float,
duree int, 
rabais float);

drop table if exists adresse;
create table if not exists adresse(
id_adresse int primary key,
rue varchar(25),
ville varchar(25),
code_postal int,
province varchar(25));

drop table if exists type_client;
create table if not exists type_client(
id_type int not null primary key,
type varchar(25));

drop table if exists client;
create table if not exists client(
id_client int not null primary key,
nom_client varchar(20),
id_adresse int not null,
id_type int not null,
num_programme int,
foreign key(id_adresse) references adresse(id_adresse),
foreign key(id_type) references type_client(id_type),
foreign key(num_programme) references prog_fidelite(num_programme));

DROP TABLE IF EXISTS fournisseur ;  
CREATE TABLE fournisseur(
siret int NOT NULL primary key, 
nom_fournisseur varchar(20), 
contact varchar(20), 
statut varchar(20), 
id_adresse int NOT NULL,
foreign key(id_adresse) references adresse(id_adresse)); 

drop table if exists commande;
create table if not exists commande(
num_commande int not null primary key,
date_commande date,
date_livraison date,
id_client int,
montant double,
foreign key(id_client) references client(id_client));


-- Associations


drop table if exists affectation;
create table if not exists affectation(
num_piece varchar(20),
num_modele int,
primary key (num_piece, num_modele),
foreign key (num_piece) references piece_rechange(num_piece),
foreign key (num_modele) references modele(num_modele));

drop table if exists aprovisionner;
create table if not exists approvisionner(
id_appro int not null,
num_piece varchar(20) not null,
siret int,
date_appro date,
quantite int,
primary key(id_appro),
foreign key (num_piece) references piece_rechange(num_piece),
foreign key (siret) references fournisseur(siret));

drop table if exists vente_modele;
create table if not exists vente_modele(
num_commande int not null primary key, 
num_modele int not null,
quantite_modele int,
id_client int,
foreign key(id_client) references client(id_client),
foreign key (num_modele) references modele(num_modele));

drop table if exists vente_piece;
create table if not exists vente_piece(
num_commande int not null primary key,
num_piece varchar(20),
quantite_piece int,
id_client int,
foreign key(id_client) references client(id_client),
foreign key (num_piece) references piece_rechange(num_piece));



INSERT INTO modele values (101, 'Kilimandjaro', 'Adultes', 569, 'VTT','2020-01-01',null ,10);
INSERT INTO modele values (102 ,'NorthPole', 'Adultes', 329 ,'VTT', '2004-06-30','2020-01-01',10);
INSERT INTO modele values(103 ,'MontBlanc', 'Jeunes', 399,  'VTT','2002-02-06', null,25);
INSERT INTO modele values(104 ,'Hooligan', 'Jeunes', 199, 'VTT','2007-05-21',null,20);
INSERT INTO modele values(105 ,'Orleans', 'Hommes', 229, 'Velo de course', '2008-04-10',null,2);
INSERT INTO modele values(106 ,'Orleans', 'Dames', 229, 'Vélo de course','2008-04-10',null,0);
INSERT INTO modele values(107 ,'BlueJay', 'Hommes', 349, 'Vélo de course','2015-08-28',null,70);
INSERT INTO modele values(108 ,'BlueJay' ,'Dames' ,349 ,'Vélo de course','2015-08-28',null,46);
INSERT INTO modele values(109 ,'Trail Explorer' ,'Filles', 129, 'Classique','2000-12-05' ,null,55);
INSERT INTO modele values(110 ,'Trail Explorer ','Garçons' ,129,'Classique', '2000-12-05',null ,49);
INSERT INTO modele values(111 ,'Night Hawk', 'Jeunes' ,189, 'Classique','2013-05-18','2020-05-18',4);
INSERT INTO modele values(112, 'Tierra Verde ','Hommes' ,199 ,'Classique','1990-07-22','2005-01-20',8);
INSERT INTO modele values(113, 'Tierra Verde', 'Dames', 199 , 'Classique','1990-07-22','2005-01-20',12);
INSERT INTO modele values(114 ,'Mud Zinger I' ,'Jeunes' ,279 ,'BMX','2021-01-21',null,18);
INSERT INTO modele values(115 ,'Mud Zinger II', 'Adultes', 359, 'BMX','2021-01-21',null,30);

INSERT INTO prog_fidelite VALUES ('1', 'Fidélio', '15', '1', '5');
INSERT INTO prog_fidelite VALUES ('2', 'Fidélio Or', '25', '2', '8');
INSERT INTO prog_fidelite VALUES ('3', 'Fidélio Platine', '60', '2', '10');
INSERT INTO prog_fidelite VALUES ('4', 'Fidélio Max', '100', '3', '12');

INSERT INTO type_client VALUES ('1','particulier');
INSERT INTO type_client VALUES ('2','entreprise');

INSERT INTO adresse values ('50','14 rue Leonard de Vinci', 'Courbevoie', '92400','France');
INSERT INTO adresse values ('51','69 rue Jean Vilar', 'Besançon', '25000','France');
INSERT INTO adresse values ('52','96 avenue Voltaire', 'Macon', '71000','France');
INSERT INTO adresse values ('53','39 Square de la Couronne', 'Paris', '75002','France');
INSERT INTO adresse values ('54','99 Rue Hubert de Lisle', 'Lunel', '34400','France');

INSERT INTO client values ('1','Pierre','50','1','3');
INSERT INTO client VALUES
 ('2','Hella', '51','1',null ),
 ('3', 'Monique','52','1','2'),
 ('4','decathlon', '53','2', '3'),
 ('5','go sport', '54','2', '3');
 
 INSERT INTO piece_rechange values
('C32',10,'2018-06-02',null,50,'cadre'),
('C34',19,'2019-03-01',null,45,'cadre'),
('C76',100,'2001-12-12',null,30,'cadre'),
('C43',12,'2009-11-02',null,35,'cadre'),
('C44f',38,'2020-12-01',null,39,'cadre'),
('C43f',59,'2000-05-12','2019-08-01',43,'cadre'),
('C01',40,'2004-03-01',null,55,'cadre'),
('C02',43,'2010-01-01',null,33,'cadre'),
('C15',14,'2007-11-14',null,46,'cadre'),
('C87',18,'2013-03-13',null,47,'cadre'),
('C87f',20,'2013-03-13',null,45,'cadre'),
('C25',19,'2015-06-23',null,42,'cadre'),
('C26',9,'2016-02-05','2020-03-06',50,'cadre'),
('G7',200,'2020-04-12',null,25, 'guidon'),
('G9',3,'2012-03-13',null,20, 'guidon'),
('G12',18,'2018-02-28',null,20, 'guidon'),
('F3',100,'2016-04-14',null,12, 'freins'),
('F9',150,'2018-07-03',null,15, 'freins'),
('S88',26,'2015-04-21',null,22, 'selle'),
('S37',49,'2018-09-17',null,20, 'selle'),
('S35',17,'2016-02-14',null,25, 'selle'),
('S36',24,'2017-07-22',null,27, 'selle'),
('S34',4,'2001-01-02','2015-08-13',20, 'selle'),
('S87',12,'2016-10-12',null,24, 'selle'),
('S02',0,'1998-10-30','2007-06-03',36, 'selle'),
('S03',3,'2002-05-12',null,30, 'selle'),
('DV133',12,'2020-12-23',null,20,'dérailleur avant'),
('DV17',39,'2018-09-17',null,26,'dérailleur avant'),
('DV87',8,'2019-06-10',null,24,'dérailleur avant'),
('DV57',4,'2020-03-13',null,21,'dérailleur avant'),
('DV15',5,'2018-08-22',null,19,'dérailleur avant'),
('DV41',9,'2019-01-16',null,23,'dérailleur avant'),
('DV132',25,'2020-02-19',null,19,'dérailleur avant'),
('DR23',12,'2021-02-12',null,32,'dérailleur arrière'),
('DR52',16,'2020-09-15',null,21,'dérailleur arrière'),
('DR56',17,'2019-04-23',null,23,'dérailleur arrière'),
('DR76',0,'2020-02-11',null,27,'dérailleur arrière'),
('DR86',1,'2019-10-13',null,24,'dérailleur arrière'),
('DR87',13,'2019-12-15',null,25,'dérailleur arrière'),

('R1',40,'2019-02-11',null,18, 'roue avant'),
('R2',19,'2020-03-19',null,22, 'roue arrière'),
('R11',32,'2019-10-15',null,19, 'roue avant'),
('R12',10,'2020-03-16',null,22, 'roue arrière'),
('R18',17,'2019-08-18',null,18, 'roue arrière'),
('R19',23,'2020-06-14',null,21, 'roue avant'),
('R44',28,'2018-07-23',null,19, 'roue avant'),
('R45',13,'2018-11-12',null,18, 'roue avant'),
('R46',48,'2019-08-12',null,21, 'roue arrière'),
('R47',64,'2018-04-09',null,14, 'roue arrière'),
('R48',19,'2017-07-17','2019-03-16',16, 'roue avant'),
('R32',26,'2017-03-12','2019-05-11',14, 'roue arrière'),

('PA73',30,'2018-06-02',null,5, 'panier'),
('PA74',20,'2018-06-02',null,7, 'panier'),
('PA05',6,'2011-01-02',null,6, 'panier'),
('PA01',15,'2013-06-05','2018-01-02',9, 'panier'),
('O4',20,'2012-05-22',null,600,'ordinateur'),
('O2',50,'2013-03-29',null,750,'ordinateur');

INSERT INTO piece_rechange values ('P12',45,'2016-09-11','2020-04-21',45,'pédalier');
INSERT INTO piece_rechange values ('P15',32,'2018-11-19',null,50,'pédalier');
INSERT INTO piece_rechange values ('P1',3,'2017-10-02',null,65,'pédalier');
INSERT INTO piece_rechange values ('P34',24,'2017-05-02',null,40,'pédalier');
INSERT INTO piece_rechange values ('R10',35,'2010-09-06','2019-02-08',12,'réflecteurs');
INSERT INTO piece_rechange values ('R09',7,'2011-01-20',null,15,'réflecteurs');
INSERT INTO piece_rechange values ('R02',45,'2015-11-06','2018-06-22',10,'réflecteurs');

INSERT INTO affectation values ('G7',101);
INSERT INTO affectation values 
('G7',102),
('G7',103),
('G7',104),
('G9',105),
('G9',106),
('G9',107),
('G9',108),
('G12',109),
('G12',110),
('G12',111),
('G12',112),
('G12',113),
('G7',114),
('G7',115),

('S88',101),
('S88',102),
('S88',103),
('S88',104),
('S37',105),
('S35',106),
('S37',107),
('S35',108),
('S02',109),
('S03',110),
('S36',111),
('S36',112),
('S34',113),
('S87',114),
('S87',115),
 
('P12',101),
('P12',102),
('P12',103),
('P12',104),
('P34',105),
('P34',106),
('P34',107),
('P34',108),
('P1',109),
('P1',110),
('P15',111),
('P15',112),
('P15',113),
('P12',114),
('P12',115),

('F3',101),
('F3',102),
('F3',103),
('F3',104),
('F9',105),
('F9',106),
('F9',107),
('F9',108),
('F9',111),
('F9',112),
('F9',113),
('F3',114),
('F3',115),

('DV133',101),
('DV17',102),
('DV17',103),
('DV87',104),
('DV57',105),
('DV57',106),
('DV57',107),
('DV57',108),
('DV15',111),
('DV41',112),
('DV41',113),
('DV132',114),
('DV133',115),

('R02',105),
('R02',106),
('R02',107),
('R02',108),
('R09',109),
('R09',110),
('R10',111),
('R10',112),
('R10',113),

('DR56',101),
('DR87',102),
('DR87',103),
('DR86',104),
('DR86',105),
('DR86',106),
('DR87',107),
('DR87',108),
('DR23',111),
('DR76',112),
('DR76',113),
('DR52',114),
('DR52',115),

('O2',101),
('O2',103),
('O4',107),
('O4',108),

('PA01',109),
('PA05',110),
('PA74',111),
('PA74',112),
('PA73',113);

INSERT INTO affectation values('C32',101);
INSERT INTO affectation values('C34',102);
INSERT INTO affectation values('C76',103);
INSERT INTO affectation values('C76',104);
INSERT INTO affectation values('C43',105);
INSERT INTO affectation values('C44f',106);
INSERT INTO affectation values('C43',107);
INSERT INTO affectation values('C43f',108);
INSERT INTO affectation values('C01',109);
INSERT INTO affectation values('C02',110);
INSERT INTO affectation values('C15',111);
INSERT INTO affectation values('C87',112);
INSERT INTO affectation values('C87f',113);
INSERT INTO affectation values('C25',114);
INSERT INTO affectation values('C26',115);

INSERT INTO affectation values
('R46',101),
('R47',102),
('R47',103),
('R32',104),
('R18',105),
('R18',106),
('R18',107),
('R18',108),
('R2',109),
('R2',110),
('R12',111),
('R12',112),
('R12',113),
('R47',114),
('R47',115);

INSERT INTO affectation values
('R45',101),
('R48',102),
('R48',103),
('R12',104),
('R19',105),
('R19',106),
('R19',107),
('R19',108),
('R1',109),
('R1',110),
('R11',111),
('R11',112),
('R11',113),
('R44',114),
('R44',115);

insert into commande values
(1000,'2022-04-01','2022-05-01', 1, 1500),
(1001,'2019-02-20','2019-03-20',2, 270),
(1002,'2018-06-18','2018-06-20',5, 760),
(1003,'2022-06-18','2022-07-07',4, 80),
(1004,'2022-07-07','2022-07-07',2, 55);

insert into vente_modele values(1000, 101,10,2);
insert into vente_modele values(1001, 115,8,1);

INSERT INTO vente_piece VALUES ('1002','G7',10,5);
INSERT INTO vente_piece VALUES ('1003','R11',25,4);
INSERT INTO vente_piece VALUES ('1004','C87',8,2);


INSERT INTO fournisseur  VALUES ('123568','VeloVente', 'velovente@gmail.com', 'SARL', '51'),
(123569, 'ByciclSeller','adresse@gmail.com', 'SARL ', 52);
INSERT INTO approvisionner VALUES (1000,'G7','123568', '2022-05-20', 10);
INSERT INTO approvisionner VALUES (1001,'DR86','123568', '2022-06-20', 30);
INSERT INTO approvisionner VALUES (1003,'R47','123569', '2022-06-20', 15);



-- select count(id_client) from client;

-- select nom_client,sum(montant) from client natural join commande group by id_client order by montant desc;

-- select nom_fournisseur, sum(quantite) from fournisseur natural join approvisionner group by siret;





