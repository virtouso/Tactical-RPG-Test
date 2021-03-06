using System.Collections;
using System.Collections.Generic;
using Panzers.DataModel;
using UnityEngine;
using Zenject;

namespace Panzers.Operators
{
    public class HexMapGenerator : MonoBehaviour, IHexMapGenerator
    {
        private HexPanelBase[,] _hexGrid;
        public HexPanelBase[,] HexGrid => _hexGrid;

        [Inject] private HexPanelBase _hexPrefab;
        [SerializeField] private float _intervalTime;
        [SerializeField] private FieldCoordinate _fieldSize;

        [Tooltip("dont touch it. these are from blender")] [SerializeField]
        private Vector2 _hexSize;

        [SerializeField] private float _hexYOffset;

        [SerializeField] private float _gap;


        private Vector3 _startPos;

        public IEnumerator GenerateHexGrid()
        {
            AddPanelsGap();
            CalculateStartPosition();
            yield return StartCoroutine(CreateGrid());
        }


        #region private functions

        private void AddPanelsGap()
        {
            _hexSize.x += _hexSize.x * _gap;
            _hexSize.y += _hexSize.y * _gap;
        }

        private void CalculateStartPosition()
        {
            float offset = 0;
            if (_fieldSize.Y / 2 % 2 != 0)
                offset = _hexSize.x / 2;

            float x = -_hexSize.x * (_fieldSize.X / 2) - offset;
            float z = _hexSize.y * _hexYOffset * (_fieldSize.Y / 2);

            _startPos = new Vector3(x, 0, z);
        }


        private IEnumerator CreateGrid()
        {
            _hexGrid = new HexPanel[_fieldSize.X, _fieldSize.Y];
            for (int y = 0; y < _fieldSize.Y; y++)
            {
                for (int x = 0; x < _fieldSize.X; x++)
                {
                    yield return new WaitForSeconds(_intervalTime);
                    HexPanelBase hexPanel = GameObject.Instantiate(_hexPrefab);
                    FieldCoordinate gridPos = new FieldCoordinate(x, y);
                    hexPanel.SetPosition(CalculateWorldPosition(gridPos));
                    hexPanel.FieldCoordinate = gridPos;
                    hexPanel.name = "Hexagon" + x + "|" + y;
                    _hexGrid[x, y] = hexPanel;
                }
            }
        }

        #endregion

        #region Utility

        private Vector3 CalculateWorldPosition(FieldCoordinate panelPosition)
        {
            float offset = 0;
            if (panelPosition.Y % 2 != 0)
                offset = _hexSize.x / 2;

            float x = _startPos.x + panelPosition.X * _hexSize.x + offset;
            float z = _startPos.z - panelPosition.Y * _hexSize.y * _hexYOffset;

            return new Vector3(x, 0, z);
        }

        #endregion
    }
}