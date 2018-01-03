using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SoundEngineSpace
{
    public class SoundEngine
    {
        public SoundEngine GlobalSoundEngine;
        private int RecusrionProof = 0;

        public void soundEngine()
        {
            if(RecusrionProof == 0)
            {
                GlobalSoundEngine = new SoundEngine();
                RecusrionProof++;
                Console.WriteLine("GlobalSoundEngine made!");
            }

        }

        BackgroundSound backgroundSound = new BackgroundSound();
        SoundEffects soundEffects = new SoundEffects();

        public void addSong(Song s)
        {
            backgroundSound.AddBackgroundSound(s);
        }

        public void addSound(SoundEffect s)
        {
            soundEffects.AddSound(s);
        }

        public void PlaySong(int s)
        {
            backgroundSound.PlayBackgroundSound(s);
        }

        public void PlaySound(int s)
        {
            soundEffects.PlaySound(s);
        }

        public void SetSongVolume(int v)
        {
            backgroundSound.SetBackGroundSoundVolume(v);
        }

        public void SetSoundVolume(int v)
        {
            soundEffects.SetSoundEffectVolume(v);
        }

    }
}


class BackgroundSound
{
    public static void backGroundSound()
    {

    }
    private List<Song> BackgroundSoundEffects = new List<Song>();
    private bool PlayingBackGroundSound = false;
    
    public void AddBackgroundSound(Song s)//addsongs to the list

    {
        BackgroundSoundEffects.Add(s);
    }

    public void PlayBackgroundSound(int s)//plays BackgroundSound based on location in the list
    {
        MediaPlayer.IsRepeating = true;//If I use this exact soundengine again, move this to it's own function instead!
        if (BackgroundSoundEffects.Count() > s)
        {
            if (PlayingBackGroundSound)
                MediaPlayer.Stop();
            MediaPlayer.Play(BackgroundSoundEffects.ElementAt(s));
            PlayingBackGroundSound = true;
        }
        else
        {
            Console.WriteLine("Couldent find the BackgroundSound");
        }
    }
    public void SetBackGroundSoundVolume(int v)
    {
        MediaPlayer.Volume = (float)v/100;
    }
}

class SoundEffects
{
    public static void soundeffects()
    {

    }
    private List<SoundEffect> soundEffects = new List<SoundEffect>();

    public void AddSound(SoundEffect s)//addsongs to the list

    {
        soundEffects.Add(s);
    }

    public void PlaySound(int s)//plays sound based on location in the list
    {
        if (soundEffects.Count() > s)
        {
            SoundEffect ToPlaySound = soundEffects.ElementAt(s);
            ToPlaySound.Play();
        }
        else
        {
            Console.WriteLine("Couldent find the sound");
        }
    }
    public void SetSoundEffectVolume(int v)
    {
        SoundEffect.MasterVolume = (float)v/100;
    }
}