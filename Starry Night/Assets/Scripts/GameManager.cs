using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameStart;
    public GameObject MenuUI;
    public GameObject PauseMenu;
    public GameObject GameOver;
    public GameObject OptionsMenu;
    public GameObject player;
    public PlayerData pData;
    public GameObject AstroidSpawner;
    public GameObject GameOverlay;
    private GameState lastState;
    public GameState currentState;
    public double score;
    public float startTime;
    public GameObject scoreText;
    public static GameManager Instance {get; private set;}

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        GameStart = false;
        currentState = GameState.MainMenu;
        HideAllUI();
        // DataHandling.LoadData(player.GetComponent<PlayerData>());
    }

    void Update() {
        switch (currentState)
        {
            case GameState.MainMenu:
                if(!MenuUI.active) MenuUI.SetActive(true);
                break;
            case GameState.Options:
                if(!OptionsMenu.active) OptionsMenu.SetActive(true);
                break;
            case GameState.GameRunning:
                if(!GameOverlay.active) GameOverlay.SetActive(true);
                score = System.Math.Floor((Time.time-startTime)*10);
                scoreText.GetComponent<Text>().text = "Score: " + score;
                CheckUpdate();
                break;
            case GameState.GamePaused:
                if(!PauseMenu.active) PauseMenu.SetActive(true);
                break;
            case GameState.GameOver:
                if(!GameOverlay.active) GameOverlay.SetActive(true);
                if(!GameOver.active) GameOver.SetActive(true);
                break;
            default:
                Debug.Log("Not a valid state");
                break;
        }
    }

    void HideAllUI(){
        MenuUI.SetActive(false);
        PauseMenu.SetActive(false);
        GameOver.SetActive(false);
        OptionsMenu.SetActive(false);
        GameOverlay.SetActive(false);
    }

    void CheckUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            ChangeState(GameState.GamePaused);
        }

    }

    public void StartGame(/*LevelData data*/){
        startTime = Time.time;
        score = 0;
        player.transform.position = Vector3.zero;
        ChangeState(GameState.GameRunning);
    }

    public void LevelOver(bool levelWon){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
        if(levelWon){
            pData.money += 50;
            pData.level += 1;
        }
        else
        {
            pData.money -= 10;
        }
        ChangeState(GameState.GameOver);
    }

    public void QuitGame(){
        DataHandling.SaveData(player.GetComponent<PlayerData>());
        Application.Quit();
    }

    public void PauseGame(){
        Time.timeScale = 0;
        ChangeState(GameState.GamePaused);
    }

    public void Options(){
        ChangeState(GameState.Options);
    }

    public void MainMenu(){
        Time.timeScale = 1;
        ChangeState(GameState.MainMenu);
    }

    public void LeaveOptionsMenu(){
        ChangeState(lastState);
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        ChangeState(GameState.GameRunning);
    }

    public void ChangeState(GameState newState){
        HideAllUI();
        lastState = currentState;
        currentState = newState;
    }

    public void SaveGame(){

    }

    public enum GameState
    {
        MainMenu,
        GameRunning,
        GamePaused,
        GameOver,
        Options
    }
}
