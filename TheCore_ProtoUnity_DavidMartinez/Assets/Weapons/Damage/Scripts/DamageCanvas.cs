using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    [SerializeField] GameObject damageableEntity;
    [SerializeField] Image lifeBar;
    [SerializeField] TMP_Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        DamageableWithLife damageableWithLife = damageableEntity.GetComponentInChildren<DamageableWithLife>();
        damageableWithLife.onChangeLifeBarNormalized.AddListener(OnChangeLifeBarNormalized);
        damageableWithLife.onChangeLifeText.AddListener(OnChangeLifeText);
    }

    public void OnChangeLifeBarNormalized(float newLifeNormalized)
    {
        lifeBar.DOFillAmount(newLifeNormalized, 1.0f);
        lifeBar.color = Color.Lerp(Color.red, Color.green, newLifeNormalized);
    }

    public void OnChangeLifeText(float newLife, float maxLife)
    {
        lifeText.text = newLife + "/" + maxLife;
        lifeText.color = Color.Lerp(Color.red, Color.green, newLife / maxLife);
    }
}
