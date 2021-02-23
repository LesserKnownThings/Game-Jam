using UnityEngine;
using LesserKnown.Player;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using LesserKnown.Public;
using UnityEngine.UI;

namespace LesserKnown.UI
{
    public class UIManager: MonoBehaviour
    {
        public GameObject menu_ui;
        private bool menu_bool;
        public GameObject options_ui;
        private bool options_bool;

        public GameObject music_ui;
        public Image music_img;
        public GameObject inputs_ui;
        private bool inputs_bool = true;
        public Image inputs_img;

       
        private void UI_Display()
        {
            menu_ui.SetActive(menu_bool);
            options_ui.SetActive(options_bool);
            music_ui.SetActive(!inputs_bool);
            inputs_ui.SetActive(inputs_bool);

            if (inputs_bool)
            {
                inputs_img.color = Color.white;
                music_img.color = Color.black;
            }else if (!inputs_bool)
            {
                inputs_img.color = Color.black;
                music_img.color = Color.white;
            }
        }

        private void Update()
        {
            UI_Display();
        }

        public void Menu_Interaction()
        {
            menu_bool = !menu_bool;
            options_bool = false;
        }

        public void Options_Interaction()
        {
            options_bool = !options_bool;
            menu_bool = !options_bool;
        }

        public void Inputs_Interaction()
        {
            inputs_bool = true;

        }

        public void Music_Interaction()
        {
            inputs_bool = false; 

        }

        public void Exit_Interaction()
        {
            Application.Quit();
        }
    }
}
