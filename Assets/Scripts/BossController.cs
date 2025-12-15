using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameManager gameManager;
    [SerializeField] TargetSpawnScript __spawnTargets;
    public float moveSpeed = .5f;

    public bool walkingForward;
    public bool isAttacking;
    public bool isStunned;
    public bool inAction;
    public bool isDead;
    public bool canAttack;

    [SerializeField] public int numberOfTargets = 5;

    public Transform pointA;
    public Transform pointB;

    public UnityEvent OnTargetsShot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkingForward = true;
        anim = GetComponent<Animator>();
        anim.ResetTrigger("Death");
        Debug.Log("Start");
        anim.SetBool("Forward", walkingForward);

        inAction = false;
        isDead = false;
        isAttacking = false;
        isStunned = false;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !inAction)
        {
            if (walkingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB.position, moveSpeed * Time.deltaTime);
                //anim.SetBool("Forward", walkingForward);
            }
            else if (isAttacking == false && isStunned == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
                //anim.SetBool("Forward", walkingForward);
            }

            float _distance = Vector3.Distance(transform.position, pointB.position);
            if (_distance < .5 && canAttack)
            {
                isAttacking = true;
                canAttack = false;
                anim.SetTrigger("Attack");
                isAttacking = true;
                __spawnTargets.SpawnTargets();
                numberOfTargets = __spawnTargets.numberOfTargets;
            }

            anim.SetBool("Attacking", isAttacking);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            walkingForward = false;
            anim.SetBool("Forward", walkingForward);
        }
        else if (other.gameObject.tag == "Start")
        {
            canAttack = true;
            walkingForward = true;
            anim.SetBool("Forward", walkingForward);
        }
    }

    public void ResetBoss()
    {
        Debug.Log("Called");
        inAction = false;
        walkingForward = false;
        isAttacking = false;
        isStunned = false;
    }

    public void StunBoss()
    {
        anim.SetTrigger("Stun");
        isStunned = true;
        numberOfTargets--;
        if (numberOfTargets <= 0)
        {
            ResetBoss();
            OnTargetsShot.Invoke();
        }
    }

    public void HurtPlayer()
    {
        Debug.Log("Hurt called");
        gameManager.HurtPlayer();
    }

    public void BossDeath()
    {
        Debug.Log("BossDeath called");
        isDead = true;
        anim.ResetTrigger("Stun");
        anim.ResetTrigger("Attack");
        anim.SetTrigger("Death");
    }

    public void ActivateCompletePanel()
    {
        gameManager.CompletePanelActivate();
    }
}
