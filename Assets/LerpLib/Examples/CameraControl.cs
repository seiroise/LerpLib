using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

namespace LerpLib.Examples {

	/// <summary>
	/// カメラ制御
	/// </summary>
	public class CameraControl : MonoBehaviour {

		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private LerpedTransform _lerpTrans;
		[SerializeField]
		private LerpOrthoSize _lerpOrtho;

		//xAngle
		private float[] _xAngles = { -90f, -30f, 0f, 30f, 90f };
		private int _xAngleIndex;
		private int _initXAngleIndex = 3;

		//yAngle
		private float[] _yAngles = { 45f, 135f, 225f, 315f };
		private int _yAngleIndex;
		private int _initYAngleIndex = 0;

		private void Start() {
			InitAngle();
		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.A)) {
				RotateLeftY();
			} else if(Input.GetKeyDown(KeyCode.D)) {
				RotateRightY();
			} else if(Input.GetKeyDown(KeyCode.W)) {
				RotateUpX();
			} else if(Input.GetKeyDown(KeyCode.S)) {
				RotateDownX();
			}
			float wheel = Input.GetAxis("Mouse ScrollWheel");
			if(wheel > 0f) {
				_lerpOrtho.ZoomIn();
			} else if(wheel < 0f) {
				_lerpOrtho.ZoomOut();
			}
		}

		/// <summary>
		/// 角度の初期化
		/// </summary>
		public void InitAngle() {
			if(_lerpTrans) {
				_xAngleIndex = _initXAngleIndex;
				_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.X, _xAngles[_xAngleIndex]);
				_yAngleIndex = _initYAngleIndex;
				_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.Y, _yAngles[_yAngleIndex]);
			}
		}

		/// <summary>
		/// x軸上回転
		/// </summary>
		public void RotateUpX() {
			_xAngleIndex = Mathf.Clamp(++_xAngleIndex, 0, _xAngles.Length - 1);
			_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.X, _xAngles[_xAngleIndex]);
		}

		/// <summary>
		/// x軸下回転
		/// </summary>
		public void RotateDownX() {
			_xAngleIndex = Mathf.Clamp(--_xAngleIndex, 0, _xAngles.Length);
			_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.X, _xAngles[_xAngleIndex]);
		}

		/// <summary>
		/// y軸左回転
		/// </summary>
		public void RotateLeftY() {
			_yAngleIndex = (_yAngleIndex + 1) % _yAngles.Length;
			_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.Y, _yAngles[_yAngleIndex]);
		}

		/// <summary>
		/// y軸右回転
		/// </summary>
		public void RotateRightY() {
			_yAngleIndex = (_yAngleIndex + (_yAngles.Length - 1)) % _yAngles.Length;
			_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.Y, _yAngles[_yAngleIndex]);
		}

		/// <summary>
		/// y軸追加回転
		/// </summary>
		/// <param name="angle">追加回転量</param>
		public void RotateY(float angle) {
			if(_lerpTrans) {
				_lerpTrans.Lerp(LerpedTransform.Elements.EulerAngles, LerpedVector3.Dimensions.Y, angle);
			}
		}
	}
}