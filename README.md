# Blackjack


#### _By Nathan Fletcher and Connor Hansen


## Technologies Used

* C#
* Object Oriented Programming
* ASP.NET Core MVC Framework
* Restful Routing Conventions

## Setup
dotnet add package Microsoft.EntityFrameworkCore -v 5.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql -v 5.0.0-alpha.2
dotnet add package Microsoft.EntityFrameworkCore.Proxies -v 5.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.0

dotnet tool install --global dotnet-ef 
//--version 3.0.0
dotnet ef migrations add Initial
dotnet ef database update


## Known Issues
* There are no known issues at this time.
* Please contact me if you find any bugs or have suggestions. 

## License

_[MIT](https://opensource.org/licenses/MIT)_  

Copyright (c) 2021 Nathan Fletcher and Connor Hansen

## Future Plans
button to auto generate multiple? - button - generate deck - behind the scenes - crud? bet - property


player
  bet
  money - current 
  name
  hand (cards)  (list) 
  boolean IsDealer - check it in an if - do something special
  score

Dealer... extra class?

game class? stretch?
  players
  winner
  money lost/gained for each player

card
  int value 11, 12, et (use a switch and if >=11 display the Letter11)

data structures - hand list 
multiple games - keep a history? - table 

static methods - had access to the private data list _instances - do they have access to the db data?
  handled in the controller? 
  CheckWinner 
  CalcScore
  Draw - button - logic in the route "Draw" route
  

Tasks
Cards
C - Generate the deck - auto or button?
R - display deck? need index, etc?
no views at all for card
Players
C get, post, view
R index, details - game ui

//user stories 
name?
post route actually creates dealer too

<!-- 
U
D 
-->

