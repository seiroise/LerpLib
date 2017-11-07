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
	public class LerpedVector3 {

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

		private static readonly int DIM = 3;
		private LerpedFloat[] _cells;
		private bool _needUpdate;

		public float x {
			get { return _cells[0].value; }
			set { _cells[0].to = value; _needUpdate = true; }
		}
		public float y {
			get { return _cells[1].value; }
			set { _cells[1].to = value; _needUpdate = true; }
		}
		public float z {
			get { return _cells[2].value; }
			set { _cells[2].to = value; _needUpdate = true; }
		}
		public Vector3 value {
			get { return new Vector3(x, y, z); }
			set { x = value.x; y = value.y; z = value.z; }
		}

		public float this[Dimensions dim] {
			get { return _cells[(int)dim].value; }
			set { _cells[(int)dim].to = value; _needUpdate = true; }
		}

		public LerpedVector3(LerpType type, Vector3 init, float t = 10f) {
			_cells = new LerpedFloat[DIM];

			if (type == LerpType.Linear) {
				for (int i = 0; i < DIM; ++i) {
					_cells[i] = new LerpedFloat(init[i], init[i], t);
				}
			} else {
				for (int i = 0; i < DIM; ++i) {
					_cells[i] = new LerpedAngle(init[i], init[i], t);
				}
			}
			_needUpdate = false;
		}

		/// <summary>
		/// 値の更新。使用するクラスでは必ず呼び出すこと
		/// </summary>
		/// <param name="delta">前フレームからの時差</param>
		public bool Update(float delta) {
			if (_needUpdate) {
				_needUpdate =
					_cells[0].Update(delta) ||
					_cells[1].Update(delta) ||
					_cells[2].Update(delta);
				return true;
			} else {
				return false;
			}
		}
	}
}