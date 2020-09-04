using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour {

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelPanel;

    [SerializeField] GameObject prefabLevelButton;

    [SerializeField] Sprite unlockCase;
    [SerializeField] Sprite lockCase;
    [SerializeField] Sprite[] star;

    [SerializeField] GameObject soundActive;
    [SerializeField] GameObject soundDisactive;
    [SerializeField] GameObject musicOff;

    public bool activeLevelPanel = false;

	void Start ()
    {
        GameManager.GetInstance();
        InitPanel();
        SoundManager.instance.PlayMusic(MusicType.menu);
    }
	
	void InitPanel()
    {
        //init menuPanel
        menuPanel.SetActive(!activeLevelPanel);

        //init levelPanel
        levelPanel.SetActive(activeLevelPanel);
        for (int i = 0; i < GameManager.GetInstance().allLevel / 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int nb = (j + (5 * i) + 1);

                GameObject newButton = Instantiate(prefabLevelButton, levelPanel.transform);
                //set Background and number and star
                if (GameManager.GetInstance().unlockLevel >= nb)
                {
                    newButton.transform.Find("Background").GetComponent<Image>().sprite = unlockCase;
                    newButton.transform.Find("Number").GetComponent<Text>().text = nb.ToString();
                    newButton.transform.Find("Star").GetComponent<Image>().enabled = true;
                    newButton.transform.Find("Star").GetComponent<Image>().sprite = star[GameManager.GetInstance().nbStar[nb - 1]];
                }
                else
                {
                    newButton.transform.Find("Background").GetComponent<Image>().sprite = lockCase;
                    newButton.transform.Find("Number").GetComponent<Text>().text = "";
                    newButton.transform.Find("Star").GetComponent<Image>().enabled = false;
                }

                newButton.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(nb); });
                
                newButton.transform.localPosition = new Vector3(700.0f + (j * 130.0f), -370.0f + (i * -130.0f), 0.0f);
            }           
        }
    }
    public void LoadLevel(int id)
    {
        if (GameManager.GetInstance().unlockLevel >= id)
        {
            GameManager.GetInstance().currentLevel = id;
            SceneManager.LoadScene("Level" + id);
        }
    }

    public void Play()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        GameManager.GetInstance().currentLevel = GameManager.GetInstance().unlockLevel;
        SceneManager.LoadScene("Level" + GameManager.GetInstance().unlockLevel);
    }
    public void ActiveLevelPanel()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        activeLevelPanel = !activeLevelPanel;
        levelPanel.SetActive(activeLevelPanel);
        menuPanel.SetActive(!activeLevelPanel);
    }
    public void Quit()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        SystemSave.SaveGameManager();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
         #else
            Application.Quit();
        #endif
    }

    public void ActiveSound()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        SoundManager.instance.activeSound = !SoundManager.instance.activeSound;
        soundActive.SetActive(SoundManager.instance.activeSound);
        soundDisactive.SetActive(!SoundManager.instance.activeSound);
        SoundManager.instance.ActiveSound();
    }
    public void ActiveMusic()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        SoundManager.instance.activeMusic = !SoundManager.instance.activeMusic;
        musicOff.SetActive(!SoundManager.instance.activeMusic);
        SoundManager.instance.ActiveMusic();
    }

    public void ResetGame()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        GameManager.GetInstance().Reset();
        SceneManager.LoadScene("Menu");
    }
}
