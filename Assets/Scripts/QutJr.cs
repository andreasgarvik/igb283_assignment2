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
	private float maxBaseAngle = 1.25f;
	private float minBaseAngle = -0.75f;
	private float LowerArmSpeed = 2f;
	private float UpperArmSpeed = 2f;
	private float BaseSpeed = 1f;
	private float maxX = 15f;
	private float minX = -15f;
	private float maxY = 9f;
	private float minY = 0f;
	private float positionX = 0;
	private float positionY = 0;
	private float translationSpeedX = 4f;
	private float translationSpeedY = 6f;
	private float test = 3f;
	private bool dead = false;
	private bool moveDown = true;
	void Start()
	{
		DrawJr();
	}

	void Update()
	{

		Limb Head = limbs[0];
		Limb LowerArm = limbs[1];
		Limb UpperArm = limbs[2];
		Limb Base = limbs[3];

		if (positionX > maxX)
		{
			Base.MoveByOffset(new Vector3(maxX - positionX, 0, 0));
			positionX = maxX;
		}
		if (positionX < minX)
		{
			Base.MoveByOffset(new Vector3(minX - positionX, 0, 0));
			positionX = minX;
		}
		if (positionY >= maxY)
		{
			Base.MoveByOffset(new Vector3(0, maxY - positionY, 0));
			positionY = maxY;
		}
		if (positionY <= minY)
		{
			Base.MoveByOffset(new Vector3(0, minY - positionY, 0));
			positionY = minY;
		}

		if (Input.GetKey(KeyCode.Z))
		{
			dead = true;
		}
		if (dead)
		{
			if (Base.angle > -3 && Base.angle < 3)
			{
				BaseSpeed = 2f;
				Base.angle += BaseSpeed * Time.deltaTime;
				if (moveDown)
				{
					if (positionY == 0)
					{
						moveDown = false;
					}
					else
					{
						if (translationSpeedY > 0)
						{
							translationSpeedY = -translationSpeedY;
						}
						var newPositionY = translationSpeedY * Time.deltaTime;
						positionY += newPositionY;
						positionY = positionY <= minY ? minY : positionY;
						Base.MoveByOffset(new Vector3(0, newPositionY, 0));
					}
				}
				Base.child.GetComponent<Limb>().RotateAroundPoint(Base.jointLocation, Base.angle, Base.lastAngle);
				Base.lastAngle = Base.angle;
			}
			else
			{
				if (!moveDown)
				{
					System.Threading.Thread.Sleep(2000);
					moveDown = true;
				}
				BaseSpeed = 0.5f;
				Base.angle += BaseSpeed * Time.deltaTime;
				Base.child.GetComponent<Limb>().RotateAroundPoint(Base.jointLocation, Base.angle, Base.lastAngle);
				Base.lastAngle = Base.angle;
				if (Base.angle > 6 || Base.angle < -6)
				{
					dead = false;
				}
			}
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

			translationSpeedX = positionX < maxX && positionX > minX ? translationSpeedX : -translationSpeedX;

			translationSpeedY = positionY <= maxY && positionY >= minY ? translationSpeedY : -translationSpeedY;

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

			var newPositionX = 0f;
			var newPositionY = 0f;

			if (Input.GetKey(KeyCode.W))
			{
				newPositionY = Mathf.Sin(Time.time * translationSpeedY) * Time.deltaTime * maxY;
			}
			else
			{
				newPositionX = translationSpeedX * Time.deltaTime;
				newPositionY = Mathf.Sin(Time.time * translationSpeedY) * Time.deltaTime * maxY;
			}
			positionX += newPositionX;
			positionY += newPositionY;

			Base.MoveByOffset(new Vector3(newPositionX, newPositionY, 0));

		}
	}

	void DrawJr()
	{
		// Head
		Limb l1 = Instantiate(limb).GetComponent<Limb>();
		l1.jointLocation = new Vector3(0, 0, 1);
		l1.jointOffset = new Vector3(0, 2, 1);
		l1.limbVertexLocations = new Vector3[] {
												new Vector3(-0.5f,0,1),
												new Vector3(-1.25f,1,1),
												new Vector3(1.25f,1,1),
												new Vector3(0.5f,0,1)
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
		l4.jointOffset = new Vector3(0, 0, 1);
		l4.limbVertexLocations = new Vector3[] {
												new Vector3(-1,-1f,1),
												new Vector3(-1,0,1),
												new Vector3(1,0,1),
												new Vector3(1,-1f,1)
				};
		l4.material = material;
		l4.color = new Color32(15, 157, 88, 255);
		l4.child = l3;
		limbs[3] = l4;
	}
}
