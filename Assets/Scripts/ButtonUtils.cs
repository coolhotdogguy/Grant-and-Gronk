using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtils : MonoBehaviour
{
    public Animator animator;
    public MusicPlayer music;
    bool changeMusic;

    float fade = 0.8f;

    public void PlayAnimation(string state)
    {
        animator.Play(state);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeMusic()
    {
        changeMusic = true;
    }

    public void SwitchRoom(int index)
    {
        SceneManager.LoadScene(index);
        if (changeMusic)
        {
            music.LinearFade(fade);
        }
    }

    public void SwitchRoom(string name)
    {
        SceneManager.LoadScene(name);
        if (changeMusic)
        {
            music.LinearFade(fade);
        }
    }
}
