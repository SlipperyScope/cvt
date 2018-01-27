using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    static public bool hasPlayer1 = true;
    static public bool hasPlayer2 = true;
    static public bool hasPlayer3 = true;
    static public bool hasPlayer4 = true;
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
