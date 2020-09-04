using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour {

    GameObject candy;
    Rigidbody2D rbCandy;
    Animator anim;

    [SerializeField] float raisePower = 0.7f;
    [SerializeField] float raisePowerMax = 1.5f;

    bool isInside = false;
    float radius;

    void Start()
    {
        candy = GameObject.FindGameObjectWithTag("Candy");
        rbCandy = candy.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        radius = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
		if (isInside)
        {
            Vector2 velocity = rbCandy.velocity;

            velocity.y += raisePower;
            
            if (velocity.x < -0.1f)
            {
                velocity.x += 0.1f;
            }
            else if (velocity.x > 0.1f)
            {
                velocity.x -= 0.1f;
            }

            if (velocity.y >= raisePowerMax)
            {
                velocity.y = raisePowerMax;
            }

            rbCandy.velocity = velocity;

            transform.position = candy.transform.position;
        }
        else
        {
            if (Vector2.Distance(gameObject.transform.position, candy.transform.position) < radius)
            {
                SoundManager.instance.PlaySound(SoundType.Bubble);
                anim.SetBool("IsInside", true);
                isInside = true;
            }
        }
    }
    
    void OnMouseDown()
    {
        if (isInside)
        {
            SoundManager.instance.PlaySound(SoundType.BubbleBreak);
            Destroy(gameObject);
        }
    }
}
