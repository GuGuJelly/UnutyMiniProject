using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking.Types;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]

public class Patroller : MonoBehaviour
{
    // ���� �÷��̾ �����ϴ� �ӵ�
    public float chaserSpeed;
    // ������ ���� �ӵ�
    public float normalSpeed;
    // �� �ΰ��� �ӵ� �� �ϳ��� �����ϰ� �ִ� ����ӵ�
    public float currentSpeed;

    // ��ȸ�� ������ ��ȯ �� ����
    public float directionChangeInterval;
    // �÷��̾ �����ϰ� ������ ���� �״� �� ����
    public bool followPlayer;

    // �̵� �ڷ�ƾ�� ����
    Coroutine moveCoroutine;
    
    [SerializeField] BoxCollider boxCollider;
    Rigidbody rigid;
    Animator animator;

    // ���� �÷��̾ �����ϱ� ���� ����
    // PlayerObject�� Transform�� �����ͼ� targetTransform�� ����
    Transform targetTransform = null;
    // ��ȸ�ϴ� ������ ������
    Vector3 endPosition;
    // ��ȸ�� ������ �ٲ� ���� ���� ������ ���ο� ������ ���ؼ� ����
    float currentAngle = 0;

    private void Start()
    {
        // ó������ normalSpeed �� ���� �ӵ��� õõ�� �����̰�
        // NormalRoutine() �޼��带 ����
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        currentSpeed = normalSpeed;
        StartCoroutine(NormalRoutine());
    }

    public IEnumerator NormalRoutine()
    {
        // ���� ��� ��ȸ�ϴ� while��
        while (true)
        {
            // ���ο� �������� �����ϴ� ����
            ChooseNewEndPoint();

            // moveCoroutine�� null���� �ƴ��� �˻��Ͽ� ���� �̵������� Ȯ��
            // null�� �ƴ϶�� ���� �̵����̶�� �ǹ��̴�.
            // �׷��� ���ο� �������� �̵��ϱ� ���� �������� �̵� �ڷ�ƾ�� ����
            if (moveCoroutine != null) 
            {
                StopCoroutine(moveCoroutine);
            }
            // moveCoroutine�� Move()�ڷ�ƾ�� �����ϰ�, ������ �ڷ�ƾ�� ������ ����
            moveCoroutine = StartCoroutine(Move(rigid,currentSpeed));

            // directionChangeInterval�� ������ ����ŭ �ڷ�ƾ�� ������ �纸�ѵ� �ٽ� ����
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void ChooseNewEndPoint()
    {
        // 0������ 360�� ������ ������ ������ ������
        // ���� ������ ���Ѵ�
        currentAngle += Random.Range(0, 360);
        // �־��� ���� ������ ���� ���� �ȿ� �鶧���� �ݺ�
        // currentAngle�� 360�� �̹Ƿ� 0���� �۰ų� 360���� Ŭ�� ����
        // 0~360�� ������ currentAngle���� ����
        currentAngle = Mathf.Repeat(currentAngle, 360);
        // Vector3FromAnle �޼��带 ȣ���� ������� endPosition�� ���ϰ� ����
        endPosition += Vector3FromAnle(currentAngle);
    }

    Vector3 Vector3FromAnle(float inputAngleDegrees)
    {
        // Vector3 ������ ��ȯ
        // �Է¹��� inputAngleDegrees���� Mathf.Deg2Rad ����� ���Ͽ� ȣ���� ��ȯ
        // ��ȯ�� ȣ���� ����Ͽ� ���� �������� ����� ���� Vector�� �����
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians),0);
    }

