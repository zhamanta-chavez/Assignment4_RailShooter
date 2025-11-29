using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    public float rotateSpeed = 30;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); 
    }
}
