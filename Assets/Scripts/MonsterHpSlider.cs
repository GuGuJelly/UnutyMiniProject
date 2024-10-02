using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSlider : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Slider monsterHP_Slider;
    [SerializeField] MonsterStatus monsterStatus;
    [SerializeField] Transform playerPosition;

    [SerializeField] float maxDistance;

    private void Awake()
    {
        canvas.enabled = false;
        monsterHP_Slider.value = monsterStatus.monsterHP;
       
    }

    private void Update()
    {
        Physics.Raycast(playerPosition.position, playerPosition.forward, out RaycastHit HitInfoHP, maxDistance);
        Debug.DrawRay(playerPosition.position, playerPosition.forward * maxDistance, Color.red);
        if (HitInfoHP.collider.gameObject.CompareTag("Monster"))
        {
            canvas.enabled = true;
            monsterHP_Slider.value = monsterStatus.monsterCurHP;
        }
        else
            canvas.enabled = false;
    }

}
