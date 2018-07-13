using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttraction : MonoBehaviour
{
	public GameObject startPanelTarget;
	public GameObject knifePanelTarget;
	public GameObject challengePanelTarget;		
	public float speed;

    private GameObject target;   
    private ParticleSystem system;
	private bool attractParticles;
	private Vector3 targetPosition;

	private bool increaseCoinCount;
   
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
	
	void OnEnable(){
		attractParticles = false;

		if(startPanelTarget.activeInHierarchy)
			target = startPanelTarget;
		else if(knifePanelTarget.activeInHierarchy)
			target = knifePanelTarget;
		else if(challengePanelTarget.activeInHierarchy)
			target = challengePanelTarget; 

		//targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);
		targetPosition = Camera.main.ScreenToWorldPoint(target.transform.position);
		targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, 0);
		
		increaseCoinCount = true;
		StartCoroutine(AttractParticles());
	}

	IEnumerator AttractParticles(){
		yield return new WaitForSeconds(1);
		attractParticles = true;
	}

    void Update()
    {
		if(attractParticles){
			if (system == null) system = GetComponent<ParticleSystem>();
		
			var count = system.GetParticles(particles);
		
			for (int i = 0; i < count; i++)
			{
				var particle = particles[i];
			
				float distance = Vector3.Distance(targetPosition, particle.position);
			
				if (distance > 0.1f)
				{
					particle.position = Vector3.MoveTowards(particle.position, targetPosition, Time.deltaTime * speed);
				
					particles[i] = particle;
				}else{
					particle.remainingLifetime = -1;
					particles[i] = particle;
					if(increaseCoinCount){
						increaseCoinCount = false;
						ScoreControllerScript.instance.IncreaseStarCount = true;
						AudioManagerScript.instance.PlayStarParticlesCollectAudio();
					}
				}
			}
			if(count == 0)
			AudioManagerScript.instance.StopStarParticlesCollectAudio();
		
			system.SetParticles(particles, count);
		}
    }
}