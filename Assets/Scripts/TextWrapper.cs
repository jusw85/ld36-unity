using UnityEngine;
using System.Collections.Generic;
using System.Text;

[RequireComponent(typeof(TextMesh))]
public class TextWrapper : MonoBehaviour {

    public float maxLineLength = 10f;

    private TextMesh textMesh;
    private TextSize textSize;

    private void Awake() {
        textMesh = GetComponent<TextMesh>();
    }

    private void Start() {
        textSize = new TextSize(textMesh);
        //textMesh.text = "aaa aa aa aaaaa";
        //textMesh.text = "a b c d e f g h i j k l m n o p qqqqqqqqq";
        //textMesh.text = "a b c d e f g h i j";

        textMesh.text = WrapTextGreedy(textMesh.text);
    }

    /// <summary>
    /// Minimal Raggedness version
    /// </summary>
    public string WrapText(string text) {
        int i, j;
        string[] words = text.Split(' ');
        int count = words.Length;

        float[] offsets = new float[count + 1];
        offsets[0] = 0;
        for (i = 1; i < offsets.Length; i++)
            offsets[i] = offsets[i - 1] + textSize.GetTextWidth(words[i - 1]);

        float[] minima = new float[count + 1];
        minima[0] = 0;
        for (i = 1; i < minima.Length; i++)
            minima[i] = float.MaxValue;

        int[] breaks = new int[count + 1];
        for (i = 0; i < breaks.Length; i++)
            breaks[i] = 0;

        for (i = 0; i < count; i++) {
            for (j = i + 1; j <= count; j++) {
                float w = offsets[j] - offsets[i] + ((j - i - 1) * textSize.GetTextWidth(" "));
                if (w > maxLineLength)
                    break;
                float cost = minima[i] + Mathf.Pow((maxLineLength - w), 2f);
                if (cost < minima[j]) {
                    minima[j] = cost;
                    breaks[j] = i;
                }
            }
        }

        j = count;
        List<string> lines = new List<string>();
        while (j > 0) {
            i = breaks[j];
            StringBuilder linesb = new StringBuilder();
            for (int k = i; k < j; k++)
                linesb.Append(words[k] + " ");
            lines.Add(linesb.ToString().Trim());
            j = i;
        }
        lines.Reverse();
        return string.Join(System.Environment.NewLine, lines.ToArray());
    }


    /// <summary>
    /// Greedy version of line wrapping
    /// </summary>
    public string WrapTextGreedy(string text) {
        var renderer = textMesh.GetComponent<Renderer>();
        var lineBuilder = new StringBuilder();
        var textBuilder = new StringBuilder();
        float maxExtent = maxLineLength / 2;
        string[] parts = text.Split(' ');

        for (int i = 0; i < parts.Length; i++) {
            string toAppend = parts[i] + " ";

            textMesh.text = lineBuilder.ToString() + toAppend;
            if (renderer.bounds.extents.x <= maxExtent) {
                lineBuilder.Append(toAppend);
            } else {
                if (lineBuilder.Length > 0) {
                    textBuilder.AppendLine(lineBuilder.ToString().Trim());
                    lineBuilder.Length = 0;
                    lineBuilder.Append(toAppend);
                } else {
                    textBuilder.AppendLine(toAppend.Trim());
                }
            }
        }
        textBuilder.AppendLine(lineBuilder.ToString().Trim());
        return textBuilder.ToString();
    }

}
