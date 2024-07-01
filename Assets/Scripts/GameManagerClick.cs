using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerClick : MonoBehaviour
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

    private float spawnTimer = 0f;
    private float SPAWN_TIME = 0.2f;

    private int _clicks;
    
    [SerializeField] private LongClickButton mainMenuBtn;

    public GameManagerClick()
    {
        _clicks = 0;
    }

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
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= SPAWN_TIME)
        {
            spawnTimer = 0f;
            SpawnObjectAt(GetRandomPositionInView());
        }
    }
   

    void SpawnObjectAt(Vector2 pos)
    {
        Vector3 point = new Vector3(pos.x, pos.y, 1);
        GameObject go = Instantiate(SpawnObj, point, Quaternion.identity, transform);
        
        Color c = _CurrentColour.ClickColours[_ColourTracker];
        _ColourTracker++;
        _ColourTracker %= _CurrentColour.ClickColours.Length;

        float targetScale = Random.Range(_MinScale, _MaxScale);
        float targetTime = Random.Range(_MinTimeInSec, _MaxTimeInSec);
        
        go.GetComponent<SpawnableObject>()?.BeginFade(c, targetScale, targetTime);
    }
    
    Vector3 GetRandomPositionInView()
    {
        // Get camera size (orthographic size for orthographic camera, half of vertical size for perspective camera)
        float cameraSize = _Cam.orthographic ? _Cam.orthographicSize : Mathf.Tan(_Cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * _Cam.transform.position.z;

        // Calculate camera bounds
        float cameraBoundsX = cameraSize * _Cam.aspect;
        float cameraBoundsY = cameraSize;

        // Generate a random position within camera bounds
        float randomX = Random.Range(-cameraBoundsX, cameraBoundsX);
        float randomY = Random.Range(-cameraBoundsY, cameraBoundsY);

        // Convert screen coordinates to world coordinates
        Vector3 randomPosition = _Cam.transform.position + new Vector3(randomX, randomY, 1f);

        return randomPosition;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ObjectClicked()
    {
        _clicks++;
    }
}
