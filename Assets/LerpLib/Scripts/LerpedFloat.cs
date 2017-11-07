using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerpLib {

	/// <summary>
	/// 一つの値(セル)の線形補間
	/// </summary>
	[Serializable]
	public class LerpedFloat {

		private static float _epsilon = 0.001f;

		protected float _value;
		public float value {
			get { return _value; }
			set { _value = value; _needUpdate = true; }
		}
		protected float _to;
		public float to { 
			get { return _to; }
			set { _to = value; _needUpdate = true; }
		}
		protected float _t;
		public float t { get { return _t; } set { _t = value; } }
		private bool _needUpdate;
		public bool needUpdate { get { return _needUpdate; } }
		private UnityEvent _onFinished;
		public UnityEvent onFinished { get { return _onFinished; } }

		public LerpedFloat(float from, float to, float t) {
			this._value = from;
			this._to = to;
			this._t = t;
			this._needUpdate = true;
			this._onFinished = new UnityEvent();
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		/// <returns>補間中はtrueを返す</returns>
		public bool Update(float delta) {
			if (_needUpdate) {
				_value = LerpProc(delta);
				if (Mathf.Abs(_to - _value) < _epsilon) {
					_value = _to;
					_needUpdate = false;
					_onFinished.Invoke();
				}
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// 線形補間処理。用途によって変える必要がある場合はオーバーライドすること。
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		/// <returns>評価値</returns>
		protected virtual float LerpProc(float delta) {
			return Mathf.Lerp(_value, _to, _t * delta);
		}
	}

	/// <summary>
	/// 一つの値(セル)の角度線形補間
	/// </summary>
	[Serializable]
	public class LerpedAngle : LerpedFloat {

		public LerpedAngle(float from, float to, float t) : base(from, to, t) { }

		protected override float LerpProc(float delta) {
			return Mathf.LerpAngle(_value, _to, _t * delta);
		}
	}
}