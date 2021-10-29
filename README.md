# Tactical-RPG-Test

## General Explanations

here is a simple video that shows how game is running:


### How Features Implemented

#### Game Scene Mangement
- there is only one *GamePlayLogic* scene that all logic of offline match is imlemented in there
- theme scenes are added additively to the *logic scene*  to have different theme maps
- right now we have  *Desert* and *Jungle* maps
- 

#### GameFlow Management
- there is *GameStateManager* that handles the state of the game. there is a *GameState* class that holdes state of the match all needed data to run the match is in there.
- any input of player is just a query to *GameState* and UI and Match Logic are seperated.



#### Game Data Management

- State of the whole game is wrapped in *GameState* model. things like what settings has player selected in the game
- player progress like experience and level is in *PlayerData*  class and deserializes to the class from file to be used
- above classes are serializable and *IFileUtility* as different implemetations like encrypted version for devices nd row version for the editor and development

#### Game Rules And GameState
- there is a class type named *ActionQuery* these are the actions sent to the *IUtilityMatchQueries* and functions of the class decide that the input is valid or not and apply it to the *MatchState*


#### Opponent And Ai
-  *Opponent* is a high level class that can implement *AIOpponent* or *OnlineOpponent*. *ApplyAction* function can have different implementations. for current we only have ai



##### best approaches for making AI For turn based games are
 - Minimax: as state space is to big, recursive minimax simply cause overflow. also making itrative version is challenging and we cant add enemy class or difficulty level.
 - Data Driven: time consuming for a test project to define possible rules but for AI with series of action works great.
 - Goal Oriented Action Planning. also great but needs some effort to make a planner integrated with such space.
 - Utility Theorem also great that on different conditions gives score to the actions and selects action with highest score. its time consuming to select best multipliers.
 
 - Chain Of Responsiblity: its a design pattern rather than being a technique for Ai. list of actions are defined and ai agent selects the first action that suits the condition. 
 there are different implementations for this design pattern but its a very simple version. we can make different classes of opponents by just changing the order of these *condition-actions*


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
