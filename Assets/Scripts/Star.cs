using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Candy")
        {
            LevelManager.instance.AddStar();
            Destroy(gameObject);
        }
    }
}
