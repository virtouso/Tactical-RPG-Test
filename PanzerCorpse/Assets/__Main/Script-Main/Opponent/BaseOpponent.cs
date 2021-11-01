using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseOpponent : MonoBehaviour
{
    public abstract ActionQuery ApplyAction(MatchModel matchModel);
}
