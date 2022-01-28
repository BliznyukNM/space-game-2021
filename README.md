# Space Game

## What is it?

This is just a prototype of custom ECS with Unity 2021 and so on. Testing new stuff, making space games. Woooo!

![AltText](https://media.giphy.com/media/Y55ahcJEAK7w8CsuQG/giphy.gif "Simulating planets trajectories before playing scene")

## ECS

I wrote custom ECS system. You can check it in ECS folder in project. It is very basic right now, but it is working at least.

It can do:
* Create, remove entities
* Add, remove, set, get components on entities
* Create systems for any specific world
* Basic filtering with `With`, `WithAll` and `WithAny` filters

## Simulation

I succesfully simulated Earth and Moon systems using approximation of available data.
Using classical Newton gravitational constant G = 6.67e-11, Earth mass = 5.9e+24kg, Moon mass = 7.35e+22kg and speed = 1022m/s I receive stable orbit.
Moon took around 28 days to make a full rotation around Earth using my code, but I must say that float calculations are not the best one to use in space science.

![AltText](https://media.giphy.com/media/9sVj1Ti7wdp8FtEODV/giphy.gif "Moon rotating around Earth")
