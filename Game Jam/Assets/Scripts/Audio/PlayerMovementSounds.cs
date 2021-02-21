using LesserKnown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Audio{
        public class PlayerMovementSounds : MonoBehaviour
        {

            //again, some of these public var will go private after Tests

            //PLACEHOLDER SOUNDS FOR THE MOMENT .. I WILL WORK ON BETTER ONES
            //WHEN EVERYTHING IS WORKING FINE :)

            [Header(" AudioClips Footsteps")]
            public AudioClip [] FS_terrain;
            public AudioClip [] FS_platform;
            public AudioClip[] currentFootsteps; //FOR TESTS 

            [Header(" AudioClips Player Mvt")]
            public AudioClip [] Snd_Jump;
            public AudioClip [] Snd_Climb;
            public AudioClip [] Snd_WallStick;
            public AudioClip [] Snd_Whoosh;
            public AudioClip [] Snd_Hit;
            public AudioClip [] Snd_Fall;
            public AudioClip [] Snd_PickUp;
            public AudioClip [] Snd_Throw;

            [Header(" Audio Clip - UI ")]
            public AudioClip Snd_Swap;

            [Header("Set Volume")]
            public float volumeFSTerrain = 0.5f;
            public float volumeFS_platform = 0.5f;
            public float volumeJump = 0.5f;
            public float volumeWallStick= 0.5f;
            public float volumeClimb= 0.5f;
            public float volumePunch= 0.5f;
            public float volumeFall= 0.5f;
            public float volumeHit= 0.5f;
            public float volumeSwap= 0.5f;

            [Header("Set Pitch")]
            public float pitchFSTerrain = 0.8f;
            public float pitchFS_platform = 0.8f;
            public float pitchJump = 0.8f;
            public float pitchClimb = 0.8f;
            public float pitchWallStick = 0.8f;
            public float pitchPunch = 0.8f;
            public float pitchFall = 0.8f;
            public float pitchSwap = 0.8f;
            //Range of value for the randoms
            private float volRange = 0.4f;
            private float pitchRange = 0.4f;

            //private values for references
            private AudioClip currentClip;
            private AudioSource currentPlayerSource;
            private int togglePlayerSource = 0;
            private string playerActive;
        
            void Start()
            {
                currentFootsteps = FS_terrain; //default start                
            }

            void Update()
            {
                // if(!AudioManager.Instance.player.is_active) //useless atm
                //     playerActive = "Solver";
                // else
                //     playerActive = "Fighter";

                currentPlayerSource = AudioManager.Instance.GetPlayerSource();


                if(AudioManager.Instance.player.IsGrounded())
                {
                    //Debug.Log("SUR TERRAIN!");
                    currentFootsteps = FS_terrain;
                }

                else if(AudioManager.Instance.player.IsOnPlatform())
                {
                    //Debug.Log("SUR PLATFORM!");
                    currentFootsteps = FS_platform;
                }

                // if(AudioManager.Instance.player.is_fighter) //useless atm
                //     togglePlayerSource = 1; 
                // else
                //     togglePlayerSource = 0;

                if (Input.GetKeyDown(KeyCode.C))
                {
                    //Swap caracters sounds
                    SetRandomVariations(volumeSwap,pitchSwap);
                    AudioManager.Instance.PlayPlayerSource(Snd_Swap);
                }

                
            }


            public void PlayFootsteps()
            {
                //Debug.Log("FOOTSTEP !");
                SetRandomVariations(volumeFSTerrain,pitchFSTerrain); //set somthing, by type of FS?
                AudioManager.Instance.PlayPlayerSource(RandomClip(currentFootsteps));
            }
            public void PlayClimb()
            {
                //Debug.Log("CLIMB !");
                SetRandomVariations(volumeClimb,pitchClimb);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Climb));
            }
             public void PlayPunch()
            {
                //Debug.Log("PUNCH !");                
                SetRandomVariations(volumePunch,pitchPunch);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Whoosh));
            }
            public void PlayWallClimb()
            {
                //Debug.Log("WALL CLIMB!");
                SetRandomVariations(volumeClimb,pitchClimb);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_WallStick));
            }
            public void PlayJump()
            {
                //Debug.Log("JUMP !");
                SetRandomVariations(volumeJump,pitchJump);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Jump));
            }
            public void PlayHit()
            {
                //Debug.Log("HIT !");
                SetRandomVariations(volumeHit,0.8f);
               AudioManager.Instance.PlayPlayerSource(Snd_Hit[0]);
            }
           public void PlayTeleport()
            {
                //Debug.Log("HIT !");
                SetRandomVariations(0.8f,0.8f);
               AudioManager.Instance.PlayPlayerSource(Snd_Hit[1]);
            }
            public void PlayFall()
            {
                //Debug.Log("FALL !");
                SetRandomVariations(volumeFall,pitchFall);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Fall));
            }
            public void PlayPickUp()
            {
                //Debug.Log("Pick up !");
                SetRandomVariations(0.8f,0.8f);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_PickUp));
            }
            public void PlayThrow()
            {
                //Debug.Log("Throw !");
                SetRandomVariations(0.8f,0.8f);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Throw));
            }

            void SetRandomVariations(float vol,float pitch)
            {
                float minV = vol - (volRange/2); //could be better 
                float maxV = (volRange/2) + vol;
                float minP = pitch - (pitchRange/2);
                float maxP = (volRange/2) +pitch;

                currentPlayerSource.pitch = (float) Random.Range(minP,maxP);
                currentPlayerSource.volume = (float) Random.Range(minV,maxV);
            }

             AudioClip RandomClip(AudioClip[] clipArray)
            {
                return clipArray[Random.Range(0, clipArray.Length-1)];
            }

    }
}
