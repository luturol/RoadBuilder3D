# Road Builder

It's a game built with Unity 3D.

## What is it?

In Road builder you will create roads to another field by pressing a button and guessing it's distance. You can collect collectable itens such as a measuring tape to measure the distance between fields or a star boost. Every time you build a road, you will get some reward (star :star:) according to the guessed size of the road.

## Features

- [x] Start Game Scene
- [x] Road scale with keyboard key press
- [ ] Moving platform
- [x] Player move toward platform
    - [x] if road does not reach the next platform player will fall and change the animation
        - [x] change animation
        - [x] fall
    - [x] if road does not reach the next platform player will fall and die (restart game)
- [ ] Power up with measure tape
- [x] Star counting
- [ ] Power up that boost star reward
- [x] Present for winning the game
- [x] Finishing the map (diying or winning) player can restart the level
    - [x] A success pop up should appear with the star count and a restart level button 
    - [x] Star count is reseted if, but the reward does not it will still be available for the next run
        - [x] reset star count
        - [ ] reset reward
- [x] Each platform has a tiny target. If the road hits that platform (end of the road hits the target), reward for that platform will doubled


## Available platforms

- [x] [itch.io](https://luturol.itch.io/road-builder-3d)
- [x] [released build on github](https://github.com/luturol/RoadBuilder3D/releases/tag/v1.0)