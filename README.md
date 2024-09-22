## Introduction

Gestion Atelier is a project for the intervention management in the garage.

## Requirements

+ Platform: Windows
+ SqlServer >= 2014
+ MS Visual Studio = 15 (2017)

### Optional :

+ Microsoft Visual Studio Installer Projects in Visual Studio

## Getting Started

### Guide for the database

+ The folder "sql/base" content the requests for create the database : "CreateDB.sql"
+ The folder "sql/old" content the requests for the full data in database : "TDB_xxx.sql"
+ The folder "sql/update" content the last requests for update the database.

### Installation & configuration

Open the file "App.config" in "GestionAtelier" and define the different value for the access to database :

```
server : access route for the database
database : name
uid : login
password
```

### Optional :

```
domain : for the database
```

Define the value for close the application for the userProfile "Encodeur" :

```
TimeHourCloseApplication : default value : 0
TimeMinuteCloseApplication : default value : 1
TimeSecondeCloseApplication : default value : 0
```

Define the time for open the application (Load the different data before open) :

```
TimerOpenApplicationInSecondes : default value : 5 (not modified)
```

Define the directory for save the files "intervention" with a barcode generate by the application :

```
DirectoryBarcodeIntervention
```

Define the format for save the files "intervention" with a barcode :

```
FormatBarcodeIntervention : default value : "pnj"
```

Define the directory for save the pictures for each intervention :

```
DirectoryPicturesIntervention
```

### Folder invoice

The invoices are saved by default in the current directory of the application :

```
"CurrentDirectory" + \Invoice\PrintDocument\
"CurrentDirectory" + \Invoice\SaveDocument\
```

File name :

```
Facture_xxx (Number invoice)
FactureDetail_xxx (Number invoice)
```

## Reporting issues

Issues can be reported via the [Github issue tracker](https://github.com/Wylath/GestionAtelier/issues).


## Copyright

Copyright Wylath © 2018-2019


## Authors &amp; Contributors

Wylath
