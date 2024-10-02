using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSlider : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Slider monsterHP_Slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] MonsterStatus monsterStatus;
    [SerializeField] Transform playerPosition;

    [SerializeField] float maxDistance;

    private void Start()
    {
        canvas.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        monsterHP_Slider.value = monsterStatus.monsterHP;
    }

    private void Update()
    {
        Physics.Raycast(playerPosition.position, playerPosition.forward, out RaycastHit HitInfoHP, maxDistance);
        Debug.DrawRay(playerPosition.position, playerPosition.forward * maxDistance, Color.red);
        if (HitInfoHP.collider.gameObject.CompareTag("Monster"))
        {
            text.gameObject.SetActive(true);
            text.text = HitInfoHP.collider.gameObject.name;
            canvas.gameObject.SetActive(true);
            monsterHP_Slider.value = monsterStatus.monsterCurHP;
        }
        else
            canvas.gameObject.SetActive(false);
    }

}
