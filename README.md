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
LoginMenu.cs
- [x] Show the login screen and read the username and PIN as plain strings
- [x] Call `BankService.Login(userName, pinText)`, with no PIN parsing or login rules in the UI
- [x] Show a message for every `LoginResult` through `ErrorHandling`
- [x] Open `MainMenu` only after `SuccessfulLogin`, otherwise stay on the login screen
- [x] Return to the login screen after logout

MainMenu.cs
- [x] Show the user's accounts and balances, and the options: Deposit, Withdraw, Transfer, Logout
- [x] Return to the main menu after every operation
- [x] Validate all input with `TryParse` (menu choice, account number, amount)
- [x] Convert the displayed account number (1, 2, 3...) to a 0-based index before calling `BankService`
- [x] Call `BankService` for deposit, withdraw and transfer, and never call `AccountService` directly
- [x] Ask for the PIN on withdraw and pass it as a string
- [x] Show the result of every transaction to the user
- [x] Save to the JSON file after every successful deposit, withdraw and transfer
- [x] Log out and return to the login screen instead of closing the program
- [x] No banking logic or balance checks in the UI

DataHandling.cs
- [x] `Load` reads the JSON file and returns a `List<User>`
- [x] `Save` writes the `List<User>` to the JSON file
- [x] The file path is defined in one place (`Data/accounts.json`)
- [x] A missing file returns an empty list instead of crashing
- [x] A broken or unreadable file is caught with `try/catch`
- [x] Uses the existing `User` and `Account` classes, with no separate JSON classes
- [x] The PIN is saved (`[JsonInclude]`) and the lock status is not (`[JsonIgnore]`)
- [x] Data survives a restart: make a change, close the program, start it again and check that the balance is still there

## Reflektion
Under vårt första möte ställdes frågan vad alla vara intresserad av att göra i projektet, sedan vilken roll som var av intresse. Jag, (Linus), blev vald som projektledare och Kristin var intresserad av User och Account delen, medan Martin var intresserad av Bank logiken, samt William som var intresserad av UI. Efter det var satt, byggde vi upp en logik flow-chart för att få en visuell representation av programmet. Utifrån det fick jag, (Linus), i uppgift att planera och strukturera projektet, samt fördela arbetsuppgifter.

Planeringen av strukturen gick till på det viset att jag försökte hålla projektet så kompakt som möjligt, samt bygga upp det i byggstenar, där vi aldrig behövde röra varandras filer. I GitHub gjordes individuella branches som alla gick till en test branch, som vidare gick tillbaka till de individuella branches eller till vår main branch, vilket skulle leda till lättare ihopsättning, samt smidigare arbetsflöde eftersom vi inte behövde arbeta samtidigt och varje individ fick en veckas deadline. Tanken bakom det var att vi skulle ha minimal tidspress och mer tid för att kunna ge stöd till varandra och bygga upp projektet i en bra takt.

Utöver strukturen av projektet, hade vi ett avstämningsmöte varje vecka där personen vars deadline gick ut, fick redovisa sin kod för gruppen och sedan gick vi igenom vad som ska göras efter. Sedan hade vi även individuell avstämning med projektledaren varje torsdag, där man stämde av vad man gjort eller planerat att göra till deadline dagen.

### Hur Var Det att Arbeta i Gruppen & med Uppgifterna?
**Linus**
Jag tycker uppgiften har varit rolig att arbeta med, och känner att jag hamnade i en mottaglig grupp, trots ett avhopp. Uppgiften i sig tycker jag var passande för kursen och utmanande på olika sätt, inte bara som projektledare men även med programmering, eftersom man behöver strukturera, förklara hur man tänker och bygga något med andra, vilket har varit väldigt lärorikt.

Som jag nämnde känner jag att gruppen var mottaglig och det är väl ett mildare sätt att säga, att jag tycker vår grupp fungerade väldigt bra. Elefanten i rummet är väl att vi hade avhopp av en team medlem som inte tillförde något, men de resterande medlemarna var lätta att arbeta med, var mottagliga till feedback och även raka med att ge feedback tillbaka. De var öppna för förändringar, alternativ och dylikt. Detta gjorde arbetet som projektledare enklare men känner ändå att jag lärde mig mer än en sak som jag kan ta med mig som projektledare och programmerare.

