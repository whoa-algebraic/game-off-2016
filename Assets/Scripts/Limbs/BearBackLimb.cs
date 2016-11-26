using UnityEngine;

public class BearBackLimb : Limb {

	public override bool Activate() {
		if (!base.Activate()) { return false; }

		Debug.Log("BearBackLimb.Activate() performed");
		return true;
	}
}
