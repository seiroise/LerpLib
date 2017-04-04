using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerpLib {

	/// <summary>
	/// 3軸の線形補間
	/// </summary>
	[Serializable]
	public class LerpVector3 {

		/// <summary>
		/// 補間タイプ
		/// </summary>
		public enum LerpType {
			Linear, Anguler
		}

		/// <summary>
		/// 次元
		/// </summary>
		public enum Dimensions {
			X, Y, Z
		}

		private const int DIM = 3;

		private LerpCell[] _cells;
		private Vector3 _eval;		//計算結果
		public Vector3 eval { get { return _eval; } }
		private int _i;				//イテレータ
		private bool _isLerping;	//線形補間中か
		private bool _check;		//線形補間を行うかどうかの確認用

		public LerpVector3(LerpType type, Vector3 init, float t = 10f) {
			_cells = new LerpCell[DIM];

			if(type == LerpType.Linear) {
				for(_i = 0; _i < DIM; ++_i) {
					_cells[_i] = new LerpCell(init[_i], init[_i], t);
				}
			} else {
				for(_i = 0; _i < DIM; ++_i) {
					_cells[_i] = new LerpAngleCell(init[_i], init[_i], t);
				}
			}
			_eval = init;
			_isLerping = false;
		}

		/// <summary>
		/// 値の更新。使用するクラスでは必ず呼び出すこと
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		public bool Update(float delta) {
			
		if(_isLerping) {
				_check = false;
				for(_i = 0; _i < DIM; ++_i) {
					_check |= _cells[_i].Update(delta);
					_eval[_i] = _cells[_i].from;
				}
				_isLerping = _check;
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// 指定した目標値へ線形補間
		/// </summary>
		/// <param name="to">角度</param>
		public void Lerp(Vector3 to) {
			for(_i = 0; _i < DIM; ++_i) {
				_cells[_i].Lerp(to[_i]);
			}
			_isLerping = true;
		}

		/// <summary>
		/// 指定した次元の目標値へ線形補間
		/// </summary>
		/// <param name="dim">次元</param>
		/// <param name="to">目標値</param>
		public void Lerp(Dimensions dim, float to) {
			_cells[(int)dim].Lerp(to);
			_isLerping = true;
		}

		/// <summary>
		/// 指定した次元の目標値に追加する
		/// </summary>
		/// <param name="dim">次元</param>
		/// <param name="add">追加</param>
		public void AddTo(Dimensions dim, float add) {
			_cells[(int)dim].AddTo(add);
			_isLerping = true;
		}

		/// <summary>
		/// 指定した次元の値を取得
		/// </summary>
		/// <returns>取得した値</returns>
		public float GetTo(Dimensions dim) {
			return _cells[(int)dim].to;
		}
	}
}