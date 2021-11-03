using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

public abstract class TempWidgetBase : MonoBehaviour
{


    public virtual void Show(float displayTime)
    {
        gameObject.SetActive(true);

        StartCoroutine(Disappear(displayTime));


    }


    private IEnumerator Disappear(float displayTime)
    {
        yield return new WaitForSeconds(displayTime);
        gameObject.SetActive(false);
    }

}
