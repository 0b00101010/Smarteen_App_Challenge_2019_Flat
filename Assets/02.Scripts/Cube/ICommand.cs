using UnityEngine;

public interface ICommand{
    void Excute(out Vector3 moveVector, out Vector3 rotateVector);
}