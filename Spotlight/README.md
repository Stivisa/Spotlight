# Spotlight
### Podesavanja pre pokretanja
  - Fajl *appseting.json* potrebno izmeniti connection string ka bazi, polja *User Id* i *Password*
  - U istom fajlu izmeniti AppEmail polja sa validnim gmail adresom, salje mejl korisnicima sa confirmation link (nije neophodno jer se confirmation link ispisuje u konzoli)
  - Iz glavnog foldera (gde je i fajl *Startup.cs*) izrvsiti naredbu: **dotnet ef database update** (kreira db na osnovu postojece migracije)
  - nakon registracije potrebno je verifikovati email. Confirmation link se ispisuje i u konzoli, za slucaj da je unet nepostojeci email (npr. example@example123.com)
