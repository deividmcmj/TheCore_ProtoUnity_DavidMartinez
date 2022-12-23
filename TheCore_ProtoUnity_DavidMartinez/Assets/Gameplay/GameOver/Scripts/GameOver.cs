using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject playerEntity;

    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DamageableWithLife damageableWithLife = playerEntity.GetComponentInChildren<DamageableWithLife>();
        damageableWithLife.onDeath.AddListener(OnDeath);
    }

    public void OnDeath()
    {
        canvasGroup.DOFade(1.0f, 5.0f);
    }
}
