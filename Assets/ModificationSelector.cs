using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificationSelector : PointerSelector
{

    ModifierSymbol _lastSelected;
    protected override void Update()
    {
        base.Update();

        if (_selection != null)
        {
            _lastSelected = _selection.gameObject.GetComponentInParent<ModifierSymbol>();
            _lastSelected.Highlight();
        }
        else
            _lastSelected?.RestoreColor();
    }
}
