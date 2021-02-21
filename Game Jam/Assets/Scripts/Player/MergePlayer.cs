using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;

public class MergePlayer : MonoBehaviour
{
    /*        public static int COINS;
        public static int APPLES;
        public static bool IS_FUSIONED;
        public static int MAX_COLLECTIBLES = 10;*/
    // Start is called before the first frame update

    public GameObject player1;
    public GameObject player2;
    public GameObject boss;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Merge();
    }

    public void Merge()
    {
        int apple = PublicVariables.APPLES;
        int coins = PublicVariables.COINS;
        int max = PublicVariables.MAX_COLLECTIBLES;

        // PublicVariables.IS_FUSIONED
        if (apple == max && coins == max)
        {
            player2.SetActive(false);
            PublicVariables.IS_FUSIONED = true;
            boss.SetActive(true);
        }

    }
}
