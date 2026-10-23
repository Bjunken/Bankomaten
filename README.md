# SyntaxSquad Bank

Ett konsolprogram i C# som simulerar en bankomat. Projektet är den sista uppgiften i kursen och görs i grupp.

## Introduktion

Programmet är en bankomat i konsolen. Användaren loggar in med användarnamn och pinkod och kan därefter:

- Se sina konton och saldo
- Göra överföringar mellan sina konton
- Ta ut pengar
- Logga ut

Saldon sparas mellan körningar i en JSON-fil, så att de inte återställs när programmet startas om.

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
- `Array` för kontonamn.

### Motivering
List använder vi för kontoinformationen eftersom det är ett av de bättre strukturerna att träna på eftersom den används på ett eller annat sätt överallt. Den passar även vår datastruktur.
Array använder vi för att sätta namnen, då den datastrukturen passade bäst eftersom den inte behöver ändras och ska ha ett fast antal namn i sig.

## Extrautmaning: spara saldon till JSON

Saldon sparas mellan körningar till en JSON-fil, så att de inte återställs. JSON-filen initieras redan vid uppstart av programmet.

### Implementering

## Arbetssätt
Vårt arbetssätt var något vi planerade ut tidigt i kursen. Vi började med att välja roller och utgick sedan från det.
Jag, (Linus), blev vald som projektledare och tog på mig ansvaret att fördela projektet i arbetsuppgifter, vilket skapade ett relativt enkelt arbetssätt.

All i gruppen fick filer och uppgifter tilldelades, sedan en egne deadline i ordning för att kunna användas som byggstenar. Vardera team medlem har sin egen branch, som i sintur pushas till en test branch,
där jag går igenom koden, lämnar feedback och kommentarer för att guida dem så pass bra jag kan för att uppnå datastrukturen som vi har valt ut.

Sedan har vi ett vecko-satt avstämningsmöte där vi går igenom vad som gjorts, samt vad som ska göras efter. Samt öppen kommunikation vid Discord ifall problem uppstår.

### Git

- **main:** Linus håller koll på main branchen.
- **test:** När klara med sina uppgifter görs en merge request till test från deras branch. Man kan även ta från test för att uppdatera det man arbetar med.
- Alla har lokala kopior av samma uppgift.
- Planerat flöde: egen branch → test → main.

### Samarbete

- Vårt arbetssätt är uppbygt så att vi ej behöver arbeta i samma filer, men om en ändring behövs göra, så kommunicerar vi vem som gör vad och när.
- Ingen sitter i samma fil samtidigt, för att minska konflikter och överskrivningar.
- Alla tittar på varandras delar för att lära sig vad de andra gjort och hur de tänkt.
- Kontinuerliga möten för att hålla alla uppdaterade.

### Kodkonventioner

- Namnsättning och kommentarer skrivs på engelska, men övrig utskriv i program på svenska.
- Klasser skrivs med stor bokstav i varje ord, till exempel `AccountUsers`.
- Metoder skrivs med liten bokstav i första ordet och stor i nästa, till exempel `accountUsers`.
- Koden ska förklara vad som görs. Kommentarer är korta och ska inte vara flera steg långa.
- Privata variabler börjar med '_'.

## Arbetsfördelning

### Gruppmedlemmar

- Linus, projektledare
- Kristin, Kodare
- Martin, Kodare
- ~~William, Kodare~~

### Roller

- **Linus:** projektledare och ansvarig för repot och kommunikation, samt hjälp och stöd vid behov. Fördelar även uppgifterna, och ansvarar för Program.cs / ErrorMessages.cs.
- **Kristin:** hantear block B, vilket är Models + Validering. (User.cs / Account.cs / ValidationHelper.cs).
- **Martin:** Ansvarar för block C, vilket service logik, (BankService.cs och AccountService.cs).
- ~~**William:** Har hand om block D, som här UI + Json, (LgoinMenu.cs / MainMenu.cs / DataHandling.cs).~~

### Fördelning av uppgifter

Jag, (Linus), fick i uppgift att fördela projektet i gruppen som projektledare, och kom med förslaget att dela upp det i olika block för att så smidigt som möjligt hantera GitHub som nybörjare och även ha koll på respektives arbete på ett lättare sätt. Jag la upp förslaget och sedan fick medlemmar välja själva vilken del dem vill göra.

Detta var min planering:
Block A - Progam.cs / HerrorMessages / GitHub / README / testning vid merge / kommunikation
Block B - Models + Validering | Eftersom dem hänger ihop med nästan allt grupperade jag dem tillsammans.
Block C - Service logik | Eftersom detta kan bli rätt mycket så tänkte jag att det bara två filer, för blir lätt för mycket annars.
Block D - UI + Json | All frontend av en person blir lättare än om all ska hålla på med frontend, sen la jag även till DataHandling, alltså Json för att inte överbelasta block C.

#### Checklista För Varje Block.
Block B
Kristin CHECKLIST: (Klar)

 User.cs
 - [X] User contains the required user information.
 - [X] Accounts uses the group's chosen List data structure.
 - [X] Users can have different numbers of accounts.
 - [X] Login/user lookup returns a User that other classes can use.

 Account.cs
 - [X] Account contains account name and decimal balance.
 - [X] Balance cannot be freely changed without following bank rules.
 - [X] Account supports the structure needed by AccountService.

 ValidationHelper.cs
 - [X] PIN validation has a maximum number of attempts.
 - [X] Invalid PIN input counts as an attempt.
 - [X] Method returns a result that Service/UI can use.
 - [X] No unnecessary UI/login-flow logic is added here.

 Before handing over to Martin:
 - [X] Build succeeds.
 - [X] Classes and property names are agreed upon.
 - [X] Constructor signatures are stable.

 Block C
 Martin CHECKLIST: (Klar)

