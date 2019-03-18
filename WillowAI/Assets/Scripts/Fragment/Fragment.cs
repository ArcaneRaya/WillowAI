using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : Model<FragmentView, Fragment>, ISpottable {

    public static Fragment Create(FragmentBlueprint fragmentBlueprint, Vector3 position) {
        return new Fragment(fragmentBlueprint, position);
    }

    public Vector3 Position { get; private set; }

    public Fragment(Blueprint<FragmentView, Fragment> blueprint, Vector3 position) : base(blueprint) {
        this.Position = position;
    }
}
