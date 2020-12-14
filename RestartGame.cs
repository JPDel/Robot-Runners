using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void Reload()
    {
        Debug.Log("Reload");
        SceneManager.LoadScene("SampleScene");
    }
}
