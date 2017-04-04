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
	public class LerpCell {

		private static float _epsilon = 0.001f;

		protected float _from;
		public float from { get { return _from; } }
		protected float _to;
		public float to { get { return _to; } }
		protected float _t;
		public float t { get { return _t; } set { _t = value; } }
		private bool _isLerping;
		public bool isLerping { get { return _isLerping; } }
		private UnityEvent _onFinished;
		public UnityEvent onFinished { get { return _onFinished; } }

		public LerpCell(float from, float to, float t) {
			this._from = from;
			this._to = to;
			this._t = t;
			this._isLerping = true;
			this._onFinished = new UnityEvent();
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		/// <returns>補間中はtrueを返す</returns>
		public bool Update(float delta) {
			if(_isLerping) {
				_from = LerpProc(delta);
				if(Mathf.Abs(_to - _from) < _epsilon) {
					_from = _to;
					_isLerping = false;
					_onFinished.Invoke();
				}
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// 線形補間処理。用途によっては変える必要がある場合はオーバーライドすること。
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		/// <returns>評価値</returns>
		protected virtual float LerpProc(float delta) {
			return Mathf.Lerp(_from, _to, _t * delta);
		}

		/// <summary>
		/// 線形補間する
		/// </summary>
		/// <param name="to">目標値</param>
		public void Lerp(float to) {
			_to = to;
			_isLerping = true;
		}

		/// <summary>
		/// 線形補間する
		/// </summary>
		/// <param name="from">開始値</param>
		/// <param name="to">目標値</param>
		public void Lerp(float from, float to) {
			_from = from;
			_to = to;
			_isLerping = true;
		}

		/// <summary>
		/// 目標値に差分を追加する
		/// </summary>
		/// <param name="add">追加分</param>
		public virtual void AddTo(float add) {
			_to += add;
			_isLerping = true;
		}
	}

	/// <summary>
	/// 一つの値(セル)の角度線形補間
	/// </summary>
	[Serializable]
	public class LerpAngleCell : LerpCell {

		public LerpAngleCell(float from, float to, float t) : base(from, to, t) { }

		protected override float LerpProc(float delta) {
			return Mathf.LerpAngle(_from, _to, _t * delta);
		}
	}
}