BankService.cs
- [x] Hold the user list, the logged-in user (`_currentUser`), `ValidationHelper` and `AccountService`, all passed in through the constructor
- [x] `LocateUser(userName)` finds a user, ignoring upper/lower case, and returns `null` if there is no match
- [x] `Login(userName, pinText)` returns a `LoginResult`
  - Order: find user, parse PIN with `int.TryParse`, check `IsLocked`, verify PIN, then set `_currentUser`
- [x] `CurrentUser` is a read-only property, and `Logout()` clears the logged-in user
- [x] `IsLoggedIn()` is the single place that checks whether someone is logged in
- [x] `Deposit`, `Transfer` and `GetBalance` check login, then pass `_currentUser` to `AccountService` and return its result unchanged
- [x] `Withdraw` requires PIN confirmation (a wrong PIN counts toward the lockout), then calls `AccountService`
- [x] Return enum results (`LoginResult`, `TransactionResult`) instead of messages, and contain no `Console` calls

AccountService.cs
- [x] Work on the user's `Account` objects through `user.Accounts`, identified by 0-based index
- [x] `TryGetAccount` validates the index, and `IsValidAmount` defines a valid amount (> 0)
- [x] `Deposit`, `Withdraw`, `Transfer` and `GetBalance` all return a `TransactionResult`
- [x] Call `Account.Deposit()` / `Withdraw()` instead of duplicating balance logic
- [x] `Transfer` checks everything first (indexes, same account, amount, funds), so one account can never change alone
- [x] No UI logic and no PIN logic (the PIN check lives in `BankService`)

Block D
Linus/Kristin/Martin CHECKLIST: (Klar)
(lägg till block d checklista sen)
## Reflektion
Under vårt första möte ställdes frågan vad alla vara intresserad av att göra i projektet, sedan vilka roller som var av intresse. Jag, (Linus), blev snabbt vald som projektledare och Kristin var intresserad av User och Account delen, medan Martin var intresserad av bank logiken, samt William som var intresserad av UI. Efter det var satt, byggde vi upp en logik flow-chart för att få en visuell representation av programmet. Utifrån det fick jag, (Linus), i uppgift att planera och strukturera projektet, samt fördela arbetsuppgifter.

Planeringen av strukturen gick till så att, jag försökte hålla projektet så kompakt som möjligt, samt bygga upp det i byggstenar, där vi aldrig behövde röra varandras filer. I GitHub gjordes individuella branches som alla gick till en test branch, som vidare gick tillbaka till de individuella branches eller till vår main branch, vilket skulle leda till lättare ihop sättning, samt smidigare arbetsflöde eftersom vi inte behövde arbeta samtidigt och varje individ fick en veckas deadline. Tanken bakom det var att vi skulle ha så lite press som möjligt och mycket tid för att kunna ge stöd till varandra och bygga upp projektet i en bra takt.

Utöver strukturen av projektet, hade vi ett avstämnings möte varje vecka där individien vars deadline gick ut, fick redovisa sin kod för gruppen och sedan gick vi igenom vad som ska göras efter. Sedan hade vi även individuell avstämning med projekledaren varje torsdag, där man stämnde av vad man gjort eller planerat göra tills deadline dagen.

### Hur Var Det att Arbeta i Gruppen & med Uppgifterna?
**Linus**


**Kristin**
Det har varit en bra känsla i gruppen hela tiden. vi har kunnat fråga fritt utan att känna sig "dum" eller okunnig. bra feedback vid frågor eller funderingar. alla har getts chansen att fråga/komma till tals under gruppmöten.
**förbättring:**
kunde haft fler gruppmöten för att tillsammans diskutera kod och rubberducka mellan varandra för att lära oss än mer av varandra samt förstå upplägg av projektet och de olika delarna i det.

**Martin**
Stöttningen har varit bra, man har blivit bra bemött när man har bett om hjälp och gruppen har varit öppen för att hjälpa till. Även om man själv kanske har känt sig lite dum när man kört fast vid något tillfälle så har man fått stöttning och blivit peppad och påmind om att vi faktiskt fortfarande är nybörjare. Tycker det har varit bra upplägg där vi had Linus som blev projektledare som har kunnat stötta oss och vi andra har fått våra delar vi har haft ansvar över och end tydlig tidsplan. Varit bra med att både ha veckoavstämningar och individuella avsämningar med projektledaren.
Fått tydlig feedback hur man kan förbättra och göra om i koden för att följa hur det är tänkt att vi skulle strukturera koden.  Det kan vara lite svårt ibland när man har sin egen tanke kring hur det ska vara uppbyggt, men eftersom man fått bra feedback har det inte varit allt för svårt att redigera efter hur projektledaren hade sin tanke kring strukturen.
På slutet körde vi även lite par kodningen vilket jag tyckte var väldigt lärorikt och effektivt för att fånga upp fel.
**förbättring:**
kunde haft fler gruppmöten för att tillsammans diskutera kod och rubberducka mellan varandra för att lära oss än mer av varandra samt förstå upplägg av projektet och de olika delarna i det.

### Övervägda alternativ

### Kritisk Granskning av Kod och Förbättringar

### Val av datastrukturer
Vi valde att använda en Array, som innehåller kontonamn i ett fält. Tanken bakom det var att göra projketet så okompliserat som möjligt.
Sedan valde vi att använda List för att hålla våra konton och saldon eftersom det underlättade inte bara skapandet men återanvändning av dem med uppackning av listorna.  
### Klassindelning
Klasserna har delats in utöver arbetsuppgifterna och fördelning av projektet utifrån det som alla medlemmar var intresserade av att arbeta med, samt att försöka hålla projektet inom OOP-ramen som vi kunde.
