using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private ColourSO Colour;

    [SerializeField] 
    private string gameSceneOne;
    [SerializeField]
    private string gameSceneTwo;
    
    private Camera _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _cam.backgroundColor = Colour.BackgroundColour;
    }

    public void GameOne()
    {
        SceneManager.LoadScene(gameSceneOne);
    }

    public void GameTwo()
    {
        SceneManager.LoadScene(gameSceneTwo);
    }
}
