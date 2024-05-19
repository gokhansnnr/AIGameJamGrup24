using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static state gameState;

    public static state getState() { return gameState; }

    public static void changeState(state newState) 
    { 
        gameState = newState;
    }
}

public enum state { play, pause }