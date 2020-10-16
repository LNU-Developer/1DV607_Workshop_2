# BoatClub (Workshop 2)
The grade 2 assignment for Workshop 2 in 1DV607 at LNU.

Created by: Filippa Jakobsson (fj222nq), Rickard Marjanovic (rm222jx) and Pernilla Göth (pg222jx)

## Instructions

### Installation
- Download/pull this repo
- Install [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer).

### General use
- Open a command prompt. Please not that the command prompt needs to be interactive (e.g. Windows Command promt).
- Navigate to the folder where you downloaded the repository
- Run command `dotnet run`.
- Interact with the menu and use the application.

### General use (alternative)
- Download file from [here](https://drive.google.com/file/d/1iE7BXSN7vws8y_U_ofAeVWKtI8Yv9kpu/view?usp=sharing)
- Extract file "BoatClub.zip".
- Run file "workshop_2.exe".
- Enjoy!

### Information
- For the UML class diagram we have not included any properties, since this is not deemed as important as the other information included.
- Furthermore in the UML class diagram we have also included dependencies to boat/member even though these om some cases only are used as a part of a forEach.
- For the sequence diagrams we have assumed the actor has selected the menu items for the respective sequence.
- Boat and Member needed to be constructorless classes in order to work with Firestores attribute functionality (easy serialization/convertation).
- Added error handling functionality in code and displayed this in the UML even though this was not a requirement for grade 2.