using System.Collections;
using System.Collections.Generic;
using Panzers.AI;
using UnityEngine;

namespace Panzers.Configurations
{
    namespace Panzers.Configurations
    {
        [CreateAssetMenu(fileName = "Match GeneralSettings", menuName = "Config/Match General Settings")]
        public class MatchGeneralSettings : ScriptableObject, IMatchGeneralSettings
        {
            [SerializeField] private AiTypes _matchSelectedAiType;
            public AiTypes MatchSelectedAiType => _matchSelectedAiType;

            [SerializeField] private float _unitsMoveSpeed;
            public float UnitsMoveSpeed => _unitsMoveSpeed;
            [SerializeField] private Material _hexNormalMaterial;
            public Material NormalMaterial => _hexNormalMaterial;


            [SerializeField] private Material _hexAttackMaterial;
            public Material AttackMaterial => _hexAttackMaterial;


            [SerializeField] private Material _hexMoveMaterial;
            public Material MoveMaterial => _hexMoveMaterial;

            public float LongWaitTime => _longWaitTime;
            [SerializeField] private float _longWaitTime;

            public float AverageWaitTime => _averageWaitTime;
            [SerializeField] private float _averageWaitTime;
            public float ShortWaitTime => _shortWaitTime;
            [SerializeField] private float _shortWaitTime;

            [SerializeField] private int _towersInitialHealth;
            public int TowersInitialHealth => _towersInitialHealth;
        }
    }
}