# Robot-Runners
My game is a very barebones recreation of the board game “Robo-Rally” from 1994
    
Since it’s based off of a board game, it is 2D, with a top-down perspective
    
Players get 9 random actions a turn and must choose a configuration of 5 that the robot they control will execute consecutively. Actions are unique functions that can vary from turning left/right, moving a certain number of spaces, backing up, etc.

The objective of the game is for players to reach an end goal, so it’s sort of a mix between a race game and a strategy game.

While the original board game had many mechanics, I was able to implement the following:

- A player robot
- An AI robot
- A goal
- Robot movement
- Robot interaction

As far as technical components:

My main focus was implementing game AI, allowing for the AI robot to make informed decisions as to how it uses its actions.

I also, allowed players to “crash” into each other, and bump one another to different spaces.

This game was programmed in C# using unity, however I wasn't able to get github to agree with the file sizes, so I uploaded my code here, and the dropbox link to my full project can be found here: https://www.dropbox.com/s/gcp8xxx0b1x9qwl/Robot-Runners-2D.zip?dl=0

The demo video can be found here:

[![Robot Runners](https://img.youtube.com/vi/TQSOTFvJ7tk/0.jpg)](http://www.youtube.com/watch?v=TQSOTFvJ7tk)
