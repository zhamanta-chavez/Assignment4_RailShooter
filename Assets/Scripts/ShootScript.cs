using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public float fireRange = 500f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    void FireWeapon()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, fireRange))
        {
            Debug.Log("You just shot: " + hit.transform.name);
            Destroy(hit.transform.gameObject);
        }
    }
}
