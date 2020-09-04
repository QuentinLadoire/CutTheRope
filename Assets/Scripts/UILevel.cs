using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevel : MonoBehaviour {

    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;

    [SerializeField] GameObject[] star;

    [SerializeField] Sprite starFill;
    [SerializeField] Sprite starEmpty;

    [SerializeField] GameObject soundActive;
    [SerializeField] GameObject soundDisactive;
    [SerializeField] GameObject musicOff;

    bool activePause = false;
    bool activeWin = false;

    void Start()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(activePause);
        winPanel.SetActive(activeWin);

        if (!SoundManager.instance.IsPlaying(MusicType.game))
        {
            SoundManager.instance.PlayMusic(MusicType.game);
        }

        soundActive.SetActive(SoundManager.instance.activeSound);
        soundDisactive.SetActive(!SoundManager.instance.activeSound);
        musicOff.SetActive(!SoundManager.instance.activeMusic);
    }

    void Update()
    {
        if (LevelManager.instance.win && !activeWin)
        {
            ActiveWinPanel();
        }
    }

    public void Pause()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        activePause = !activePause;
        if (activePause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        pausePanel.SetActive(activePause);
    }
    public void Replay()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void NextLevel()
    {
        SoundManager.instance.PlaySound(SoundType.Tap);
        if (GameManager.GetInstance().currentLevel + 1 <= GameManager.GetInstance().allLevel)
        {
            GameManager.GetInstance().currentLevel++;
            SceneManager.LoadScene("Level" + GameManager.GetInstance().currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void ActiveSound()
    {
        SoundManager.instance.activeSound = !SoundManager.instance.activeSound;
        soundActive.SetActive(SoundManager.instance.activeSound);
        soundDisactive.SetActive(!SoundManager.instance.activeSound);
        SoundManager.instance.ActiveSound();
    }
    public void ActiveMusic()
    {
        SoundManager.instance.activeMusic = !SoundManager.instance.activeMusic;
        musicOff.SetActive(!SoundManager.instance.activeMusic);
        SoundManager.instance.ActiveMusic();
    }

    public void ActiveWinPanel()
    {
        activeWin = !activeWin;
        winPanel.SetActive(activeWin);

        if (GameManager.GetInstance().nbStar[GameManager.GetInstance().currentLevel - 1] < LevelManager.instance.nbStar)
        {
            GameManager.GetInstance().nbStar[GameManager.GetInstance().currentLevel - 1] = LevelManager.instance.nbStar;
        }
        if (GameManager.GetInstance().currentLevel == GameManager.GetInstance().unlockLevel && GameManager.GetInstance().unlockLevel < GameManager.GetInstance().allLevel)
        {
            GameManager.GetInstance().unlockLevel++;
        }

        for (int i = 0; i < 3; i++)
        {
            if (i < LevelManager.instance.nbStar)
            {
                star[i].GetComponent<Image>().sprite = starFill;
            }
            else
            {
                star[i].GetComponent<Image>().sprite = starEmpty;
            }
        }
    }
}
