using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SortingLayerRenderer : MonoBehaviour 
{
	#region Variables

	[SerializeField] [SortingLayerAttribute("SortingLayerID")] int sortingLayerID;
	[SerializeField] [PropertyValueAttribute("SortingOrder")] int sortingOrder;

	Renderer cachedRenderer;

	#endregion


	#region Property

	public int SortingLayerID
	{
		get { return sortingLayerID; }
		set
		{
			sortingLayerID = value;
			if (CachedRenderer)
			{                
                CachedRenderer.sortingLayerID = sortingLayerID;
			}
		}
	}


	public int SortingOrder
	{
		get { return sortingOrder; }
		set
		{
			sortingOrder = value;
			if (CachedRenderer)
			{
				CachedRenderer.sortingOrder = sortingOrder;
			}
		}
	}


	Renderer CachedRenderer
	{
		get
		{
			if (cachedRenderer == null)
			{
				cachedRenderer = GetComponent<Renderer>();
			}
			return cachedRenderer;
		}
	}

	#endregion


	#region Unity lifecycles

	void Awake()
	{
		SortingLayerID = sortingLayerID;
		SortingOrder = sortingOrder;
	}

	#endregion
}