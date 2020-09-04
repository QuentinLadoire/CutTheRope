using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

    void Awake()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    void Update()
    {
        HingeJoint2D[] hingeTab = GetComponents<HingeJoint2D>();
        for (int i = 0; i < hingeTab.Length; i++)
        {
            if (hingeTab[i].connectedBody == null)
            {
                Destroy(hingeTab[i]);
            }
        }

        if (transform.position.y > 10.0f || transform.position.y < -10.0f || transform.position.x < -10.0f || transform.position.x > 10.0f)
        {
            LevelManager.instance.fail = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pike")
        {
            SoundManager.instance.PlaySound(SoundType.CandyBreak);
            LevelManager.instance.fail = true;
        }
    }
}
