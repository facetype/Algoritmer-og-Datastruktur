# README
Obligatorisk innlevering i Algoritmer og Datastruktur.

oppgavetekst:

Obligatorisk innlevering 2
Frist: Se Canvas
I denne oppgaven (oppgave 1 -4) skal vi først lære å lese data fra CSV filer. Dette er et mye
brukt filformat, selv om det er enkelt, og ikke veldig high performance. Vi skal bruke klasser
for å gjøre koden mer strukturert og gjenbrukbar senere.
Vi skal også bruke serialiser/deserialisering (oppgave 5 og 6) for å lese inn en Dictionary fra
JSON (vi har ikke lært hvordan en Dictionary fungerer, men dere bør kjenne igjen
datastrukturen godt nok til å kunne løse oppgaven. Spør hvis noe er uklart).
Til slutt skal vi teste å søke i en string ved å betrakte denne som et char array, og lete etter
oppgitt pattern.
Oppgave 1
Lag en DataTable klasse som kan lese inn en CSV datafil og konvertere denne til en brukbar
data-tabell i minne. Bruk System.IO.File til å lese alle linjene i fra vedlagt fil data.csv, inn i
ett string[]. Lag en for-loop som itererer igjennom hele arrayet. Første rad inneholder
«header» tekster, altså navn på kolonner. For hver rad, bruk String.Split til å dele opp hver
rad i ett nytt string array. Disse, unntatt første rad, samles i en lang List<string[]>. Du kan
velge om du gjør alt dette i Constructur, eller i en egen «LoadDataFromFile» metode. Lagre
header raden som en egen property med «Coulmn names» for senere bruk.
Oppgave 2
Lag en ny DataRow klasse som kan representere hver enkelt rad i tabellen. Denne må ha en
DateTime timestamp property og et double[] array kalt «values». Konverter alle verdiene ifra
List<string[]> listen i pkt 1, slik at a) string[0] i hver rad gjøres om til et DateTime timetamp,
mens resten av string verdiene konverteres til doubles. Hver rad i CSV filen skal lagres som
en DataRow objekt, som inneholder timetamp og verdi-array. For DateTime konvertering,
bruk «invariant» culture slik at decimal tegn er dot (.) og ikke komma(,). Dette er vanlig i
tekniske datasystemer.
Oppgave 3
Nå har du konvertert en CSV fil til en brukbar data struktur. Lag en GetColumn(string name)
funksjon, som kan lete fram i header array en gitt kolonnenavn, finne riktig indeks, og så
loope igjennom alle DataRow’ene og hente ut verdien i denne kolonnen. Returner ett
double array på lengde med antal DataRow’s i tabellen din.
Oppgave 4
Lag en «Sort» metode (bruk innebygget Sort, eller lag din egen Sort algoritme basert på de vi
har lært), som sorterer alle radene etter stigende DateTime. Tips: DateTime kan
sammenliknes slik som andre tall, men du må finne ut hvordan det gjøres riktig. Lag en
enkel test kode som sammenlikner to DateTime variabler for å sjekke at predikatet til Sort
algoritmen din er riktig formuler.
Oppgave 5
I denne oppgaven skal vi lese inn data.json filen og konvertere denne til en dictionary. Bruk
File.ReadAllText til å lese alt innholdet inn i en enkelt string. Merk: IKKE bruk ReadAllLines
og les til string array slik som i oppgave 1.
Oppgave 6
Åpne NuGet package manager og link in «Newtonsoft». Se selv eksempler på hvordan
denne pakken brukes (linker til både docs og GIT finnes i NuGet packet manageren). Bruk
denne til å konvertere json-stringen lest fra fil til en Dictionary. Bruk breakpoints/debugging
for å sjekke at dictionaryen er lest inn riktig.
Oppgave 7
Lag en string variabel og assign «abcdefghijklmnopqrstuvwxyz». Lag en funksjon int
MatchPattern(string data, string target) som tar en string som input, og søker igjennom
denne stringen etter en annen oppgitt string. Returner index til første char i stringen som
matcher oppgitt pattern. Bruk din egen linear search implementasjon, eller en innebygget
search metode. Bruk indexing på stringen (data[0] = ‘a’) for å lese characters i stringen på
gitt index.