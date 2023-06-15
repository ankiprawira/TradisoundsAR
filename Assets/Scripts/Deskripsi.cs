using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Deskripsi : MonoBehaviour
{
    public GameObject mDeskripsi;
    public GameObject mPanelAngklung;
    public Button infoButton;
    public GameObject mPanelInfo;
    bool eMDeskripsi;
    bool eMPanelAngklung;
    bool eMPanelInfo;

    void Update()
    {
        eMDeskripsi = mDeskripsi.activeInHierarchy;
        eMPanelInfo = mPanelInfo.activeInHierarchy;
        eMPanelAngklung = mPanelAngklung.activeInHierarchy;
        if (eMDeskripsi)
        {
            mPanelAngklung.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
        }
        if (eMPanelInfo)
        {
            mDeskripsi.gameObject.SetActive(false);
            infoButton.gameObject.SetActive(false);
        }
    }
}
