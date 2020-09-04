using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickProut : MonoBehaviour {

    [SerializeField] Prout prout;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        anim.SetTrigger("IsPress");
        prout.Click();
    }
}
