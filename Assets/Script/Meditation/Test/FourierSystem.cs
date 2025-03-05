using System;
using System.Collections.Generic;
using UnityEngine;

public enum FourierDirection
{
    Forward = 1,
    Backward = -1
}

public struct Complex
{
    public double Real;
    public double Imaginary;

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }
}

public class FourierSystem : MonoBehaviour
{
    public static FourierSystem Instance;

    [Header("ªÊÕº…Ë÷√")]
    public LineRenderer drawingLine;
    public int samplePoints = 512;
    public float drawSpeed = 1f;

    private List<Vector3> positions = new List<Vector3>();
    private Complex[] fftResults;
    private float timer;

    void Awake()
    {
        Instance = this;
    }

    public void RecordPosition(int index, Vector3 pos)
    {
        while (positions.Count <= index) positions.Add(Vector3.zero);
        positions[index] = pos;
    }

    public void StartDrawing()
    {
        Complex[] complexData = new Complex[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            complexData[i] = new Complex(positions[i].x, positions[i].y);
        }

        FFT(complexData, FourierDirection.Forward);
        fftResults = complexData;

        drawingLine.positionCount = 0;
        timer = 0f;
    }

    void Update()
    {
        if (fftResults == null) return;

        timer += Time.deltaTime * drawSpeed;
        if (timer > 1f) timer -= 1f;

        Vector2 sum = Vector2.zero;
        for (int k = 0; k < fftResults.Length; k++)
        {
            float angle = 2 * Mathf.PI * k * timer;
            Vector2 basis = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            sum += new Vector2(
                (float)(fftResults[k].Real * basis.x - fftResults[k].Imaginary * basis.y),
                (float)(fftResults[k].Real * basis.y + fftResults[k].Imaginary * basis.x)
            );
        }

        drawingLine.positionCount++;
        drawingLine.SetPosition(drawingLine.positionCount - 1, sum);
    }

    void FFT(Complex[] buffer, FourierDirection direction)
    {
        int n = buffer.Length;
        if (n <= 1) return;

        Complex[] even = new Complex[n / 2];
        Complex[] odd = new Complex[n / 2];

        for (int i = 0; i < n / 2; i++)
        {
            even[i] = buffer[2 * i];
            odd[i] = buffer[2 * i + 1];
        }

        FFT(even, direction);
        FFT(odd, direction);

        for (int k = 0; k < n / 2; k++)
        {
            double angle = -2 * Math.PI * k / n * (int)direction;
            Complex t = new Complex(Math.Cos(angle), Math.Sin(angle)) * odd[k];

            buffer[k] = even[k] + t;
            buffer[k + n / 2] = even[k] - t;
        }
    }
}