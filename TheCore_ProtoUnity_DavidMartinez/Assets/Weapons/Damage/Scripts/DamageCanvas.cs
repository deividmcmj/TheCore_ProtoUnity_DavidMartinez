using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    [SerializeField] GameObject damageableEntity;
    [SerializeField] Image lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        DamageableWithLife damageableWithLife = damageableEntity.GetComponentInChildren<DamageableWithLife>();
        damageableWithLife.onChangeLifeNormalized.AddListener(OnChangeLifeNormalized);
    }

    public void OnChangeLifeNormalized(float newLifeNormalized)
    {
        lifeBar.DOFillAmount(newLifeNormalized, 1.0f);
    }
}
