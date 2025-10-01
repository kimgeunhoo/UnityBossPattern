using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] public string parameterName; // Master, BGM, SFX
    [SerializeField] float multiplier = 25f;      // ���� �����̴� 1 �ٲ� �� �����ϴ� ��ġ (-80db ~ 20db) <- ("0 ~ 1")
    [SerializeField] AudioMixer audioMixer;       // Master, BGM, SFX

    public void SetSlider(float value) // 0 ~ 1 �Ҽ���
    {
        float newValue = Mathf.Log10(Mathf.Max(value, 0.00001f)) * multiplier; // Log 10 0 = ���� ���Ѵ� 0 -> �ִ�ġ
        audioMixer.SetFloat(parameterName, newValue); // -80, 20 0.5db
    }

}
