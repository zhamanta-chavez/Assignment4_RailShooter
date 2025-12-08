using JetBrains.Annotations;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator anim;
    public float moveSpeed = .5f;

    public bool walkingForward;
    public bool isAttacking;
    public bool inAction;
    public bool isDead;

    [SerializeField] public int numberOfTargets = 5;

    public Transform pointA;
    public Transform pointB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkingForward = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !inAction)
        {
            if (walkingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
                anim.SetBool("Forward", walkingForward);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
            }

            float _distance = Vector3.Distance(transform.position, pointB.position);
            if (_distance < 1 && !isAttacking)
            {
                isAttacking = true;
                TargetSpawnScript __spawnTargets = GameObject.FindFirstObjectByType<TargetSpawnScript>();
                __spawnTargets.SpawnTargets();
            }

            anim.SetBool("Forward", walkingForward);
            anim.SetBool("Attacking", isAttacking);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            if (walkingForward)
            {
                inAction = true;
                anim.SetTrigger("Attack");
                //walkingForward = false;
            }
            else
            {
                walkingForward = true;
            }
        }
    }

    public void ResetBoss()
    {
        Debug.Log("Called");
        inAction = false;
        walkingForward = false;
        isAttacking = false;
    }

    public void StunBoss()
    {
        anim.SetTrigger("Stun");
        numberOfTargets--;
        if (numberOfTargets <= 0)
        {
            ResetBoss();
        }
    }

    public void HurtPlayer()
    {
        Debug.Log("Hurt called");
        GameManager.Instance.HurtPlayer();
    }

    public void BossDeath()
    {
        isDead = true;
        anim.SetTrigger("Death");
    }
}
