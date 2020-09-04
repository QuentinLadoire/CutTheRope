using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmNom : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Candy")
        {
            SoundManager.instance.PlaySound(SoundType.Win);
            LevelManager.instance.win = true;
            collision.gameObject.SetActive(false);
        }
    }
}
