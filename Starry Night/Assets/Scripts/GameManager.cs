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
    public GameObject GameOverlay;
    public GameObject GameMenu;
    public GameObject InventoryMenu;
    public GameObject SelectorMenu;
    public GameObject[] inventorySlotButtons;
    public GameObject[] equippedSlotButtons;
    public Sprite noItem;
    public GameObject player;
    public GameObject[] levels;
    public GameObject[] tower;
    public PlayerData pData;
    public GameObject AstroidSpawner;
    public GameMode currentGameMode;
    private GameState lastState;
    public GameState currentState;
    public double score;
    public double levelGoal;
    public float startTime;
    public int selectedItemIndex;
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
        pData = player.GetComponent<PlayerData>();
        GameStart = false;
        currentState = GameState.MainMenu;
        HideAllUI();
        // DataHandling.LoadData(player.GetComponent<PlayerData>());
    }

    [System.Obsolete]
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
                if(currentGameMode != GameMode.Endless) CheckLevelWon();
                CheckUpdate();
                break;
            case GameState.GamePaused:
                if(!PauseMenu.active) PauseMenu.SetActive(true);
                break;
            case GameState.GameOver:
                if(!GameOverlay.active) GameOverlay.SetActive(true);
                if(!GameOver.active) GameOver.SetActive(true);
                break;
            case GameState.GameMenu:
                if(!GameMenu.active) GameMenu.SetActive(true);
                break;
            case GameState.Inventory:
                if(!InventoryMenu.active) InventoryMenu.SetActive(true);
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
        GameMenu.SetActive(false);
        InventoryMenu.SetActive(false);
        SelectorMenu.SetActive(false);
    }

    void CheckUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            ChangeState(GameState.GamePaused);
        }
    }

    void CheckLevelWon(){
        if(score >= levelGoal) LevelOver(true);
    }

    public void SelectMenu(){
        ChangeState(GameState.GameMenu);
    }

    public void StartGame(string gMode){
        startTime = Time.time;
        score = 0;
        player.transform.position = Vector3.zero;
        ChangeGameMode(gMode);
        if(currentGameMode == GameMode.Level){
            LoadLevel(levels[pData.level].GetComponent<LevelData>());
        }
        else if(currentGameMode == GameMode.Tower){
            LoadLevel(tower[pData.towerFloor].GetComponent<LevelData>());
        }
        ChangeState(GameState.GameRunning);
    }

    public void LoadLevel(LevelData lData){
        levelGoal = lData.levelGoal;
    }

    public void DestroyGameObjects(){
        foreach(GameObject astroid in GameObject.FindGameObjectsWithTag("Astroid")){
            Destroy(astroid);
        }
        foreach(GameObject alien in GameObject.FindGameObjectsWithTag("Alien")){
            Destroy(alien);
        }
        foreach(GameObject alienProjectile in GameObject.FindGameObjectsWithTag("AlienProjectile")){
            Destroy(alienProjectile);
        }
    }

    public void GiveReward(){
        if(currentGameMode == GameMode.Endless){
            pData.money += (int)score;
        }
        else if(currentGameMode == GameMode.Level){
            pData.money += levels[pData.level].GetComponent<LevelData>().reward;
            AddItem(levels[pData.level].GetComponent<LevelData>().RandomReward());
            pData.level++;
        }
        else if(currentGameMode == GameMode.Tower){
            pData.money += tower[pData.towerFloor].GetComponent<LevelData>().reward;
            pData.towerFloor++;
        }
    }

    public void LevelOver(bool levelWon){
        pData.health = pData.maxHealth;
        DestroyGameObjects();
        if(score > pData.endlessHighscore && currentGameMode == GameMode.Endless){
            pData.endlessHighscore = score;
        }
        if(levelWon){
            GiveReward();
        }
        ChangeState(GameState.GameOver);
    }

    public void AddItem(GameObject item){
        for(int i = 0; i < pData.inventory.Length; i++){
            if(pData.inventory[i]==null){
                pData.inventory[i] = item;
                inventorySlotButtons[i].GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                return;
            }
        }
    }

    public void RemoveItem(int index){
        for(int i = index; i < pData.inventory.Length; i++){
            if(i != pData.inventory.Length-1){
                pData.inventory[i] = pData.inventory[i+1];
            }
            else{
                pData.inventory[i] = null;
            }
        }
        for(int i = 0; i < pData.inventory.Length; i++){
            if(pData.inventory[i] != null){
                inventorySlotButtons[i].GetComponent<Image>().sprite = pData.inventory[i].GetComponent<SpriteRenderer>().sprite;
            }
            else{
                inventorySlotButtons[i].GetComponent<Image>().sprite = noItem;
            }
        }
    }

    public void QuitGame(){
        DataHandling.SaveData(player.GetComponent<PlayerData>());
        Application.Quit();
    }

    public void OpenInventory(){
        ChangeState(GameState.Inventory);
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

    public void SelectItem(int index){
        selectedItemIndex = index;
        SelectorMenu.transform.position = inventorySlotButtons[index].transform.position;
        SelectorMenu.SetActive(true);
    }

    public void EquipItem(){
        AddStats(pData.inventory[selectedItemIndex]);
        switch(pData.inventory[selectedItemIndex].GetComponent<TurretStats>().itemSlot){
            case TurretStats.Slot.Front:
                // Check front slots and possibly unequip the worst equipped
                // Then equip the desired item
                if(pData.equipped[0] != null){
                    if(pData.equipped[1] != null){
                        // Check which is best and unequip other item
                        // For now it will just unequip the first turret
                        GameObject tempItem = pData.equipped[0];
                        SubtractStats(tempItem);
                        pData.equipped[0] = pData.inventory[selectedItemIndex];
                        equippedSlotButtons[0].GetComponent<Image>().sprite = pData.equipped[0].GetComponent<SpriteRenderer>().sprite;
                        RemoveItem(selectedItemIndex);
                        AddItem(tempItem);
                    }
                    else{
                        pData.equipped[1] = pData.inventory[selectedItemIndex];
                        equippedSlotButtons[1].GetComponent<Image>().sprite = pData.equipped[1].GetComponent<SpriteRenderer>().sprite;
                        RemoveItem(selectedItemIndex);
                    }
                }
                else{
                    pData.equipped[0] = pData.inventory[selectedItemIndex];
                    equippedSlotButtons[0].GetComponent<Image>().sprite = pData.equipped[0].GetComponent<SpriteRenderer>().sprite;
                    RemoveItem(selectedItemIndex);
                }
                break;
            case TurretStats.Slot.Mid:
                // Check mid slots and possibly unequip the worst equipped
                // Then equip the desired item
                if(pData.equipped[2] != null){
                    if(pData.equipped[3] != null){
                        // Check which is best and unequip other item
                        // For now it will just unequip the first turret
                        GameObject tempItem = pData.equipped[2];
                        SubtractStats(tempItem);
                        pData.equipped[2] = pData.inventory[selectedItemIndex];
                        equippedSlotButtons[2].GetComponent<Image>().sprite = pData.equipped[2].GetComponent<SpriteRenderer>().sprite;
                        RemoveItem(selectedItemIndex);
                        AddItem(tempItem);
                    }
                    else{
                        pData.equipped[3] = pData.inventory[selectedItemIndex];
                        equippedSlotButtons[3].GetComponent<Image>().sprite = pData.equipped[3].GetComponent<SpriteRenderer>().sprite;
                        RemoveItem(selectedItemIndex);
                    }
                }
                else{
                    pData.equipped[2] = pData.inventory[selectedItemIndex];
                    equippedSlotButtons[2].GetComponent<Image>().sprite = pData.equipped[2].GetComponent<SpriteRenderer>().sprite;
                    RemoveItem(selectedItemIndex);
                }
                break;
            case TurretStats.Slot.Back:
                // Check back slot and possibly unequip the equipped item
                // Then equip the desired item
                if(pData.equipped[4] != null){
                    GameObject tempItem = pData.equipped[4];
                    SubtractStats(tempItem);
                    pData.equipped[4] = pData.inventory[selectedItemIndex];
                    equippedSlotButtons[4].GetComponent<Image>().sprite = pData.equipped[4].GetComponent<SpriteRenderer>().sprite;
                    RemoveItem(selectedItemIndex);
                    AddItem(tempItem);
                }
                else{
                    pData.equipped[4] = pData.inventory[selectedItemIndex];
                    equippedSlotButtons[4].GetComponent<Image>().sprite = pData.equipped[4].GetComponent<SpriteRenderer>().sprite;
                    RemoveItem(selectedItemIndex);
                }
                break;
            default:
                Debug.Log("Unknown turret slot");
                break;
        }
        SelectorMenu.SetActive(false);
    }

    public void SellItem(){
        pData.money += inventorySlotButtons[selectedItemIndex].GetComponent<itemData>().sellPrice;
        RemoveItem(selectedItemIndex);
        SelectorMenu.SetActive(false);
    }

    public void UnequipItem(int index){
        AddItem(pData.equipped[index]);
        SubtractStats(pData.equipped[index]);
        pData.equipped[index] = null;
        equippedSlotButtons[index].GetComponent<Image>().sprite = noItem;   
    }

    public void SubtractStats(GameObject turret){
        pData.attack -= turret.GetComponent<TurretStats>().attack;
        pData.maxHealth -= turret.GetComponent<TurretStats>().health;
        pData.health -= turret.GetComponent<TurretStats>().health;
    }

    public void AddStats(GameObject turret){
        pData.attack += turret.GetComponent<TurretStats>().attack;
        pData.maxHealth += turret.GetComponent<TurretStats>().health;
        pData.health += turret.GetComponent<TurretStats>().health;
    }

    public void ChangeShip(int index){
        foreach(GameObject pSprite in GameObject.FindGameObjectsWithTag("PlayerSprite")){
            Destroy(pSprite);
        }
        player.GetComponent<PlayerController>().ChangeSprite(player.GetComponent<PlayerImages>().playerGO[index]);
    }

    public void ChangeGameMode(string gMode){
        switch(gMode){
            case "Endless":
                currentGameMode = GameMode.Endless;
                break;
            case "Tower":
                currentGameMode = GameMode.Tower;
                break;
            case "Level":
                currentGameMode = GameMode.Level;
                break;
            default:
                Debug.Log("Unknown game mode");
                break;
        }
    }

    public enum GameState
    {
        MainMenu,
        GameRunning,
        GamePaused,
        GameOver,
        Options,
        GameMenu,
        Inventory
    }

    public enum GameMode{
        Endless,
        Tower,
        Level
    }
}
