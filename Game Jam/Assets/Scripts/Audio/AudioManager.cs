using LesserKnown.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LesserKnown.Audio
{
    //MusicSources : 2 x audioSource on Camera
    //PlayerSource : 1 x audioSource / player

    public class AudioManager : MonoBehaviour
    {

        [Header(" Players reference")]
        public CharacterController2D[] players;

        //[Header(" Scripts Audio")]
        private static AudioManager _instance = null; //singleton
        private AudioPlayerActions audioPlayerScript;
        private AudioMusicSystem musicSystemScript;
        private Dictionary<string, System.Action> soundEvtDict = new Dictionary<string, System.Action>();

        
        [Header(" Volume Control")]
        //quick and dirty play music on - off .... eventually ill make a better system for mute/unmute, etc.
        public bool musicPlayOnStart = false;
        public float playerVolume = 0.5f;
        public float musicVolume = 0.5f;


        [Header(" Audio Sources")]
        public AudioSource[] musicSources;
        public AudioSource[] playerSources;
        public AudioSource currentPlayerSource;

        void Start()
        {
            musicSources = UnityEngine.Camera.main.GetComponents<AudioSource>();            
            audioPlayerScript = GetComponent<AudioPlayerActions>();
            musicSystemScript = GetComponent<AudioMusicSystem>();

            _Init_AudioAnimEventsRefs();

            //volume musique speakers 
            foreach (AudioSource s in musicSources)
            {
               s.volume = musicVolume; 
            }
            //volume player speakers 
            foreach (AudioSource s in playerSources)
            {
               s.volume = musicVolume; 
            } 
        }

        // Update is called once per frame
        void Update()
        {
            currentPlayerSource = GetActivePlayerSource();
        }

        void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else if(_instance !=this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlayPlayerSource(AudioClip clip)
        {
            //Debug.Log("Speaker "+numero+"Sound : "+clip );
            currentPlayerSource.PlayOneShot(clip);
        }
        public void StopPlayerSource(AudioClip clip)
        {
            currentPlayerSource.clip = clip;
            currentPlayerSource.Stop();
        }

        //Managing the Animator event calls for all anims
        public void PlaySndEvent(AnimManager anim,string call)
        {
            if(anim.controller.is_active)
                soundEvtDict[call]();
        }

        private void _Init_AudioAnimEventsRefs()
        {
            soundEvtDict ["footsteps"] = audioPlayerScript.PlayFootsteps;
            soundEvtDict ["climb"] = audioPlayerScript.PlayClimb;
            soundEvtDict ["wallclimb"] = audioPlayerScript.PlayWallClimb;
            soundEvtDict ["jump"] = audioPlayerScript.PlayJump;
            soundEvtDict ["punch"] = audioPlayerScript.PlayPunch;
            soundEvtDict ["hit"] = audioPlayerScript.PlayHit;
            soundEvtDict ["teleport"] = audioPlayerScript.PlayTeleport;
            soundEvtDict ["fall"] = audioPlayerScript.PlayFall;
            soundEvtDict ["pickup"] = audioPlayerScript.PlayPickUp;
            soundEvtDict ["throw"] = audioPlayerScript.PlayThrow;
        }

        public AudioSource[] GetMusicSources(){return musicSources;}

        public AudioSource GetActivePlayerSource()
        {
           foreach(CharacterController2D player in players)
           {
               //Debug.Log("Player active is : ");
               if(player.is_active && player.is_fighter)
                    return playerSources[1];
                else
                    return playerSources[0];
           }
           
           return null;
        }

        public static AudioManager Instance { get { return _instance; } }  

    } 

}

// Pause all Audio Sources
//AudioListener.pause = true; -- Get Listener from Camera