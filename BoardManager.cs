using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static int rows = 8;
    public static int cols = 8;
    public static float tileSize = 1.0f;
    public int goalId;

    public GameObject endCanvas;
    public GameObject normalCanvas;

    public GameObject player;
    public GameObject ai;
    private GameObject[,] board = new GameObject[rows, cols];

    public Button Move1, Move2, BackUp, TurnLeft, TurnRight, UTurn, PlayAgain, Exit;
    private int numMove1 = 0, numMove2 = 0, numBackUp = 0, numTurnLeft = 0, numTurnRight = 0, numUTurn = 0;
    public char[] PlayerActions = new char[5];
    public char[] AIActions;
    private int goalX, goalY, ActionCounter = 0;

    public Text text1, text2, textback, textleft, textright, textu, wintext;


    private void GenerateBoard()
    {
        GameObject masterTile = (GameObject)Instantiate(Resources.Load("FactoryFloor"));
        player = (GameObject)Instantiate(Resources.Load("Player"), transform);
        ai = (GameObject)Instantiate(Resources.Load("AI"), transform);

        int startX = Random.Range(0, cols);
        int startY = Random.Range(rows-2, rows);

        goalX = Random.Range(0, cols);

        while (goalX == startX)
        {
            goalX = Random.Range(0, cols);
        }

        goalY = Random.Range(0, 2);

        ai.GetComponent<AIManager>().goalX = goalX;
        ai.GetComponent<AIManager>().goalY = goalY;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject currentTile;

                if(col == goalX && row == goalY)
                {
                    currentTile = (GameObject)Instantiate(((GameObject)Instantiate(Resources.Load("Goal Variant"))), transform);
                    
                }
                else
                {
                    currentTile = (GameObject)Instantiate(masterTile, transform);
                }
                

                float posX = col * tileSize;
                float posY = row * -tileSize;

                currentTile.transform.position = new Vector2(posX, posY);

                if (col == startX && row == startY)
                {
                    player.transform.position = new Vector2(posX, posY);
                    ai.transform.position = new Vector2(posX, posY);

                    player.GetComponent<PlayerManager>().currentTile = currentTile;
                    ai.GetComponent<AIManager>().currentTile = currentTile;
                }

                board[row, col] = currentTile;

                currentTile.GetComponent<TileManager>().id = (row * 10) + col;
                if (col == goalX && row == goalY)
                {
                    goalId = (row * 10) + col;
                    Debug.Log("Goal id" + currentTile.GetComponent<TileManager>().id);
                }

            }
        }

        Destroy(masterTile);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {

                GameObject currentTile = board[row, col];

                if(col != 0)
                {
                    currentTile.GetComponent<TileManager>().leftNeighbor = board[row, col-1];
                }

                if(col != cols-1)
                {
                    currentTile.GetComponent<TileManager>().rightNeighbor = board[row, col+1];
                }

                if(row != 0)
                {
                    currentTile.GetComponent<TileManager>().aboveNeighbor = board[row-1, col];
                }

                if (row != rows-1)
                {
                    currentTile.GetComponent<TileManager>().belowNeighbor = board[row+1, col];
                }
            }
        }

        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;

        transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);

        Vector3 temp = new Vector3(0, 0, -1.0f);
        player.transform.position += temp;
        ai.transform.position += temp;

        temp = new Vector3(0.75f, 0.75f, 0);
        player.transform.localScale = temp;
        ai.transform.localScale = temp;

    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
        GenerateValues();
        Button mov1 = Move1.GetComponent<Button>();
        Button mov2 = Move2.GetComponent<Button>();
        Button back = BackUp.GetComponent<Button>();
        Button left = TurnLeft.GetComponent<Button>();
        Button right = TurnRight.GetComponent<Button>();
        Button turn180 = UTurn.GetComponent<Button>();

        mov1.onClick.AddListener(MovePlayer1);
        mov2.onClick.AddListener(MovePlayer2);
        back.onClick.AddListener(BackPlayerUp);
        left.onClick.AddListener(TurnPlayerLeft);
        right.onClick.AddListener(TurnPlayerRight);
        turn180.onClick.AddListener(TurnPlayerU);

        player.GetComponent<PlayerManager>().ai = ai;
        ai.GetComponent<AIManager>().player = player;

        endCanvas.SetActive(false);
        normalCanvas.SetActive(true);
    }

    void MovePlayer1()
    {
        if(numMove1 > 0)
        {
            PlayerActions[ActionCounter] = '1';
            ActionCounter++;
            numMove1--;
            text1.text = "Quantity: " + numMove1;
        }
    }

    void MovePlayer2()
    {
        if(numMove2 > 0)
        {
            PlayerActions[ActionCounter] = '2';
            ActionCounter++;
            numMove2--;
            text2.text = "Quantity: " + numMove2;
        }
    }

    void BackPlayerUp()
    {
        if(numBackUp > 0)
        {
            PlayerActions[ActionCounter] = 'B';
            ActionCounter++;
            numBackUp--;
            textback.text = "Quantity: " + numBackUp;
        }
    }

    void TurnPlayerLeft()
    {
        if(numTurnLeft > 0)
        {
            PlayerActions[ActionCounter] = 'L';
            ActionCounter++;
            numTurnLeft--;
            textleft.text = "Quantity: " + numTurnLeft;
        }
    }

    void TurnPlayerRight()
    {
        if(numTurnRight > 0)
        {
            PlayerActions[ActionCounter] = 'R';
            ActionCounter++;
            numTurnRight--;
            textright.text = "Quantity: " + numTurnRight;
        }
    }

    void TurnPlayerU()
    {
        if(numUTurn > 0)
        {
            PlayerActions[ActionCounter] = 'U';
            ActionCounter++;
            numUTurn--;
            textu.text = "Quantity: " + numUTurn;
        }
    }

    void GenerateValues()
    {
        int random;

        for (int i = 0; i < 9; i++)
        {
            random = Random.Range(0, 84);
            if(random <= 6)
            {
                numUTurn++;
            }
            else if (7 <= random && random < 25)
            {
                numTurnLeft++;
            }
            else if (25 <= random && random < 43)
            {
                numTurnRight++;
            }
            else if (43 <= random && random < 49)
            {
                numBackUp++;
            }
            else if(49 <= random && random < 78)
            {
                numMove1++;
            }
            else
            {
                numMove2++;
            }
        }

        UpdateText();
    }

    void UpdateText()
    {
        text1.text = "Quantity: " + numMove1;
        text2.text = "Quantity: " + numMove2;
        textback.text = "Quantity: " + numBackUp;
        textleft.text = "Quantity: " + numTurnLeft;
        textright.text = "Quantity: " + numTurnRight;
        textu.text = "Quantity: " + numUTurn;
    }

    /*void HandlePlayerCrash(bool forwards)
    {
        switch (playerDir)
        {
            case direction.up:
                ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.getComponent<TileManager>().aboveNeighbor;
                break;
        }
    }*/

    void Update()
    {
        if(ActionCounter >= 5)
        {
            ActionCounter = 0;
            AIActions = ai.GetComponent<AIManager>().CalculateActions();
            numMove1 = 0;
            numMove2 = 0;
            numBackUp = 0;
            numTurnLeft = 0;
            numTurnRight = 0;
            numUTurn = 0;

            UpdateText();

            StartCoroutine(RunTurn());

        }
    }

    IEnumerator RunTurn()
    {
        char PlayerAction, AIAction;

        for (int i = 0; i < 5; i++)
        {
            PlayerAction = PlayerActions[i];
            AIAction = AIActions[i];

            // Would use a switch statement here, but need these to be performed in a specific order
            if (PlayerAction == '2')
            {
                player.GetComponent<PlayerManager>().Move2();
            }
            if(AIAction == '2')
            {
                ai.GetComponent<AIManager>().Move2();
            }

            if(PlayerAction == '1')
            {
                player.GetComponent<PlayerManager>().Move1();
            }
            if(AIAction == '1')
            {
                ai.GetComponent<AIManager>().Move1();
            }

            if (PlayerAction == 'B')
            {
                player.GetComponent<PlayerManager>().BackUp();
            }
            if (AIAction == 'B')
            {
                ai.GetComponent<AIManager>().BackUp();
            }

            if (PlayerAction == 'R')
            {
                player.GetComponent<PlayerManager>().RotateRight();
            }
            if (AIAction == 'R')
            {
                ai.GetComponent<AIManager>().RotateRight();
            }

            if (PlayerAction == 'L')
            {
                player.GetComponent<PlayerManager>().RotateLeft();
            }
            if (AIAction == 'L')
            {
                ai.GetComponent<AIManager>().RotateLeft();
            }

            if (PlayerAction == 'U')
            {
                player.GetComponent<PlayerManager>().UTurn();
            }
            if (AIAction == 'U')
            {
                ai.GetComponent<AIManager>().UTurn();
            }

            yield return new WaitForSeconds(1.0f);
        }
        GenerateValues();

        int playerID = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().id;
        int aiID = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().id;
        if (playerID == goalId)
        {
            wintext.text = "Player Win!";
            normalCanvas.SetActive(false);
            endCanvas.SetActive(true);
        }
        if (aiID == goalId)
        {
            wintext.text = "AI Win!";
            normalCanvas.SetActive(false);
            endCanvas.SetActive(true);
        }
        Debug.Log("Player Id: " + playerID);


        yield break;
    }
}
