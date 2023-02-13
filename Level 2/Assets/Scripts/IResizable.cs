using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResizable
{
    public void SetDefaultSize();
    public void UpdateSize(Vector3 size);
}
