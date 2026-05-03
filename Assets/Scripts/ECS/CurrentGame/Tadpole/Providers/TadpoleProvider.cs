using System;
using EPOOutline;
using UnityEngine;

[Serializable]
public struct TadpoleProvider
{
    public GameObject CaviarMetamorphosisStepView;
    public GameObject TadpoleMetamorphosisStepView;
    public Renderer[] TadpoleMeshRenderers;
    public Renderer CaviarMeshRenderer;
    public Collider Collider;
    public Outlinable Outlinable;
    public GameObject Arrow;
}