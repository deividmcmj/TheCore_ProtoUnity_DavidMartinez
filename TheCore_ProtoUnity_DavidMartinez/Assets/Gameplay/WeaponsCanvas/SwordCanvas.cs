using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwordCanvas : MonoBehaviour
{
    [SerializeField] GameObject swordEntity;
    [SerializeField] Image timeBar;
    [SerializeField] TMP_Text usesText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = swordEntity.GetComponentInChildren<PlayerController>();
        playerController.onUseTimeSwordNormalized.AddListener(OnChangeTimeBarNormalized);
        playerController.onUseSword.AddListener(OnChangeUsesText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeTimeBarNormalized(float newTimeNormalized)
    {
        timeBar.DOFillAmount(newTimeNormalized, 1.0f);
    }

    public void OnChangeUsesText(int newUses, int maxUses)
    {
        usesText.text = newUses + "/" + maxUses;
    }
}
