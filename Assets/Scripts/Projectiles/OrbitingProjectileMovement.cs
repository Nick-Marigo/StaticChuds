using UnityEngine;

public class OrbitingProjectileMovement : ProjectileMovement {
	
	private Transform _target;
	private float _radius = 3.0f;

	public OrbitingProjectileMovement(Transform target) : base(0.0f) {
		_target = target;
	}

	public override void Movement(Transform transform) {
		if(_target == null) { return; }

		float translation = 5.0f * Time.deltaTime;
		Vector2 delta = transform.position - _target.position;

		translation = delta.magnitude - _radius;

		transform.Translate(new(translation, 0.0f), Space.Self);
		transform.RotateAround(_target.position, new Vector3(0.0f, 0.0f, 1.0f), 50.0f * Time.deltaTime);
	}

}
