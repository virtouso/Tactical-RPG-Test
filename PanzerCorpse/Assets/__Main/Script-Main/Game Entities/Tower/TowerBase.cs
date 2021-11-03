using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    [SerializeField] private GameObject _damageEffect;
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private float _timeKeepActionAlive;
    [SerializeField] private AudioSource _damageSound;
    [SerializeField] private AudioSource _destroySound;
    
    public abstract void Init(Vector3 position, FieldCoordinate coordinate, TowerCurrentStats initStats);

    public Vector3 Position { get; }
    public abstract FieldCoordinate FieldCoordinate { get; }

    public abstract TowerCurrentStats TowerCurrentStats { get; set; }



    protected void OnGetDamage(int health)
    {
        if (health > 0)
            StartCoroutine(ApplyDamage());
        else
            StartCoroutine(ApplyDestroy());
    }

    private IEnumerator ApplyDamage()
    { 
        _damageEffect.SetActive(true);
        _damageSound.Play();
        yield return new WaitForSeconds(_timeKeepActionAlive);
        _damageEffect.SetActive(false);
    }

    private IEnumerator ApplyDestroy()
    {
        _destroyEffect.SetActive(true);
        _destroySound.Play();
        yield return new WaitForSeconds(_timeKeepActionAlive);
        gameObject.SetActive(false);
    }


    
   
}