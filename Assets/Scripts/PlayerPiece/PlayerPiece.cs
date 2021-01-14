using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    //variable


    [SerializeField]
    public Transform _base;
    public bool _movePcOnce;
    public bool isReady;
    public bool canMove;
    public bool moveNow;
    public int numberOfStepsAlreadyMoved;
    public PathObjectsParent pathsParent;
    public PathPoints previousPathPoint;
    public PathPoints currentPathPoint;
    Coroutine moveSteps_Coroutine;


    int _index;
    public bool movementDone,blueAi,broughtFromHome;
    private void Awake()
    {


        pathsParent = FindObjectOfType<PathObjectsParent>();
    }

    public void MoveSteps(PathPoints[] pathPointsToMoneOn_)
    {
        moveSteps_Coroutine = StartCoroutine(MoveSteps_Enum(pathPointsToMoneOn_));

    }

    public void MakePlayerReadyToMove(PathPoints[] pathPointsToMoneOn_)
    {
        isReady = true;
        transform.position = pathPointsToMoneOn_[0].transform.position;
        numberOfStepsAlreadyMoved = 1;
        previousPathPoint = pathPointsToMoneOn_[0];
        currentPathPoint = pathPointsToMoneOn_[0];
        GameManager.gm.AddPathPoint(currentPathPoint);

    }

    IEnumerator MoveSteps_Enum(PathPoints[] pathPointsToMoneOn_)
    {
        yield return new WaitForSeconds(0.25f);
        int numOfStepsToMove = GameManager.gm.numOfStepsToMove;

        if (canMove)
        {
            previousPathPoint.RescaleAndReposAllPlayerPieces();
            for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numOfStepsToMove); i++)
            {
                if (isPathPointsAvailableToMove(numOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoneOn_))
                {
                    transform.position = pathPointsToMoneOn_[i].transform.position;
                    yield return new WaitForSeconds(0.5f);

                }
            }
           
        }
            movementDone = true;
           
            GameManager.gm.State(this);
        if (isPathPointsAvailableToMove(numOfStepsToMove, numberOfStepsAlreadyMoved, pathPointsToMoneOn_))
        {
            numberOfStepsAlreadyMoved += numOfStepsToMove;

            GameManager.gm.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);
            currentPathPoint = pathPointsToMoneOn_[numberOfStepsAlreadyMoved - 1];
            currentPathPoint.AddPlayerPiece(this);
            GameManager.gm.AddPathPoint(currentPathPoint);
            previousPathPoint = currentPathPoint;
        }
        if (moveSteps_Coroutine != null)
        {
            StopCoroutine(moveSteps_Coroutine);
        }
    }

    public bool isPathPointsAvailableToMove(int numOfStepsToMove_, int numberOfStepsAlreadyMoved_, PathPoints[] pathPointsToMoneOn_)
    {
        int leftNumOfPathPoints = pathPointsToMoneOn_.Length - numberOfStepsAlreadyMoved_;
        if (leftNumOfPathPoints >= numOfStepsToMove_)
        {
            return true;
        }
        return false;
    }

    public void ResetToken(PlayerPiece _p, PathPoints pathPoints)
    {

        print("Pathpoint " + pathPoints);
        pathPoints.RemovePlayerPiece(_p);
        _p.transform.localPosition = _p._base.localPosition;
        //_p.currentPathPoint.RemovePlayerPiece(_p);
        pathPoints.RemovePlayerPiece(_p);
        _p.isReady = false;
        _p.canMove = false;
        _p.numberOfStepsAlreadyMoved = 0;
        return;


    }
    public bool DeletePawn(PlayerPiece _P,int num)
    {
        if (_P.isReady)
        {
            _index = GetIndex(_P);

            _index = _index + num;
            if (currentPathPoint != null && currentPathPoint.tag!="Home")
                if(_index <= pathsParent.redPathPoints.Length)
                if (pathsParent.bluePathPoints[_index].playerPiecesList.Count == 1)
                {
                    if (pathsParent.bluePathPoints[_index].playerPiecesList[0].tag == "Red" && pathsParent.bluePathPoints[_index].tag != "SafeState")
                    {

                        ResetToken(pathsParent.bluePathPoints[_index].playerPiecesList[0], pathsParent.bluePathPoints[_index]);
                        //StartCoroutine(StartBlue(3, false));
                        return true;
                    }
                }
           
        } return false;
    }
    private int GetIndex(PlayerPiece playerPiece)
    {
      
        for (int i = 0; i <= pathsParent.bluePathPoints.Length; i++)
        {
            Debug.Log(" Player Piece " + playerPiece + "Index " + i);
            if (pathsParent.bluePathPoints[i] == playerPiece.currentPathPoint)
            {
                _index = i;
                break;
            }
        }
        return _index;
    }
}

