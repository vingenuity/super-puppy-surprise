using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace SuperPuppySurprise.Sounds
{
    public class SoundManager
    {
        ContentManager content;

        public static SoundManager Sounds;

        public enum ConstantSounds
        {
            Ambient,
            IntroVideo,
            WalkSoft,
            MenuBackground,
        };

        public enum SoundEffects
        {
            MenuSelect,
            Hit,
            PlayerAttack,
            PlayerAttackLong,
            MonsterAttack,
        };

        AudioEngine engine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue menuback, ambient;

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
            engine = new AudioEngine("Content/Sound/Soundauto.xgs");
            soundBank = new SoundBank(engine, "Content/Sound/Sound Bank.xsb");
            waveBank = new WaveBank(engine, "Content/Sound/Wave Bank.xwb");

            Sounds = this;
            Reset();
        }
        void Reset()
        {
            random = new Random();
            hit = new List<SoundEffect>();
            playerattack = new List<SoundEffect>();
            monsterattack = new List<SoundEffect>();
        }
        public void Load()
        {
            content = Game1.game.Content;
            /*monsterattack = new List<SoundEffect>();
            monsterattack.Add(content.Load<SoundEffect>("zzz"));
            menuselect = content.Load<SoundEffect>("Sound/Menu/UI_Misc12");
            menuback = soundBank.GetCue("MenuBackground");
            ambient = soundBank.GetCue("NightAmbienceSimple_02");
            string asdfasf = menuback.ToString();
            playerattack.Add(content.Load<SoundEffect>("Sound/PlayerAttemptAttack/Whoosh2"));
            playerattack.Add(content.Load<SoundEffect>("Sound/PlayerAttemptAttack/Whoosh4"));
            playerattacklong = content.Load<SoundEffect>("Sound/PlayerAttemptAttack/long_whoosh_00");
            hit.Add(content.Load<SoundEffect>("Sound/Hit/Body_Hit_01"));
            hit.Add(content.Load<SoundEffect>("Sound/Hit/Body_Hit_02"));*/
        }
        public void PlaySound(SoundEffects sound)
        {
            SoundEffectInstance sb;
            switch (sound)
            {
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
        public void TurnSoundOn(ConstantSounds sound)
        {
            switch (sound)
            {
               /* case ConstantSounds.MenuBackground:
                    if (!menuback.IsPlaying)
                        menuback.Play();
                    menuback.Resume();
                    break;
                case ConstantSounds.Ambient:
                    if (!ambient.IsPlaying)
                        ambient.Play();
                    ambient.Resume();
                    break;*/
            };
        }
        public void TurnSoundOff(ConstantSounds sound)
        {
            switch (sound)
            {
                    /*
                case ConstantSounds.MenuBackground:
                    if (menuback.IsPlaying)
                        menuback.Pause();

                    break;
                case ConstantSounds.Ambient:
                    if (ambient.IsPlaying)
                        ambient.Pause();
                    break;
                     * */
            };
        }
    }
}