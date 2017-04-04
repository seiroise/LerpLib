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
	public class LerpTransform : MonoBehaviour{

		public enum Elements {
			Position, EulerAngles, Scale
		}

		private Transform _trans;
		private LerpVector3[] _lerpVecs;
		public LerpVector3 position { get { return _lerpVecs[0]; } }
		public LerpVector3 eulerAngles { get { return _lerpVecs[1]; } }
		public LerpVector3 localScale { get { return _lerpVecs[2]; } }

		private void Awake() {
			_trans = GetComponent<Transform>();
			_lerpVecs = new LerpVector3[3];
			_lerpVecs[0] = new LerpVector3(LerpVector3.LerpType.Linear, _trans.position);
			_lerpVecs[1] = new LerpVector3(LerpVector3.LerpType.Anguler, _trans.eulerAngles);
			_lerpVecs[2] = new LerpVector3(LerpVector3.LerpType.Linear, _trans.localScale);
		}

		private void Update() {
			if(_lerpVecs[0].Update(Time.deltaTime)) {
				_trans.position = _lerpVecs[0].eval;
			}
			if(_lerpVecs[1].Update(Time.deltaTime)) {
				_trans.eulerAngles = _lerpVecs[1].eval;
			}
			if(_lerpVecs[2].Update(Time.deltaTime)) {
				_trans.localScale = _lerpVecs[2].eval;
			}
		}

		/// <summary>
		/// 要素と軸を指定した線形補間
		/// </summary>
		/// <param name="elem">要素</param>
		/// <param name="dim">軸</param>
		/// <param name="to">目標値</param>
		public void Lerp(Elements elem, LerpVector3.Dimensions dim, float to) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)elem].Lerp(dim, to);
		}

		/// <summary>
		/// 要素と軸を指定して目標値に追加する
		/// </summary>
		/// <param name="elem">要素</param>
		/// <param name="dim">軸</param>
		/// <param name="add">追加</param>
		public void AddTo(Elements elem, LerpVector3.Dimensions dim, float add) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)elem].AddTo(dim, add);
		}

		/// <summary>
		/// 指定した角度へ回転させる
		/// </summary>
		/// <param name="angles">角度</param>
		public void Rotate(Vector3 angles) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].Lerp(angles);
		}

		/// <summary>
		/// x軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">x軸の角度</param>
		public void RotateX(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].Lerp(LerpVector3.Dimensions.X, angle);
		}

		/// <summary>
		/// y軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">y軸の角度</param>
		public void RotateY(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].Lerp(LerpVector3.Dimensions.Y, angle);
		}

		/// <summary>
		/// z軸を指定した角度に回転させる
		/// </summary>
		/// <param name="angle">z軸の角度</param>
		public void RotateZ(float angle) {
			if(_lerpVecs == null) return;
			_lerpVecs[(int)Elements.EulerAngles].Lerp(LerpVector3.Dimensions.Z, angle);
		}
	}
}
