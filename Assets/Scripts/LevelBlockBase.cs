using UnityEngine;
using System.Collections.Generic;

public class LevelBlockBase : MonoBehaviour 
{
    public enum BlockMaterialType
    {
        Green = 1,
        Blue = 2,
    }

    [System.Serializable]
    protected class BlockMaterial
    {
        public Material material;
        public BlockMaterialType type;
    }

    #region Fields

    [SerializeField] protected Transform meshTransform;
    [SerializeField] protected BoxCollider blockCollider;

    [SerializeField] new protected Renderer renderer;

    [SerializeField] protected List<BlockMaterial> materials;

    [SerializeField] BlockMaterialType materialType;

    #endregion


    #region Properties

    virtual public Vector3 Size
    {
        get { return meshTransform.localScale; }

        set 
        {            
            meshTransform.localScale = value;
            blockCollider.size = value;

            Material mat = null;

            #if UNITY_EDITOR

            if (UnityEditor.PrefabUtility.GetPrefabType(this) == UnityEditor.PrefabType.PrefabInstance)
            {
                mat = this.renderer.material;
            }
            else
            {
                mat = this.renderer.sharedMaterial;
            }

            #else

            mat = this.renderer.material;

            #endif

            mat.mainTextureScale = new Vector2(Mathf.Max(value.x, value.z), value.y);
        }
    }


    public virtual BlockMaterialType MaterialType
    {
        get { return materialType; }

        set
        {
            materialType = value;
            BlockMaterial mat = materials.Find(obj =>
                {
                    return obj.type == value;
                });

            if (mat != null)
            {
                renderer.material = mat.material;
            }

            Size = Size;
        }
    }


    public bool ColliderEnabled
    {
        get
        {
            return blockCollider.enabled;
        }

        set
        {
            blockCollider.enabled = value;
        }
    }

    #endregion
}
