using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Entities
{
    public class MouseTankMono : FightingUnitMonoBase
    {
        protected override IEnumerator PlayAttack(Vector3 goal)
        {
            ShootEffect.Play();
            Vector3 direction = goal - transform.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            ShootSound.Play();
            yield return new WaitForSeconds(TimeKeepActionAlive);
            ShootEffect.Stop();
        }

        protected override IEnumerator PlayGetDamage()
        {
            TakeDamageEffect.Play();
            DamageSound.Play();
            yield return new WaitForSeconds(TimeKeepActionAlive);
            ShootEffect.Stop();
        }

        protected override IEnumerator PlayDeath()
        {
            DeathEffect.Play();
            DeathSound.Play();
            yield return new WaitForSeconds(TimeKeepActionAlive);
            DeathEffect.Stop();
            gameObject.SetActive(false);
        }

        protected override IEnumerator Move(Vector3 startPosition, Vector3 endPosition, float speed)
        {
            MoveSound.Play();
            float timer = 0;
            Vector3 direction = (endPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            while (timer <= 1)
            {
                yield return new WaitForEndOfFrame();
                transform.position = GeneralMatchUtility.CalculateLine(startPosition, endPosition, timer);
                timer += GeneralSettings.UnitsMoveSpeed;
            }

            MoveSound.Stop();
        }
    }
}