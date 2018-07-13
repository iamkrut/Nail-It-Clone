using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	public static AudioManagerScript instance;

	public GameObject hammerWoodAudioSourceHolder;
	public GameObject hammerNailAudioSourceHolder;
	public GameObject starCollectAudioSourceHolder;
	public GameObject bombExplosionAudioSourceHolder;

	private AudioSource hammerWoodAudioSource;
	private AudioSource hammerNailAudioSource;
	private AudioSource starCollectAudioSource;
	private AudioSource bombExplosionAudioSource;

	void Awake(){
		instance = this;
	}

	void Start(){
		hammerWoodAudioSource = hammerWoodAudioSourceHolder.GetComponent<AudioSource>();
		hammerNailAudioSource = hammerNailAudioSourceHolder.GetComponent<AudioSource>();		
		starCollectAudioSource = starCollectAudioSourceHolder.GetComponent<AudioSource>();
		bombExplosionAudioSource = bombExplosionAudioSourceHolder.GetComponent<AudioSource>();
	}

	public void PlayHammerWoodAudio(){
		hammerWoodAudioSource.Play();
	}

	public void PlayHammerNailAudio(){
		hammerNailAudioSource.Play();
	}

	public void PlayStarCollectAudio(){
		starCollectAudioSource.Play();
	}

	public void PlayBombExplosionAudio(){
		bombExplosionAudioSource.Play();
	}

	public void PlayStarRewardAudio(){

	}

	public void PlayStarParticlesCollectAudio(){

	}

	public void StopStarParticlesCollectAudio(){
		
	}
}
