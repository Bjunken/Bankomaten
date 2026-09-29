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

### Roller

- **Linus:** projektledare och ansvarig för repot. Fördelar uppgifterna, övervakar så att arbetet sker parallellt och äger README.

### Fördelning av uppgifter

## Reflektion

### Motivering av lösningen

### Övervägda alternativ

### Kritisk granskning och förbättringar

### Val av datastrukturer

### Klassindelning
