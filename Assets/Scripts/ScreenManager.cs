using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Convert between various coordinate systems based on active camera.
/// 
/// Coordinate systems supported:
/// 
/// World: (0,0,0) is origin, x rightward, y upward, z forward
/// 
/// Ortho: bottom left of screen (0,0) inclusive to top right of screen 
///         (2*activeCamera.aspect*activeCamera.orthographicSize, 2*activeCamera.orthographicSize)
///         exclusive 
/// 
/// Screen: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
///         (top right pixel is (width-1, height-1))
/// 
/// Viewport: bottom left of screen (0,0) inclusive to top right of screen (1,1) exclusive
///         
/// Sprite: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
///         (top right pixel is (width-1, height-1))
///
/// </summary>
public class ScreenManager : MonoBehaviour {
    //TODO convenience methods

    [Tooltip("If unset, script tries to find it automatically")]
    public Camera activeCamera;

    /// <summary>
    /// Clamp screen space vector to be on screen
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Vector3 ClampToScreen(Vector3 v) {
        float x = Mathf.Clamp(v.x, 0, activeCamera.pixelWidth - 1);
        float y = Mathf.Clamp(v.y, 0, activeCamera.pixelHeight - 1);
        return new Vector3(x, y, v.z);
    }

    /// <summary>
    ///  Viewport: bottom left of screen (0,0) inclusive to top right of screen (1,1) exclusive
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToViewport(Position v) {
        EnsureActiveCamera();
        return new Position(activeCamera.WorldToViewportPoint(v.ToVector3()));
    }

    /// <summary>
    ///  Viewport: bottom left of screen (0,0) inclusive to top right of screen (1,1) exclusive
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToViewport(Vector3 v) {
        EnsureActiveCamera();
        return new Position(activeCamera.WorldToViewportPoint(v));
    }


    /// <summary>
    ///  Viewport: bottom left of screen (0,0) inclusive to top right of screen (1,1) exclusive
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position ViewportToWorld(Position v) {
        EnsureActiveCamera();
        return new Position(activeCamera.ViewportToWorldPoint(v.ToVector3()));
    }

    /// <summary>
    ///  Viewport: bottom left of screen (0,0) inclusive to top right of screen (1,1) exclusive
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position ViewportToWorld(Vector3 v) {
        EnsureActiveCamera();
        return new Position(activeCamera.ViewportToWorldPoint(v));
    }

