using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WTMK.Command;
using SimpleJSON;
using UnityEngine.UI;
public class Main : MonoBehaviour
{

    //screen set up
    [SerializeField]
    private List<GameObject> goGameScreens;
    private Dictionary<ScreenID,IScreen> gameScreens;
    private ScreenID currentScreen;
    //

    //pool set up
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private GameObject poolable;
    private GameObjectPooler gameObjectPooler;
    //

    //twitch connection
    private TwitchClient twitchClient;
    private List<string> validCmds;
    private Dictionary<string,ICommand> cmdDictionary;
    private TwitchCmdManager twitchCmdManager;
    //

    //Game
    [SerializeField]
    private string cmdFile, secretsFile;
    [SerializeField]
    private List<GameObject> goodSpawnPoints;
    private PlayerModel playerModel;
    private List<ICommand> playerCollectCommands;
    public int numberOfCollectCommands;
    private bool hasAnnoucned = false;
    [SerializeField]
    private List<Image> images;
    [SerializeField]
    private Image selfDouptImage,confidenceWordImg,selfDouptWordImg,helpScreen;
    
    [SerializeField]
    private GameObject player;
    private PlayerMovement playerMovement;
    private int currentLevel;
    [SerializeField]
    private List<Sprite> evolutionLevels;
    [SerializeField]
    private Button toggelHelp;
    private bool toggle = true;
    // [SerializeField]
    // private List<GameObject> badSpawnPoints;
    //

    private Secrets Secrets;

    private void ToggelHelpButton()
    { 
        if(!toggle)
        {
            toggle = true;
        }else{
            toggle = false;
        }

        helpScreen.gameObject.SetActive(toggle);
    }
    void Awake()
    {
        InitSecrets(); // fill with jason data streamer will need to fill that out to play
        Connect();
    }

    private void Connect() // requires Secrets instance
    {
        twitchClient = new TwitchClient(Secrets.channel_name,Secrets.bot0_name,Secrets.bot0_access_token);
    }

    void OnEnable()
    {
        twitchClient.OnMessageReceived += MessageReceived;
    }

    void OnDisable()
    {
        twitchClient.OnMessageReceived -= MessageReceived;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        playerModel.LevelUp();

        if(currentLevel >= 0)
        {
            confidenceWordImg.gameObject.SetActive(true);
            selfDouptWordImg.gameObject.SetActive(false);
            images[currentLevel].fillAmount = playerModel.CurrentConfidence;
        }
        else
        {
            confidenceWordImg.gameObject.SetActive(false);
            selfDouptWordImg.gameObject.SetActive(true);
            foreach(var img in images)
            {
                img.fillAmount = 0.0f;
            }
            selfDouptImage.fillAmount = playerModel.CurrentConfidence;
        }

        if(currentLevel == 1)
        {
            images[0].fillAmount = 1.0f;
        }

        if(currentLevel == 2)
        {
            images[1].fillAmount = 1.0f;
        }

        if(currentLevel == 3)
        {
            images[2].fillAmount = 1.0f;
        }
        
        if(!hasAnnoucned && twitchClient.InChannel())
        {
            foreach(var txt in validCmds)
            {
                twitchClient.SendMessage("Type: " +txt);
            }

            hasAnnoucned = true;
        }

        CheckForEvolution();
        CheckGameState();
    }

    private void Init()
    {
        playerModel = new PlayerModel();
        currentLevel = playerModel.Level;
        gameScreens = new Dictionary<ScreenID,IScreen>();
        cmdDictionary = new Dictionary<string,ICommand>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.Init(playerModel);
        LoadScreens();
        currentScreen = ScreenID.StartScreen;

        if(poolable != null)
        {
            LoadPlayerCollectCommands();
            BuildPool();
        }

        LoadCmds();

        gameScreens[currentScreen].Show();
        toggelHelp.onClick.AddListener(ToggelHelpButton);
    }

    private void LoadPlayerCollectCommands()
    {
        playerCollectCommands = new List<ICommand>();
        var count = 0;
        do
        {
            playerCollectCommands.Add(new PlayerCollectCommand(playerModel,count));
            count++;
        }while (count < numberOfCollectCommands);
    }

