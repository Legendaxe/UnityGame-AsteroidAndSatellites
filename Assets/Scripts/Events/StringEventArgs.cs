using System;
public class StringEventArgs : EventArgs
{
    public string Text { get; set; }

    public StringEventArgs(string text)
    {
        Text = text;
    }
}