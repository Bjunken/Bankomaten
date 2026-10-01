# SyntaxSquad Bank

Ett konsolprogram i C# som simulerar en bankomat. Projektet är den sista uppgiften i kursen och görs i grupp.

## Introduktion

Programmet är en bankomat i konsolen. Användaren loggar in med användarnamn och pinkod och kan därefter:

- Se sina konton och saldo
- Göra överföringar mellan sina konton
- Ta ut pengar
- Logga ut

Saldon sparas mellan körningar i en JSON-fil, så att de inte återställs när programmet startas om.

## Installation och körning

## Projektstruktur

```
BankApp/
|-- Program.cs                    <- Startar programmet och skapar/laddar in Json filen.
|
|-- Models/
|   |-- User.cs                   <- klass för en användare: username, pin och en List<Account>.
|   `-- Account.cs                <- Hanterar bara datan för kontona, (saldo, namn, ect. Exempelvis med TryWithdraw/Deposit.
|
|-- Service/
|   |-- BankService.cs            <- Login logiken, tar mot username och pin, å hittar rätt user, samt räknar felförsök, (felförsök kommer behöva va sin egna metod så vi kan använda den igen).
|   |-- AccountService.cs         <- Hanterar rätt konto, ser alltså till att använda kontot's egna metoder.
|   `-- ValidationHelper.cs       <- Alla metoder som validerar inputs innan dem ska nå Models/Service.
|
|-- UI/
|   |-- LoginMenu.cs              <- Frontend för login menyn.
|   `-- MainMenu.cs               <- Frontend för huvudmenyn.
|
|-- Utils/
|  |-- ErrorMessage.cs            <- Alla metoder som printar ut fel meddelanden.
|  '-- DataHandling.cs            <- Metoder för att Spara och ladda information från Json filen.
|
|-- Data/
|   `-- accounts.json             <- Lokala databasen.
|
`-- README.md
```

### Klassöversikt

## Datastrukturer

- `List` för kontoinformation
- `` Inte besämt andra datastrukturen än.

### Motivering

## Extrautmaning: spara saldon till JSON

Saldon sparas mellan körningar till en JSON-fil, så att de inte återställs. JSON-filen initieras redan vid uppstart av programmet.

### Implementering

## Arbetssätt

### Git

- **main:** Linus håller koll på main-branchen.
- **test:** alla lägger sina uppdrag till test. Man kan även ta från test för att uppdatera det man arbetar med.
- Alla har lokala kopior av samma uppgift.
- Planerat flöde: egen branch → test → main.

### Samarbete

- Ska flera arbeta i samma fil kommunicerar vi vem som gör vad och när.
- Ingen sitter i samma fil samtidigt, för att minska konflikter och överskrivningar.
- Alla tittar på varandras delar för att lära sig vad de andra gjort och hur de tänkt.
- Kontinuerliga möten för att hålla alla uppdaterade.

### Kodkonventioner

- Namnsättning och kommentarer skrivs på engelska.
- Klasser skrivs med stor bokstav i varje ord, till exempel `AccountUsers`.
- Metoder skrivs med liten bokstav i första ordet och stor i nästa, till exempel `accountUsers`.
- Koden ska förklara vad som görs. Kommentarer är korta och ska inte vara flera steg långa.

## Arbetsfördelning

### Gruppmedlemmar

- Linus, projektledare
- Kristin, Kodare
- Martin, Kodare
- William, Kodare

### Roller

- **Linus:** projektledare och ansvarig för repot och kommunikation, samt hjälp och stöd vid behov. Fördelar även uppgifterna, och ansvarar för Program.cs / ErrorMessages.cs.
- **Kristin:** hantear block B, vilket är Models + Validering. (User.cs / Account.cs / ValidationHelper.cs).
- **Martin:** Ansvarar för block C, vilket service logik, (BankService.cs och AccountService.cs).
- **William:** Har hand om block D, som här UI + Json, (LgoinMenu.cs / MainMenu.cs / DataHandling.cs).

### Fördelning av uppgifter

Jag, (Linus), fick i uppgift att fördela projektet i gruppen som projektledare, och kom med förslaget att dela upp det i olika block för att så smidigt som möjligt hantera GitHub som nybörjare och även ha koll på respektives arbete på ett lättare sätt. Jag la upp förslaget och sedan fick medlemmar välja själva vilken del dem vill göra.

Detta var min planering:
Block A - Progam.cs / HerrorMessages / GitHub / README / testning vid merge / kommunikation
Block B - Models + Validering | Eftersom dem hänger ihop med nästan allt grupperade jag dem tillsammans.
Block C - Service logik | Eftersom detta kan bli rätt mycket så tänkte jag att det bara två filer, för blir lätt för mycket annars.
Block D - UI + Json | All frontend av en person blir lättare än om all ska hålla på med frontend, sen la jag även till DataHandling, alltså Json för att inte överbelasta block C.

## Reflektion

### Motivering av lösningen

### Övervägda alternativ

### Kritisk granskning och förbättringar

### Val av datastrukturer

### Klassindelning
