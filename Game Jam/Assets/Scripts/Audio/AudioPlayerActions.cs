using LesserKnown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Audio{
        public class AudioPlayerActions : MonoBehaviour
        {

            [Header(" AudioClips Footsteps")]
            public AudioClip [] FS_terrain;
            public AudioClip [] FS_platform;
            public AudioClip[] currentFootsteps; 

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
            
            void Start()
            {
                currentFootsteps = FS_terrain; //default start  FS              
            }

            void Update()
            {
                foreach(CharacterController2D player in AudioManager.Instance.players)
                {
                    if (player.IsGrounded())
                    {
                        //Debug.Log("FS terrain!");
                        currentFootsteps = FS_terrain;
                    }
                    else if(player.IsOnPlatform())
                    {
                        //Debug.Log("Fs plateform !");
                        currentFootsteps = FS_platform;
                    }
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    PlaySwap();
                }
            }

            public void PlayFootsteps()
            {
                //Debug.Log("FOOTSTEP !");
                SetRandomVariations(volumeFSTerrain,pitchFSTerrain); //set somthing, by type of FS?
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(currentFootsteps));
            }
            public void PlayClimb()
            {
                //Debug.Log("CLIMB !");
                SetRandomVariations(volumeClimb,pitchClimb);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_Climb));
            }
             public void PlayPunch()
            {
                //Debug.Log("PUNCH !");                
                SetRandomVariations(volumePunch,pitchPunch);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_Whoosh));
            }
            public void PlayWallClimb()
            {
                //Debug.Log("WALL CLIMB!");
                SetRandomVariations(volumeClimb,pitchClimb);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_WallStick));
            }
            public void PlayJump()
            {
                //Debug.Log("JUMP !");
                SetRandomVariations(volumeJump,pitchJump);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_Jump));
            }
            public void PlayHit()
            {
                //Debug.Log("HIT !");
                SetRandomVariations(volumeHit,0.8f);
                AudioManager.Instance.PlayPlayerSource(Snd_Hit[0]);
            }
           public void PlayTeleport()
            {
                //Debug.Log("Teleport !");
                SetRandomVariations(0.8f,0.8f);
                AudioManager.Instance.PlayPlayerSource(Snd_Hit[1]);
            }
            public void PlayFall()
            {
                //Debug.Log("FALL !");
                SetRandomVariations(volumeFall,pitchFall);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_Fall));
            }
            public void PlayPickUp()
            {
                //Debug.Log("Pick up !");
                SetRandomVariations(0.8f,0.8f);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_PickUp));
            }
            public void PlayThrow()
            {
                //Debug.Log("Throw !");
                SetRandomVariations(0.8f,0.8f);
                AudioManager.Instance.PlayPlayerSource(PickRandomClip(Snd_Throw));
            }
            public void PlaySwap()
            {
                //Debug.Log("swap !");
                SetRandomVariations(volumeSwap,pitchSwap);
                AudioManager.Instance.PlayPlayerSource(Snd_Swap);
            }
            void SetRandomVariations(float vol,float pitch)
            {
                //working, but could be dealt with better
                float minV = vol - (volRange/2); 
                float maxV = (volRange/2) + vol;
                float minP = pitch - (pitchRange/2);
                float maxP = (volRange/2) +pitch;

                AudioManager.Instance.currentPlayerSource.pitch = (float) Random.Range(minP,maxP);
                AudioManager.Instance.currentPlayerSource.volume = (float) Random.Range(minV,maxV);
            }

             AudioClip PickRandomClip(AudioClip[] clipArray)
            {
                if (clipArray != null)
                    return clipArray[Random.Range(0, clipArray.Length - 1)];
                else
                    return null;
            }
    }
}