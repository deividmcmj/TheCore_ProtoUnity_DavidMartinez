using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwordCanvas : MonoBehaviour
{
    [SerializeField] GameObject swordEntity;
    [SerializeField] TMP_Text usesText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController playerController = swordEntity.GetComponentInChildren<PlayerController>();
        playerController.onUseSword.AddListener(OnChangeUsesText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeUsesText(int newUses, int maxUses)
    {
        usesText.text = newUses + "/" + maxUses;
    }
}
