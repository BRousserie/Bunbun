# Bunbun
Unity Project - Based on Slay The Spire but with rabbits

## Intentions
I love Slay The Spire. When I start a new run, I can't stop it until I die. It might be my favorite game of the last 5 years. However, some things bother me with this game :
 - It relies too much on luck (in my humble opinion), which leads plenty of different experiences feeling unfair (mainly having the bad draws during a combat or the wrong cards when looting).
 - It is played alone. A big part of what I like with games is the Social part of it. I take screenshots when funny things happen during the game and send it to my friends who like Slay the Spire (like that time my whole draw was *Dazed* cards...), but it's not even close to the feeling of actually playing with/against a friend.

<p>I want to try to address thoses issues.</p>

<p>Concerning luck, I will reduce the amount of cards integrated in the game (well... it also helps me for the development lol but actually I felt like it was easier to build synergies in the Ironclad's deck during the first run, as it had less options -but still strong options-).<br>
I might try to integrate hidden helping algorithms (for example, if the ennemy is going to attack and you have blocking cards in your draw pile, you'd draw at least one of them, to avoid the situations when you have a full block cards hand when the ennemy doesn't intend to attack vice-versa).</p>

<p>Most importantly, concerning the social-dimention I want to give to the game, I thought of a co-op mode, to play with a friend locally or from remote.<br>
  I also want to implement a Versus mode where the players would either fight with randomly generated or template decks, or fight with decks they won runs with.<p>
    
## Planning
At new year I'd like to have a prototype with the following features :
 - Procedural generation of the map
 - All types of room are functionnal (MinorEnemy, EliteEnemy, Boss, RestSite, Treasure, Store, Mystery)
 - 2 players-combat support
 - >=20 cards implemented (10 cards per player at least)
 - 1 playable act, which implies
   - >=2 normal ennemies
   - >=2 elite ennemies
   - 1 boss
 - Save/Load players progression
