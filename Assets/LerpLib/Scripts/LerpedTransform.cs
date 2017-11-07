using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerpLib {

	/// <summary>
	/// Transformのposition, eulerAngles, localScaleの線形補間
	/// </summary>
	[RequireComponent(typeof(Transform))]
	public class LerpedTransform : MonoBehaviour{

		public enum Elements {
			Position, EulerAngles, Scale
		}

		private Transform _trans;
		private LerpedVector3[] _lerpVecs;
		public Vector3 position {
			get { return _lerpVecs[0].value; }
			set { _lerpVecs[0].value = value; }
		}
		public Vector3 eulerAngles {
			get { return _lerpVecs[1].value; }
			set { _lerpVecs[1].value = value; }
		}
		public Vector3 localScale {
			get { return _lerpVecs[2].value; }
			set { _lerpVecs[2].value = value; }
		}

		private void Awake() {
			_trans = GetComponent<Transform>();
			_lerpVecs = new LerpedVector3[3];
			_lerpVecs[0] = new LerpedVector3(LerpedVector3.LerpType.Linear, _trans.position);
			_lerpVecs[1] = new LerpedVector3(LerpedVector3.LerpType.Anguler, _trans.eulerAngles);
			_lerpVecs[2] = new LerpedVector3(LerpedVector3.LerpType.Linear, _trans.localScale);
		}

		private void Update() {
			if(_lerpVecs[0].Update(Time.deltaTime)) {
				_trans.position = _lerpVecs[0].value;
			}
			if(_lerpVecs[1].Update(Time.deltaTime)) {
				_trans.eulerAngles = _lerpVecs[1].value;
			}
			if(_lerpVecs[2].Update(Time.deltaTime)) {
				_trans.localScale = _lerpVecs[2].value;
			}
		}

		/// <summary>
		/// 要素と軸を指定した線形補間
		/// </summary>
		/// <param name="elem">要素</param>
		/// <param name="dim">軸</param>
		/// <param name="to">目標値</param>
		public void Lerp(Elements elem, LerpedVector3.Dimensions dim, float to) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)elem][dim] = to;
		}

		/// <summary>
		/// 要素と軸を指定して目標値に追加する
		/// </summary>
		/// <param name="elem">要素</param>
		/// <param name="dim">軸</param>
		/// <param name="add">追加</param>
		public void AddTo(Elements elem, LerpedVector3.Dimensions dim, float add) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)elem][dim] += add;
		}

		/// <summary>
		/// 指定した角度へ回転させる
		/// </summary>
		/// <param name="angles">角度</param>
		public void Rotate(Vector3 angles) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].value = angles;
		}

		/// <summary>
		/// x軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">x軸の角度</param>
		public void RotateX(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].x = angle;
		}

		/// <summary>
		/// y軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">y軸の角度</param>
		public void RotateY(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].y = angle;
		}

		/// <summary>
		/// z軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">z軸の角度</param>
		public void RotateZ(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].z = angle;
		}
	}
}
