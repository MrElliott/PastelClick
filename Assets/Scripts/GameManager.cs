using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ColourSO[] Colours;
    [SerializeField]
    private GameObject SpawnObj;

    [SerializeField] private float _MinScale;
    [SerializeField] private float _MaxScale;

    [SerializeField] private float _MinTimeInSec;
    [SerializeField] private float _MaxTimeInSec;
    
    private ColourSO _CurrentColour;
    private ColourSO _NextColour;

    private Camera _Cam;

    private int _ColourTracker = 0;
    
    [SerializeField] private LongClickButton mainMenuBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        _CurrentColour = Colours[0];
        _Cam = Camera.main;

        _Cam.backgroundColor = _CurrentColour.BackgroundColour;
        
        mainMenuBtn.onLongClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        int tc = Input.touchCount;
        // Check for touches
        if (tc > 0)
        {
            Debug.Log("Touch Events triggering");
            for (int i = 0; i < tc; i++)
            {
                Touch touch = Input.GetTouch(i); // Get the first touch (assuming only one touch at a time)

                if (touch.phase == TouchPhase.Began) // Check if the touch just began
                {
                    // Trigger event if a touch is detected
                    Vector2 touchPosition = touch.position;
                    SpawnObjectAt(touchPosition);
                }
            }
        }
#else
        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Vector2 mousePosition = Input.mousePosition;
            SpawnObjectAt(mousePosition);
        }
#endif
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(pointerEventData.pointerCurrentRaycast.worldPosition);
        Vector3 spawnPos = pointerEventData.pointerCurrentRaycast.worldPosition;
        spawnPos.z = 1;
        SpawnObjectAt(spawnPos);
    }
    

    void SpawnObjectAt(Vector2 pos)
    {
        Vector3 point = _Cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1));
        GameObject go = Instantiate(SpawnObj, point, Quaternion.identity, transform);
        
        Color c = _CurrentColour.ClickColours[_ColourTracker];
        _ColourTracker++;
        _ColourTracker %= _CurrentColour.ClickColours.Length;

        float targetScale = Random.Range(_MinScale, _MaxScale);
        float targetTime = Random.Range(_MinTimeInSec, _MaxTimeInSec);
        
        go.GetComponent<SpawnableObject>()?.BeginFade(c, targetScale, targetTime);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
