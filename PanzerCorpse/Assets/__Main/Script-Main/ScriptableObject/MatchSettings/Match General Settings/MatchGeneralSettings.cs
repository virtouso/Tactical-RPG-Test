using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Match GeneralSettings", menuName = "Config/Match General Settings")]
public class MatchGeneralSettings : ScriptableObject, IMatchGeneralSettings
{
    [SerializeField] private AiTypes _matchSelectedAiType;
    public AiTypes MatchSelectedAiType => _matchSelectedAiType;

    [SerializeField] private float _unitsMoveSpeed;
    public float UnitsMoveSpeed { get; }

    [SerializeField] private Material _hexNormalMaterial;
    public Material NormalMaterial => _hexNormalMaterial;
    [SerializeField] private Material _hexAttackMaterial;
    public Material AttackMaterial => _hexAttackMaterial;
    [SerializeField] private Material _hexMoveMaterial;
    public Material MoveMaterial => _hexMoveMaterial;
}