using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField]
    private string MenuSceneName = "MainMenu";

    [SerializeField]
    private bool playingBlackJack = false;

    [SerializeField]
    private int pot = 0;

    [SerializeField]
    private Player thePlayer;

    [SerializeField]
    private Dealer theDealer;

    [SerializeField]
    private Text playerLabelText;

    [SerializeField]
    private Text dealerLabelText;


    [SerializeField]
    private PlayingCard[] playerHandObj;

    [SerializeField]
    private PlayingCard[] dealerHandObj;

    [SerializeField]
    private Text winnerText;

    [SerializeField]
    private Text dealtext;

    [SerializeField]
    private InputField stashInput;

    [SerializeField]
    private InputField nameInput;

    [SerializeField]
    private Button confirmNameButton;

    [SerializeField]
    private Button dealButton;

    [SerializeField]
    private Button betButton;

    [SerializeField]
    private Button foldButton;

    [SerializeField]
    private InputField betInputField;

    [SerializeField]
    private Text potText;

    [SerializeField]
    private Button QuitButton;

    [SerializeField]
    private Text CardCountText;

    [SerializeField]
    private Image DeckVisual; 

    private bool needsReset;

    private bool madeBet = false;

    private string dataPath = "";
    // Use this for initialization
    void Start()
    {
        winnerText.text = "";
        dealButton.gameObject.SetActive(false);
        foldButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        betInputField.gameObject.SetActive(false);
        potText.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        CardCountText.gameObject.SetActive(false);
        DeckVisual.gameObject.SetActive(false); 
        dataPath = Path.Combine(Application.persistentDataPath, "Data.txt");
        if (File.Exists(dataPath))
        {
            winnerText.text = "Player data exists. Loading old values.";
            thePlayer = LoadPlayer(dataPath);
            nameInput.text = thePlayer.GetName();
            stashInput.text = thePlayer.GetStash().ToString(); 
        }
    }

    private void UpdateNameLabels()
    {
        dealerLabelText.text = "Name: " + theDealer.GetName() + "\n" + "Stash: " + theDealer.GetStash();
        playerLabelText.text = "Name: " + thePlayer.GetName() + "\n" + "Stash: " + thePlayer.GetStash();
        CardCountText.text = theDealer.GetDeckSize().ToString() + " Cards";
        potText.text = "Pot: " + pot; 
    }
    
    
    public void PreparePlayer()
    {

        if(!File.Exists(dataPath))
        {
            string playerName = nameInput.text;
            int playerStash = int.Parse(stashInput.text);
            thePlayer = new Player(playerName, playerStash);
            
        }
        else
        {
           
            thePlayer = new Player(thePlayer.GetName(), thePlayer.GetStash());
            winnerText.text = "";
        }
        potText.gameObject.SetActive(true);
        DeckVisual.gameObject.SetActive(true);
        CardCountText.gameObject.SetActive(true); 
        nameInput.gameObject.SetActive(false);
        stashInput.gameObject.SetActive(false);
        confirmNameButton.gameObject.SetActive(false);
        theDealer = new Dealer("Dealer", (int)5 * (int)thePlayer.GetStash(), playingBlackJack);
        UpdateNameLabels();
        dealButton.gameObject.SetActive(true);
    }

    /**
     * Declare winner player.
     *
     * @return the Winner
     */
    public Player DeclareWinner()
    {
        if(!playingBlackJack)
        {
            if (thePlayer.ScoreHand() > theDealer.ScoreHand())
            {
                thePlayer.SetStash(thePlayer.GetStash() + pot);
                pot = 0;
                return thePlayer;
            }

            else
            {
                pot = 0;
                theDealer.setStash(theDealer.GetStash() + pot);
                return theDealer;


            }
        }
        else
        {
            if(thePlayer.ScoreBlackJackHand() <= 21 && thePlayer.ScoreBlackJackHand() > theDealer.ScoreBlackJackHand())
            {
                if(thePlayer.GetHandSize() == 2 && thePlayer.ScoreBlackJackHand() == 21)
                {
                    thePlayer.SetStash(thePlayer.GetStash() + (pot * 1.5f)); 
                }
                else
                {
                    thePlayer.SetStash(thePlayer.GetStash() + pot);
                }
                pot = 0;
                return thePlayer; 
            }
            else if(thePlayer.ScoreBlackJackHand() > 21 && theDealer.ScoreBlackJackHand() > 21)
            {
                pot = 0;
                theDealer.setStash(theDealer.GetStash() + pot);
                return theDealer;
            }
            else if(thePlayer.ScoreBlackJackHand() > 21)
            {
                pot = 0;
                theDealer.setStash(theDealer.GetStash() + pot);
                return theDealer;
            }
            else if(theDealer.ScoreBlackJackHand() > 21)
            {
                if (thePlayer.GetHandSize() == 2 && thePlayer.ScoreBlackJackHand() == 21)
                {
                    thePlayer.SetStash(thePlayer.GetStash() + (pot * 1.5f));
                }
                else
                {
                    thePlayer.SetStash(thePlayer.GetStash() + pot);
                }
                pot = 0;
                return thePlayer;
            }
            else
            {
                return null; //Nobody should have lost or won yet 
            }
        }
    }

    public void Fold()
    {
        winnerText.text = "Dealer Won! ";
        theDealer.SetStash(theDealer.GetStash() + pot);
        MakeHandsVisible();
        dealtext.text = "Restart";
        foldButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(true);
        CardCountText.gameObject.SetActive(false); 
        needsReset = true;

    }

    public void DealToPlayers()
    {
        if(needsReset)
        {
            ResetTable();
            needsReset = false;
            return; 
        }
        else
        {
            if(thePlayer.GetHandSize() == 0)
            {
                theDealer.DealFirstCardToPlayer(thePlayer);
                theDealer.DealFirstCardToPlayer(theDealer);
                if(playingBlackJack)
                {
                    thePlayer.GetHand().Get(0).Show();
                    theDealer.GetHand().Get(0).Show();
                }
                UpdateHands();
                UpdateNameLabels();
                betButton.gameObject.SetActive(true);
                foldButton.gameObject.SetActive(true); 
                return; 
            }
            else
            {
                theDealer.DealToPlayer(thePlayer);
                theDealer.DealToPlayer(theDealer);
                if (playingBlackJack)
                {
                    thePlayer.GetHand().Get(thePlayer.GetHandSize() - 1).Hide();
                    theDealer.GetHand().Get(theDealer.GetHandSize() - 1).Hide();
                }
                UpdateHands();
                UpdateNameLabels();
                if (playingBlackJack)
                {
                    Player player = DeclareWinner();
                    if(player != null)
                    {
                        MakeHandsVisible();
                        winnerText.text = player.GetName() + " Won! ";
                        needsReset = true;
                        foldButton.gameObject.SetActive(false);
                        betButton.gameObject.SetActive(false);
                        QuitButton.gameObject.SetActive(true);
                        dealtext.text = "Restart";
                    }
                }

                if (thePlayer.GetHandSize() == 5 && !playingBlackJack)
                {
                    Player player = DeclareWinner();
                    MakeHandsVisible();
                    winnerText.text = player.GetName() + " Won! ";
                    needsReset = true;
                    foldButton.gameObject.SetActive(false);
                    betButton.gameObject.SetActive(false);
                    QuitButton.gameObject.SetActive(true);
                    dealtext.text = "Restart";
                }
            }
        }
    }

    private void MakeHandsVisible()
    {
        for(int i = 0; i < thePlayer.GetHandSize(); i++)
        {
            thePlayer.GetHand().Get(i).Show();
            theDealer.GetHand().Get(i).Show();
        }
        UpdateHands();

    }

    public void Quit()
    {
        SavePlayer(thePlayer, dataPath);
        SceneManager.LoadScene(MenuSceneName); 
    }

    static void SavePlayer(Player data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    static Player LoadPlayer(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<Player>(jsonString);
        }
    }



    public void MakeBet()
    {
        if(!madeBet)
        {
            betInputField.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(false);
            foldButton.gameObject.SetActive(false);
            madeBet = true; 
        }
        else
        {

            int betAmount = int.Parse(betInputField.text);
            if (betAmount > thePlayer.GetStash())
            {
                betAmount -= (int)thePlayer.GetStash();
                Debug.Log("Table.MakeBet : bet amount too big");
            }
            pot += (betAmount * 2);
            theDealer.SetStash(theDealer.GetStash() - betAmount);
            thePlayer.SetStash(thePlayer.GetStash() - betAmount);
            UpdateNameLabels();
            DealToPlayers();
            UpdateHands(); 
            betInputField.gameObject.SetActive(false);
            foldButton.gameObject.SetActive(true);
            dealButton.gameObject.SetActive(true);
            madeBet = false; 
        }
    }


    private void ResetTable()
    {
        if (theDealer.GetStash() > thePlayer.GetStash())
        {
            theDealer = new Dealer("Dealer", (int)theDealer.GetStash(), playingBlackJack);
            thePlayer = new Player(thePlayer.GetName(), (int)thePlayer.GetStash());
        }

        else
        {
            theDealer = new Dealer("Dealer", (int)theDealer.GetStash() * 5, playingBlackJack);
            thePlayer = new Player(thePlayer.GetName(), (int)thePlayer.GetStash());
        }

        for(int i = 0; i < playerHandObj.Length; i++)
        {
            playerHandObj[i].SetCardProperties(null);
            dealerHandObj[i].SetCardProperties(null); 
        }

        winnerText.text = "";
        dealtext.text = "Deal";
        foldButton.gameObject.SetActive(true);
        betButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(false);
        UpdateNameLabels(); 
    }


    private void UpdateHands()
    {
        List<SolitaireCardProperties> playerHand = thePlayer.GetHand();
        List<SolitaireCardProperties> dealerHand = theDealer.GetHand();
        for (int i = 0; i < thePlayer.GetHandSize() && i < playerHandObj.Length; i++)
        {
            playerHandObj[i].SetCardProperties(playerHand.Get(i));
        }

        for (int i = 0; i < theDealer.GetHandSize() && i < dealerHandObj.Length; i++)
        {
            dealerHandObj[i].SetCardProperties(dealerHand.Get(i));
        }

    }
}
