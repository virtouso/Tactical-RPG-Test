using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public abstract class FightingUnitMonoBase : MonoBehaviour
{
    public FightingUnitPerLevelStats InitialStats;
    public FightingUnitCurrentStats CurrentState;
    
    [SerializeField] protected float TimeKeepActionAlive;
    [SerializeField] private MeshRenderer _body;
    [SerializeField] protected GameObject DeathEffect;
    [SerializeField] protected GameObject TakeDamageEffect;
    [SerializeField] protected GameObject ShootEffect;
    [SerializeField] protected AudioSource MoveSound;
    [SerializeField] protected AudioSource DamageSound;
    [SerializeField] protected AudioSource DeathSound;
    [SerializeField] protected AudioSource ShootSound;
    [SerializeField] private Animator _animator;


    [Inject] protected IMatchGeneralSettings GeneralSettings;
    [Inject] private IUtilityMatchQueries _queryUtility;
    [Inject] protected IUtilityMatchGeneral GeneralMatchUtility;
    public Model<FieldCoordinate> FieldCoordinate;


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


    protected abstract IEnumerator PlayAttack(Vector3 goal);
    protected abstract IEnumerator PlayGetDamage();
    protected abstract IEnumerator PlayDeath();

    protected abstract IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed);

    private void OnCoordinateChange(FieldCoordinate coord)
    {
        Vector3 destination = _queryUtility.GetHexPanelPosition(coord);
        StartCoroutine(Move(transform.position, destination, GeneralSettings.UnitsMoveSpeed));
    }

    public void OnAttack(Vector3 goal)
    {
        StartCoroutine(PlayAttack(goal));
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