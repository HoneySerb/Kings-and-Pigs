using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject _bPause, _fButtons;
    [SerializeField] private GameObject _iSoundOn, _iSoundOff;
    [SerializeField] private Text _finishText;
 
    private byte _playState = 0;

    public void ChangePlayState(byte state) => _playState = state;

    public void Pause() => ShowButtons(true);

    public void Exit()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        Time.timeScale = 1f;

        if (_playState == 0)
        {
            ShowButtons(false);
        }
        else if (_playState == 1)
        {
            int actualLevelIndex = PlayerPrefs.GetInt("ActualLevel");
            int levelsQuantity = PlayerPrefs.GetInt("LevelsQuantity");
            if (actualLevelIndex + 1 < levelsQuantity)
            {
                PlayerPrefs.SetInt("ActualLevel", actualLevelIndex + 1);
                SceneManager.LoadScene("Play");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else
        {
            SceneManager.LoadScene("Play");
        }
    }

    public void Sound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") == 0 ? 1 : 0);

        GetSoundState();
    }

    private void Start()
    {
        if (name != "ButtonSound") { return; }

        GetSoundState();
    }

    private void ShowButtons(bool isShow)
    {
        switch(_playState)
        {
            case 0: _finishText.text = "Pause"; break;
            case 1: _finishText.text = "Victory!"; break;
            case 2: _finishText.text = "Loser"; break;
        }

        _fButtons.SetActive(isShow);

        _bPause.SetActive(!isShow);

        Time.timeScale = isShow ? 0f : 1f;
    }

    private void GetSoundState()
    {
        bool state = PlayerPrefs.GetInt("Sound") == 1;

        _iSoundOn.SetActive(state);
        _iSoundOff.SetActive(!state);
    }
}
