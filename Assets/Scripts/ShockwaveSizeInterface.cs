using UnityEngine;
using System.Collections;

//Last Edited during Assessment3


public class ShockwaveSizeInterface : MonoBehaviour 
{

	ParticleSystem particles;
	//Conversion from unity unit radius to startSize for this effect.
	void Start() {
		particles = gameObject.GetComponent<ParticleSystem> ();
	}

	float sizeMultiplier = 3.3333f;

	void SetSize(float radius)
	{
		particles.startSize = 1+radius*sizeMultiplier;
	}
}
