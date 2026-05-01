using UnityEngine;

public class DoctorAction : MonoBehaviour
{
    public Transform tableTarget;
    public Transform playerTarget;
    public float walkSpeed = 1.5f;

    private Animator anim;
    private bool isWalking = false;
    private bool lookAtPlayer = false;
    private Transform currentTarget;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isWalking && currentTarget != null)
        {
            Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);

            Vector3 direction = (targetPos - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            anim.SetBool("isWalking", true);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                                 new Vector2(currentTarget.position.x, currentTarget.position.z)) < 0.2f)
            {
                StopWalkingAndInteract();
            }
        }
        else if (lookAtPlayer && playerTarget != null)
        {
            RotateTowards(playerTarget.position);
        }
    }

    private void RotateTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void StartWalkingToTable()
    {
        currentTarget = tableTarget;
        isWalking = true;
        lookAtPlayer = false;
    }

    public void StartWalkingToPlayer()
    {
        currentTarget = playerTarget;
        isWalking = true;
        lookAtPlayer = false;
    }

    public void LookAtPlayer() { lookAtPlayer = true; }

    private void StopWalkingAndInteract()
    {
        isWalking = false; 

        if (anim != null)
        {
            anim.SetBool("isWalking", false);

            if (currentTarget == tableTarget)
            {
                anim.SetTrigger("pickUp");
            }
            else if (currentTarget == playerTarget)
            {
                LookAtPlayer();
            }
        }
    }
}