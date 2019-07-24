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
            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Up().GetColor());


        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //moveVector = Vector3.back;
            rotateVector = Vector3.right;
            MoveAndRotate();
            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Down().GetColor());

          
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //moveVector = Vector3.left;
            rotateVector = Vector3.forward;
            MoveAndRotate();
            ComparerColor(colorState.CurColor.GetColor(), colorState.CurColor.Left().GetColor());


        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //moveVector = Vector3.right;
            rotateVector = Vector3.back;
            MoveAndRotate();
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
        if (from.Equals(Color.magenta)) {
            if (to.Equals(Color.green))
                return;

            if (to.Equals(Color.red)) {
                colorState.RotateLeft();
            }

            else if (to.Equals(Color.black)) {
                colorState.RotateRight();
            }

            else if (colorState.CurColor.Up().GetColor().Equals(Color.yellow))
            {
                colorState.CurColor.Up().Left().LeftRightMirror();
                colorState.CurColor.Up().Right().Right().LeftRightMirror();
                colorState.CurColor.Up().Right().LeftRightMirror();
                colorState.CurColor.Up().LeftRightMirror();

            }

            else if (colorState.CurColor.Down().GetColor().Equals(Color.yellow) )
            {
                colorState.CurColor.Down().Left().LeftRightMirror();
                colorState.CurColor.Down().Right().Right().LeftRightMirror();
                colorState.CurColor.Down().Right().LeftRightMirror();
                colorState.CurColor.Down().LeftRightMirror();


            }
        }

        else if (from.Equals(Color.red)) {
            if (to.Equals(Color.green))
                return;

            if (to.Equals(Color.magenta)) {
                colorState.RotateRight();
            }

            else if (to.Equals(Color.blue)) {
                colorState.RotateLeft();
            }

            else if (colorState.CurColor.Left().GetColor().Equals(Color.yellow) )
            {
                colorState.CurColor.Left().Up().UpDownMirror();
                colorState.CurColor.Left().Down().Down().UpDownMirror();
                colorState.CurColor.Left().Down().UpDownMirror();
                colorState.CurColor.Left().UpDownMirror();

            }

            else if (colorState.CurColor.Right().GetColor().Equals(Color.yellow) )
            {
                colorState.CurColor.Right().Up().UpDownMirror();
                colorState.CurColor.Right().Down().Down().UpDownMirror();
                colorState.CurColor.Right().Down().UpDownMirror();
                colorState.CurColor.Right().UpDownMirror();

            }

        }

        else if (from.Equals(Color.blue)) {

            if (to.Equals(Color.green))
                return;

            if (to.Equals(Color.red)) {
                colorState.RotateRight();
            }

            else if (to.Equals(Color.black)) {
                colorState.RotateLeft();
            }

            else if (colorState.CurColor.Up().GetColor().Equals(Color.yellow) && ++yellowCount % 2 == 1)
            {
                colorState.CurColor.Up().Left().LeftRightMirror();
                colorState.CurColor.Up().Right().Right().LeftRightMirror();
                colorState.CurColor.Up().Right().LeftRightMirror();
                colorState.CurColor.Up().LeftRightMirror();

            }

            else if (colorState.CurColor.Down().GetColor().Equals(Color.yellow) && ++yellowCount % 2 == 1)
            {
                colorState.CurColor.Down().Left().LeftRightMirror();
                colorState.CurColor.Down().Right().Right().LeftRightMirror();
                colorState.CurColor.Down().Right().LeftRightMirror();
                colorState.CurColor.Down().LeftRightMirror();


            }
        }

        else if (from.Equals(Color.black)) {
            if (to.Equals(Color.green))
                return;

            if (to.Equals(Color.magenta)) {
                colorState.RotateLeft();
            }

            else if (to.Equals(Color.blue)) {
                colorState.RotateRight();
            }

            else if (colorState.CurColor.Left().GetColor().Equals(Color.yellow) && ++yellowCount % 2 == 1)
            {
                colorState.CurColor.Left().Up().UpDownMirror();
                colorState.CurColor.Left().Down().Down().UpDownMirror();
                colorState.CurColor.Left().Down().UpDownMirror();
                colorState.CurColor.Left().UpDownMirror();

            }

            else if (colorState.CurColor.Right().GetColor().Equals(Color.yellow) && ++yellowCount % 2 == 1)
            {
                colorState.CurColor.Right().Up().UpDownMirror();
                colorState.CurColor.Right().Down().Down().UpDownMirror();
                colorState.CurColor.Right().Down().UpDownMirror();
                colorState.CurColor.Right().UpDownMirror();

            }
        }



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
