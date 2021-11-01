using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class FightingUnitMonoBase : MonoBehaviour
{
    public FightingUnitPerLevelStats InitialStats;
    public FightingUnitCurrentStats CurrentState;
    [SerializeField] private MeshRenderer _body;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _takeDamageEffect;
    [SerializeField] private GameObject _shootEffect;
    [Inject] private IMatchGeneralSettings _generalSettings;
    [Inject] private IUtilityMatchQueries _queryUtility;
    public Model<FieldCoordinate> FieldCoordinate;


    [SerializeField] private Animator _animator;


    public void Init(FightingUnitPerLevelStats config,
        FieldCoordinate coordinate, Vector3 position, Material bodyMaterial, Vector3 lookDirection)
    {
        InitialStats = config;
        CurrentState = new FightingUnitCurrentStats(new Model<int>(config.DamageAmount),
            new Model<int>(config.HealthAmount),
            new Model<int>(config.MovingUnitsInTurn));
        FieldCoordinate = new Model<FieldCoordinate>(coordinate);
        transform.position = position;
        _body.material = bodyMaterial;
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }


    protected abstract IEnumerator PlayAttack();
    protected abstract IEnumerator PlayGetDamage();
    protected abstract IEnumerator PlayDeath();

    protected abstract IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed);

    private void OnCoordinateChange(FieldCoordinate coord)
    {
        Vector3 destination = _queryUtility.GetHexPanelPosition(coord);
        StartCoroutine(Move(transform.position, destination, _generalSettings.UnitsMoveSpeed));
    }

    public void OnAttack()
    {
        StartCoroutine(PlayAttack());
    }

    private void OnGetDamage(int health)
    {
        if (health > 0)
            StartCoroutine(PlayGetDamage());
        else
            StartCoroutine(PlayDeath());
    }

    private void Start()
    {
        FieldCoordinate.Action += OnCoordinateChange;
        CurrentState.HealthAmount.Action += OnGetDamage;
    }
}