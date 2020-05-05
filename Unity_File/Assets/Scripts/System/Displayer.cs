using UnityEngine;

class Displayer:MonoBehaviour
{
    
    private void Update()
    {
        //SetGameObjPosition(obj, new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), Camera.main.transform.position.z + Camera.main.farClipPlane - 1);

    }

    public enum PivotType : int
    {

        CenterCenter, BottomCenter

    };



    public Vector3 screenTo3D(float x2, float y2, float z)
    {

        return Camera.main.ScreenToWorldPoint(new Vector3(x2, y2, z));

    }

    public Rect bound3D(float z)
    {

        Vector3 leftBottom = screenTo3D(0, 0, z);

        Vector3 rigthTop = screenTo3D(Camera.main.pixelWidth, Camera.main.pixelHeight, z);

        return new Rect(

            leftBottom.x, rigthTop.y,

            rigthTop.x - leftBottom.x, rigthTop.y - leftBottom.y

        );

    }



    public void SetGameObjPosition(GameObject obj, Rect pos, float z, PivotType pivot = PivotType.CenterCenter)
    {

        switch (pivot)
        {

            case PivotType.CenterCenter:

                //pos.x = pos.x - (pos.width/2);

                //pos.y = pos.y - (pos.height/2);

                break;

            case PivotType.BottomCenter:

                //pos.x = pos.x - (pos.width/2);

                break;

        }



        Vector3 leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, z));

        Vector3 rigthTop = Camera.main.ScreenToWorldPoint(new Vector3(pos.x + pos.width, pos.y + pos.height, z));



        Rect bound = new Rect(leftBottom.x, rigthTop.y,

                 rigthTop.x - leftBottom.x, rigthTop.y - leftBottom.y);



        float zRatio = obj.transform.localScale.z / obj.transform.localScale.x;



        obj.transform.localScale = new Vector3(bound.width, bound.height, bound.width * zRatio);

        obj.transform.position = new Vector3(bound.x + (bound.width / 2), bound.y - (bound.height / 2), Camera.main.transform.position.z + z);

    }
}



