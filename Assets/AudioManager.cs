using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

 public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
   public Sound[] musicSounds,sfxSounds;
   public AudioSource musicSource,sfxSource;
   private void Awake() 
   {
     if (Instance==null)
     {
        Instance=this;
        DontDestroyOnLoad(gameObject);

     }
     else
     {
        Destroy(gameObject);
     }
   }
    void Start()
    {
        PlayMusic("music" + UnityEngine.Random.Range(1,7));
    }
    private void Update() {
        if(!musicSource.isPlaying)
        {
            PlayMusic("music" + UnityEngine.Random.Range(1,7));
        }
    }
    public void PlayMusic(string name)
   {
       Sound s=Array.Find(musicSounds,x=>x.name==name);
       if (s==null)
       {
            Debug.Log("ses yok");
       }
       else
       {
          musicSource.clip=s.clip;
          musicSource.Play();
       }
   }
   public void PlaySFX(string name)
   {
       Sound s=Array.Find(sfxSounds,x=>x.name==name);
       if (s==null)
       {
            Debug.Log("ses yok");
       }
       else
       {
         sfxSource.PlayOneShot(s.clip);
       }
   }
   public void CloseSounds()
   {
        sfxSource.volume = 0;
        musicSource.volume = 0;
   }
   public void OpenSounds()
   {
        sfxSource.volume = 1;
        musicSource.volume = 0.03f;   
   }
   
  
}
[System.Serializable]
public class Sound 
{
    
    public string name;
    public AudioClip clip;
}

