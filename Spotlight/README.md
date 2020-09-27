# Spotlight
### Podesavanja pre pokretanja	
1. Fajl *appseting.json* potrebno izmeniti connection string ka bazi, polja *User Id* i *Password*

2. U istom fajlu izmeniti AppEmail polja sa validnim gmail adresom, salje mejl korisnicima sa confirmation link. Dodatno u google account dozvoliti less secure app access. (unos gmail podataka nije neophodno jer se confirmation link ispisuje u konzoli)

3. Iz glavnog foldera (gde je i fajl *Startup.cs*) izrvsiti naredbe:
<pre>dotnet ef database update --context AppIdentityDbContext</pre>
<pre>dotnet ef database update --context ListingDbContext</pre>
<pre>dotnet ef database update --context NewsDbContext</pre>

4. Nakon registracije potrebno je verifikovati email. Confirmation link se ispisuje i u konzoli, za slucaj da je unet nepostojeci email (npr. example@example123.com)
