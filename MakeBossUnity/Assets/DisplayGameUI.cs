using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameUI : MonoBehaviour
{
    private Mushroom mushroom; // 개별 객체를 가져온다. 데미지를 입으면 함수 연결

    [SerializeField] Image BossHealthBar; // 어떤 체력바를 선택할지 고르시오
    [SerializeField] Image HUDHealthBar;
    [SerializeField] TextMeshProUGUI RagedText;

    private void Awake()
    {
        mushroom = GetComponent<Mushroom>();  
    }

    private void Start()
    {
        RagedText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        mushroom.OnHealthbarUpdate += OnUpdateHealthBar; // int, int 함수로 저장할 수 있는 타입을 선언 -> Action < > 두 타입 선언  OnHealthbarUpdate
        mushroom.OnPatternStart += OnRanged;
    }
    private void OnDisable()
    {
        mushroom.OnHealthbarUpdate -= OnUpdateHealthBar;
        mushroom.OnPatternStart -= OnRanged;
    }

    private void OnRanged(bool enable)
    {
        if(RagedText.gameObject.activeSelf) { return; }
        RagedText.gameObject.SetActive(enable);
    }

    private void OnUpdateHealthBar(int current, int max) // 현재 체력 / 최대 체력 -> 0 ~ 1 소숫점 => fillamount
    {
        if (BossHealthBar != null)
        { BossHealthBar.fillAmount = (float)current / max; }

        if (HUDHealthBar != null)
        { HUDHealthBar.fillAmount = (float)current / max; }
    }

}
