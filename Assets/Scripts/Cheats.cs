﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 *Assessment 4 Addition of Cheats 
 */

public class Cheats : MonoBehaviour {

	public static Cheats instance;

	//booleans for pause menu cheating
	public bool infinitehealth;
	public bool boostspeed;


	//boolean arrays for ingame cheats 
	public bool[] ghealthcode = new bool[4];
	public bool[] gboostcode = new bool[4];
	public bool[] gpluscode = new bool[4];
	public bool[] greducepoints = new bool[4];

	public bool cheatMenuOpen;
	public InputField cheatIn;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}


	void OnLevelWasLoaded() {
        FindCheatIn();
	}

    private void FindCheatIn(){
        cheatIn = GameObject.FindGameObjectWithTag("CheatBox").GetComponent<InputField>();
    }

    // Update is called once per frame
	void Update () {
		if (cheatMenuOpen) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				//entered cheat

			    if (cheatIn == null)
			    {
                    FindCheatIn();
			    }

			    if (TakeString(cheatIn.text))
			    {
                    //Clear box if cheat was accepted.
                    cheatIn.text = "";
                }
			    
			}
		} else {
			

            //for while game is running cheats
			//These are keycode ones, so GHGH, or KLKL, or UIUI, or OPOP
			// Infinite Health
			// Boost Speed
			// Add Points
			// Remove Points

			if (Input.GetKey (KeyCode.G)) {
				ArrayReset (gpluscode);
				ArrayReset (greducepoints);
				ArrayReset (gboostcode);
				ghealthcode [0] = true;
				if (ghealthcode [1]) {
					ghealthcode [2] = true;
				}
			} else if (Input.GetKey (KeyCode.H)) {
				ArrayReset (gpluscode);
				ArrayReset (greducepoints);
				ArrayReset (gboostcode);
				if (ghealthcode [0]) {
					ghealthcode [1] = true;
				} 
				if (ghealthcode [2]) {
					if (infinitehealth) {
						TakeString ("infinitehealth off");
					} else {
						TakeString ("infinitehealth on");
					}
					ArrayReset (ghealthcode);
				}
			} else if (Input.GetKey (KeyCode.K)) {
				ArrayReset (greducepoints);
				ArrayReset (gpluscode);
				ArrayReset (ghealthcode);
				gboostcode [0] = true;
				if (gboostcode [1]) {
					gboostcode [2] = true;
				}
			} else if (Input.GetKey (KeyCode.L)) {
				ArrayReset (greducepoints);
				ArrayReset (gpluscode);
				ArrayReset (ghealthcode);
				if (gboostcode [0]) {
					gboostcode [1] = true;
				}
				if (gboostcode [2]) {
					if (boostspeed) {
						TakeString ("boostspeed off");
					} else {
						TakeString ("boostspeed on");
					}
					ArrayReset (gboostcode);
				}
			} else if (Input.GetKey (KeyCode.U)) {
				ArrayReset (greducepoints);
				ArrayReset (gboostcode);
				ArrayReset (ghealthcode);
				gpluscode [0] = true;
				if (gpluscode [1]) {
					gpluscode [2] = true;
				}
			} else if (Input.GetKey (KeyCode.I)) {
				ArrayReset (greducepoints);
				ArrayReset (ghealthcode);
				ArrayReset (gboostcode);
				ArrayReset (gboostcode);
				if (gpluscode [0]) {
					gpluscode [1] = true;
				}
				if (gpluscode [2]) {
						TakeString ("pluspoints");

					ArrayReset (gpluscode);
				}

			} else if (Input.GetKey (KeyCode.O)) {
				ArrayReset (gpluscode);
				ArrayReset (ghealthcode);
				ArrayReset (gboostcode);
				greducepoints [0] = true;
				if (greducepoints [1]) {
					greducepoints [2] = true;
				}
			} else if (Input.GetKey (KeyCode.P)) {
				ArrayReset (gpluscode);
				ArrayReset (gboostcode);
				ArrayReset (ghealthcode);
				if (greducepoints [0]) {
					greducepoints [1] = true;
				}
				if (greducepoints [2]) {
					TakeString ("reducepoints");
					ArrayReset (greducepoints);

				} else if (Input.anyKey) {
					ArrayReset (ghealthcode);
					ArrayReset (gboostcode);
					ArrayReset (gpluscode);
					ArrayReset (greducepoints);
				}

			}
		}
	}



	void ArrayReset(bool[] inArr) {
		for (int i = 0; i < inArr.Length; i++) {
			inArr [i] = false;
		}
	}



    //Typed into pause menu text box
	//same cheat types as in game, but need to type in full codes
    //returns true if the cheat was valid.
	public bool TakeString(string inString) {

        Debug.Log("Cheat Entered: " + inString );
		string[] parts = inString.Split (' ');
		if (parts.Length > 2 || parts.Length <1) {
			//Wrong amount of paramaters
		    return false;
		} else if (parts.Length == 2) {
			if (parts [0] == "infinitehealth") {
				if (parts [1] == "on") {
					//infinitehealth is on
					infinitehealth = true;
					boostspeed = false;
				    return true;
				} else if (parts [1] == "off") {
					//infinitehealth is off
					infinitehealth = false;
                    return true;

                }
			} else if (parts [0] == "boostspeed") {
				if (parts [1] == "on") {
					//boostspeed is on
					boostspeed = true;
					infinitehealth = false;
				    return true;

				} else if (parts [1] == "off") {
					//boostspeed is off
					boostspeed = false;
				    return true;

				}
			}
		} else if (parts.Length == 1)
		{
		    if (parts[0] == "pluspoints")
		    {
		        //add 100 points
		        boostspeed = false;
		        infinitehealth = false;
		        PlayerProperties.inst.Score = PlayerProperties.inst.Score + 100;
		        return true;

		    }
		    else if (parts[0] == "reducepoints")
		    {
		        //minus 100 points
		        infinitehealth = false;
		        boostspeed = false;
		        PlayerProperties.inst.Score = 0;
		        return true;
		    }
		}

        //Input was not recognised.
	    return false;
	}

}
