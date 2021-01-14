using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public PathObjectsParent pathObjParent;
    public List<PlayerPiece> playerPiecesList = new List<PlayerPiece>();

    private void Start()
    {
        pathObjParent = GetComponentInParent<PathObjectsParent>();
    }

    public void AddPlayerPiece(PlayerPiece playerPiece_)
    {
        playerPiecesList.Add(playerPiece_);
        RescaleAndReposAllPlayerPieces();
    }

    public void RemovePlayerPiece(PlayerPiece playerPiece_)
    {
        if(playerPiecesList.Contains(playerPiece_))
        {
            playerPiecesList.Remove(playerPiece_);
        }  
    }

    public void RescaleAndReposAllPlayerPieces()
    {
        int plsCount = playerPiecesList.Count;
        bool isOdd = (plsCount % 2) ==0 ? false : true;
        int spriteLayers = 0;

        int extent = plsCount / 2;
        int counter =0;

        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjParent.scales[plsCount - 1], pathObjParent.scales[plsCount - 1], 1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjParent.positionsDifference[plsCount - 1]),
                transform.position.y, 0f);
                counter++;
            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjParent.scales[plsCount - 1], pathObjParent.scales[plsCount - 1], 1f);
                playerPiecesList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjParent.positionsDifference[plsCount - 1]),
                transform.position.y, 0f);
                counter++;
            }
        }

        for (int i = 0; i < playerPiecesList.Count; i++)
        {
            playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayers;
            spriteLayers++;
        }
    }

}
