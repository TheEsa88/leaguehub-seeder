\# Ohje agentille



\## Lue ensin



1\. `specs/`-kansiosta se spec, jota muutos koskee. Jos speciä ei ole, kirjoita se ensin ja pysähdy.

2\. `README.md`:n haaraosio. Muutos lähtee developmentista feature-haarassa.



\## Rajat



\- `LeagueHub.Seeder` ei viittaa Domainiin, Applicationiin eikä Infrastructureen.

\- Domainiin ei lisätä riippuvuuksia.

\- `leaguehub.db`:tä ei avata eikä commitoida.

\- Et commitoi etkä yhdistä pull requesteja. Ne tekee ihminen checkpointin jälkeen.



\## Tarkistus, jonka ihminen ajaa



\- `dotnet run --project LeagueHub.Seeder` kahdesti käynnissä olevaa APIa vasten.

\- Nollattu kanta: rivit `luotiin`. Toinen ajo: rivit `oli jo`.

\- Molemmat päättyvät: `joukkueet 3/3, pelaajat 3/3, ottelut 2/2`.

\- Jos jokin specin rivi puuttuu, prosessi päättyy virheeseen.



Jos spec ja tämä tiedosto ovat ristiriidassa, pysähdy ja kysy.

