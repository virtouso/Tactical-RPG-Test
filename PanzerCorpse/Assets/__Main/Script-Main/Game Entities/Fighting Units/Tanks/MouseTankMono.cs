using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTankMono : FightingUnitMonoBase
{
    protected override IEnumerator PlayAttack(Vector3 goal)
    {
        ShootEffect.SetActive(true);
        Vector3 direction = goal - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        ShootSound.Play();
        yield return new WaitForSeconds(TimeKeepActionAlive);
        ShootEffect.SetActive(false);
    }

    protected override IEnumerator PlayGetDamage()
    {
        TakeDamageEffect.SetActive(true);
        DamageSound.Play();
        yield return new WaitForSeconds(TimeKeepActionAlive);
        ShootEffect.SetActive(false);
    }

    protected override IEnumerator PlayDeath()
    {
        DeathEffect.SetActive(true);
        DeathSound.Play();
        yield return new WaitForSeconds(TimeKeepActionAlive);
        DeathEffect.SetActive(false);
        gameObject.SetActive(false);
    }

    protected override IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed)
    {
        MoveSound.Play();
        float timer = 0;
        Vector3 direction = (endPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction,Vector3.up);
        while (timer <= 1)
        {
            yield return new WaitForEndOfFrame();
            transform.position = GeneralMatchUtility.CalculateLine(startPosition,endPosition,timer);
            timer += GeneralSettings.UnitsMoveSpeed;
        }

        MoveSound.Stop();
    }
}