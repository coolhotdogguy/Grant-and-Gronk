using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtils : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlayAnimation(string state)
    {
        animator.Play(state);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SwitchRoom(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SwitchRoom(string name)
    {
        SceneManager.LoadScene(name);
    }
}
