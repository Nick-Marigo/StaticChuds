using UnityEngine;
using System;
using System.Collections.Generic;


public class ProjectileManager : MonoBehaviour
{
    public GameObject[] projectiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.projectileManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateProjectile(int which, string trajectory, Transform where, Vector3 direction, float speed, List<Action<Hittable,Vector3>> onHitCallbacks)
    {
        GameObject new_projectile = Instantiate(projectiles[which], where.position + direction.normalized*1.1f, Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg));
        new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed, where);

		foreach(Action<Hittable, Vector3> onHitCallback in onHitCallbacks) {
			new_projectile.GetComponent<ProjectileController>().OnHit += onHitCallback;
		}
    }

    public void CreateProjectile(int which, string trajectory, Transform where, Vector3 direction, float speed, List<Action<Hittable, Vector3>> onHitCallbacks, float lifetime)
    {
        GameObject new_projectile = Instantiate(projectiles[which], where.position + direction.normalized * 1.1f, Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        new_projectile.GetComponent<ProjectileController>().movement = MakeMovement(trajectory, speed, where);
        new_projectile.GetComponent<ProjectileController>().SetLifetime(lifetime);

		foreach(Action<Hittable, Vector3> onHitCallback in onHitCallbacks) {
			new_projectile.GetComponent<ProjectileController>().OnHit += onHitCallback;
		}
    }

    public ProjectileMovement MakeMovement(string name, float speed, Transform source)
    {
        if (name == "straight")
        {
            return new StraightProjectileMovement(speed);
        }
        if (name == "homing")
        {
            return new HomingProjectileMovement(speed);
        }
        if (name == "spiraling")
        {
            return new SpiralingProjectileMovement(speed);
        }
		if(name == "orbiting") {
			return new OrbitingProjectileMovement(source);
		}
        return null;
    }

}