    private void LoadCmds()
    {
        validCmds = new List<string>();
        var data = File.ReadAllText(Application.dataPath + "\\" + cmdFile);
        twitchCmdManager = new TwitchCmdManager(data);
        validCmds = twitchCmdManager.GetValidCmds();

        var count = 0;
        do
        {  
            cmdDictionary.Add(validCmds[count],new SpawnObjectCommand(gameObjectPooler, goodSpawnPoints[count].transform.position,count));
            count++;
        } while (count < validCmds.Count);
        var x = validCmds.Count - 1;
        Debug.Log(validCmds[x]);
    }

    private void InitSecrets()
    {
        var data = File.ReadAllText(Application.dataPath + "\\" + secretsFile);
        Secrets = new Secrets(data);
    }

    private void BuildPool()
    {
        gameObjectPooler = new GameObjectPooler();
        var count = 0;
        do
        {
            GameObject clone = Instantiate(poolable);
            IPoolable iPoolable = clone.GetComponent<IPoolable>();
            iPoolable.Init(gameObjectPooler);
            gameObjectPooler.SetPoolable(clone);

            TIGPoolable tIGPoolable = clone.GetComponent<TIGPoolable>();

            if(tIGPoolable != null)
            {
                tIGPoolable.SetCommands(playerCollectCommands);
            }

            count++;
        }while(count < poolSize);
    }

    private void LoadScreens()
    {
        foreach( GameObject go in goGameScreens )
        {
            IScreen screen = go.GetComponent<IScreen>();
            screen.Init();
            gameScreens.Add(screen.GetID(), screen);
        }   
    }

    public void SetCurrentScreen(ScreenID screenId)
    {
        gameScreens[currentScreen].Hide();
        currentScreen = screenId;
        if(currentScreen == ScreenID.GameScreen)
        {
            player.SetActive(true);
        }
        gameScreens[currentScreen].Show();
    }

    private void MessageReceived(string userName, string message)
    {
        
        switch(currentScreen)
        {
            case ScreenID.StartScreen:
            HandelStartScreen(userName,message);
            break;
            case ScreenID.LobbyScreen:
            HandelStartScreen(userName,message);
            break;
            case ScreenID.GameScreen:
            HandelGameScreen(userName,message);
            break;
        }
    }

    private void HandelStartScreen(string userName, string message)
    {
        Debug.Log(userName);
        Debug.Log(userName + " says " + message);
    }

    private void HandelGameScreen(string userName, string message)
    {
        
        Debug.Log(userName + " says " + message);

        if(message[0] !='$')
        {
            return;
        }

        var isValid = validCmds.Contains(message);

        if(isValid)
        {
            Debug.Log(userName);
            cmdDictionary[message].Execute();
        }
    }

    private void CheckForEvolution()
    {
        if(currentLevel != playerModel.Level)
        {
            if(currentLevel > playerModel.Level)
            {
                playerMovement.LevelDown();
                switch(playerModel.Level)
                {
                    case 0:
                    images[0].fillAmount = 0.0f;
                    images[1].fillAmount = 0.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                    case 1:
                    images[0].fillAmount = 1.0f;
                    images[1].fillAmount = 0.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                    case 2:
                    images[0].fillAmount = 1.0f;
                    images[1].fillAmount = 1.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                }
            }

            if(currentLevel < playerModel.Level)
            {

                playerMovement.LevelUp();
                switch(playerModel.Level)
                {
                    case 0:
                    images[0].fillAmount = 0.0f;
                    images[1].fillAmount = 0.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                    case 1:
                    images[0].fillAmount = 1.0f;
                    images[1].fillAmount = 0.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                    case 2:
                    images[0].fillAmount = 1.0f;
                    images[1].fillAmount = 1.0f;
                    images[2].fillAmount = 0.0f;
                    break;
                }
            }
            currentLevel = playerModel.Level;
        }
    }

    private void CheckGameState()
    {
        if(playerModel.hasWon)
        {
            gameScreens[currentScreen].Hide();
            currentScreen = ScreenID.CreditsScreen;
            gameScreens[currentScreen].Show();
        }

        if( playerModel.hasLost)
        {
            gameScreens[currentScreen].Hide();
            currentScreen = ScreenID.GameOverScreen;
            gameScreens[currentScreen].Show();
            player.SetActive(false);
        }


    }

}
