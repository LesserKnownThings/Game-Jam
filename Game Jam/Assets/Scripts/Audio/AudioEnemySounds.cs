using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Audio {
    public class AudioEnemySounds : MonoBehaviour
    {
        AudioSource enemySource;

        [Header(" Audio Clips")]
        public AudioClip[] mushAttack;
        public AudioClip[] mushDeath;
        public AudioClip[] mushAppear;
        public AudioClip[] mushHit;
        private Dictionary<string, AudioClip[]> soundEvtDict_AI = new Dictionary<string, AudioClip[]>();

        //Range of value for the randoms
        private float volRange = 0.4f;
        private float pitchRange = 0.4f;

        void Start()
        {
            enemySource = GetComponent<AudioSource>();
            _Init_AudioAnimEventsRefs();
        }

        void Update()
        {
        }

        public void PlaySndEventAI(string call)
        {
            PlayEnemySound(soundEvtDict_AI[call]);
        }

        public void PlayEnemySound(AudioClip[] clipArray)
        {
            SetRandomVariations(0.8f, 1);
            enemySource.PlayOneShot(PickRandomClip(clipArray));
        }
        private void _Init_AudioAnimEventsRefs()
        {
            soundEvtDict_AI["appear"] = mushAppear;
            soundEvtDict_AI["attack"] = mushAttack;
            soundEvtDict_AI["death"] = mushDeath;
            soundEvtDict_AI["hurt"] = mushHit;
        }

        void SetRandomVariations(float vol, float pitch)
        {
            //working, but could be dealt with better
            float minV = vol - (volRange / 2);
            float maxV = (volRange / 2) + vol;
            float minP = pitch - (pitchRange / 2);
            float maxP = (volRange / 2) + pitch;

            enemySource.pitch = (float)Random.Range(minP, maxP);
            enemySource.volume = (float)Random.Range(minV, maxV);
        }

        AudioClip PickRandomClip(AudioClip[] clipArray)
        {
            if (clipArray != null)
                return clipArray[Random.Range(0, clipArray.Length - 1)];
            else
                return null;
        }

        //destroy on death?
    }
}