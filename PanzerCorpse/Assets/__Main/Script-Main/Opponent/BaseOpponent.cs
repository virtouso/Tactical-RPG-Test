using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BaseOpponent : MonoBehaviour
{
    [Inject] protected IMatchGeneralSettings   GeneralSettings;
    [Inject] protected IUtilityMatchGeneral UtilityMatchGeneral;
    [Inject] protected IUtilityMatchQueries UtilityMatchQueries;
    
    public abstract ActionQuery ApplyAction(MatchModel matchModel);
}
