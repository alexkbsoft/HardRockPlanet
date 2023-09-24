
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	public CarStorage MainStorage;
	public bool IsBlocked = false;
	public WheelCollider[] WColForward; //2
	public WheelCollider[] WColBack;  //3

	public Transform[] wheelsFL; //1
	public Transform[] wheelsFR;
	public Transform[] wheelsBL; //1
	public Transform[] wheelsBR;

	public float wheelOffset = 0.0f; //2
	public float wheelRadius = 1.08f; //2
									  // Start is called before the first frame update
	public float maxSteer = 30; //1
	public float maxAccel = 25; //2
	public float maxBrake = 50; //3

	public Transform CenterMass;

	public static CarController Instance => _instance;

	private static CarController _instance;

	public class WheelData
	{ //3
		public Transform wheelTransform; //4
		public WheelCollider col; //5
		public Vector3 wheelStartPos; //6 
		public float rotation = 0.0f;  //7
	}

	protected WheelData[] wheelsL;
	protected WheelData[] wheelsR;

	void Awake()
	{
		_instance = this;
	}

	public void OnDead() {
		EventBus.Instance.Dead?.Invoke("Машина уничтожена");
	}

	void Start()
	{
		GetComponent<Rigidbody>().centerOfMass = CenterMass.localPosition;

		wheelsL = new WheelData[4]; //8
		wheelsR = new WheelData[4];

		wheelsR[0] = SetupWheels(wheelsFR[0], WColForward[0]); //9
		wheelsL[0] = SetupWheels(wheelsFL[0], WColForward[1]); //9

		wheelsR[1] = SetupWheels(wheelsFR[1], WColForward[2]); //9
		wheelsL[1] = SetupWheels(wheelsFL[1], WColForward[3]); //9

		wheelsR[2] = SetupWheels(wheelsBR[0], WColBack[0]); //9
		wheelsL[2] = SetupWheels(wheelsBL[0], WColBack[1]); //9

		wheelsR[3] = SetupWheels(wheelsBR[1], WColBack[2]);
		wheelsL[3] = SetupWheels(wheelsBL[1], WColBack[3]); //9

	}

	private WheelData SetupWheels(Transform wheel, WheelCollider col)
	{ //10
		WheelData result = new WheelData();

		result.wheelTransform = wheel; //10
		result.col = col; //10
		result.wheelStartPos = wheel.transform.localPosition; //10

		return result; //10

	}
	void FixedUpdate()
	{
		if (IsBlocked) {
			return;
		}

		float accel = 0;
		float steer = 0;

		accel = Input.GetAxis("Vertical");  //4
		steer = Input.GetAxis("Horizontal");     //4	

		CarMove(accel, steer); //5
		UpdateWheels();

		MainStorage.Fuel -= accel * (maxAccel / 10) * Time.fixedDeltaTime;

		if (MainStorage.Fuel <= 0 ) {
			EventBus.Instance.Dead?.Invoke("Горючее кончилось");
		}
	}

	private void UpdateWheels()
	{ //11
		float delta = Time.fixedDeltaTime; //12


		foreach (WheelData w in wheelsR)
		{ //13
			WheelHit hit; //14

			Vector3 lp = w.wheelTransform.localPosition; //15
			if (w.col.GetGroundHit(out hit))
			{ //16
				lp.y -= Vector3.Dot(w.wheelTransform.position - hit.point, transform.up) - wheelRadius; //17
			}
			else
			{ //18

				lp.y = w.wheelStartPos.y - wheelOffset; //18
			}
			w.wheelTransform.localPosition = lp; //19


			w.rotation = Mathf.Repeat(w.rotation + delta * w.col.rpm * 360.0f / 60.0f, 360.0f); //20
			w.wheelTransform.localRotation = Quaternion.Euler(w.rotation, w.col.steerAngle, 0f); //21
		}

		foreach (WheelData w in wheelsL)
		{ //13
			WheelHit hit; //14

			Vector3 lp = w.wheelTransform.localPosition; //15
			if (w.col.GetGroundHit(out hit))
			{ //16
				lp.y -= Vector3.Dot(w.wheelTransform.position - hit.point, transform.up) - wheelRadius; //17
			}
			else
			{ //18

				lp.y = w.wheelStartPos.y - wheelOffset; //18
			}
			w.wheelTransform.localPosition = lp; //19


			w.rotation = Mathf.Repeat(w.rotation - delta * w.col.rpm * 360.0f / 60.0f, 360.0f); //20
			w.wheelTransform.localRotation = Quaternion.Euler(w.rotation, w.col.steerAngle + 180, 0f); //21
		}
	}

	private void CarMove(float accel, float steer)
	{ //5

		foreach (WheelCollider col in WColForward)
		{ //6
			col.steerAngle = steer * maxSteer; //6
		}

		if (accel == 0)
		{ //7
			foreach (WheelCollider col in WColBack)
			{  //7
				col.brakeTorque = maxBrake; //7
			}

		}
		else
		{ //8

			foreach (WheelCollider col in WColBack)
			{ //8
				col.brakeTorque = 0; //8
				col.motorTorque = accel * maxAccel; //8
			}

			foreach (WheelCollider col in WColForward)
			{ //8
				col.brakeTorque = 0; //8
				col.motorTorque = accel * maxAccel; //8
			}

		}
	}
}

