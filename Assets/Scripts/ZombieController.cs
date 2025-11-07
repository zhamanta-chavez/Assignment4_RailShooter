using UnityEngine;

public enum STATES
{ 
    moving,
    attack,
    hurt
}
public class ZombieController : MonoBehaviour
{
    public STATES _states;

    ZombieShootScript zShoot;
    Animator _anim;
    Transform _Player;

    public float moveSpeed = 1.5f;
    public float attackDistance;

    public bool inAction;
    public bool isHurt;
    public bool isDead;
    public bool isMoving;

    private void Start()
    {
        zShoot = GetComponent<ZombieShootScript>();
        _anim = GetComponent<Animator>();
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDead && !isHurt)
        {
            float _distance = Vector3.Distance(transform.position, _Player.position);

            if (_distance > attackDistance)
            {
                _states = STATES.moving;
                isMoving = true;
                transform.position = Vector3.MoveTowards(transform.position, _Player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (!isHurt)
                {
                    _states = STATES.attack;
                    isMoving = false;
                }
                else
                {
                    _states = STATES.hurt;
                }
            }

            _anim.SetBool("Moving", isMoving);
            _anim.SetBool("Hurt", isHurt);
            _anim.SetBool ("Dead", isDead);
        }
    }
}
