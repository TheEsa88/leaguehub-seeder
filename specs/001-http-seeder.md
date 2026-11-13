# Spec 001 — HTTP-seeder

## Käsitteet

- Seeder: erillinen ohjelma, API:n asiakas. Ei kerros.
- Demodata: kolme joukkuetta, kolme pelaajaa, kaksi ottelua.

## Tekemiset

- varmista joukkueet Mikkelin Myrsky, Kouvolan Kiekko, Savonlinnan Sähly
- varmista pelaajat Matti Meikäläinen 7 ja Teppo Testaaja 10 Myrskyyn, Kaisa Kiekkonen 7 Kiekkoon
- varmista ottelut Myrsky–Kiekko ja Kiekko–Sähly

## Säännöt ja paikka

| Sääntö | Paikka | Miksi |
|--------|--------|-------|
| Joukkueella on nimi | Domain, `Team.Create` | Sääntö on jo olemassa. Seeder ei kopioi sitä. |
| Sama nimi ei tule kahdesti | Seeder | Päätös vaatii haun tästä ympäristöstä. |
| Sama pelaaja ei tule kahdesti samaan joukkueeseen | Seeder | Haku rosterista, sitten POST vain jos nimi puuttuu. |
| Sama ottelupari ei tule kahdesti | Seeder | Avain on koti- ja vierasjoukkueen id, ei kellonaika. |
| Käynnistys ei lisää demojoukkueita | API `Program.cs` | Käynnistys ajaa vain migraation. |
| Taulut | Migraatio | Skeema ei ole demodataa. |
| Ajo tarkistaa rivit | Seeder, GET | Tarkistus kulkee saman rajan yli kuin luonti. |
| Reitti on /api/v1 | API, controllerin `[Route]` | Versio on HTTP-sopimus. Domain ei tiedä siitä. |

## Hyväksymiskriteerit

- Nollattu kanta, API käynnissä, yksi ajo: yhteenveto `joukkueet 3/3, pelaajat 3/3, ottelut 2/2`.
- Toinen ajo ilman nollausta: lokissa `oli jo`, sama yhteenveto.
- Puuttuva nimi: prosessi päättyy virheeseen ja loki kertoo nimen.
- Vaihdat vain osoitteen. Seederin luontilogiikka pysyy samana.
- `LeagueHub.Seeder` ei viittaa Domainiin, Applicationiin eikä Infrastructureen.

## Ei-tavoitteet

- Ei kirjautumista eikä tokeneita.
- Ei tuloksen kirjausta.
- Ei uutta reittiä eikä uutta domain-sääntöä.