**Kristin**
Det har varit en bra känsla i gruppen hela tiden. Vi har kunnat fråga fritt utan att känna sig "dum" eller okunnig. Bra feedback vid frågor eller funderingar. Alla har getts chansen att fråga/komma till tals under gruppmöten. Bra feedback och diskussioner med Linus genom projektet för att få tips och förslag på hur man kan fortsätta när man kört fast. Det bästa var att vi alla redan från start visste vad vi ville göra vilket i sin tur skapade en bra grund utan diskussioner och gnäll utan vi kunde starta igång projektet direkt utifrån datastrukturen.
**Förbättringsförslag framåt:**
Kunde haft fler gruppmöten för att tillsammans diskutera kod och rubberducka mellan varandra för att lära oss än mer av varandra samt förstå upplägg av projektet och de olika delarna i det.

**Martin**
Stöttningen har varit bra, man har blivit bra bemött när man har bett om hjälp och gruppen har varit öppen för att hjälpa till. Även om man själv kanske har känt sig lite dum när man kört fast vid något tillfälle så har man fått stöttning och blivit peppad och påmind om att vi faktiskt fortfarande är nybörjare. Känslan i gruppen har varit bra. Tycker det har varit bra upplägg där vi had Linus som blev projektledare som har kunnat stötta oss och vi andra har fått våra delar vi har haft ansvar över och en tydlig tidsplan. Varit bra med att både ha veckoavstämningar och individuella avsämningar med projektledaren.
Fått tydlig feedback hur man kan förbättra och göra om i koden för att följa hur det är tänkt att vi skulle strukturera koden.  Det kan vara lite svårt ibland när man har sin egen tanke kring hur det ska vara uppbyggt, men eftersom man fått bra feedback har det inte varit allt för svårt att redigera efter hur projektledaren hade sina tankar kring strukturen.
På slutet körde vi även lite par kodningen vilket jag tyckte var väldigt lärorikt och effektivt för att fånga upp fel. Det har även varit kul att få testa på att jobba med git i grupp, var lite svårt i början och man gjorde lite misstag i början med det vilket även blev väldigt lärorikt och hjälpte till att komma in i det.
**förbättring:**
kunde haft fler gruppmöten för att tillsammans diskutera kod och rubberducka mellan varandra för att lära oss än mer av varandra samt förstå upplägg av projektet och de olika delarna i det.

### Övervägda alternativ
Jag, (Linus), svarar på denna fråga eftersom det var jag som strukturerade projektet och tog hand om den delen. När jag tänker efter hur och varför jag strukturerade projektet, känner jag inte att jag övervägde flera alternativ, utan det kändes rätt rakt fram i hur det skulle göras.

Så de alternativ som kan ha kommit att övervägas var mer, "hur ska vi arbeta tillsammans", "Hur ska vi kunna hjälpa varandra", eftersom för det mesta följde alla medlemmar mina "TODO"-lists som jag gjorde åt dem.

### Kritisk Granskning av Kod och Förbättringar
Kan tycka att detta är en rätt svår fråga, eftersom man utgår från sin egen förmåga och vet/kan man inte bättre så blir det svårt att få fram ett svar. Men det som jag hade velat granska är robustheten, tron är att koden är robust och vid tester har programmet inte gått att krascha, men det finns några förbättringar som kommer till åtanke.

Vi använder "StringComparison.OrdinalIgnoreCase" i vår "locateUser" metod, vad den gör är att låta användaren logga in med stor eller liten bokstav, det gör att kontot: "Linus" kan loggas in som: "linus". En förbättring hade varit att köra utan och vara strikta med användarens inloggnings namn.

Ytterligare en förbättring hade varit att utveckla "ValidationHelper.cs" så den innehåller mer av verifikationen av pin-koden. Som den är uppsatt nu är den mer en bool, som skickar vidare för verifikation inne i User.cs, och fungerar då som en mellan man istället för en "motor" för verifikations processen.

Utöver det hade UI generellt kunna förbättras, men pågrund av avhopp och en total ombyggnad för UI, fanns det inte tid över till ett mer detaljerat UI.

### Val av datastrukturer
Vi valde att använda en Array, som innehåller kontonamn i ett fält. Tanken bakom det var att göra projketet så okomplicerat som möjligt.
Sedan valde vi att använda List för att hålla våra konton och saldon eftersom det underlättade inte bara skapandet utan även återanvändning av dem med uppackning av listorna.  
### Klassindelning
Klasserna har delats in utöver arbetsuppgifterna och fördelning av projektet utifrån det som alla medlemmar var intresserade av att arbeta med, samt att försöka hålla projektet inom OOP-ramen som vi kunde.
