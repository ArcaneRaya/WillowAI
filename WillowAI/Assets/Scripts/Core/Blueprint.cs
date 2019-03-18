using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint<U, T> : ScriptableObject where U : View<T> where T : Model<U, T> {
    public U Prefab;
}