    public IEnumerator Move(Rigidbody rigidToMove, float speed)
    {
        // Move() �ڷ�ƾ�� ������ �ӷ����� RigidBody�� ������ġ���� endPosition ������ ��ġ�� �ű�� ����
        // transform.position - endPosition�� ����� Vector3 ��
        // sqrMagnitude �Ӽ��� ����ؼ� ���� ���� ��ġ�� ������ ������ �뷫���� �Ÿ��� ���ϰ� 
        // remainingDistance �� ����
        // sqrMagnitude �Ӽ��� ������ ũ�⸦ ������ ����� �� �ִ�
        float remainingDistance = 10*(transform.position - endPosition).sqrMagnitude;

        // ���� ��ġ�� endPosition ���̿� ���� �Ÿ��� 0 ���� ū�� Ȯ��
        // �´ٸ� ����
        while (remainingDistance > float.Epsilon) 
        {
            // ���� �÷��̾ ���� ���̸� targetTransform�� null�� �ƴ� �÷��̾��� transform
            // endPosition�� ���� targetTransform�� position���� ���.
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            // 
            if (rigidToMove != null)
            {
                // �ȴ� �ִϸ��̼� ���
                animator.SetBool("isWalking", true);
                // Rigidbody�� �������� ����� �� ���(MoveToWards(������ġ, ������ġ, ������ �ȿ� �̵��� �Ÿ�))
                Vector3 newPosition = Vector3.MoveTowards(rigidToMove.position, endPosition, speed*Time.deltaTime);

                // MovePosition() �� �Ἥ ������ ����� rigidbody�� newPosition������ �̵�
                rigid.MovePosition(newPosition);
                // sqrMagnitude �Ӽ��� ����Ͽ� ���� �Ÿ��� �����ϰ� remainingDistance�� ����
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            // ���� ���� ������ ������Ʈ���� ������ �纸�Ѵ�
            yield return new WaitForFixedUpdate();
        }
        // endPoint�� �����ϸ� ���ο� ������ ������ ��ٸ��� �ȴ� �ִϸ��̼ǿ��� ��� �ִϸ��̼����� ����
        animator.SetBool("isWalking", false);

        // �̻��� ���� �̸����� ���ƴٴϴ� ��ȸ �˰���
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� "Player" ����, followPlayer �Ӽ��� true���� Ȯ��
        // �� ������ ���̸� if�� ���� ������ ����
        if (other.gameObject.CompareTag("Player") && followPlayer)
        {
            // �浹�� ������Ʈ�� Player�̱� ������ ����ӵ��� chaserSpeed�� ����
            currentSpeed = chaserSpeed;
            // �浹�� ������Ʈ�� Player�� transform�� ����
            targetTransform = other.gameObject.transform;
            // moveCoroutine�� null�� �ƴ϶�� �Ҹ��� ���� �����̴� ���̶�� �ǹ�
            // �ٽ� �����̱� ���� ����� �ϱ� ������ StopCoroutine�� �Ἥ �����
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // �浹�� ������Ʈ�� Player�� transform�� endPosition�� �����߱� ������
            // Move()�� ȣ���Ͽ� ���� �÷��̾� ������ �����̰� ��
            moveCoroutine = StartCoroutine(Move(rigid,currentSpeed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ �浹���� ����� �ִϸ��̼��� ���� �ٲٰ�, ����ӵ��� ��ȸ�ӵ��� ����
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = normalSpeed;

            // ���� ������ ����� �ϱ� ������ MoveCoroutine�� ����
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // targetTransform�� null�� �����Ͽ� �÷��̾ �������� �ʰ� �Ѵ�
            targetTransform = null;
        }
    }

    // 1. �÷��̾ ���� �þ߿� ���Դ��� �����ϴ� boxCollider�� �ܰ����� �����ִ� �����
    // 2. ���� ��ġ�� ���� �������� �մ� ���� �����ִ� �����
    // 2������ ����� �ʿ�
    // OnDrawGizmos() �޼���� 1�� ����� �����ִ� ���
    private void OnDrawGizmos()
    {
        // boxCollider�� ������ null�� �ƴ��� Ȯ��
        // null�� �ƴϸ� DrawWireCube()�� ȣ���ϰ�, Cube()�� �׸��� �ʿ��� ��ġ�� ũ�⸦ ����
        if (boxCollider != null)
        {
            // �ʿ��� �Ű������� (transform.position�� ������Ʈ ��ġ, boxCollider.size �ڽ��ݶ��̴��� ũ��)
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
    }

    // 2�� ������ Update() ���� ����
    // Debug.DrawLine �� ����� Ȱ���ؾ� ���δ�
    // �Ű������� Debug.DrawLine(������ġ, ������ġ, ���� ����)�� �޴´�
    private void Update()
    {
        Debug.DrawLine(rigid.position, endPosition, Color.red);
    }
}
