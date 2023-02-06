using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Transform/WorldToLocal")]
public class WorldToLocalMatrixBinder : VFXBinderBase
{
    [VFXPropertyBinding("UnityEngine.Vector4")]
    public ExposedProperty MatrixRow0Property;
    [VFXPropertyBinding("UnityEngine.Vector4")]
    public ExposedProperty MatrixRow1Property;
    [VFXPropertyBinding("UnityEngine.Vector4")]
    public ExposedProperty MatrixRow2Property;
    [VFXPropertyBinding("UnityEngine.Vector4")]
    public ExposedProperty MatrixRow3Property;

    public Transform target;

    public override bool IsValid(VisualEffect component)
    {
        return target != null && component.HasVector4(MatrixRow0Property) && component.HasVector4(MatrixRow1Property) && component.HasVector4(MatrixRow2Property) && component.HasVector4(MatrixRow3Property);
    }

    public override void UpdateBinding(VisualEffect component)
    {
        Matrix4x4 targetMatrix = target.worldToLocalMatrix;
        component.SetVector4(MatrixRow0Property,targetMatrix.GetRow(0));
        component.SetVector4(MatrixRow1Property,targetMatrix.GetRow(1));
        component.SetVector4(MatrixRow2Property,targetMatrix.GetRow(2));
        component.SetVector4(MatrixRow3Property,targetMatrix.GetRow(3));
    }
}
