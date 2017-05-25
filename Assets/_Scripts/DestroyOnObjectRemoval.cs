using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DestroyOnObjectRemoval : MonoBehaviour {

    public List<GameObject> ObjectList;
    public UnityEvent OnDestroyCallback;

	void Update () {

        //cool
        ObjectList.RemoveAll(obj => obj == null);

        if (ObjectList.Count == 0)
        {
            Debug.Log("Removing " + name + " because tracking list is empty.");

            Destroy(this.gameObject);

            if (OnDestroyCallback != null)
                OnDestroyCallback.Invoke();

        }
	
	}
}
