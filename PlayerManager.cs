using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject currentTile, ai;
    public enum direction { left, right, up, down };
    public direction currentDir;

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
                if(currentTile.GetComponent<TileManager>().leftNeighbor != null)
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
        if(currentTile.GetComponent<TileManager>().id == ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().id)
        {
            switch (currentDir)
            {
                case direction.left:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().leftNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().leftNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.right:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().rightNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().rightNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.up:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().aboveNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.down:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().belowNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().belowNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
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
        if (currentTile.GetComponent<TileManager>().id == ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().id)
        {
            switch (currentDir)
            {
                case direction.right:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().leftNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().leftNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().rightNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.left:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().rightNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().rightNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().leftNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.down:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().aboveNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().aboveNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    else
                    {
                        currentTile = currentTile.GetComponent<TileManager>().belowNeighbor;
                        transform.position = currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
                    }
                    break;
                case direction.up:
                    if (ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().belowNeighbor != null)
                    {
                        ai.GetComponent<AIManager>().currentTile = ai.GetComponent<AIManager>().currentTile.GetComponent<TileManager>().belowNeighbor;
                        ai.GetComponent<AIManager>().transform.position = ai.GetComponent<AIManager>().currentTile.GetComponent<Transform>().position + new Vector3(0, 0, -1);
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

    // Start is called before the first frame update
    void Start()
    {
        currentDir = direction.up;
        Quaternion newQuat = new Quaternion();
        newQuat.Set(0, 0, 1, 1);
        transform.rotation = newQuat;
    }

}
