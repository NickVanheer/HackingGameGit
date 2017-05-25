using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RemoveWhenChildIsGone : MonoBehaviour {

    public Transform ChildToTrack;
    public UnityEvent OnDestroyCallback;

	void Start () {
	
	}
	
	void Update () {

        if (ChildToTrack == null)
        {
            Destroy(this.gameObject);

            if (OnDestroyCallback != null)
                OnDestroyCallback.Invoke();
        }
	
	}
}
