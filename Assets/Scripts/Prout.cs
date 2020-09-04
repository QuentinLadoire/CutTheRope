using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prout : MonoBehaviour {

    GameObject candy;

    [SerializeField] float proutPower = 60.0f;

    bool isInside = false;

    void Start()
    {
        candy = GameObject.FindGameObjectWithTag("Candy");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Candy")
        {
            isInside = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Candy")
        {
            isInside = false;
        }
    }

    public void Click()
    {
        if (isInside)
        {
            candy.GetComponent<Rigidbody2D>().AddForce(transform.right * proutPower, ForceMode2D.Impulse);
        }
    }
}
