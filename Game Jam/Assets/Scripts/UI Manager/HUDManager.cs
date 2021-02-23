using UnityEngine;
using UnityEngine.UI;
using LesserKnown.Public;

namespace LesserKnown.UI
{
    public class HUDManager: MonoBehaviour
    {
        public Image health_ui;
        public Image[] gold_ui = new Image[2];
        public Sprite[] digits = new Sprite[10];

       


        public void Set_Amounts(int health, int gold)
        {
            health_ui.sprite = digits[health];

           
                int first_digit = gold / 10;
                int second_digit = gold - (first_digit * 10);

            gold_ui[0].sprite = digits[first_digit];
            gold_ui[1].sprite = digits[second_digit];
            
        }
    }
}
