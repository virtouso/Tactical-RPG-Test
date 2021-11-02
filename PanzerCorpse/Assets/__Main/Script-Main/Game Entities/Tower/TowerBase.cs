using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] private GameObject _damageEffect;
    [SerializeField] private GameObject _destroyEffect;
    
    public abstract void Init(Vector3 position, FieldCoordinate coordinate, TowerCurrentStats initStats);

    public Vector3 Position { get; }
    public FieldCoordinate FieldCoordinate { get; }
    public TowerCurrentStats TowerCurrentStats { get; set; }



    private void OnGetDamage(int health)
    {
        if (health > 0)
            StartCoroutine(ApplyDamage());
        else
            StartCoroutine(ApplyDestroy());
    }

    private IEnumerator ApplyDamage()
    {
        _damageEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        _damageEffect.SetActive(false);
    }

    private IEnumerator ApplyDestroy()
    {
        _destroyEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private void Start()
    {
        TowerCurrentStats.Health.Action += OnGetDamage;
    }
    
}