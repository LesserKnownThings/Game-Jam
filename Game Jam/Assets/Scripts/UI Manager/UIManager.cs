using UnityEngine;
using LesserKnown.Player;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using LesserKnown.Public;

namespace LesserKnown.UI
{
    public class UIManager: MonoBehaviour
    {    
        private bool menu_display;

        public CanvasGroup menu_ui;
        public CanvasGroup first_message;

        [Header("Collectibles UI")]
        public TextMeshProUGUI coin_text;
        public TextMeshProUGUI heart_text;

        private void Start()
        {
            menu_ui.alpha = 0f;
            menu_ui.gameObject.SetActive(false);
        }

        private void Update()
        {
            Collection_Manager();
        }
        private void Collection_Manager()
        {
            coin_text.text = $"{PublicVariables.COINS:0} / {PublicVariables.MAX_COLLECTIBLES:0}";
            heart_text.text = $"{PublicVariables.APPLES:0} / {PublicVariables.MAX_COLLECTIBLES:0}";
        }

          

        public void Display_Options()
        {

        }

        public void Resume_Game()
        {

        }

        public void Quit_Game()
        {
            Application.Quit();
        }

        public void Continue()
        {
            StartCoroutine(ContinueIE());
        }

        private IEnumerator ContinueIE()
        {
            while(first_message.alpha > 0)
            {
                first_message.alpha -= Time.deltaTime;
                yield return null;
            }
            first_message.gameObject.SetActive(false);
        }
    }
}
