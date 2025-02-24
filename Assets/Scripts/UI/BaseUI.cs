using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected UIManager uIManager;

    public virtual void Init(UIManager uIManager)
    {
        this.uIManager = uIManager;
    }
}
