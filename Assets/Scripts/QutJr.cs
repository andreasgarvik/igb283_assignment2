using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QutJr : MonoBehaviour
{
	public GameObject limb;
	public int limbsCount = 4;
	public Material material;

	private Limb[] limbs = new Limb[4];
	private float maxLowerArmAngle = 1f;
	private float minLowerArmAngle = -1f;
	private float maxUpperArmAngle = 1f;
	private float minUpperArmAngle = 0;
	private float maxBaseAngle = 1f;
	private float minBaseAngle = -0.5f;
	private float LowerArmSpeed = 2f;
	private float UpperArmSpeed = 2f;
	private float BaseSpeed = 1f;
	private float maxX = 15f;
	private float minX = -15f;
	private float maxY = 3f;
	private float minY = 0f;
	private float positionX = 0;
	private float positionY = 0;
	private float translationSpeedX = 4f;
	private float translationSpeedY = 6f;
	private float test = 3f;
	private bool dead = false;

	void Start()
	{
		// Head
		Limb l1 = Instantiate(limb).GetComponent<Limb>();
		l1.jointLocation = new Vector3(0, 0, 1);
		l1.jointOffset = new Vector3(0, 2, 1);
		l1.limbVertexLocations = new Vector3[] {
												new Vector3(-0.1f,0,1),
												new Vector3(-0.1f,1,1),
												new Vector3(0.1f,1,1),
												new Vector3(0.1f,0,1)
				};
		l1.material = material;
		l1.color = new Color32(66, 133, 244, 255);
		limbs[0] = l1;

		//Lower Arm
		Limb l2 = Instantiate(limb).GetComponent<Limb>();
		l2.jointLocation = new Vector3(0, 2, 1);
		l2.jointOffset = new Vector3(0, 2, 1);
		l2.limbVertexLocations = new Vector3[] {
												new Vector3(-0.25f,0,1),
												new Vector3(-0.25f,2,1),
												new Vector3(0.25f,2,1),
												new Vector3(0.25f,0,1)
				};
		l2.material = material;
		l2.color = new Color32(219, 68, 55, 255);
		l2.child = l1;
		limbs[1] = l2;

		// Upper Arm
		Limb l3 = Instantiate(limb).GetComponent<Limb>();
		l3.jointLocation = new Vector3(0, 2, 1);
		l3.jointOffset = new Vector3(0, 2, 1);
		l3.limbVertexLocations = new Vector3[] {
												new Vector3(-0.25f,0,1),
												new Vector3(-0.25f,2,1),
												new Vector3(0.25f,2,1),
												new Vector3(0.25f,0,1)
				};
		l3.material = material;
		l3.color = new Color32(244, 180, 0, 255);
		l3.child = l2;
		limbs[2] = l3;

		// Base
		Limb l4 = Instantiate(limb).GetComponent<Limb>();
		l4.jointLocation = new Vector3(0, 0, 1);
		l4.jointOffset = new Vector3(0, 0, 0);
		l4.limbVertexLocations = new Vector3[] {
												new Vector3(-1,-1,1),
												new Vector3(-1,0,1),
												new Vector3(1,0,1),
												new Vector3(1,-1,1)
				};
		l4.material = material;
		l4.color = new Color32(15, 157, 88, 255);
		l4.child = l3;
		limbs[3] = l4;
	}

	void Update()
	{
		Limb Head = limbs[0];
		Limb LowerArm = limbs[1];
		Limb UpperArm = limbs[2];
		Limb Base = limbs[3];

		if (Input.GetKey(KeyCode.Z))
		{
			dead = true;
		}
		if (dead)
		{

		}
		else
		{

			// Movement
			LowerArm.angle += LowerArmSpeed * Time.deltaTime;

			LowerArm.angle = LowerArm.angle >= maxLowerArmAngle ? maxLowerArmAngle : LowerArm.angle;
			LowerArm.angle = LowerArm.angle <= minLowerArmAngle ? minLowerArmAngle : LowerArm.angle;

			LowerArmSpeed = LowerArm.angle < maxLowerArmAngle && LowerArm.angle > minLowerArmAngle ? LowerArmSpeed : -LowerArmSpeed;

			LowerArm.child.GetComponent<Limb>().RotateAroundPoint(LowerArm.jointLocation, LowerArm.angle, LowerArm.lastAngle);
			LowerArm.lastAngle = LowerArm.angle;

			UpperArm.angle += UpperArmSpeed * Time.deltaTime;

			UpperArm.angle = UpperArm.angle >= maxUpperArmAngle ? maxUpperArmAngle : UpperArm.angle;
			UpperArm.angle = UpperArm.angle <= minUpperArmAngle ? minUpperArmAngle : UpperArm.angle;

			UpperArmSpeed = UpperArm.angle < maxUpperArmAngle && UpperArm.angle > minUpperArmAngle ? UpperArmSpeed : -UpperArmSpeed;

			UpperArm.child.GetComponent<Limb>().RotateAroundPoint(UpperArm.jointLocation, UpperArm.angle, UpperArm.lastAngle);
			UpperArm.lastAngle = UpperArm.angle;

			Base.angle += BaseSpeed * Time.deltaTime;

			Base.angle = Base.angle >= maxBaseAngle ? maxBaseAngle : Base.angle;
			Base.angle = Base.angle <= minBaseAngle ? minBaseAngle : Base.angle;

			BaseSpeed = Base.angle < maxBaseAngle && Base.angle > minBaseAngle ? BaseSpeed : -BaseSpeed;

			Base.child.GetComponent<Limb>().RotateAroundPoint(Base.jointLocation, Base.angle, Base.lastAngle);
			Base.lastAngle = Base.angle;

			// Translation
			positionX = positionX >= maxX ? maxX : positionX;
			positionX = positionX <= minX ? minX : positionX;

			positionY = positionY >= maxY ? maxY : positionY;
			positionY = positionY <= minY ? minY : positionY;

			translationSpeedX = positionX < maxX && positionX > minX ? translationSpeedX : -translationSpeedX;

			translationSpeedY = positionY < maxY && positionY > minY ? translationSpeedY : -translationSpeedY;

			if (Input.GetKey(KeyCode.D))
			{
				if (translationSpeedX < 0)
				{
					translationSpeedX = -translationSpeedX;
				}
			}
			else if (Input.GetKey(KeyCode.A))
			{
				if (translationSpeedX > 0)
				{
					translationSpeedX = -translationSpeedX;
				}
			}

			var newPositionX = translationSpeedX * Time.deltaTime;
			var newPositionY = Mathf.Sin(Time.time * translationSpeedY) * Time.deltaTime * 8f;

			positionX += newPositionX;
			positionY += newPositionY;

			Base.MoveByOffset(new Vector3(newPositionX, newPositionY, 0));
		}
	}


}
