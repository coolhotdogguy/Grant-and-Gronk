using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
    public Animator animator;
    public MusicPlayer music;

    public string fadeOutAnimationState;
    public string fadeInAnimationState;

    float fade = 0.8f;

    public void PlayAnimation(string state)
    {
        animator.Play(state);
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator FadeOutIn(int index)
    {
        music.LinearFadeOut(fade);
        animator.Play(fadeOutAnimationState);

        yield return new WaitForSeconds(fade);

        SceneManager.LoadScene(index);

        music.LinearFadeIn(fade);
        animator.SetTrigger("FadeIn");
    }

    public void SwitchRoom(int index)
    {
        StartCoroutine(FadeOutIn(index));
    }

    public void SwitchRoom(string name)
    {
        StartCoroutine(FadeOutIn(SceneManager.GetSceneByName(name).buildIndex));
    }
}
