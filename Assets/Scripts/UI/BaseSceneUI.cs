using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneUI : BaseUI
{
    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}
