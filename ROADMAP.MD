# Plan rozwoju projektu

Utworzyć procedury, które ułatwią pracę
z dokumentami XML. Po ich utworzeniu wystarczy,
że się wywoła je w C# poprzez funkcje w API.

## I. Wyszukiwanie

### Całe drzewo XML

 1. Wyszukiwanie produktów po id
 2. Wyszukiwanie produktów po nazwie
 3. Wyszukiwanie produktów wegetariańskich
 4. Wyszukiwanie po rozmiarze produktu
 5. Wyszukiwanie produktu po kryterium ceny (w przypadku znalezienia kilku rozmiarów, 
	wypisywane są wszystkie spełniające kryteria dlatego też dla mnie dużo prościej byłoby utworzyć tag price z atrybutem type)
    1. niższa,
    2. wyższa,
    3. z zadanego przedziału

### Kategorie

 1. Wypisywanie całej kategorii
 2. Wyszukiwania z punktu **Całe drzewo XML** ale jeszcze według wybranej kategorii
    1. można się skusić na wypisanie produktów z kilku kategorii

Wszystkie lub większość operacji przeszukiwania powinny oprócz "zwykłego" wyniku zwracać wynik w postaci XML.

## II. Wstawianie do bazy

1. Dodawanie kategorii - po prostu wpisać nazwę kategorii, w XML pojawi się pusta kategoria
2. Dodawanie produktu
  
    Przy dodawaniu produktu będzie trzeba utworzyć formularz, w którym będzie konieczne wybranie następujących elementów:

    - Kategoria produktu
    - Nazwa produktu
    - Czy jest wegetariański
    - Cena podstawowa produktu
    - Czy chcesz dodać inne rozmiary produktu? Jeśli tak to jakie?
    - Dodawanie dodatkowych rozmiarów

    Przy każdym punkcie powinno się znaleźć także anulowanie produktu, powrót do głównego menu/podmenu

    Można zrobić to trochę w inny sposób - np. po wybraniu kategorii dodajemy do niej produkt, a można zastosować obydwa rozwiązania

3. Dodawanie cen do istniejących produktów

    - Wybierz produkt
    - Wybierz rozmiar jaki chcesz dodać
    - Podaj cenę

> _Zakładamy, iż produkt nie może stać się nagle wegetariański, więc póki co nie rozważam modyfikacji atrybutu wegetarianizmu produktu, ale
jak co, to jest to nieoficjalny punkt 4_

## III. Modyfikacja bazy

1. Zmiana nazwy kategorii
2. Zmiana cech produktu - nazwy, ceny/cen
3. Przeniesienie produktu do innej kategorii (?)

## IV. Usuwanie z bazy

1. Usuwanie produktu po jego parametrach - nazwie i/lub ID,
byłby on także usunięty z kategorii
2. Usuwanie cen produktu - ale nie za średni rozmiar!

    - Byłoby to menu typu "Wybierz rozmiar do usunięcia" i wybrany rozmiar byłby usunięty
3. Usuwanie całej kategorii wraz z produktami - po ID i/lub nazwie
4. Usunięcie całego menu (xD)

## V. Struktura projektu - C# API

### Menu główne

- Wybierz kategorię (pojawiają się wylistowane kategorie, wybranie ID - wybranie kategorii)
- Dodaj kategorię
- Przeszukuj menu po podanych parametrach
- Usuń menu

### Podmenu kategorii

- Zmień nazwę kategorii
- Wybierz produkt (nowe podmenu dla produktu)
- Usuń kategorię
- Przeszukuj kategorię
- Dodaj produkt

### Podmenu produktu

- Zmień nazwę produktu
- Dodaj/usuń/zmień ceny produktu
- Usuń produkt

### Propozycja

Można byłoby zrobić tak, że po wybraniu kategorii/produktu można
byłoby zrobić menu z opcjami:

- Zarządzaj - wtedy przeniesie nas do podmenu dokumentu
- Usuń - usuwa wybrany dokument

W każdym podmenu byłaby opcja powrotu poziom wyżej w programie
oraz wyjścia z programu.

Aby aplikacja zyskała na UX, należy po każdej komendzie czyścić konsolę.

## VI. Postępy

| Etap projektu           | Część sqlowa              | Część C#                 |
| ----------------------- | -----------               | ------------------------ |
| I. Wyszukiwanie         | :white_check_mark: Done   |  :clock4: Waiting        |
| II. Wstawianie do bazy  | :white_check_mark: Done   |  :clock4: Waiting        |
| III. Modyfikacja bazy   | :white_check_mark: Done   |  :clock4: Waiting        |
| IV. Usuwanie z bazy     | :point_right: In progress |  :clock4: Waiting        |
