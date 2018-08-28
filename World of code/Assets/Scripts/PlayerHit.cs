using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Animator animator;

	void Start ()
    {
	}
	
	void Update ()
    {
		animator.SetBool("Hitting", Input.GetMouseButton(0));
	}
}
