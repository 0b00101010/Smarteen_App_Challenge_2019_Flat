using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    [SerializeField]
    private int m_side_color;
    public int SideColor { get => m_side_color; }
}
