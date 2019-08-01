using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    private Rigidbody rigidBody;

    private ColorState colorState;

    private int count = 90;
    private int yellowCount;   

    private Vector3 moveVector;
    private Vector3 rotateVector;

    private void Start()
    {
        colorState = gameObject.GetComponent<ColorState>();
        rigidBody = gameObject.GetComponent<Rigidbody>();

        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //moveVector = Vector3.forward;
            rotateVector = Vector3.left;
            MoveAndRotate();


            if (colorState.CurColor.Up().GetColor().Equals(Color.yellow))
            {
               colorState.CurColor.LeftRightMirror();
               colorState.CurColor.Left().LeftRightMirror();
               colorState.CurColor.Right().LeftRightMirror();
                colorState.CurColor.Left().Left().LeftRightMirror();
            }
            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Up().GetColor());

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //moveVector = Vector3.back;
            rotateVector = Vector3.right;
            MoveAndRotate();

            if (colorState.CurColor.Down().GetColor().Equals(Color.yellow))
            {
                colorState.CurColor.LeftRightMirror();
                colorState.CurColor.Left().LeftRightMirror();
                colorState.CurColor.Right().LeftRightMirror();
                colorState.CurColor.Left().Left().LeftRightMirror();


            }
            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Down().GetColor());

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            MoveAndRotate();

            if (colorState.CurColor.Right().GetColor().Equals(Color.yellow))
            {
                colorState.CurColor.UpDownMirror();
                colorState.CurColor.Up().UpDownMirror();
                colorState.CurColor.Down().UpDownMirror();
                colorState.CurColor.Up().Up().UpDownMirror();
            }


            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Left().GetColor());

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //moveVector = Vector3.right;
            rotateVector = Vector3.back;
            MoveAndRotate();


            if (colorState.CurColor.Left().GetColor().Equals(Color.yellow))
            {
                colorState.CurColor.UpDownMirror();
                colorState.CurColor.Up().UpDownMirror();
                colorState.CurColor.Down().UpDownMirror();
                colorState.CurColor.Up().Up().UpDownMirror();
            }

            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Right().GetColor());


        }
    }

    private void MoveAndRotate()
    {
        // colorState.CurColor.ColorDebug();
        cube.transform.Rotate(rotateVector * 90, Space.World);
        cube.transform.Translate(moveVector, Space.World);

        moveVector = Vector3.zero;
        rotateVector = Vector3.zero;
    }

    public void ComparerColor(Color from, Color to)
    {
        if (from.Equals(Color.magenta))
        {

            if (to.Equals(Color.red))
            {
                colorState.RotateLeft();
            }

            else if (to.Equals(Color.black))
            {
                colorState.RotateRight();
            }

            //else if (to.Equals(Color.yellow))
            //{
            //    colorState.CurColor.LeftRightMirror();
            //    colorState.CurColor.Left().Left().LeftRightMirror();
            //    colorState.CurColor.Left().LeftRightMirror();
            //    colorState.CurColor.Right().LeftRightMirror();
            //}

        }

        else if (from.Equals(Color.red))
        {
            
            if (to.Equals(Color.magenta))
            {
                colorState.RotateRight();
            }

            else if (to.Equals(Color.blue))
            {
                colorState.RotateLeft();
            }

            //else if (from.Equals(Color.yellow))
            //{
            //    colorState.CurColor.UpDownMirror();
            //    colorState.CurColor.Up().Up().UpDownMirror();
            //    colorState.CurColor.Up().UpDownMirror();
            //    colorState.CurColor.Down().UpDownMirror();
            //}
        }

        else if (from.Equals(Color.blue))
        {

            if (to.Equals(Color.red))
            {
                colorState.RotateRight();
            }

            else if (to.Equals(Color.black))
            {
                colorState.RotateLeft();
            }


            //else if (to.Equals(Color.yellow))
            //{
            //    colorState.CurColor.LeftRightMirror();
            //    colorState.CurColor.Left().Left().LeftRightMirror();
            //    colorState.CurColor.Left().LeftRightMirror();
            //    colorState.CurColor.Right().LeftRightMirror();
            //}

        }

        else if (from.Equals(Color.black))
        {
            
            if (to.Equals(Color.magenta))
            {
                colorState.RotateLeft();
            }

            else if (to.Equals(Color.blue))
            {
                colorState.RotateRight();
            }

            //else if (from.Equals(Color.yellow))
            //{
            //    colorState.CurColor.UpDownMirror();
            //    colorState.CurColor.Up().Up().UpDownMirror();
            //    colorState.CurColor.Up().UpDownMirror();
            //    colorState.CurColor.Down().UpDownMirror();
            //}
        }

        //if (to.Equals(Color.yellow))
        //{
        //    if (from.Equals(Color.magenta)) {
        //        

        //        // to = Color.blue;
        //    }

        //    else if (from.Equals(Color.blue))
        //    {
        //        colorState.CurColor.LeftRightMirror();
        //        colorState.CurColor.Left().Left().LeftRightMirror();
        //        colorState.CurColor.Left().LeftRightMirror();
        //        colorState.CurColor.Right().LeftRightMirror();

        //        // to = Color.magenta;
        //    }

        //    else if (from.Equals(Color.red)) {
        //        colorState.CurColor.UpDownMirror();
        //        colorState.CurColor.Up().Up().UpDownMirror();
        //        colorState.CurColor.Up().UpDownMirror();
        //        colorState.CurColor.Down().UpDownMirror();
        //        // to = Color.black;
        //    }

        //    else if (from.Equals(Color.black))
        //    {
        //        colorState.CurColor.UpDownMirror();
        //        colorState.CurColor.Up().Up().UpDownMirror();
        //        colorState.CurColor.Up().UpDownMirror();
        //        colorState.CurColor.Down().UpDownMirror();
        //        // to = Color.red;
        //    }
        //}


        if (colorState.CurColor.Up().GetColor().Equals(to))
            colorState.CurColor = colorState.CurColor.Up();

        else if (colorState.CurColor.Down().GetColor().Equals(to))
            colorState.CurColor = colorState.CurColor.Down();

        else if (colorState.CurColor.Left().GetColor().Equals(to))
            colorState.CurColor = colorState.CurColor.Left();

        else if (colorState.CurColor.Right().GetColor().Equals(to))
            colorState.CurColor = colorState.CurColor.Right();

    }

}
