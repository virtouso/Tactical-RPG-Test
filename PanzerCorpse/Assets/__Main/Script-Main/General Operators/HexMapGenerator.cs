using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HexMapGenerator : MonoBehaviour, IHexMapGenerator
{
    [Inject] private HexPanelBase _hexPrefab;

    [SerializeField] private FieldCoordinate _fieldSize;
    [Tooltip("dont touch it. these are from blender")]
    [SerializeField] private Vector2 _hexSize;
    [SerializeField] private float _gap;




    private Vector3 _startPos;

    public IEnumerator GenerateHexGrid()
    {
        yield return new WaitForSeconds(1f);
        AddPanelsGap();
        CalculateStartPosition();
        StartCoroutine(CreateGrid());
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
        float z = _hexSize.y * 0.75f * (_fieldSize.Y / 2);

        _startPos = new Vector3(x, 0, z);
    }



    private IEnumerator CreateGrid()
    {
        for (int y = 0; y < _fieldSize.Y; y++)
        {
            for (int x = 0; x < _fieldSize.X; x++)
            {
                yield return new WaitForSeconds(0.1f);
                HexPanelBase hexPanel = GameObject.Instantiate(_hexPrefab);
                FieldCoordinate gridPos = new FieldCoordinate(x, y);
                hexPanel.SetPosition(CalculateWorldPosition(gridPos));
                //  hexPanel.parent = this.transform;
                hexPanel.name = "Hexagon" + x + "|" + y;
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
        float z = _startPos.z - panelPosition.Y * _hexSize.y * 0.75f;

        return new Vector3(x, 0, z);
    }


    #endregion




    private void Start()
    {
        StartCoroutine(GenerateHexGrid());
    }


}
