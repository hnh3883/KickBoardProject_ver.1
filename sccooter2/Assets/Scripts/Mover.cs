using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[2]; //�� �ݶ��̴��� �޾ƿ´�.
    public Transform[] tires = new Transform[2]; //������ ���ư��� �� ǥ���ϱ����� �޽��� �޾ƿ´�.

    public float maxF = 50.0f; //�ڵ��������� ������ ��
    public float power = 3000.0f; //�ڵ����� �о��ִ� ��(���������δ� �ʹ� ������..)
    public float rot = 45;

    Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>(); //������ٵ� �޾ƿ´�.

        wheels[0].steerAngle = 90;
        wheels[1].steerAngle = 90;//���� ������ ���ݶ��̴��� ������ �����Ѵٸ� 90���� ����������.
        wheels[0].ConfigureVehicleSubsteps(1000, 1000, 1000);

        rb.centerOfMass = new Vector3(0, 0, -1); //�����߽��� ����� ���缭 ���������� �����ϵ��� �Ѵ�.
    }

    private void Update()
    {
        UpdateMeshesPostion(); //������ ���ư��°� ���̵��� ��
    }

    void FixedUpdate()
    {
        float accelerate = Input.GetAxis("Vertical");

        rb.AddForce(transform.rotation * new Vector3(accelerate * power, 0, 0)); //�ڿ��� �о��ش�.

        for (int i = 0; i < 2; i++)
        {
            wheels[i].motorTorque = maxF * accelerate; //������ ������.
        }

        float steer = rot * Input.GetAxis("Horizontal");
        wheels[0].steerAngle = steer+90; //���⵵ ������ �ݶ��̴��� �����λ���� + 90�� ������Ѵ�.
        wheels[1].steerAngle = steer + 90; //���⵵ ������ �ݶ��̴��� �����λ���� + 90�� ������Ѵ�.
    }

    void UpdateMeshesPostion()
    {
        for(int i = 0; i <2; i++)
        {
            Quaternion quat;
            Vector3 pos;

            wheels[i].GetWorldPose(out pos, out quat);
            tires[i].position = pos;
            tires[i].rotation = quat;
        }

    }
}