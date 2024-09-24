using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using SurviveCoding.BuffSystem;

public class BuffInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buffNameText;
    [SerializeField]
    private TextMeshProUGUI buffDescriptionText;
    [SerializeField]
    private GameObject buffIcon;
    [SerializeField]
    private GameObject debuffIcon;
    [SerializeField]
    private Image buffIconImage;

    /*public void SetBuffInfo(string buffName, string buffDescription, bool isBuff)
    {
        buffNameText.text = buffName;
        buffDescriptionText.text = buffDescription;
        buffIcon.SetActive(isBuff);
        debuffIcon.SetActive(!isBuff);
        //buffIconImage.sprite = ;
    }*/

    public void SetBuffInfo(Buff buff)
    {
        buffNameText.text = buff.Data.BuffName;
        buffDescriptionText.text = buff.Data.Description;
        buffIconImage.sprite = buff.Data.Icon;
    }
}
