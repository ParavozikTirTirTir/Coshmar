using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    //общедоступный объект сценирия
    public static AudioManager2 instance;

    //связываем объект со скриптом
    void Awake() { instance = this; }

    public List<AudioClip> sfxLibrary;
    public AudioClip sfx_jump, sfx_char, sfx_dash, sfx_attack, sfx_attack2, sfx_enemy1attack, sfx_enemy2attack, sfx_bow;

    public AudioClip music_back, music_village;
    //текущая музыка
    public GameObject currentMusicObject;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "jump":
                SoundObjectCreation(sfx_jump, 0.1f);
                break;
            case "char":
                SoundObjectCreation(sfx_char, 0.1f);
                break;
            case "dash":
                SoundObjectCreation(sfx_dash, 0.1f);
                break;
            case "attack":
                SoundObjectCreation(sfx_attack, 0.05f);
                break;
            case "attack2":
                SoundObjectCreation(sfx_attack2, 0.1f);
                break;
            case "en1_attack":
                SoundObjectCreation(sfx_enemy1attack, 0.05f);
                break;
            case "en2_attack":
                SoundObjectCreation(sfx_enemy2attack, 0.05f);
                break;
            case "bow":
                SoundObjectCreation(sfx_bow, 0.05f);
                break;
            default:
                break;
        }
    }


    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "music_back":
                MusicObjectCreation(music_back);
                break;
            case "music_village":
                MusicObjectCreation(music_village);
                break;
            default:
                break;
        }
    }


    void SoundObjectCreation(AudioClip clip, float volume)
    {
        //Создаем звуковой объект
        GameObject newObject = Instantiate(soundObject, transform);
        //Подставляем нужный звук
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().volume = volume;
        newObject.GetComponent<AudioSource>().loop = false;
        //Воспроизводим звук
        newObject.GetComponent<AudioSource>().Play();

    }

    public void MusicObjectCreation(AudioClip clip)
    {
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().volume = 0.05f;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        currentMusicObject.GetComponent<AudioSource>().Play();
    }

}
