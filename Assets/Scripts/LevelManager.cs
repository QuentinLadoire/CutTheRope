using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    GameObject candy;

    public bool win = false;
    public bool fail = false;
    float timeFail = 0.0f;
    public int nbStar = 0;

    public void AddStar()
    {
        nbStar++;
        switch (nbStar)
        {
            case 1:
                SoundManager.instance.PlaySound(SoundType.Star1);
                break;

            case 2:
                SoundManager.instance.PlaySound(SoundType.Star2);
                break;

            case 3:
                SoundManager.instance.PlaySound(SoundType.Star3);
                break;
        }
    }

    void Start()
    {
        candy = GameObject.FindGameObjectWithTag("Candy");
    }

    void Update()
    {
        if (fail)
        {
            candy.SetActive(false);
            if (timeFail == 0.0f)
            {
                SoundManager.instance.PlaySound(SoundType.MonsterSad);
            }
            timeFail += Time.deltaTime;
            if (timeFail > 1.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
