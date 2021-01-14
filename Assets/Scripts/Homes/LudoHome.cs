using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LudoHome : MonoBehaviour
{
    public RollingDice rollingDice;
    public PathPoints _homeState;
    public PlayerPiece[] playerPieces;
    public bool IsAi;


    public bool CheckGameOver()
    {
        if (_homeState.playerPiecesList.Count == 4)
        {
            return true;        
        }
        return false;
        
    }

   
}
