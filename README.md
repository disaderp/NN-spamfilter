# WO.SN.6 - DOKUMENTACJA

## Paweł Zych

## Piotr Grabarski

## Karol Janiszewski

Celem projektu było napisanie programu realizującego film antyspamowy oparty na
sieci neuronowej. Program napisany został przy użyciu języka C#, w środowisku
Visual Studio 2015 bez użycia zewnętrznych bibliotek wysokiego poziomu.

## 1.Decyzje projektowe

Pierwszym i kluczowym etapem realizacji projektu, było zastanowienie się nad
rodzajami “wejść” sieci i ,w konsekwencji, sparsowanie maila do postaci
akceptowalnej przez sieć neuronową. Konieczne było wybranie parametrów, które
potencjalnie mogą mieć wpływ na to, czy sieć uzna danego maila za spam, czy też
nie. Oprócz oczywistych własności tekstu, takich jaki liczba słów, liczba znaków,
liczba zdać, liczba znaków specjalnych etc., liczyliśmy także nieco bardziej złożone ,
takie jak ​hapaks legomenon,miarę sichela, czy też miarę simpsona​**.** ​ Uznaliśmy, iż
zliczanie znaczników html (font, href) i linków(http) może mieć istotne znaczenie na
etapie różnicowania (wyniki pokazały, że niebezpodstawnie). W trakcie realizacji
etapu parsowania, natknęliśmy się na szereg problemów wynikających z różnych
form, w jakich email był podany. W przypadku, gdy treść była podana miedzy
znacznikami html, trzeba było się ich pozbyć. Zdarzało się, że podany mail był
zakodowany (base64), więć oczywistym było, iż przed jego analizą konieczne jest
jego zdekodowanie.
Sieć użyta w projekcie jest typową siecią jednokierunkową z jedną warstwą ukrytą
oraz jednym neuronem wyjściowym(0 - brak wykrytego spamu, a 1 - spam).
Perceptrony używają funkcji sigmoidalnej do obliczania wyjść neuronu.


Do uczenia się wykorzystywany jest algorytm propagacji wstecznej.
Sieć ma 3 główne parametry które można modyfikować:

- liczba neuronów warstwy ukrytej
- współczynnik uczenia
- mnożnik epoki - co epokę współczynnik uczenia jest mnożony przez tę
    wartość

## 2.Instrukcja dla użytkownika

**Kompilacja:**
Plik NN-spamfilter.sln zawiera solucję programu Visual Studio - po jej otworzeniu
dostaniemy się do wszystkich plików, które są zawarte w projekcie.
_Opcjonalnie_ ​ można pominąć proces kompilacji i przejść do wypróbowania działania.
W tym celu w folderze ​ **_exe/_** ​ znajdują się skompilowane pliki binarne.
**Uruchomienie:**
Uruchomienie pliku ​ **_Parser.exe_** ​ spowoduje że program zacznie przetwarzać e-maile
zapisane w podfolderach folderu ​ _EMAILS_ ​ oznaczone nazwami {easyHam,
easyHam2, easyHam3, hardHam, hardHam2, spam, spam2, spam3, spam4}.
Po zakończeniu przetwarzania program wygeneruje 2 zestawy danych - zestaw
treningowy, oraz zestaw walidacyjny. Każdy z nich posiada w losowej kolejności
wybrane elementy z całego zbioru. Ponadto zbiory nie mają części wspólnej i
podzielone są w stosunku 2:1.
Następnie możemy uruchomić plik ​ **_NeuralNetwork.exe_** ​ który wczyta dane zapisane
przez Parser a następnie zacznie szkolić sieć. Po określonej liczbie epok
(standardowo 30) program wczyta dane do walidacji i poda ostateczny wynik
osiągnięty przez sieć.


## 3. Struktura programu

