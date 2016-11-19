using UnityEngine;

public class BearLimb : Limb {

	public BearLimb() {
		base.coolDown = 5;
	}

	public override bool Activate() {
		if(!base.Activate()) { return false; }

		Debug.Log("BearLimb.Activate() performed");
		return true;
	}

}
