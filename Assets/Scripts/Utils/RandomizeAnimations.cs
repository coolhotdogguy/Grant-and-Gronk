using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string state;

    void Start()
    {
        animator.Play(state, 0, Random.Range(0f, 1f));
    }
}