Program podzielony jest na dwie kluczowe części, to jest Parser i Sieć. W pierwszej
części, realizowany jest proces obliczania charakterystyk, ich normalizacja oraz
zapis do pliku. Tak przygotowane dane przekazywane są do sieci.
Parser składa się z pięciu klas:
**Parser:** ​ najważniejsza klasa, odpowiedzialna jest za obliczenie zadanych cech
danego maila
**Normalizer:** ​ klasa, która realizuje proces normalizacji danych do zakresu
optymalnego dla sieci neuronowej (-1,1)
**EMail:** ​ klasa przechowująca informacje o poszczególnym emailu m. in. treść i
metadane
**Constants:** ​ statyczna klasa przechowująca wszystkie wartości typu “readonly” użyte
w programie
**Program:** ​ klasa, zawierająca funkcję Main, w której realizujemy cały proces
parsowania. Dla każdego emaila z repozytorium, obliczamy charakterystyki,
normalizujemy je i zapisujemy do dwóch plików: “learning.txt” oraz “validation.txt”.
Natomiast sieć z poniższych klas:
**Shared:** ​klasa statyczna przechowująca generator liczb losowych
**Neuron** ​: klasa pojedynczego neuronu, zawierająca funkcje obliczania wyjścia,
aktualizujące wagi wewnątrz sieci oraz jej rekurencyjne odwołania do neuronów
‘rodziców’
**Network:** ​ klasa przechowująca wszystkie neurony obecne w sieci włącznie z
funkcjami które pozwalają na wywołanie obliczania wyniku oraz błędu na wszystkich
neuronach.
**Program:** ​standardowa klasa zawierająca funkcje Main, która wywoływana jest po
uruchomieniu programu. Zajmuje się wczytaniem danych, rozpoczęciem treningu
oraz wyświetleniem prawdziwych danych.


## 4. Uzyskane wyniki

Sieć o parametrach LearningRate = 4 oraz EpochDecrease = 0,8 wydała nam się
najlepsza podczas testów, dlatego te parametry użyliśmy do dalszych prób.
**5.Wnioski:**
Uzyskane wyniki można uznać za zadowalające. Rzecz jasna, program nie
zastąpi komercyjnych rozwiązań i nie taki był cel jego implementacji. Jeszcze na
etapie projektowania, uznaliśmy iż rozpoznanie na poziomie większym niż 80%
będzie dobrym wynikiem i założenie to udało się spełnić. Sieć miała największe
trudności, z odróżnieniem maili znajdujących się w folderach “hardHam*”, od maili z
folderów “spam*”. Wynika to, oczywiście, z licznych podobieństw między nimi. Na


stronie repozytorium, z którego pobrane zostały maile, folder “hardHam”
zdefiniowany został jako: “250 non-spam messages which are closer in many
respects to typical spam: use of HTML, unusual HTML markup, coloured text,
"spammish-sounding" phrases etc”.
Spoglądając szerzej na problem rozpoznawania spamu, wydawać by się
mogło, iż do realizacji takiego zadania lepiej nadawałoby się coś w rodzaju
klasyfikacji semantycznej. Niemniej jednak projekt, jak mniemamy, miał na celu
zapoznanie nas z zagadnieniem sieci neuronowych, a stworzenie filtru
antyspamowego samo w sobie.
Uzyskany wynik, próbowaliśmy poprawiać “eliminując” niektóre wejścia sieci i
rzeczywiście, odpuściliśmy kilka początkowo wyznaczonych przez nas cech.
Kluczową sprawą nad którą musieliśmy się zastanowić były parametry
uczenia sieci, a dokładniej LearningRate oraz EpochDecrease opisane wcześniej.
Po wielu próbach okazało się że sieć najlepiej uczyła się dla wartości LearningRate
= 4 oraz EpochDecrease = 0,8. Oznacza to że co każdą epokę wartość
LearningRate była mnożona przez 0,8, aż ostatecznie w 30 epoce osiągała wartość
4 * 0,8^30 = 0,
Próby zwiększania EpochDecrease( parametr LearningRate obniżał się
wolniej) zakończyły się bez większego powodzenia, sieć bardzo powoli dochodziła
do wyższych wartości, a dodatkowo można było zaobserwować liczne spadki
sprawności.
Zbyt małe wartości EpochDecrease( parametr LearningRate spadał szybciej)
powodowały że sieć nie zdążyła osiągnąć wysokich wyników, przed tym jak
współczynnik uczenia był już na tyle mały, żeby nie móc wpłynąć znacząco na
parametry.
Z kolei zbyt wysokie wartości początkowe LearningRate powodowały, że sieć
uczyła się oznaczać wszystkie maile jako ‘niespam’ ponieważ maili tych było więcej
(w proporcji 65:35), dlatego też sieć osiągała wynik 65%.


