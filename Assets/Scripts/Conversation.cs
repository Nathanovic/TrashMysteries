using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Conversation", menuName="Custom/Conversation", order=1)]
public class Conversation : ScriptableObject {

	[TextArea]
	public string[] Texts;

}