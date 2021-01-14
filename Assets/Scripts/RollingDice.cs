using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RollingDice : MonoBehaviour
{
    [SerializeField] public int numberGot;
    [SerializeField] GameObject rollingDiceAnim;
    [SerializeField] SpriteRenderer numberedSpHolder;
    [SerializeField] Sprite[] numberedSprites;
    public bool canDiceRoll = true;
    Coroutine generateRandomNumOnDice_Coroutine;
    public RedPP redPP;
    //  public BluePP bluePP;


    public bool _bTurn, _rTurn;

    DiceManager diceManager;

    private void Start()
    {

        diceManager = GameObject.Find("DiceManager").GetComponent<DiceManager>();
        _bTurn = false;
        _rTurn = true;
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "Player") {
            if (_bTurn == false)
            {
                _bTurn = true;

                diceManager.MakePlayerMovable(true);
                generateRandomNumOnDice_Coroutine = StartCoroutine(GenerateRandomNumberOnDice_Enum());
                StartCoroutine(HoldBlue(1.2f));
            }
        }
    }
    private void Update()
    {
        if (_rTurn == false)
        {
            _rTurn = true;
            generateRandomNumOnDice_Coroutine = StartCoroutine(GenerateRandomNumberOnDice_Enum());
            StartCoroutine(HoldRed(1.2f));

        }
    }
    IEnumerator HoldRed(float sec)
    {
        yield return new WaitForSeconds(sec);
        diceManager.Red();
    }
    IEnumerator HoldBlue(float sec)
    {
        yield return new WaitForSeconds(sec);
        diceManager.BlueAIMode();
        diceManager.Blue();
    }
    internal IEnumerator GenerateRandomNumberOnDice_Enum()
    {
        //yield return new WaitForEndOfFrame();

        if (canDiceRoll)
        {
            canDiceRoll = false;
            numberedSpHolder.gameObject.SetActive(false);
            rollingDiceAnim.SetActive(true);

            yield return new WaitForSeconds(1f);

            numberGot = Random.Range(0, 6);

            numberedSpHolder.sprite = numberedSprites[numberGot];
            numberGot += 1;

            GameManager.gm.numOfStepsToMove = numberGot;
            GameManager.gm.rolledDice = this;

            numberedSpHolder.gameObject.SetActive(true);
            rollingDiceAnim.SetActive(false);

            yield return new WaitForEndOfFrame();
            if (redPP != null)
            {
                redPP.aiMove = true;
            }
            //if (bluePP != null)
            //{
            //    bluePP.aiMode = true;
            //}

      

            if (generateRandomNumOnDice_Coroutine != null)
            {
                StopCoroutine(generateRandomNumOnDice_Coroutine);
            }
            canDiceRoll = true;
            yield return new WaitForSeconds(2f);




        }
    }


}
