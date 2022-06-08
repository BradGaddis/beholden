using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterModel
{
    public class Character : MonoBehaviour
    {
        // all characters should have dialouge
        // all characters should have activities -- in a list?
        protected GameObject player;
        [TextArea(10,5)]
        [SerializeField] protected List<string> dialogues = new List<string>();

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }
}