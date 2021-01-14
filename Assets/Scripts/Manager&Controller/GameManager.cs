using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RollDice
{
    BLUE,
    RED
}
public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public int numOfStepsToMove;
    public RollingDice rolledDice;
    [SerializeField]
    RollingDice Player, Ai;
    public bool killed;
    float killTime,holdTime=1f;
    List<PathPoints> playerOnPathPointsList = new List<PathPoints>();


    private void Awake()
    {
        gm = this;

    }

    public void AddPathPoint(PathPoints pathPoint_)
    {
        playerOnPathPointsList.Add(pathPoint_);
    }

 
    public void RemovePathPoint(PathPoints pathPoint_)
    {
        if (playerOnPathPointsList.Contains(pathPoint_))
        {
            playerOnPathPointsList.Remove(pathPoint_);
        }
        else
        {
            Debug.Log("PP Removed");
        }
    }
    public void State(PlayerPiece player)

    {

       
        Debug.Log("Player " + player);
        Debug.Log("Dice " + rolledDice);
        if (player.movementDone || player.broughtFromHome)
        {
           
            
            player.movementDone = false;
            player.broughtFromHome = false;

          
            if (rolledDice == Player)
            {

                killed = player.DeletePawn(player,Player.numberGot);
                //killed = AIManager.current.CheckForKill(Player.numberGot, player, "Red", player.pathsParent.bluePathPoints);
                killTime = 0;
                if (rolledDice.numberGot == 6 || killed == true )
                {
                    if (killed == true)
                    {
                    
                        killed = false;
                        killTime = 3f;
                    }
                    StartCoroutine(StartBlue((holdTime + killTime), false));
                }
                else
                {
                    StartCoroutine(StartRed(1, false));
                }


            }
            if (rolledDice == Ai)
            {
                killTime = 0;
                if (rolledDice.numberGot == 6 || killed == true)
                {
                    if (killed == true)
                    {
                       
                        killed = false;
                        killTime = 3f;
                    }
                    
                    StartCoroutine(StartRed((holdTime+killTime), false)); 
                }
                else
                {
                    StartCoroutine(StartBlue(1, false));
                }
            }

        }
    }

    public IEnumerator StartBlue(float sec, bool _bool)
    {
        yield return new WaitForSeconds(sec);
        Player._bTurn = _bool;
    }
    public IEnumerator StartRed(float sec, bool _bool)
    {
        yield return new WaitForSeconds(sec);
        Ai._rTurn = _bool;
    }
}
