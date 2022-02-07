using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using UnityEngine;

namespace Panzers.Configurations
{
    [CreateAssetMenu(fileName = "Unit Intial Placement Config", menuName = "Config/Unit Placement/Unit Placements")]
    public class UnitInitialPlacementConfig : ScriptableObject, IUnitInitialPlacementConfig
    {
        [SerializeField] private List<IndexCoordinatePair> _indexCoordinatePairs;
        private Dictionary<int, FieldCoordinate> _indexCoordinates;

        public Dictionary<int, FieldCoordinate> IndexCoordinates
        {
            get
            {
                if (_indexCoordinates == null)
                {
                    _indexCoordinates = new Dictionary<int, FieldCoordinate>(_indexCoordinatePairs.Count);
                    foreach (var item in _indexCoordinatePairs)
                    {
                        _indexCoordinates.Add(item.Index, item.Coordinate);
                    }
                }

                return _indexCoordinates;
            }

        }

    }


    [System.Serializable]
    public class IndexCoordinatePair
    {
        public int Index;
        public FieldCoordinate Coordinate;
    }


}
