using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookAnalyticsManagerScript : MonoBehaviour {

	public static FacebookAnalyticsManagerScript instance;

	void Awake ()
	{
		instance = this;
		DontDestroyOnLoad (instance);
		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init( () => {
				FB.ActivateApp();
			});
		}
	}
}
