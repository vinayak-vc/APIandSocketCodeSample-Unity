using System;

using UnityEngine;

[Serializable]
public class CheckAlignment {
    public bool centered;
}

[Serializable]
public class TableData {
    public double timestamp;

    // [width, height]
    public int[] image_size;

    // 4 corners → each corner has [x, y]
    public Vector2[] table_corners_cam;

    // [width, height]
    public int[] mask_original_size_px;

    // Optional helper properties (clean access)
    //public int ImageWidth => image_size != null && image_size.Length > 0 ? image_size[0] : 0;
    //public int ImageHeight => image_size != null && image_size.Length > 1 ? image_size[1] : 0;

    //public int MaskWidth => mask_original_size_px != null && mask_original_size_px.Length > 0 ? mask_original_size_px[0] : 0;
    //public int MaskHeight => mask_original_size_px != null && mask_original_size_px.Length > 1 ? mask_original_size_px[1] : 0;
}
[Serializable]
public class FrameData {
    public string origin;
    public FrameSize frame_size;
    public int ball_count;
    public BallData[] balls;
    public int frame_number;
    //public Float2Array[] table_corners_cam;
    public string timestamp;
}

[Serializable]
public class FrameSize {
    public int width;
    public int height;
}

[Serializable]
public class BallData {
    public int ball_number;
    public Vector2 center_px;
    public float[] bbox_px;   // [x1, y1, x2, y2]
}
