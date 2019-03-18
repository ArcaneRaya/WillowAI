using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentView : View<Fragment> {

    protected override void OnInitialize() {
        base.OnInitialize();
        transform.position = Data.Position;
    }

    protected override void OnTick(float elapsedTime) {
        base.OnTick(elapsedTime);
        transform.Rotate(transform.up, 45 * elapsedTime);
    }
}
