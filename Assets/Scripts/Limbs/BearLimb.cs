using UnityEngine;
using System.Collections;

public class BearLimb : Limb {

	public override bool Activate() {
		if(!base.Activate()) { return false; }

		Debug.Log("BearLimb.Activate() performed");
		return true;
	}

}
