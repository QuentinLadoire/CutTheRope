using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager {

    static GameManager instance = null;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
            instance.Init();
        }

        return instance;
    }

    public int allLevel = 20;
    public int currentLevel = 0;
    public int unlockLevel = 11;
    public int[] nbStar;

    void Init()
    {
        //set all default value
        nbStar = new int[allLevel];
        for (int i = 0; i < nbStar.Length; i++)
        {
            nbStar[i] = 0;
        }

        //load and set save value
        GameManager loadGameManager = SystemSave.LoadGameManager();
        if (loadGameManager != null)
        {
            unlockLevel = loadGameManager.unlockLevel;
            for (int i = 0; i < loadGameManager.nbStar.Length; i++)
            {
                nbStar[i] = loadGameManager.nbStar[i];
            }
        }
    }
    public void Reset()
    {
        unlockLevel = 1;
        for (int i = 0; i < nbStar.Length; i++)
        {
            nbStar[i] = 0;
        }
    }
}
