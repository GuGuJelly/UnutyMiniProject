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
    // 적이 플레이어를 추적하는 속도
    public float chaserSpeed;
    // 평상시의 적의 속도
    public float normalSpeed;
    // 위 두개의 속도 중 하나를 유지하고 있는 현재속도
    public float currentSpeed;

    // 베회할 방향의 전환 빈도 설정
    public float directionChangeInterval;
    // 플레이어를 추적하고 말것을 껐다 켰다 할 변수
    public bool followPlayer;

    // 이동 코루틴의 참조
    Coroutine moveCoroutine;
    
    [SerializeField] BoxCollider boxCollider;
    Rigidbody rigid;
    Animator animator;

    // 적이 플레이어를 추적하기 위한 변수
    // PlayerObject의 Transform을 가져와서 targetTransform에 대입
    Transform targetTransform = null;
    // 배회하는 몬스터의 목적지
    Vector3 endPosition;
    // 배회할 방향을 바꿀 때는 기존 각도에 새로운 각도를 더해서 구현
    float currentAngle = 0;

    private void Start()
    {
        // 처음에는 normalSpeed 로 느린 속도로 천천히 움직이고
        // NormalRoutine() 메서드를 시작
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        currentSpeed = normalSpeed;
        StartCoroutine(NormalRoutine());
    }

    public IEnumerator NormalRoutine()
    {
        // 적이 계속 배회하는 while문
        while (true)
        {
            // 새로운 목적지를 선택하는 구문
            ChooseNewEndPoint();

            // moveCoroutine이 null인지 아닌지 검사하여 적이 이동중인지 확인
            // null이 아니라면 적이 이동중이라는 의미이다.
            // 그래서 새로운 방향으로 이동하기 전에 실행중인 이동 코루틴을 중지
            if (moveCoroutine != null) 
            {
                StopCoroutine(moveCoroutine);
            }
            // moveCoroutine에 Move()코루틴을 시작하고, 시작한 코루틴의 참조를 저장
            moveCoroutine = StartCoroutine(Move(rigid,currentSpeed));

            // directionChangeInterval에 설정한 값만큼 코루틴의 실행을 양보한뒤 다시 루프
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void ChooseNewEndPoint()
    {
        // 0도에서 360도 사이의 랜덤한 각도를 가지고
        // 현재 각도에 더한다
        currentAngle += Random.Range(0, 360);
        // 주어진 값이 지정한 값의 범위 안에 들때까지 반복
        // currentAngle은 360도 이므로 0보다 작거나 360보다 클수 없고
        // 0~360도 각도를 currentAngle각에 대입
        currentAngle = Mathf.Repeat(currentAngle, 360);
        // Vector3FromAnle 메서드를 호출한 결과값을 endPosition에 더하고 대입
        endPosition += Vector3FromAnle(currentAngle);
    }

    Vector3 Vector3FromAnle(float inputAngleDegrees)
    {
        // Vector3 형식을 반환
        // 입력받은 inputAngleDegrees값에 Mathf.Deg2Rad 상수를 곱하여 호도로 변환
        // 변환한 호도를 사용하여 적의 방향으로 사용할 방향 Vector를 만든다
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians),0);
    }

    public IEnumerator Move(Rigidbody rigidToMove, float speed)
    {
        // Move() 코루틴은 정해진 속력으로 RigidBody를 현재위치에서 endPosition 변수의 위치로 옮기는 역할
        // transform.position - endPosition의 결과는 Vector3 값
        // sqrMagnitude 속성을 사용해서 적의 현재 위치와 목적지 사이의 대략적인 거리를 구하고 
        // remainingDistance 에 대입
        // sqrMagnitude 속성은 벡터의 크기를 빠르게 계산할 수 있다
        float remainingDistance = 10*(transform.position - endPosition).sqrMagnitude;

        // 현재 위치와 endPosition 사이에 남은 거리가 0 보다 큰지 확인
        // 맞다면 루프
        while (remainingDistance > float.Epsilon) 
        {
            // 적이 플레이어를 추적 중이면 targetTransform은 null이 아닌 플레이어의 transform
            // endPosition의 값에 targetTransform의 position으로 덮어씀.
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            // 
            if (rigidToMove != null)
            {
                // 걷는 애니메이션 재생
                animator.SetBool("isWalking", true);
                // Rigidbody의 움직임을 계산할 때 사용(MoveToWards(현재위치, 최종위치, 프레임 안에 이동할 거리))
                Vector3 newPosition = Vector3.MoveTowards(rigidToMove.position, endPosition, speed*Time.deltaTime);

                // MovePosition() 를 써서 위에서 계산한 rigidbody의 newPosition값으로 이동
                rigid.MovePosition(newPosition);
                // sqrMagnitude 속성을 사용하여 남은 거리를 수정하고 remainingDistance에 대입
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            // 다음 고정 프레임 업데이트까지 실행을 양보한다
            yield return new WaitForFixedUpdate();
        }
        // endPoint에 도착하면 새로운 방향의 선택을 기다리며 걷는 애니메이션에서 대기 애니메이션으로 변경
        animator.SetBool("isWalking", false);

        // 이상이 적이 이리저리 돌아다니는 배회 알고리즘
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player" 인지, followPlayer 속성이 true인지 확인
        // 두 조건이 참이면 if문 안의 내용이 실행
        if (other.gameObject.CompareTag("Player") && followPlayer)
        {
            // 충돌한 오브젝트가 Player이기 때문에 현재속도를 chaserSpeed로 변경
            currentSpeed = chaserSpeed;
            // 충돌한 오브젝트인 Player의 transform을 대입
            targetTransform = other.gameObject.transform;
            // moveCoroutine이 null이 아니라는 소리는 적이 움직이는 중이라는 의미
            // 다시 움직이기 전에 멈춰야 하기 때문에 StopCoroutine을 써서 멈춘다
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // 충돌한 오브젝트인 Player의 transform을 endPosition에 설정했기 때문에
            // Move()를 호출하여 적을 플레이어 쪽으로 움직이게 함
            moveCoroutine = StartCoroutine(Move(rigid,currentSpeed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 충돌에서 벗어나면 애니메이션을 대기로 바꾸고, 현재속도를 배회속도로 변경
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = normalSpeed;

            // 적의 추적을 멈춰야 하기 때문에 MoveCoroutine을 중지
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // targetTransform에 null을 대입하여 플레이어를 추적하지 않게 한다
            targetTransform = null;
        }
    }

    // 1. 플레이어가 적의 시야에 들어왔는지 감지하는 boxCollider의 외곽선을 보여주는 기즈모
    // 2. 적의 위치와 적의 목적지를 잇는 선을 보여주는 기즈모
    // 2가지의 기즈모가 필요
    // OnDrawGizmos() 메서드는 1번 기즈모를 보여주는 기능
    private void OnDrawGizmos()
    {
        // boxCollider의 참조가 null이 아닌지 확인
        // null이 아니면 DrawWireCube()를 호출하고, Cube()를 그릴때 필요한 위치와 크기를 전달
        if (boxCollider != null)
        {
            // 필요한 매개변수는 (transform.position인 오브젝트 위치, boxCollider.size 박스콜라이더의 크기)
            Gizmos.DrawWireCube(transform.position, boxCollider.size);
        }
    }

    // 2번 기즈모는 Update() 에서 설정
    // Debug.DrawLine 은 기즈모를 활성해야 보인다
    // 매개변수는 Debug.DrawLine(현재위치, 최종위치, 선의 색깔)을 받는다
    private void Update()
    {
        Debug.DrawLine(rigid.position, endPosition, Color.red);
    }
}
