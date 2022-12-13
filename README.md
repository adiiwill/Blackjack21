# Blackjack 21

### Készítette: Gazdag Ádám

A program telepítés nelkül használható, 2 mappát (Source, és Game) tartalmaz.
A Source mappa tartalmazza az osztályokat és a Program.cs-t.
A játék a Game mappában lévő exe fájllal futtatható.

A játéknak van egy menüje 3 lehetőséggel:
- **Játék**
- **Statisztikák**
- **Kilépés (Mentés)**

A menüben való navigálás a következőképp lehetséges:
- A választott opció sorszámát beütve
- A választott opció nevét beütve

A Játék gomb elindít egy játékot, azt befejezve visszatér a főmenübe.
A Statisztika fül megmutatja a játékosról gyűjtött statisztikákat, illetve lehetőséget kínál azok törlésére.
A Kilépés opció pedig kilép a játékból elmentve azt. ***(FONTOS: A játék más módon való kiléptetése adatvesztéssel jár!)***

A játék tartalmaz szint rendszert is. A játékos tapasztaltságát TIER-ben méri, amihez a megfelelő titulust jeleníti meg.
A legmagasabb titulust TIER 10-nél kapja meg.

Szintet csak akkor léphet a játékos ha elérte a következő szinthez szükséges tapasztalati pontok mennyiségét amiket a következőképp kap:
- **Győz:** _+3xp_
- **Döntetlen:** _+2xp_
- **Veszít vagy besokall:** _+1xp_
- **Ha BLACKJACK-et kap:** végeredményhez hozzáadott _+3xp_

\
Ha bármiféle hibát tapasztal kérem keressen meg:
> gazdagadam@gmail.com
