# Krypto

![wallpaper](/logo/wallpapers/wallpaper-krypto.png)

## Background
Krypto is a simple mobile-first website to track upcoming cryptocurrencies. It works best for small groups. Currencies can be added and rated by other members.

This website was made in the “golden days” of cryptocurrency trading as a small side project.

Current rates of cryptocurrencies are loaded from the coinmarketcap.com API.


## Directories

    - database    Database table create queries
    - logo        Krypto logo files
    - screenshots Screenshots
    - src         Project source code 

## Development setup

### Requirements

#### Microsoft Visual Studio 2017

    https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=Community&rel=15
    
#### Microsoft MSSQL Server (Express)

    https://www.microsoft.com/nl-nl/sql-server/sql-server-editions-express

#### Microsoft MSSQL Server Management Studio

    https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

### Event logger

Add the event log source using PowerShell:

    PS> New-EventLog -LogName "Application" -Source "Krypto"

### Database

The required database tables can be created with the SQL scripts in the `database` directory of this repository.

## Screenshots

### Home screen
![screenshot-a](screenshots/screenshot-a.png)

### Currency information and voting
![screenshot-b](screenshots/screenshot-b.png)

### Invite new members
![screenshot-c](screenshots/screenshot-c.png)

### Menu
![screenshot-d](screenshots/screenshot-d.png)