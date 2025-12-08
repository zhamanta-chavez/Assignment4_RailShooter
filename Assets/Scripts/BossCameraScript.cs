using UnityEngine;

public class BossCameraScript : MonoBehaviour
{
    public Transform lookTarget;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt( lookTarget );
    }
}
