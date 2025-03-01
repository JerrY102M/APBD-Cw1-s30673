# Projekt w C# - APBD-Cw1-s30673

Jest to prosta aplikacja konsolowa napisana w C#. System pozwala dodawać użytkowników i sprzęt, wypożyczać sprzęt, zwracać go, sprawdzać opóźnienia i wyświetlać prosty raport końcowy.

Projekt uruchamiamy przez komendę:
`dotnet run`

## Co jest w projekcie

### Modele
- `Equipment` i 3 typy sprzętu: `Laptop`, `Projector`, `Camera`
- `User` i 2 typy użytkowników: `Student`, `Employee`
- `Rental` jako wypożyczenie

### Serwisy
- `EquipmentService` - dodawanie sprzętu i oznaczanie jako niedostępny
- `UserService` - dodawanie użytkowników
- `RentalService` - wypożyczenia i zwroty
- `RulesService` - limity i naliczanie kar
- `ReportService` - raport końcowy

### Repozytoria
Repozytoria są w pamięci (`List<>`), żeby projekt był prostszy.

## Reguły biznesowe
- student może mieć maksymalnie 2 aktywne wypożyczenia
- pracownik może mieć maksymalnie 5 aktywnych wypożyczeń
- sprzęt niedostępny nie może być wypożyczony
- za spóźnienie nalicza się kara 10 zł za każdy dzień

## Gdzie widać kohezję, coupling i odpowiedzialności klas

Nie chciałem robić wszystkiego w `Program.cs`, dlatego:
- klasy w folderze `Models` trzymają dane domenowe,
- klasy w folderze `Services` robią logikę,
- klasy w folderze `Repositories` przechowują dane w pamięci.

Moim zdaniem dzięki temu:
- **kohezja** jest lepsza, bo np. `RentalService` zajmuje się tylko wypożyczeniami,
- **coupling** jest mniejszy, bo serwisy korzystają z repozytoriów przez interfejsy,
- klasy mają w miarę jedną odpowiedzialność.

Nie jest to jakiś super rozbudowany projekt, ale kod jest bardziej podzielony niż gdyby wszystko było w jednej klasie.

## Scenariusz pokazany w Main

W `Main` są pokazane wymagane rzeczy z zadania:
- dodanie kilku egzemplarzy sprzętu różnych typów,
- dodanie kilku użytkowników różnych typów,
- poprawne wypożyczenie,
- próby niepoprawnych operacji,
- zwrot w terminie,
- zwrot opóźniony z karą,
- lista przeterminowanych wypożyczeń,
- raport końcowy.

