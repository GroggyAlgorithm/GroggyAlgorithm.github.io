using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Static class for user extensions in the unity game engine
public static class UnityExtensions {
    

    //Checking if a layer is in a layer mask
    public static bool ContainsLayer(this LayerMask layerMask, int layer) {
        return layerMask == (layerMask | (1 << layer));
	}
        

    //Checking if a layer is not in a layer mask
    public static bool ExcludesLayer(this LayerMask layerMask, int layer) {
        return layerMask == (layerMask & (1 << layer));
	}





}
