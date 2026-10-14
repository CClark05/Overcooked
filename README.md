# Boiling Point

A 2D single-player cooking game inspired by Overcooked. Customers walk in, place orders, and get more or less happy depending on how fast you serve them. Rack up the highest score before time runs out.

<img width="624" height="351" alt="Gameplay" src="https://github.com/user-attachments/assets/0eb3c933-f58d-45c5-ac4e-ae22961c5dda" />
<img width="624" height="351" alt="Game Over" src="https://github.com/user-attachments/assets/ac3f2e99-2c99-4020-8216-8ab029f09202" />
<img width="624" height="351" alt="Level Select" src="https://github.com/user-attachments/assets/e4a35cf4-49f2-467e-96ec-f553e4de6ef2" />


## Features
- **Customer happiness system:** every customer has a happiness score that drops the longer they wait, and it directly affects your score
- **Prepare food:** cook, chop, assemble food, and wash dishes to complete orders 
- **Custom A\* pathfinding:** customers navigate the restaurant to find seats and the counter
- **Finite state machines:** customer behavior (entering, ordering, waiting, leaving) is driven by state machines
- **Save system:** game data is saved to and loaded from JSON
- **Original pixel art:** all sprites, visuals, animations and visual effects are hand-made using Aseprite 
- **Custom level design:** levels are built in the Tiled map editor and imported into Unity with Super Tiled2Unity
- **In-game tutorial:** tutorial that guides the player through the game mechanics
- **Level Selection:** choose from multiple levels with different layouts and challenges
## Controls
| Action             | Key   |
|--------------------|-------|
| Move               | WASD  |
| Dash               | Space |
| Interact / Pick up | E     |
| Chop               | F     |
| Pause              | Esc   |

 

## How to Play
Customers arrive and order food. Prepare each order and deliver it before their patience runs out. Faster service means happier customers and a higher score. The game ends when time runs out, so aim for the highest score you can.

## Run
**Play the build:** https://ccharlie05.itch.io/boiling-point

## Tech
- Unity (2023.2.1f1), C#
- JetBrains Rider for code editing
- Super Tiled2Unity for tilemaps
- JSON for save data
- Aseprite for pixel art

## Credits
- Based loosely on [TUTORIAL TITLE](https://www.youtube.com/watch?v=AmGSEH7QcDg) by CodeMonkey, then heavily expanded with my own systems and design
- Sound effects and music: from the tutorial above

## Status
Feature-complete and playable. All core systems are implemented, however there are only three levels currently playable. 
I do not intend to add more levels as this was a learning project and I've moved onto other work.
