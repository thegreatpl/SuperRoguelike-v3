using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public Vector3Int CurrentLocation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentLocation = Vector3Int.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLocation = GameManager.instance?.mapManager?.GetTileFromWorld(transform.position) ?? Vector3Int.zero;
    }
}
