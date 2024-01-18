using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAnimation : MonoBehaviour
{
    private string spinAnimation = "Clock_Spin";
    [SerializeField] private Animator animator;
    private void Start()
    {
        GameManager.Instance.OnGameStarted += () =>
        {
            animator.Play(spinAnimation);
        };
        GameManager.Instance.OnGameEnded += () =>
        {
            animator.speed = 0;
        };
    }
}
