using UnityEngine;

namespace Additions.Cameras
{
	[RequireComponent(typeof(Camera))]
	public class StrategyCameraController : MonoBehaviour
	{
		[Header("Custom Settings")]
		[SerializeField] private float moveSpeed = 10f;
		[SerializeField] private float zoomSpeed = 10f;

		private Vector3 _startMousePos;

		private const string mouseScrollWheel = "Mouse ScrollWheel";
		private const string horizontalAxis = "Horizontal";
		private const string verticalAxis = "Vertical";

		private Vector3 MousePos => Input.mousePosition;
		private float GetHorizontalAxis => Input.GetAxis(horizontalAxis);
		private float GetVerticalAxis => Input.GetAxis(verticalAxis);

		void Update()
		{
			KeyboardMoving();
			MouseMoving();

			CameraZoom();
		}

		private void MouseMoving()
		{
			if (Input.GetMouseButtonDown(2))
			{
				_startMousePos = MousePos;
			}
			else if (Input.GetMouseButton(2))
			{
				Vector3 delta = MousePos - _startMousePos;
				transform.position += new Vector3(-delta.x, 0, -delta.y) * moveSpeed * Time.deltaTime;
				_startMousePos = MousePos;
			}
		}

		private void CameraZoom() =>
			transform.Translate(new Vector3(0, 0, Input.GetAxis(mouseScrollWheel) * zoomSpeed * Time.deltaTime));

		private void KeyboardMoving() =>
			transform.position += new Vector3(GetHorizontalAxis, 0, GetVerticalAxis) * moveSpeed * Time.deltaTime;
	}
}