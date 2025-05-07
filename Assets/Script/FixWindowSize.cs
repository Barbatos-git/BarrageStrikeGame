using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FixWindowSize : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool AdjustWindowRectEx(ref RECT lpRect, uint dwStyle, bool bMenu, uint dwExStyle);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private const uint SWP_NOZORDER = 0x0004;
    private const uint SWP_NOACTIVATE = 0x0010;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left, top, right, bottom;
    }
    // Start is called before the first frame update
    void Start()
    {
        FixWindow(414, 896);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixWindow(int targetWidth, int targetHeight)
    {
        IntPtr hWnd = GetActiveWindow();
        if (hWnd == IntPtr.Zero) return;

        RECT rect;
        GetWindowRect(hWnd, out rect);

        int windowWidth = rect.right - rect.left;
        int windowHeight = rect.bottom - rect.top;

        int extraWidth = windowWidth - targetWidth;
        int extraHeight = windowHeight - targetHeight;

        SetWindowPos(hWnd, IntPtr.Zero, rect.left, rect.top, targetWidth - extraWidth, targetHeight - extraHeight, SWP_NOZORDER | SWP_NOACTIVATE);
    }
}
