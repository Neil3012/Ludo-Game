using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRollingDice : MonoBehaviour
{
    [SerializeField] int numberGot;
    [SerializeField] GameObject rollingDiceAnim;
    [SerializeField] SpriteRenderer numberedSpHolder;
    [SerializeField] Sprite[] numberedSprites;
    bool canDiceRoll = true;
    Coroutine generateRandomNumOnDice_Coroutine;

    public bool isAi, _turn;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if ( RollingDice.rolling._turn == true)
        {
            generateRandomNumOnDice_Coroutine = StartCoroutine(GenerateRandomNumberOnDice_Enum());
            _turn = false;
        }
    }
    IEnumerator GenerateRandomNumberOnDice_Enum()
    {
        yield return new WaitForEndOfFrame();

        if (canDiceRoll)
        {
            canDiceRoll = false;
            numberedSpHolder.gameObject.SetActive(false);
            rollingDiceAnim.SetActive(true);
            _turn = true;
            yield return new WaitForSeconds(1f);

            numberGot = Random.Range(0, 6);
            numberedSpHolder.sprite = numberedSprites[numberGot];
            numberGot += 1;

            GameManager.gm.numOfStepsToMove = numberGot;
           // GameManager.gm.rolledDice = this;

            numberedSpHolder.gameObject.SetActive(true);
            rollingDiceAnim.SetActive(false);

            yield return new WaitForEndOfFrame();
            canDiceRoll = true;

            if (generateRandomNumOnDice_Coroutine != null)
            {
                StopCoroutine(generateRandomNumOnDice_Coroutine);

            }

        }
    }
}
