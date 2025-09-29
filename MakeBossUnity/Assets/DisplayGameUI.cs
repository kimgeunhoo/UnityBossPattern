using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameUI : MonoBehaviour
{
    private Mushroom mushroom; // ���� ��ü�� �����´�. �������� ������ �Լ� ����

    [SerializeField] Image BossHealthBar; // � ü�¹ٸ� �������� ���ÿ�
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
        mushroom.OnHealthbarUpdate += OnUpdateHealthBar; // int, int �Լ��� ������ �� �ִ� Ÿ���� ���� -> Action < > �� Ÿ�� ����  OnHealthbarUpdate
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

    private void OnUpdateHealthBar(int current, int max) // ���� ü�� / �ִ� ü�� -> 0 ~ 1 �Ҽ��� => fillamount
    {
        if (BossHealthBar != null)
        { BossHealthBar.fillAmount = (float)current / max; }

        if (HUDHealthBar != null)
        { HUDHealthBar.fillAmount = (float)current / max; }
    }

}
