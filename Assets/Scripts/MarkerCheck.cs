using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;

public class MarkerCheck : MonoBehaviour
{
    [Header("Isi Deskripsi")]
    public string nama;

    [TextArea]
    public string deskripsi;

    [Header("UI Deskripsi")]
    public TextMeshProUGUI txtNama;
    public TextMeshProUGUI txtDeskripsi;

    void Update()
    {
        txtNama.text = nama;
        txtDeskripsi.text = deskripsi;
    }
}
