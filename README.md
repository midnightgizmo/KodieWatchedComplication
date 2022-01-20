# Kodi TV watched list extractor

Gets all the tv shows from the database with information about if a show has or has not been watched. 

Also allows extracted data to be saved to a json file which can then be loaded back at a later date.

# How to use project

Within the root of the project is an appsettings.json (It also has appsettings.Development.json & appsettings.Production.json). The values in this file will need to be changed to access your database. If running in debug mode change the appsettings.Development.json file. If running in Release mode change the appsettings.Production.json file.

Below is an example of an appsettings.json file

``` json
  "Database": {
    "ServerLocation": "localhost",
    "dbUser": "SomeUser",
    "dbPassword": "Password",
    "DatabaseName" :  "myvideos123"
  }
```

