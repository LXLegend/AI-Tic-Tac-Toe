using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    public Toggle twoPlayersToggle;
    public Toggle setAIFirstToggle;
    public Slider difficultySlider;

    private void Start()
    {
        if (setAIFirstToggle)
            setAIFirstToggle.isOn = GameSettings.AIFirst;

        if (twoPlayersToggle)
            twoPlayersToggle.isOn = GameSettings.twoPlayers;

        if (difficultySlider)
            difficultySlider.value = GameSettings.Difficulty;
    }
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetAIFirst()
    {
        if (setAIFirstToggle)
            GameSettings.AIFirst = setAIFirstToggle.isOn;
    }
    public void SetTwoPlayers()
    {
        if (twoPlayersToggle)
            GameSettings.twoPlayers = twoPlayersToggle.isOn;
        if (setAIFirstToggle)
        {
            if (twoPlayersToggle.isOn)
                setAIFirstToggle.gameObject.SetActive(false);
            else
                setAIFirstToggle.gameObject.SetActive(true);
        }
        
    }

    public void SetDifficulty()
    {
        if (difficultySlider.value > 0 && difficultySlider.value < 9)
            GameSettings.Difficulty = (int) difficultySlider.value;
        else
            GameSettings.Difficulty = 8;

        print(GameSettings.Difficulty);
    }
}
