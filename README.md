# Tactical-RPG-Test

## General Explanations

## How Features Implemented

### GameFlow Management

### Game Rules And GameState

### Opponent Ai



## Conventions
- there are many rules for naming and formating the code but i used rider/resharper default conventions
- there is good use of oop and classes are short

## Design Patterns 
#### Dependency Injection
- best for testablity(writing unit tests)
- loosely coupled and changability and flexiblity(can simply change implementations of an interface)
- cleaner than singletons
- better use of OOP and polymophism


#### MVVM
- very good for ui based apps and parts of the code that use ui
- it seperates binding of UI in other classes called #View-Model# and seperates UI logic from bindings so its great for testablity
- most mvvm plugins for unity are over complicated so made a simple framework  to use mvvm in this project

#### chain of responsibility
- explained in AI part




## Plugins Used In Project

### Zenject(Extenject)
a *dependency injection* container for unity that supports unity contexts. 

### UniRx
a plugin to use *reactive extensions* and use reactive programming in unity. 
its a great tool for making sequential events and making web requests and coroutines cleaner.

### Other:
- environment and tanks 3d models assets
- addressables to manage memory and asset loading
