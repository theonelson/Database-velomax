# Database-velomax

Date : 18/05/2022

## INTRODUCTION

### Problem

The goal of this project was to implement a complete data management tool for a bike store "Velomax". To do that, we created a database in SQL, and automated it in C#. We used Visual Studio 2019 and MySQL Workbench.

### How to use

- To run the app, we can run the .exe file in the Debug folder (Projet_BDD_code\bin\Debug\Projet_BDD_code.exe), a little menu appears in the console where we can select different functionnalities. We navigate through the meny by typing the number of the action that we want to do

- The SQL file "velomax" can be found in the Debug folder. The connection identifiers to the database in c# are root/root.

## 1. CREATION OF THE DATABASE (SQL)

The problem statement made possible several interpretations regarding the establishment of the database tables. We decided, for example, to make a single table grouping individual and business customers, or to make a table grouping the addresses of customers and suppliers, because we thought that this configuration was the most efficient.

We have defined the following tables:
- modele
- piece_rechange
- client
- prog_fidelite
- addresse
- type_client (individual or company)
- fournisseur
- commande (main tables) 
- affectation 
- approvisionner 
- vente_modele
- vente_piece (tables that derive from associations) .

![Modele EA VeloMax](https://user-images.githubusercontent.com/115212826/224854535-3340042e-c9d8-4843-be1b-cb44dd035e11.png)
*(the foreign keys does not appear in the UML schema)*

## 2. AUTOMATING THE OPERATIONS (C#)

### 2.1 Interface

We made a console interface composed of a menu with 6 entries, which allow a user who does not have access to the database to display, add, delete or update a specific information (Sales, Material, Customers, Suppliers), as well as the demo part.

![image](https://user-images.githubusercontent.com/115212826/224858040-2f3612ab-0e9c-4c5b-a1ca-e7ac15b5c55b.png)

### 2.2 Functionalities 

We have designed this code to minimize human intervention on the base. That's why we added some features like bike assembly/disassembly, which automatically updates the parts and bike inventory when parts are used to assemble bikes or, conversely, are recovered from a bike. 

### 2.3 Structure

In order to manipulate the data more easily, we have created the following classes: Bike, Part, Customer, Order, OrderSupplier, Supplier and Address.

The interest of these classes is first to link more easily some information (For example the class Velo contains an attribute list_pieces, which allows to access and manipulate easily the parts which compose it).

The classes we have created generally have several constructors, which allows to quickly retrieve some data from an entity from input data, but also to create entities of different types from the same class. For example "Velo v1 = new Velo(ID)" retrieves the bike in the database with its information (including its stock), while "Velo v2 = new Velo(ID, quantity)" represents a bike with a desired quantity (in the case of a purchase for example).

The classes also contain methods to modify the database more easily. All methods related to the database are called from the Interface() method in the Program class.


