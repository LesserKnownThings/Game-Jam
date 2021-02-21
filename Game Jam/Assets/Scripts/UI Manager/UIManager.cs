using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace LesserKnown.UI
{
    public class UIManager: MonoBehaviour
    {
        public float info_timer_disable = 2f;
        public CanvasGroup info_ui;
        public CanvasGroup game_lost_ui;
        public CanvasGroup game_won_ui;

        private void Start()
        {
            game_lost_ui.gameObject.SetActive(false);
            game_lost_ui.alpha = 0;

            game_won_ui.gameObject.SetActive(false);
            game_won_ui.alpha = 0f;

            StartCoroutine(Disable_Delay());
        }

        private IEnumerator Disable_Delay()
        {
            yield return new WaitForSeconds(info_timer_disable);

            while(info_ui.alpha > 0)
            {
                info_ui.alpha -= Time.deltaTime * 1.5f;
                yield return null;
            }

            info_ui.gameObject.SetActive(false);
        }

        public void Game_Lost()
        {
            StartCoroutine(Game_LostIE());
        }

        private IEnumerator Game_LostIE()
        {
            game_lost_ui.gameObject.SetActive(true);
            while (game_lost_ui.alpha < 1)
            {
                game_lost_ui.alpha += Time.deltaTime * 1.5f;
                yield return null;
            }
            
        }

        public void Game_Won()
        {
            StartCoroutine(Game_WonIE());
        }

        private IEnumerator Game_WonIE()
        {
            game_won_ui.gameObject.SetActive(true);
            while(game_won_ui.alpha < 1)
            {
                game_won_ui.alpha += Time.deltaTime;
                yield return null;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ReloadLevel()
        {
            int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(_currentSceneIndex);
        }
    }
}
