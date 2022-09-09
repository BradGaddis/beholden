using System.Collections;
using System.Collections.Generic;

namespace Beholden.Dialogue
{
    [System.Serializable]
    public class DialogueNode 
    {
        public string uniqueID;
        public string text;
        public string[] children;
    }
}
