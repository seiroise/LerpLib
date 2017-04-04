using UnityEngine;
using System.Collections;

/// <summary>
/// テスト
/// </summary>
public class Voxel : MonoBehaviour {

	[SerializeField]
	private Camera _cam;

	[SerializeField]
	private GameObject _voxelPrefab;

	private void Update() {
		if(Input.GetMouseButtonDown(0)) {
			AddVoxelAtMouse();
		} else if(Input.GetMouseButtonDown(1)) {
			RemoveVoxelAtMouse();
		}
	}

	/// <summary>
	/// マウスの位置にボクセルを配置
	/// </summary>
	private void AddVoxelAtMouse() {
		var r = _cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r, out hit, 100f)) {
			Vector3 pos = hit.transform.position + hit.normal;
			var obj = (GameObject)Instantiate(_voxelPrefab, pos, Quaternion.identity);
			obj.transform.SetParent(transform);
		}
	}

	/// <summary>
	/// マウスの位置にあるボクセルを削除
	/// </summary>
	private void RemoveVoxelAtMouse() {
		var r = _cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(r, out hit, 100f)) {
			if(!hit.transform.name.Equals("BaseVoxel")) {
				Destroy(hit.transform.gameObject);
			}
		}
	}
}