using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject aboveNeighbor = null;
    public GameObject belowNeighbor = null;
    public GameObject leftNeighbor = null;
    public GameObject rightNeighbor = null;
    public int id;

    private void SetupTile()
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupTile();   
    }
}