    /// <summary>
    ///  Screen: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToScreen(Position v) {
        EnsureActiveCamera();
        return new Position(activeCamera.WorldToScreenPoint(v.ToVector3()));
    }

    /// <summary>
    ///  Screen: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToScreen(Vector3 v) {
        EnsureActiveCamera();
        return new Position(activeCamera.WorldToScreenPoint(v));
    }

    /// <summary>
    ///  Screen: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position ScreenToWorld(Position v) {
        EnsureActiveCamera();
        return new Position(activeCamera.ScreenToWorldPoint(v.ToVector3()));
    }

    /// <summary>
    ///  Screen: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position ScreenToWorld(Vector3 v) {
        EnsureActiveCamera();
        return new Position(activeCamera.ScreenToWorldPoint(v));
    }

    /// <summary>
    /// Ortho: bottom left of screen (0,0) inclusive to top right of screen 
    ///         (2*activeCamera.aspect*activeCamera.orthographicSize, 2*activeCamera.orthographicSize)
    ///         exclusive 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToOrtho(Position v) {
        EnsureActiveCamera();
        float minX = activeCamera.transform.position.x - activeCamera.orthographicSize * activeCamera.aspect;
        float minY = activeCamera.transform.position.y - activeCamera.orthographicSize;
        return new Position(v.x - minX, v.y - minY);
    }

    /// <summary>
    /// Ortho: bottom left of screen (0,0) inclusive to top right of screen 
    ///         (2*activeCamera.aspect*activeCamera.orthographicSize, 2*activeCamera.orthographicSize)
    ///         exclusive 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position WorldToOrtho(Vector3 v) {
        EnsureActiveCamera();
        float minX = activeCamera.transform.position.x - activeCamera.orthographicSize * activeCamera.aspect;
        float minY = activeCamera.transform.position.y - activeCamera.orthographicSize;
        return new Position(v.x - minX, v.y - minY);
    }

    /// <summary>
    /// Ortho: bottom left of screen (0,0) inclusive to top right of screen 
    ///         (2*activeCamera.aspect*activeCamera.orthographicSize, 2*activeCamera.orthographicSize)
    ///         exclusive 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position OrthoToWorld(Position v) {
        EnsureActiveCamera();
        float minX = activeCamera.transform.position.x - activeCamera.orthographicSize * activeCamera.aspect;
        float minY = activeCamera.transform.position.y - activeCamera.orthographicSize;
        return new Position(v.x + minX, v.y + minY);
    }

    /// <summary>
    /// Ortho: bottom left of screen (0,0) inclusive to top right of screen 
    ///         (2*activeCamera.aspect*activeCamera.orthographicSize, 2*activeCamera.orthographicSize)
    ///         exclusive 
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public Position OrthoToWorld(Vector3 v) {
        EnsureActiveCamera();
        float minX = activeCamera.transform.position.x - activeCamera.orthographicSize * activeCamera.aspect;
        float minY = activeCamera.transform.position.y - activeCamera.orthographicSize;
        return new Position(v.x + minX, v.y + minY);
    }

    /// <summary>
    /// Sprite: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public Position WorldToSprite(Position v, GameObject spriteObj) {
        Sprite sprite = spriteObj.GetComponent<SpriteRenderer>().sprite;
        Vector3 center = spriteObj.transform.position;
        Vector3 lowerLeft = center
            - spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            - spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        Vector3 upperRight = center
            + spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            + spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        return new Position((v.x - lowerLeft.x) / (upperRight.x - lowerLeft.x) * sprite.texture.width,
           (v.y - lowerLeft.y) / (upperRight.y - lowerLeft.y) * sprite.texture.height, 0);
    }

    /// <summary>
    /// Sprite: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public Position WorldToSprite(Vector3 v, GameObject spriteObj) {
        Sprite sprite = spriteObj.GetComponent<SpriteRenderer>().sprite;
        Vector3 center = spriteObj.transform.position;
        Vector3 lowerLeft = center
            - spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            - spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        Vector3 upperRight = center
            + spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            + spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;

        return new Position((v.x - lowerLeft.x) / (upperRight.x - lowerLeft.x) * sprite.texture.width,
             (v.y - lowerLeft.y) / (upperRight.y - lowerLeft.y) * sprite.texture.height, 0);
    }


    /// <summary>
    /// Sprite: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public Position SpriteToWorld(Position v, GameObject spriteObj) {
        Sprite sprite = spriteObj.GetComponent<SpriteRenderer>().sprite;
        Vector3 center = spriteObj.transform.position;
        Vector3 lowerLeft = center
            - spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            - spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        Vector3 upperRight = center
            + spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            + spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        float z = lowerLeft.z + (upperRight.z - lowerLeft.z) * Mathf.Sqrt(v.x * v.x + v.y * v.y) / Mathf.Sqrt(sprite.texture.width * sprite.texture.width + sprite.texture.height * sprite.texture.height);

        return new Position(v.x / sprite.texture.width * (upperRight.x - lowerLeft.x) + lowerLeft.x,
            v.y / sprite.texture.height * (upperRight.y - lowerLeft.y) + lowerLeft.y, z);

    }

    /// <summary>
    /// Sprite: bottom left pixel (0,0) inclusive to top right (width, height) exclusive 
    ///         (top right pixel is (width-1, height-1))
    /// </summary>
    /// <param name="v"></param>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public Position SpriteToWorld(Vector3 v, GameObject spriteObj) {
        Sprite sprite = spriteObj.GetComponent<SpriteRenderer>().sprite;
        Vector3 center = spriteObj.transform.position;
        Vector3 lowerLeft = center
            - spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            - spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;
        Vector3 upperRight = center
            + spriteObj.transform.right * spriteObj.transform.localScale.x * sprite.texture.width / 2 / sprite.pixelsPerUnit
            + spriteObj.transform.up * spriteObj.transform.localScale.y * sprite.texture.height / 2 / sprite.pixelsPerUnit;

        float z = lowerLeft.z + (upperRight.z - lowerLeft.z) * Mathf.Sqrt(v.x * v.x + v.y * v.y) / Mathf.Sqrt(sprite.texture.width * sprite.texture.width + sprite.texture.height * sprite.texture.height);

        return new Position(v.x / sprite.texture.width * (upperRight.x - lowerLeft.x) + lowerLeft.x,
            v.y / sprite.texture.height * (upperRight.y - lowerLeft.y) + lowerLeft.y, z);
    }

    // Use this for initialization
    void Start() {
        EnsureActiveCamera();
    }

    /// <summary>
    /// Make sure activeCamera is set to the active camera
    /// </summary>
    /// <returns></returns>
    public bool EnsureActiveCamera() {
        if (!activeCamera) {
            FindActiveCamera();
        } else if (!activeCamera.isActiveAndEnabled) {
            Camera c = activeCamera;
            FindActiveCamera();
            if (!activeCamera.isActiveAndEnabled) {
                activeCamera = c;
            }
        }
        return (bool)activeCamera;
    }

    void FindActiveCamera() {
        if (Camera.main.isActiveAndEnabled) {
            activeCamera = Camera.main;
            return;
        }
        if (activeCamera && activeCamera.isActiveAndEnabled) {
            return;
        }
        foreach (Camera c in FindObjectsOfType<Camera>()) {
            if (c.isActiveAndEnabled) {
                activeCamera = c;
                return;
            }
        }
        Debug.LogWarning("Could not find an active camera; trying inactive cameras");
        if (Camera.main) {
            activeCamera = Camera.main;
            return;
        }
        foreach (Camera c in FindObjectsOfType<Camera>()) {
            activeCamera = c;
            return;
        }
        Debug.LogError("Could not find any camera");
    }


    /// <summary>
    /// Holder for position that can be a Vector3 or integers.
    /// </summary>
    public struct Position {
        public float x, y, z;

        public Position(float x = 0, float y = 0, float z = 0) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Position(Vector3 v) : this(v.x, v.y, v.z) {
        }

        public Position(Vector2 v) : this(v.x, v.y) {
        }

        public Position(int[] v) : this(v[0], v[1]) {
            if (v.Length >= 3) {
                z = v[2];
            }
        }

        public Position(float[] v) : this(v[0], v[1]) {
            if (v.Length >= 3) {
                z = v[2];
            }
        }

        public void Set(Vector3 v) {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public void Set(Vector2 v) {
            x = v.x;
            y = v.y;
            z = 0;
        }

        public void Set(float[] v) {
            x = v[0];
            y = v[1];
            if (v.Length > 2) {
                z = v[2];
            } else {
                z = 0;
            }
        }

        public void Set(int[] v) {
            x = v[0];
            y = v[1];
            if (v.Length > 2) {
                z = v[2];
            } else {
                z = 0;
            }
        }

        public void Set(float x = 0, float y = 0, float z = 0) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 ToVector3() {
            return new Vector3(x, y, z);
        }

        public Vector2 ToVector2() {
            return new Vector2(x, y);
        }

        public int[] ToIntArray() {
            return new int[] { (int)x, (int)y, (int)z };
        }

        public float[] TofloatArray() {
            return new float[] { x, y, z };
        }

        public override string ToString() {
            return "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
        }
    }

}

