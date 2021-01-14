using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPP : PlayerPiece
{
    public bool aiMove;
    public RollingDice redHomeRollingDice;
    RedHome redHome;
    public AIManager aiManager;
    DiceManager diceManager;
 
    
    private void Start()
    {
        
        redHome = GetComponentInParent<RedHome>();
        redHomeRollingDice = GetComponentInParent<RedHome>().rollingDice;
        aiManager = GetComponentInParent<AIManager>();
        diceManager = GameObject.Find("DiceManager").GetComponent<DiceManager>();
    }

    private void OnMouseDown()
    {
        //if (GameManager.gm.rolledDice != null)
        //{
        //    if (!isReady)
        //    {
        //        if (GameManager.gm.rolledDice == redHomeRollingDice && GameManager.gm.numOfStepsToMove == 6)
        //        {
        //            MakePlayerReadyToMove(pathsParent.redPathPoints);
        //            GameManager.gm.numOfStepsToMove = 0;
        //            // StartCoroutine(AISix(3f));
        //            return;
        //        }
        //    }
        //    if (GameManager.gm.rolledDice == redHomeRollingDice && isReady)
        //    {
        //        canMove = true;
        //    }
        //}
        //MoveSteps(pathsParent.redPathPoints);

        // DiceManager.current.Halt(State.GREEN);
    }
    private void Update()
    {
        //print("NUmber On red Script " + redHomeRollingDice.numberGot);
        if (aiMove == true)
        {           
            aiManager.CalculateProbablity(redHomeRollingDice.numberGot, redHome.playerPieces);
            //if (redHomeRollingDice.numberGot == 6)
            //{
            //    StartCoroutine(AISix(3f));
            //}
            aiMove = false;
            //diceManager.Red();
        }
    }


   
}
