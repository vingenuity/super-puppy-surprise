using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SuperPuppySurprise.Sounds
{
    public enum ConstantSounds
    {
        Ambient,
        IntroVideo,
        WalkSoft,
        MenuBackground,
    };

    public enum SoundEffects
    {
        explode,
        MenuSelect,
        Hit,
        PlayerAttack,
        PlayerAttackLong,
        MonsterAttack,
    };
    public class SoundManager
    {
        ContentManager soundContent;
        public Vector2 PlayerVelocity = Vector2.Zero;
        public static SoundManager Sounds;


        AudioEngine engine;
        WaveBank waveBank;
        SoundBank soundBank;
        static Cue menuback, explosion;

        System.IO.Stream wavStream;
        SoundEffect ambient;
        SoundEffectInstance ambientInstance;

        SoundEffect menuselect;
        List<SoundEffect> hit;
        List<SoundEffect> playerattack;
        List<SoundEffect> monsterattack;
        SoundEffect playerattacklong;
        Random random;

        SoundEffect MenuBackground;
        SoundEffectInstance menubackground;
        public SoundManager()
        {
            try
            {
                engine = new AudioEngine("Content/Sound/Soundauto.xgs");
                soundBank = new SoundBank(engine, "Content/Sound/Sounds.xsb");
                waveBank = new WaveBank(engine, "Content/Sound/Waves.xwb");

                
                Sounds = this;
            }
            catch { }
            Reset();
        }
        void Reset()
        {
            random = new Random();
            hit = new List<SoundEffect>();
            playerattack = new List<SoundEffect>();
            monsterattack = new List<SoundEffect>();
            PlayerVelocity = Vector2.Zero;
        }
        public void Load()
        {
            try
            {
                soundContent = Game1.game.Content;
                /*monsterattack = new List<SoundEffect>();
                monsterattack.Add(content.Load<SoundEffect>("zzz"));
                menuselect = content.Load<SoundEffect>("Sound/Menu/UI_Misc12");
                
            
                string asdfasf = menuback.ToString();
                playerattack.Add(content.Load<SoundEffect>("Sound/PlayerAttemptAttack/Whoosh2"));
                playerattack.Add(content.Load<SoundEffect>("Sound/PlayerAttemptAttack/Whoosh4"));
                playerattacklong = content.Load<SoundEffect>("Sound/PlayerAttemptAttack/long_whoosh_00");
                hit.Add(content.Load<SoundEffect>("Sound/Hit/Body_Hit_01"));
                hit.Add(content.Load<SoundEffect>("Sound/Hit/Body_Hit_02"));*/
                explosion = soundBank.GetCue("explode");
                
               // wavStream = TitleContainer.OpenStream("Content/Sound/Complete/hover.wav");
                ambient = soundContent.Load<SoundEffect>("Sound/Complete/hover");
                ambientInstance = ambient.CreateInstance();
                ambientInstance.IsLooped = true;

                
                menuback = soundBank.GetCue("Intro");
                PlayerVelocity = Vector2.Zero;
                
            }
            catch (Exception e){ }
        }

        public void StartHover(){
            float volume = .2F;
            ambientInstance.Volume = volume;
            ambientInstance.Play();
        }

        public void StopHover(){
            ambientInstance.Stop();
        }

        public void PlaySound(SoundEffects sound)
        {
            try
            {
                SoundEffectInstance sb;
                switch (sound)
                {
                   case SoundEffects.explode:
                        Cue explosion1 = soundBank.GetCue("explode");
                        explosion1.Play();
                        
                        break;
                    case SoundEffects.Hit:
                        SoundEffectInstance s2 = hit[(random.Next(playerattack.Count))].CreateInstance();
                        s2.Volume = s2.Volume / 7;
                        s2.Play();
                        break;
                    case SoundEffects.MenuSelect:
                        SoundEffectInstance s = menuselect.CreateInstance();
                        s.Volume = s.Volume / 7;
                        s.Pitch = s.Pitch - 1;
                        s.Play();
                        break;
                    case SoundEffects.MonsterAttack:
                        monsterattack[(random.Next(monsterattack.Count))].Play();
                        break;
                    case SoundEffects.PlayerAttack:
                        SoundEffectInstance s1 = playerattack[(random.Next(playerattack.Count))].CreateInstance();
                        s1.Volume = s1.Volume / 7;
                        s1.Play();
                        break;
                    case SoundEffects.PlayerAttackLong:
                        sb = playerattacklong.CreateInstance();
                        sb.Volume = sb.Volume / 2;
                        sb.Play();
                        break;
                };
            }
            catch { }
        }

        public void Update() { 
          //  pitch = ambient.GetVariable
          //  ambient.
        }
        public void TurnSoundOn(ConstantSounds sound)
        {
            try
            {
                switch (sound)
                {
                    case ConstantSounds.MenuBackground:
                         if (!menuback.IsPlaying)
                             menuback.Play();
                         menuback.Resume();
                         break;
               /*     case ConstantSounds.Ambient:
                        if (!ambient.IsPlaying)
                            ambient.Play();
                        ambient.Resume();
                        break;*/
                };
            }
            catch { }
        }
        public void TurnSoundOff(ConstantSounds sound)
        {
            try
            {
                switch (sound)
                {
                    
                case ConstantSounds.MenuBackground:
                    if (menuback.IsPlaying)
                        menuback.Pause();
                    
                    break;
                     

                 /*   case ConstantSounds.Ambient:
                        if (ambient.IsPlaying)
                            ambient.Pause();
                        break; */

                };
            }
            catch { }
        }
    }
}