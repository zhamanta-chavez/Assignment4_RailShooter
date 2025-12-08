using UnityEngine;

public class BossTargetScript : MonoBehaviour
{
    public float killTime = 4;
    public Transform cam;

    void Start()
    {
        Destroy(gameObject, killTime);
        cam = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(cam);
    }
}
