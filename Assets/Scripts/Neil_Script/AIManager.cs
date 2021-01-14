using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PathObjectsParent pathsParents;

    public List<RedPP> _player;
    public Transform _homeState;
    [SerializeField] RollingDice rollingDice;
    public PlayerPiece playerPiece;
    public DiceManager diceManager;
    public int _index = 0, _Count = 0, pos = 0;
    public bool _safe, _Once, _step1, _step2, _step3, _step4, _skipTurn;
    string _Safe = "SafeState", _Blue = "Blue";
  
    void Start()
    {
        _skipTurn = true;
        rollingDice = GetComponentInChildren<RollingDice>();
    }
    private void Awake()
    {
        pathsParents = FindObjectOfType<PathObjectsParent>();
    }
    // Update is called once per frame


    public void CalculateProbablity(int num, PlayerPiece[] playerPiece)
    {
        //for (int i = 0; i < playerPiece.Length; i++)
        //{

        //}
        for (int i = 0; i < playerPiece.Length; i++)
        {
            if (CheckForINside(playerPiece[i]))
            {
                if (GameManager.gm.rolledDice == rollingDice && GameManager.gm.numOfStepsToMove == 6)
                {
                    _player[i].MakePlayerReadyToMove(_player[i].pathsParent.redPathPoints);
                    GameManager.gm.numOfStepsToMove = 0;
                    _player[i].broughtFromHome = true;
                    GameManager.gm.State(_player[i]);
                    _player[i].canMove = true;
                    return;
                }
            }
            if (CheckForKill(num, playerPiece[i]))
            {
                GameManager.gm.killed = true;
                playerPiece[i].MoveSteps(playerPiece[i].pathsParent.redPathPoints);
                //StartCoroutine(diceManager.StartRed(3, false));
                print("Killed");
                return;
            }

            if (CheckForSafe(num, playerPiece[i]))
            {
                playerPiece[i].MoveSteps(playerPiece[i].pathsParent.redPathPoints);
                return;
            }
            else if (CanReachedHome(num, playerPiece[i]))
            {
                playerPiece[i].MoveSteps(playerPiece[i].pathsParent.redPathPoints);
                return;
            }
            else if (FollowForKill(num, playerPiece[i], 6))
            {
                playerPiece[i].MoveSteps(playerPiece[i].pathsParent.redPathPoints);
                return;
            }

        }

        if (_step1 && _step2 && _step3 && _step4)
        {
            _skipTurn = true;
            for (int i = 0; i < _player.Count; i++)
            {
                if (_player[i].canMove && playerPiece[i].isPathPointsAvailableToMove(rollingDice.numberGot, playerPiece[i].numberOfStepsAlreadyMoved, playerPiece[i].pathsParent.redPathPoints))
                {
                    _skipTurn = false; ;
                    
                    playerPiece[i].MoveSteps(playerPiece[i].pathsParent.redPathPoints);
                    return;
                }
            }
            if (_skipTurn == true)
            {
                //  _skipTurn = false;
                Debug.Log("SKIP turn TRUE");
                StartCoroutine(diceManager.StartBlue(1, false));
            }
        }
    }


   public bool CheckForKill(int num, PlayerPiece playerPiece)
    {

        if (playerPiece.isReady)
        {
            _index = GetIndex(playerPiece);

            _index = _index + num;
            if (_index < pathsParents.redPathPoints.Length)
            {
                if (pathsParents.redPathPoints[_index].playerPiecesList.Count > 0)
                {
                    if (pathsParents.redPathPoints[_index].playerPiecesList[0].tag == _Blue && pathsParents.redPathPoints[_index].tag != _Safe)
                    {
                        playerPiece.ResetToken(pathsParents.redPathPoints[_index].playerPiecesList[0], pathsParents.redPathPoints[_index]);
                        return true;
                    }
                }
            }
        }

        _step2 = true;
        return false;
    }

    private bool CheckForINside(PlayerPiece _player)
    {
        // for (int i = 0; i < _player; i++)
        {
            if (!_player.isReady)
            {
                return true;
                //if (GameManager.gm.rolledDice == rollingDice && GameManager.gm.numOfStepsToMove == 6)
                //{
                //    _player.MakePlayerReadyToMove(_player.pathsParent.redPathPoints);
                //    GameManager.gm.numOfStepsToMove = 0;
                //    _player.canMove = true;
                //    return;
                //}
            }
            else
                return false;
        }
    }

    public bool CheckForSafe(int num, PlayerPiece playerPiece)
    {
        if (playerPiece.isReady)
        {
            _index = GetIndex(playerPiece);
            _index = _index + rollingDice.numberGot;
            if (_index < pathsParents.redPathPoints.Length)
            {
                if (pathsParents.redPathPoints[_index].transform.tag == _Safe)
                {
                    return true;
                }
            }
        }
        _step1 = true;
        return false;
    }

    private bool CanReachedHome(int num, PlayerPiece playerPiece)
    {
        if (playerPiece.isReady)
        {
            _index = GetIndex(playerPiece);
            _index = _index + rollingDice.numberGot;
            if (_index < pathsParents.redPathPoints.Length)
            {
                if (pathsParents.redPathPoints[_index].transform.position == _homeState.position)
                {
                    playerPiece.canMove = false;
                    return true;
                }
            }
        }
        _step3 = true;
        return false;
    }

    private int GetIndex(PlayerPiece playerPiece)
    {
        for (int i = 0; i <= pathsParents.redPathPoints.Length; i++)
        {
            if (pathsParents.redPathPoints[i] == playerPiece.currentPathPoint)
            {
                _index = i;
                break;
            }
        }
        return _index;
    }

    public bool FollowForKill(int num, PlayerPiece playerPiece, int followNO)
    {
        if (playerPiece.isReady)
        {
            _index = GetIndex(playerPiece);


            for (int i = _index; i <= _index + 6; i++)
            {
                if (_index + followNO < pathsParents.redPathPoints.Length)
                {
                    if (pathsParents.redPathPoints[i].playerPiecesList.Count > 0)
                    {
                        if (pathsParents.redPathPoints[i].playerPiecesList[0].tag == _Blue)
                        {
                            if (num < i - _index)
                            {
                                return true;
                            }

                        }
                    }
                }
            }
        }
        _step4 = true;
        return false;
    }

}
