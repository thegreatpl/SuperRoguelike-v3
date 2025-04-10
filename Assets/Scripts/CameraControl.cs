using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject FollowTarget; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowTarget !=  null)
            transform.position = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y, transform.position.z);
    }
}
