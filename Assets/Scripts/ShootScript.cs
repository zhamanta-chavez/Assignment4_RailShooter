using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public float fireRange = 500f;
    public int shotType = 1;
    public bool isShotGun = false;

    public UnityEvent OnShootSound;

    private void Awake()
    {
        //gameManager = GameObject.FindFirstObjectByType<GameManager>();  
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }*/
        if (Input.GetKeyDown(KeyCode.Mouse1) && gameManager.shellCount <= 0)
        {
            gameManager.bulletCount = gameManager.maxBulletCount;
            gameManager.reloadDisplay = 0;
        }
    }

    public void OnReload()
    {
        gameManager.ResetNormalBullets();
    }

    public void OnQuit()
    {
        gameManager.ExitGame();
    }

    public void OnShoot()
    {
        if (gameManager.bulletCount > 0 || gameManager.shellCount > 0)
        {
            if (gameManager.bossCanDie == true)
            {
                OnShootSound.Invoke();
                if (gameManager.shellCount > 0)
                {
                    gameManager.shellCount--;
                    shotType = 5;
                    isShotGun = true;
                }
                else if (gameManager.bulletCount > 0)
                {
                    gameManager.bulletCount--;
                    shotType = 1;
                    isShotGun = false;
                }

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

                    if (hit.transform.gameObject.tag == "Shotgun")
                    {
                        gameManager.shellCount = 10;
                        Destroy(hit.transform.gameObject);
                    }

                    if (hit.transform.gameObject.tag == "Health")
                    {
                        gameManager.playerHealth++;
                        Destroy(hit.transform.gameObject);
                    }

                    if (hit.transform.gameObject.tag == "Boss Target")
                    {
                        Debug.Log("TargetHit!");
                        BossController _boss = GameObject.FindFirstObjectByType<BossController>();
                        gameManager.bossHealth -= 4;
                        _boss.StunBoss();
                        Destroy(hit.transform.gameObject);
                    }

                    if (hit.transform.gameObject.tag == "WeakPoint")
                    {
                        Debug.Log("weak Point Hit!");
                        gameManager.bossHealth -= 1;
                    }
                }
            }
        }
        else
        {
            gameManager.reloadDisplay = gameManager.reloadDisplayTime;
        }
    }
}
