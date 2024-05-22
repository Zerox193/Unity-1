using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // reference to the overlay text to display winning text, etc
    public TextMeshProUGUI m_MessageText;
    public TextMeshProUGUI m_TimerText;


    public GameObject[] m_Tanks;

    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }
    
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;

    public GameState State { get { return m_GameState; } }

    public void Awake()
    {
        m_GameState = GameState.Start;
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }
        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (m_GameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;

        }
    }

    private void GameStateStart()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
           
            /* m_GameState = GameState.Playing;

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[1].SetActive(true);
            }*/
        }
    }

    private void GameStatePlaying()
    {
        bool isGameOver = false;

        m_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(m_gameTime);

        m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                     (seconds / 60), (seconds % 60));

        if (IsPlayerDead() == true)
        {
            //Debug.Log("You Lose");
            m_MessageText.text = "TRY AGAIN";
            isGameOver = true;
        }
        else if (OneTankLeft() == true)
        {
            //Debug.Log("You Win");
            m_MessageText.text = "WINNER";
            isGameOver = true;
        }

        if (isGameOver == true)
        {
            m_GameState = GameState.GameOver;
        }
    }

    private void GameStateGameOver()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
            /* m_gameTime = 0;
            m_GameState = GameState.Playing;

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[1].SetActive(true);
            }*/
        }
    }

    public void OnNewGame()
    {
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = " ";

        m_gameTime = 0;
      
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(true);
        }
        m_GameState = GameState.Playing;
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }

        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[1].tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
}
