using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    static public bool hasPlayer1 = true;
<<<<<<< HEAD
    static public bool hasPlayer2 = false;
    static public bool hasPlayer3 = false;
    static public bool hasPlayer4 = false;
=======
    static public bool hasPlayer2 = true;
    static public bool hasPlayer3 = false;
    static public bool hasPlayer4 = false;

    static public int playerScore1 = 0;
    static public int playerScore2 = 0;
    static public int playerScore3 = 0;
    static public int playerScore4 = 0;

>>>>>>> fef8f1d94832b3b1e2cd8d9c8b56af079957207f
    static public int NumPlayers()
    {
        int numPlayers = 0;
        numPlayers += hasPlayer1 ? 1 : 0;
        numPlayers += hasPlayer2 ? 1 : 0;
        numPlayers += hasPlayer3 ? 1 : 0;
        numPlayers += hasPlayer4 ? 1 : 0;

        return numPlayers;
    }
}
