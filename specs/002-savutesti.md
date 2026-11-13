\# Spec 002 — Savutesti



\## Käsitteet



\- Savutesti: erillinen ohjelma, joka kertoo onko API hereillä. Ei kerros.

\- Health-endpoint: APIn osoite /health, joka vastaa 200 kun prosessi on ylhäällä.



\## Tekemiset



\- lisää APIin osoite /health

\- tee GET /health ja kerro tulos paluukoodilla



\## Säännöt ja paikka



| Sääntö | Paikka | Miksi |

|--------|--------|-------|

| Onnistuminen on paluukoodi 0, virhe muu | SmokeTest | Skripti ja agentti lukevat koodin, eivät tulostetta. |

| Osoite luetaan asetuksista | SmokeTest | Sama sääntö kuin seederillä: ympäristö vaihtuu, koodi ei. |

| Ei kirjoituksia | SmokeTest | Savutesti ei muuta kantaa. |

| Vointi kysytään osoitteesta /health, ei domain-reitiltä | API `Program.cs` | Terveys ei ole osa domain-APIa eikä versioidu. |



\## Hyväksymiskriteerit



\- API käynnissä: GET /health palauttaa 200 ja selain näyttää Healthy.

\- API käynnissä: tuloste `OK` ja osoite, paluukoodi 0.

\- API sammutettuna: tuloste kertoo virheen, paluukoodi ei ole 0.



\## Ei-tavoitteet



\- Ei POST-kutsuja eikä rivien laskentaa — se on seederin tarkistuksen työ.

\- Ei kantatarkistusta — kantaongelmat näkyvät seederin omassa virheessä.

