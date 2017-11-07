using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerpLib.Examples {

	[RequireComponent(typeof(Camera))]
	public class LerpOrthoSize : MonoBehaviour {

		/// <summary>
		/// 拡縮レベル
		/// </summary>
		private class ZoomLevel {

			private float _size;
			private Vector3 _position;

			public ZoomLevel(float size, Vector3 position) {
				this._size = size;
				this._position = position;
			}

			/// <summary>
			/// カメラへの拡縮レベルの設定
			/// </summary>
			/// <param name="cam">設定するカメラ</param>
			public void SetZoomLevel(Camera cam, LerpedFloat lerpCell) {
				if (cam) {
					cam.transform.localPosition = _position;
					lerpCell.value = _size;
				}
			}
		}

		private Camera _cam;
		private LerpedFloat _lerpSize;
		private ZoomLevel[] _levels = {
			new ZoomLevel(5f, Vector3.forward * -10f),
			new ZoomLevel(10f, Vector3.forward * -20f),
			new ZoomLevel(20f, Vector3.forward * -40f),
			new ZoomLevel(30f, Vector3.forward * -60f),
		};
		private int _levelIndex;
		private int _initLevelIndex = 0;

		public void Awake() {
			_cam = GetComponent<Camera>();
			_lerpSize = new LerpedFloat(_cam.orthographicSize, _cam.orthographicSize, 10f);
			InitZoom();
		}

		public void Update() {
			if (_lerpSize.Update(Time.deltaTime)) {
				_cam.orthographicSize = _lerpSize.value;
			}
		}

		/// <summary>
		/// 拡縮レベルの初期化
		/// </summary>
		public void InitZoom() {
			_levelIndex = _initLevelIndex;
			_levels[_levelIndex].SetZoomLevel(_cam, _lerpSize);
		}

		/// <summary>
		/// 拡大
		/// </summary>
		public void ZoomIn() {
			_levelIndex = Mathf.Clamp(--_levelIndex, 0, _levels.Length - 1);
			_levels[_levelIndex].SetZoomLevel(_cam, _lerpSize);
		}

		/// <summary>
		/// 縮小
		/// </summary>
		public void ZoomOut() {
			_levelIndex = Mathf.Clamp(++_levelIndex, 0, _levels.Length - 1);
			_levels[_levelIndex].SetZoomLevel(_cam, _lerpSize);
		}
	}
}