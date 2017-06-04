using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace core.audio
{
    /// <summary>
    /// Handles the transitions and playback of background music
    /// </summary>
    public class MusicController
    {
        public const string DIALOG_PARAM_MUSIC_FADEIN = "MusicFadeIn";
        public const string DIALOG_PARAM_MUSIC_FADEOUT = "MusicFadeOut";

        public const string SONG_1 = "Factory-On-Mercury_Looping";
        public const string SONG_2 = "Retro-Sci-Fi-Planet_Looping";

        private static MusicController instance;

        /// <summary>
        /// The volume change per frame update when transitioning to a new song
        /// </summary>
        private const float TRANSITION_SPEED = 0.01f;

        private bool inTransition = false;

        private Dictionary<string, AudioClip> musicMap;

        private string queuedSongName = "";

        private float fadeRate = 0f;

        /// <summary>
        /// The audio source component attached to the core game object
        /// </summary>
        private AudioSource source;

        private MusicController()
        {
            musicMap = new Dictionary<string, AudioClip>();
        }

        public static MusicController GetInstance()
        {
            if (instance == null)
            {
                instance = new MusicController();
            }

            return instance;
        }

        public void Update()
        {
            if (!inTransition)
            {
                return;
            }

            // Delta the volume either up or down
            source.volume += fadeRate;

            // If we hit silent then let's try to transition back in.
            if (source.volume <= 0f)
            {
                if (!string.IsNullOrEmpty(queuedSongName))
                {
                    // Let's fade it back in
                    fadeRate = TRANSITION_SPEED;
                    source.clip = musicMap[queuedSongName];
                    source.Play();

                    // We've started playing the new song so nothing in the queue left
                    queuedSongName = "";
                    return;
                }
                else
                {
                    // We meant to fade it out and thus we should stop now.
                    source.clip = null;
                    source.Stop();

                    inTransition = false;
                }
            }

            // Once we hit max volume then stop the transition
            if (source.volume >= 1.0f)
            {
                inTransition = false;
            }
        }

        /// <summary>
        /// Preloads the music files
        /// </summary>
        public void PreloadMusic()
        {
            AudioClip[] clipArray = Resources.LoadAll<AudioClip>("Music/");

            for (int i = 0, count = clipArray.Length; i < count; i++)
            {
                AudioClip clip = clipArray[i];

                musicMap[clip.name] = clip;
            }

            GameObject core = GameObject.Find("Core");
            source = core.AddComponent<AudioSource>();
            source.loop = true;
        }

        /// <summary>
        /// Begins the fade out of the current song or fade in if no song is active.
        /// </summary>
        public void TransitionToNewSong(string name)
        {
            // Don't allow transitioning to the same song
            if (source.clip != null && source.clip.name == name)
            {
                return;
            }

            queuedSongName = name;

            inTransition = true;
            if (source.clip != null)
            {
                fadeRate = -TRANSITION_SPEED;
            }
            // Just fade in
            else
            {
                source.volume = 0f;
                fadeRate = TRANSITION_SPEED;
                source.clip = musicMap[queuedSongName];
                source.Play();
            }
        }

        public void FadeOutCurrentSong()
        {
            inTransition = true;
            if (source.clip != null)
            {
                fadeRate = -TRANSITION_SPEED;
            }

            queuedSongName = null;
        }
        
    }
}