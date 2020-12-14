using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIManager : MonoBehaviour
{
    public GameObject currentTile, player;
    public enum direction { left, right, up, down };
    public direction currentDir;
    public int goalX, goalY, xDist, yDist;
    private int numMove1 = 0, numMove2 = 0, numBackUp = 0, numTurnLeft = 0, numTurnRight = 0, numUTurn = 0;

    public void RotateLeft()
    {
        Quaternion newQuat = new Quaternion();

        switch (currentDir)
        {
            case direction.left:
                newQuat.Set(0, 0, -1, 1);
                transform.rotation = newQuat;
                currentDir = direction.down;
                break;
            case direction.right:
                newQuat.Set(0, 0, 1, 1);
                transform.rotation = newQuat;
                currentDir = direction.up;
                break;

            case direction.up:
                newQuat.Set(0, 0, 1000, 1);
                transform.rotation = newQuat;
                currentDir = direction.left;
                break;

            case direction.down:
                newQuat.Set(0, 0, 0, 1);
                transform.rotation = newQuat;
                currentDir = direction.right;
                break;
        }
    }

    public void RotateRight()
    {
        Quaternion newQuat = new Quaternion();

        switch (currentDir)
        {
            case direction.left:
                newQuat.Set(0, 0, 1, 1);
                transform.rotation = newQuat;
                currentDir = direction.up;
                break;
            case direction.right:
                newQuat.Set(0, 0, -1, 1);
                transform.rotation = newQuat;
                currentDir = direction.down;
                break;

            case direction.up:
                newQuat.Set(0, 0, 0, 1);
                transform.rotation = newQuat;
                currentDir = direction.right;
                break;

            case direction.down:
                newQuat.Set(0, 0, 1000, 1);
                transform.rotation = newQuat;
                currentDir = direction.left;
                break;
        }
    }

    public void UTurn()
    {
        Quaternion newQuat = new Quaternion();

        switch (currentDir)
        {
            case direction.left:
                newQuat.Set(0, 0, 0, 1);
                transform.rotation = newQuat;
                currentDir = direction.right;
                break;
            case direction.right:
                newQuat.Set(0, 0, 1000, 1);
                transform.rotation = newQuat;
                currentDir = direction.left;
                break;

            case direction.up:
                newQuat.Set(0, 0, -1, 1);
                transform.rotation = newQuat;
                currentDir = direction.down;
                break;

            case direction.down:
                newQuat.Set(0, 0, 1, 1);
                transform.rotation = newQuat;
                currentDir = direction.up;
                break;
        }
    }

    public void Move1()
    {
        switch (currentDir)
        {
            case direction.left:
                if (currentTile.GetComponent<TileManager>().leftNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;

                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.right:
                if (currentTile.GetComponent<TileManager>().rightNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.up:
                if (currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().aboveNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.down:
                if (currentTile.GetComponent<TileManager>().belowNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
        }
        HandleCrash();
    }

    public void Move2()
    {
        Move1();
        Move1();
    }

    public void BackUp()
    {
        switch (currentDir)
        {
            case direction.left:
                if (currentTile.GetComponent<TileManager>().rightNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.right:
                if (currentTile.GetComponent<TileManager>().leftNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.up:
                if (currentTile.GetComponent<TileManager>().belowNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
            case direction.down:
                if (currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                {
                    currentTile = currentTile.GetComponent<TileManager>().aboveNeighbor;
                    transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                }
                break;
        }
        HandleBackwardsCrash();
    }

    void HandleCrash()
    {
        if (currentTile.GetComponent<TileManager>().id == player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().id)
        {
            switch (currentDir)
            {
                case direction.left:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().leftNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().leftNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.right:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().rightNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().rightNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.up:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().aboveNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.down:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().belowNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().belowNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().aboveNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
            }
        }
    }

    void HandleBackwardsCrash()
    {
        if (currentTile.GetComponent<TileManager>().id == player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().id)
        {
            switch (currentDir)
            {
                case direction.right:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().leftNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().leftNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.left:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().rightNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().rightNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.down:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().aboveNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.up:
                    if (player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().belowNeighbor != null)
                    {
                        player.GetComponent<PlayerManager>().currentTile = player.GetComponent<PlayerManager>().currentTile.GetComponent<TileManager>().belowNeighbor;
                        player.GetComponent<PlayerManager>().transform.position = player.GetComponent<PlayerManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().aboveNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
            }
        }
    }

    void GenerateValues()
    {
        int random;
        numUTurn = 0;
        numTurnLeft = 0;
        numTurnRight = 0;
        numBackUp = 0;
        numMove1 = 0;
        numMove2 = 0;

        for (int i = 0; i < 9; i++)
        {
            random = Random.Range(0, 84);
            if (random <= 6)
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
            else if (49 <= random && random < 78)
            {
                numMove1++;
            }
            else
            {
                numMove2++;
            }
        }
    }

    void UpdateGoalDistance()
    {
        string idString = currentTile.GetComponent<TileManager>().id.ToString();
        if(currentTile.GetComponent<TileManager>().id < 10)
        {
            idString = "0" + idString;
        }

        int row = (int)char.GetNumericValue(idString[0]);
        int col = (int)char.GetNumericValue(idString[1]);

        Debug.Log("idString = " + idString);
        Debug.Log("row = " + row);
        Debug.Log("col = " + col);
        Debug.Log("goalX = " + goalX);
        Debug.Log("goalY = " + goalY);

        xDist = goalX - col; // If xDist is positive, the goal is to the right
                             // If negative, the goal is to the left
        yDist = goalY - row; // If yDist is positive, the goal is below
                             // If negative, the goal is above
    }

    public char[] CalculateActions()
    {
        char[] actions = new char[5];
        char[] priority = new char[6];
        bool found;
        direction originalDir = currentDir;

        GenerateValues();
        UpdateGoalDistance();
        int ActionCounter, PriorityCounter = 0;

        for(ActionCounter = 0; ActionCounter < 5; ActionCounter++)
        {
            switch (currentDir)
            {
                case direction.left:
                    if (xDist < 0)
                    {
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        if (yDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        break;
                    }
                    if (xDist > 0)
                    {
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        if (yDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        break;
                    }
                    if (xDist == 0)
                    {
                        if (yDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        priority[PriorityCounter] = '2';
                        break;
                    }
                    break;
                case direction.right:
                    if (xDist > 0)
                    {
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        if (yDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        break;
                    }
                    if (xDist < 0)
                    {
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        if (yDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        break;
                    }
                    if (xDist == 0)
                    {
                        if (yDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        priority[PriorityCounter] = '2';
                        break;
                    }
                    break;
                case direction.up:
                    if(yDist < 0)
                    {
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        if (xDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        break;
                    }
                    if(yDist > 0)
                    {
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        if (xDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        break;
                    }
                    if(yDist == 0)
                    {
                        if (xDist > 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        priority[PriorityCounter] = '2';
                        break;
                    }
                    break;
                case direction.down:
                    if (yDist > 0)
                    {
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        if (xDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        break;
                    }
                    if (yDist < 0)
                    {
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        if (xDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = '2';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        break;
                    }
                    if (yDist == 0)
                    {
                        if (xDist < 0)
                        {
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                        }
                        else
                        {
                            priority[PriorityCounter] = 'L';
                            PriorityCounter++;
                            priority[PriorityCounter] = 'R';
                            PriorityCounter++;
                        }
                        priority[PriorityCounter] = 'U';
                        PriorityCounter++;
                        priority[PriorityCounter] = 'B';
                        PriorityCounter++;
                        priority[PriorityCounter] = '1';
                        PriorityCounter++;
                        priority[PriorityCounter] = '2';
                        break;
                    }
                    break;
                    
            }
            found = false;
            Debug.Log(xDist + " " + yDist + " " + priority[0]);
            for (int i = 0; i < 6; i++)
            {
                char currPriority = priority[i];
                switch (currPriority)
                {
                    case '1':
                        if(numMove1 > 0)
                        {
                            actions[ActionCounter] = '1';
                            numMove1--;
                            found = true;
                            switch (currentDir)
                            {
                                case direction.left:
                                    xDist++;
                                    break;
                                case direction.right:
                                    xDist--;
                                    break;
                                case direction.up:
                                    yDist++;
                                    break;
                                case direction.down:
                                    yDist--;
                                    break;
                            }
                        }
                        break;
                    case '2':
                        if(numMove2 > 0)
                        {
                            actions[ActionCounter] = '2';
                            numMove2--;
                            found = true;
                            switch (currentDir)
                            {
                                case direction.left:
                                    xDist += 2;
                                    break;
                                case direction.right:
                                    xDist -= 2;
                                    break;
                                case direction.up:
                                    yDist += 2;
                                    break;
                                case direction.down:
                                    yDist -= 2;
                                    break;
                            }
                        }
                        break;
                    case 'B':
                        if (numBackUp > 0)
                        {
                            actions[ActionCounter] = 'B';
                            numBackUp--;
                            found = true;
                            switch (currentDir)
                            {
                                case direction.left:
                                    xDist--;
                                    break;
                                case direction.right:
                                    xDist++;
                                    break;
                                case direction.up:
                                    yDist--;
                                    break;
                                case direction.down:
                                    yDist++;
                                    break;
                            }
                        }
                        break;
                    case 'L':
                        if (numTurnLeft > 0)
                        {
                            actions[ActionCounter] = 'L';
                            numTurnLeft--;
                            found = true;
                        }
                        break;
                    case 'R':
                        if (numTurnRight > 0)
                        {
                            actions[ActionCounter] = 'R';
                            numTurnRight--;
                            found = true;
                        }
                        break;
                    case 'U':
                        if (numUTurn > 0)
                        {
                            actions[ActionCounter] = 'U';
                            numUTurn--;
                            found = true;
                        }
                        break;
                }
                if (found)
                {
                    break;
                }
            }
            PriorityCounter = 0;
        }
        return actions;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDir = direction.up;
        Quaternion newQuat = new Quaternion();
        newQuat.Set(0, 0, 1, 1);
        transform.rotation = newQuat;
    }
}
