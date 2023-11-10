using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public GameObject ball;
    }

    public bool isFiring = false;
    public int fireRate = 1;
    public float bulletSpeed = 20f;
    public float bulletDrop = 0.0f;
    public ParticleSystem lightI;

    public float damage = 10f;


    public Transform raycastOrigin;
    public Transform raycastDestination;
    public GameObject ballObject;


    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;

    Vector3 GetPosition(Bullet bullet)
    {

        //p + vt + 1/2 g t^2
        Vector3 gravity = Vector3.down * bulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time + 0.5f * gravity * bullet.time * bullet.time;
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.ball = Instantiate(ballObject, position, Quaternion.identity);
        return bullet;
    }
    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateWeapon(float deltaTime)
    {
     
        if (isFiring)
        {
            UpdateFiring(deltaTime);
        }

        accumulatedTime += deltaTime;
        UpdateBullets(deltaTime);

    }
    public void UpdateFiring(float deltaTime)
    {

        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }



    public void UpdateBullets(float deltaTime)
    {

        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });

    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => ((bullet.time >= maxLifeTime)));
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 5.0f);
            bullet.ball.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;
            end = hitInfo.point;


            //Collision Impulse
            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }
            var hitBox = hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRayCastHit(this, ray.direction);
            }
        }


        bullet.ball.transform.position = end;

    }

    public void FireBullet()
    {
        lightI.Emit(1);

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
