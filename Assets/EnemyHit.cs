using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private string animation = "Death1";

    public void Die()
    {
        selfAnimator.StopPlayback(); 
        selfAnimator.Play(animation);
    }

    public void disableSelf()
    {
        self.SetActive(false);
    }
}
