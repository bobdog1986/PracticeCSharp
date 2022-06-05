using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2296. Design a Text Editor
    //Design a text editor with a cursor that can do the following:

    //Add text to where the cursor is.
    //Delete text from where the cursor is (simulating the backspace key).
    //Move the cursor either left or right.
    //When deleting text, only characters to the left of the cursor will be deleted.
    //The cursor will also remain within the actual text and cannot be moved beyond it.
    //More formally,we have that 0 <= cursor.position <= currentText.length always holds.

    //Implement the TextEditor class:
    //TextEditor() Initializes the object with empty text.
    //void addText(string text) Appends text to where the cursor is. The cursor ends to the right of text.
    //int deleteText(int k) Deletes k characters to the left of the cursor.Returns the number of characters actually deleted.
    //string cursorLeft(int k) Moves the cursor to the left k times.Returns the last min(10, len) characters to the left of the cursor, where len is the number of characters to the left of the cursor.
    //string cursorRight(int k) Moves the cursor to the right k times.Returns the last min(10, len) characters to the left of the cursor, where len is the number of characters to the left of the cursor.
    public class TextEditor
    {
        private readonly Stack<char> head;
        private readonly Stack<char> tail;
        private readonly Dictionary<int, string> dict;
        private int pos;

        public TextEditor()
        {
            head = new Stack<char>();
            tail = new Stack<char>();
            dict = new Dictionary<int, string>();
            pos = 0;
            dict.Add(pos, "");
        }

        public void AddText(string text)
        {
            foreach(var c in text)
            {
                AddCharToHead(c);
            }
        }

        private void AddCharToHead(char c)
        {
            head.Push(c);
            var prev = dict[pos];
            pos++;
            var curr = prev + c;
            if(curr.Length>10)
                curr=curr.Substring(curr.Length-10,10);
            if (dict.ContainsKey(pos)) dict[pos] = curr;
            else dict.Add(pos, curr);
        }

        public int DeleteText(int k)
        {
            int count = 0;
            while(k-->0 && head.Count > 0)
            {
                head.Pop();
                count++;
            }
            pos -= count;
            return count;
        }

        public string CursorLeft(int k)
        {
            while(k-->0 && head.Count > 0)
            {
                tail.Push(head.Pop());
                pos--;
            }
            return dict[pos];
        }

        public string CursorRight(int k)
        {
            while (k-- > 0 && tail.Count > 0)
            {
                AddCharToHead(tail.Pop());
            }
            return dict[pos];
        }
    }
}
