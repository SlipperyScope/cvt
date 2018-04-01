using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    static private int winningScore = 50;
    static public bool hasPlayer1 = true;
    static public bool hasPlayer2 = true;
    static public bool hasPlayer3 = true;
    static public bool hasPlayer4 = false;

    static public int playerScore1 = 0;
    static public int playerScore2 = 0;
    static public int playerScore3 = 0;
    static public int playerScore4 = 0;

    static public int numFinished = 0;
    static public int numDead = 0;
    static public CarSpecs Spec;
	static public List<PartPlacement> parts = new List<PartPlacement>();

    static public int NumPlayers()
    {
        int numPlayers = 0;
        numPlayers += hasPlayer1 ? 1 : 0;
        numPlayers += hasPlayer2 ? 1 : 0;
        numPlayers += hasPlayer3 ? 1 : 0;
        numPlayers += hasPlayer4 ? 1 : 0;

        return numPlayers;
    }

    static public bool haveWinner()
    {
        return playerScore1 >= winningScore || playerScore2 >= winningScore || playerScore3 >= winningScore || playerScore4 >= winningScore;
    }

    static public void resetStats()
    {
        hasPlayer1 = false;
        hasPlayer2 = false;
        hasPlayer3 = false;
        hasPlayer4 = false;

        playerScore1 = 0;
        playerScore2 = 0;
        playerScore3 = 0;
        playerScore4 = 0;

        numFinished = 0;
        numDead = 0;
        Spec = new CarSpecs();
        parts = new List<PartPlacement>();
    }
}
