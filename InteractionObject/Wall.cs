using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Color Number")]
    [SerializeField]
    private int colorNumber;

    public int ColorNumber { get => colorNumber; }
}
