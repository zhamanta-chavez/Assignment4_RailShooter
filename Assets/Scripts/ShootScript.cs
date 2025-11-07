using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public float fireRange = 500f;
    public int shotType = 1;
    public bool isShotGun = false;

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
            if (hit.transform.gameObject.tag == "Head")
            {
                Debug.Log("Head Shot!");
                bool _head = true;
                GameObject _zombie = hit.transform.gameObject;
                ZombieShootScript zShoot = _zombie.GetComponentInParent<ZombieShootScript>();
                zShoot.TakeDamage(shotType, _head, isShotGun);
            }

            if (hit.transform.gameObject.tag == "Body")
            {
                Debug.Log("Body Shot!");
                bool _head = false;
                GameObject _zombie = hit.transform.gameObject;
                ZombieShootScript zShoot = _zombie.GetComponentInParent<ZombieShootScript>();
                zShoot.TakeDamage(shotType, _head, isShotGun);
            }
        }
    }
}
