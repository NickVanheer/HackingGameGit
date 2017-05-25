using UnityEngine;
using System.Collections;

public class PlayerHealthIndicator : MonoBehaviour {

    // Use this for initialization
    public Color HealthTwoLeft = new Color(0.6f, 0.3f, 0.3f);
    public Color HealthOneLeft = new Color(0.8f, 0.0f, 0.0f);

    public Renderer RenderComponent;
	
	void Update () {

        int h = GetComponent<DestroyOnTriggerEnter>().Health;

        if(h == 2)
            RenderComponent.material.color = HealthTwoLeft;

        if (h == 1)
            RenderComponent.material.color = HealthOneLeft;

    }
}
