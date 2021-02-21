using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Public;
using UnityEngine.SceneManagement;
using LesserKnown.UI;
namespace LesserKnown.Manager
{
    /// <summary>
    /// This is the game manager class
    /// It controlls the game
    /// It was done for another scene so it needs some tweaking
    /// </summary>
    public class GameManagement : MonoBehaviour
    {
        public GameObject health_obj;
        public Transform health_holder;
        public GameObject mobile_ui;

        private GameObject player;
        private UIManager ui_manager;

        private List<GameObject> health_list = new List<GameObject>();

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ui_manager = GetComponent<UIManager>();
            
            if (SystemInfo.deviceType != DeviceType.Handheld)
                mobile_ui.SetActive(false);
            

            for (int i = 0; i < PublicVariables.health; i++)
            {
                var _copy = Instantiate(health_obj);
                _copy.transform.SetParent(health_holder, true);
                health_list.Add(_copy);
            }
        }

        public void Die()
        {            
            StartCoroutine(DelayDestroy());
        }

        private IEnumerator DelayDestroy()
        {
            int _index = health_list.FindLastIndex(x => { return x != null; });

            health_list[_index].GetComponent<Animator>().SetTrigger("Destroy");

            yield return new WaitForSeconds(.2f);
            Destroy(health_list[_index]);
            PublicVariables.Lose_Health(1);

            if (PublicVariables.health == 0)
            {
                Game_Over(false);
            }
                
        }

        public void Game_Over(bool won)
        {
            player.SetActive(false);

            
        }

        private void OnLevelWasLoaded(int level)
        {
            PublicVariables.Reset_Level(10);
        }
    }
}
