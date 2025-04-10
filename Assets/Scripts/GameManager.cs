using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PrefabManager))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(SpriteManager))]
[RequireComponent(typeof(ItemManager))]
[RequireComponent(typeof(EntityManager))]
[RequireComponent(typeof(TileManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PrefabManager prefabManager;


    public TimeManager timeManager;

    public SpriteManager spriteManager;

    public UIController UIController;

    public ItemManager itemManager;

    public EntityManager entityManager;

    public TileManager tileManager;

    public MapManager mapManager;


    public CameraControl CameraControl; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {   
            Destroy(this);
            return; 
        }
        instance = this;

        prefabManager = GetComponent<PrefabManager>();
        timeManager = GetComponent<TimeManager>();
        spriteManager = GetComponent<SpriteManager>();
        itemManager = GetComponent<ItemManager>();
        entityManager = GetComponent<EntityManager>();
        tileManager = GetComponent<TileManager>();
        StartNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartNewGame()
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        timeManager.ResetTime();
        yield return null;
        spriteManager.LoadSprites();
        yield return null;
        yield return StartCoroutine(itemManager.LoadItems());
        yield return null;
        yield return StartCoroutine(tileManager.CreateTiles());
        yield return null;
        yield return StartCoroutine(mapManager.MapGenerator.GenetateFloor()); 
        

        timeManager.StartTime(); 
    }


    public void ShowMessage(string message, Color color)
    {
       UIController?.MessageCenter?.ShowMessage(message, color);
    }
}
