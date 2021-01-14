using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiceManager : MonoBehaviour
{
 
    public RollingDice redHomeRollingDice, blueHomeRollingDice;
    BlueHome _BlueHome;
    RedHome _RedHome;

    private void Start()
    {
        // _aiTurn = false;
        _BlueHome = GameObject.Find("BlueHome").GetComponent<BlueHome>();
        _RedHome = GameObject.Find("RedHome").GetComponent<RedHome>();
        blueHomeRollingDice = GameObject.Find("BlueHome").GetComponent<BlueHome>().rollingDice;
        redHomeRollingDice = GameObject.Find("RedHome").GetComponent<RedHome>().rollingDice;       
    }
   
   public IEnumerator StartRed(int sec, bool _set)
    {
        yield return new WaitForSeconds(sec);
        redHomeRollingDice._rTurn = _set;
    }
    public void MakePlayerMovable(bool _value)
    {
        foreach(PlayerPiece p in _BlueHome.playerPieces)
        {
            p._movePcOnce = _value;
            p.blueAi = true;
        }
       
        //PlayerPiece playerPiece=GetComponent<PlayerPiece>()
    }
  
    IEnumerator DiceChange(float second)
    {
       yield return new WaitForSeconds(second);

    }


    public bool AreHome(PlayerPiece[] _players)
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].isReady == true)
            {
                return true;
            }
        }
        return false;
    }

   public void BlueAIMode()
    {
        foreach (PlayerPiece p in _BlueHome.playerPieces)
        {
            
            p.blueAi = true;
        }
    }
    public void Blue()
    {
    //    print("Blue ");
        if (AreHome(_BlueHome.playerPieces) == false)
        {
            if (blueHomeRollingDice.numberGot !=6)
            {
                StartCoroutine(StartRed(1, false));
                blueHomeRollingDice._bTurn = true;
            }
        }
        //else
        //{
        //    if (blueHomeRollingDice.numberGot == 6)
        //    {
        //        StartCoroutine(StartBlue(3, false));
        //        //blueHomeRollingDice._bTurn = false;
        //    }
        //    //else
        //    //{
        //    //    StartCoroutine(StartRed(3, false));
        //    //    // redHomeRollingDice._rTurn = false;
        //    //}
        //}

    }

    public void Red()
    {       
        if (AreHome(_RedHome.playerPieces) == false)
        {
            if (redHomeRollingDice.numberGot != 6)
            {
                StartCoroutine(StartBlue(1, false));
                redHomeRollingDice._rTurn = true;
            }
        }
        //else
        //{
        //    //   print("RED ");
        //    if (redHomeRollingDice.numberGot != 6)
        //    {
        //        StartCoroutine(StartBlue(3, false));
        //        // StartCoroutine(StartRed(3, false));
        //        //redHomeRollingDice._rTurn = false;
        //    }
        //    //else
        //    //{

        //    //    //blueHomeRollingDice._bTurn = false;
        //    //}
        //}
    }
    public IEnumerator StartBlue(int sec,bool _bool)
    {
        yield return new WaitForSeconds(sec);
        blueHomeRollingDice._bTurn = _bool;
    }
    //void SkipTurn(PlayerPiece[] playerPieces,RollingDice rollingDice)
    //{
    //    foreach(PlayerPiece _player in playerPieces)
    //    {
    //        if (_player.canMove)
    //        {

    //        }
    //    }
    //    if (rollingDice == blueHomeRollingDice)
    //    {
    //        StartCoroutine(StartBlue(2, false));
    //    }
    //    else
    //    {
    //        StartCoroutine(StartRed(2, false));
    //    }
    //}

}
