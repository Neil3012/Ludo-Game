using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class BluePP : PlayerPiece
{

    public RollingDice blueHomeRollingDice;
    public DiceManager diceManager;
    BlueHome blueHome;
    PlayerPiece temp;
    public bool aiMode;

    private void Start()
    {
        temp = GetComponent<PlayerPiece>();
        _movePcOnce = true;
        blueHome = GetComponentInParent<BlueHome>();
        blueHomeRollingDice = blueHome.rollingDice;
        diceManager = GameObject.Find("DiceManager").GetComponent<DiceManager>();
    }

    private void OnMouseDown()
    {
        if (_movePcOnce == true)
        {
            diceManager.MakePlayerMovable(false);
            if (GameManager.gm.rolledDice != null)
            {
                if (!isReady)
                {
                    if (GameManager.gm.rolledDice == blueHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
                    {
                        MakePlayerReadyToMove(pathsParent.bluePathPoints);
                        GameManager.gm.numOfStepsToMove = 0;
                        broughtFromHome = true;
                        GameManager.gm.State(this);
                        //StartCoroutine(StartBlue(1, false));
                        canMove = true; blueAi = false;
                        return;
                    }
                }
                if (GameManager.gm.rolledDice == blueHomeRollingDice && isReady)
                {

                    //diceManager.Blue();
                    //if (blueHomeRollingDice.numberGot != 6 && isReady == true)
                    //{
                    //    StartCoroutine(StartRed(3, false));
                    //}
                    MoveSteps(pathsParent.bluePathPoints);
                    
                }
            }

        }

    }

    public void CheckForAIMove()
    {
        int count = 0;
        foreach (PlayerPiece _p in blueHome.playerPieces)
        {
            if (_p.canMove && _p.isReady)
            {
                if (isPathPointsAvailableToMove(blueHomeRollingDice.numberGot, _p.numberOfStepsAlreadyMoved, pathsParent.bluePathPoints))
                {
                    temp = _p;
                    count += 1;
                }
            }
        }
        if (count == 1 && isReady)
        {
            if (isPathPointsAvailableToMove(blueHomeRollingDice.numberGot, numberOfStepsAlreadyMoved, pathsParent.bluePathPoints))
            {              
                MoveSteps(pathsParent.bluePathPoints);             
            }

        }
        else if(count==0 && diceManager.AreHome(blueHome.playerPieces) == true)
        {
            movementDone = true;
            GameManager.gm.State(this);
        }
    
 
    }

    private void Update()
    {
        if (blueAi == true && GameManager.gm.rolledDice == blueHomeRollingDice)
        {
            blueAi = false;
            if (blueHomeRollingDice.numberGot != 6)
            {
                CheckForAIMove();
            }
        }
     
        //if (movementDone == true)
        //{
        //    movementDone = false;
        //    print("Count ");          
    }

    IEnumerator StartBlue(int sec, bool _bool)
    {
        yield return new WaitForSeconds(sec);
        blueHomeRollingDice._bTurn = _bool;
    }
    IEnumerator StartRed(int sec, bool _bool)
    {
        yield return new WaitForSeconds(sec);
        diceManager.redHomeRollingDice._rTurn = _bool;
    }


}
