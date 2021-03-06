﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: UIElement that responds to VR hands and generates UnityEvents
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class UIElement : MonoBehaviour
	{
		public CustomEvents.UnityEventHand onHandClick;

        protected Hand currentHand;

		//custom
		public const float clickTime = 0.2f; //seconds to distinguish a click
		float clickTimer;

		//-------------------------------------------------
		protected virtual void Awake()
		{
			Button button = GetComponent<Button>();
			if ( button )
			{
				button.onClick.AddListener( OnButtonClick );
			}
		}


		//-------------------------------------------------
		protected virtual void OnHandHoverBegin( Hand hand )
		{
			currentHand = hand;
			InputModule.instance.HoverBegin( gameObject );
			ControllerButtonHints.ShowButtonHint( hand, hand.uiInteractAction);

			//custom
			//clickTimer = clickTime;
		}


        //-------------------------------------------------
        protected virtual void OnHandHoverEnd( Hand hand )
		{
			InputModule.instance.HoverEnd( gameObject );
			ControllerButtonHints.HideButtonHint( hand, hand.uiInteractAction);
			currentHand = null;

			//custom
			//if (clickTimer > 0)
			//         {
			//	InputModule.instance.Submit(gameObject);
			//}
			InputModule.instance.Submit(gameObject);
		}


        //-------------------------------------------------
  //      protected virtual void HandHoverUpdate( Hand hand )
		//{
		//	if ( hand.uiInteractAction != null && hand.uiInteractAction.GetStateDown(hand.handType) )
		//	{
		//		InputModule.instance.Submit( gameObject );
		//		ControllerButtonHints.HideButtonHint( hand, hand.uiInteractAction);
		//	}
		//}

		//custom
		protected virtual void HandHoverUpdate(Hand hand)
        {
			if (clickTimer > 0) clickTimer -= Time.deltaTime;
        }


        //-------------------------------------------------
        protected virtual void OnButtonClick()
		{
			onHandClick.Invoke( currentHand );
		}
	}

#if UNITY_EDITOR
	//-------------------------------------------------------------------------
	[UnityEditor.CustomEditor( typeof( UIElement ) )]
	public class UIElementEditor : UnityEditor.Editor
	{
		//-------------------------------------------------
		// Custom Inspector GUI allows us to click from within the UI
		//-------------------------------------------------
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			UIElement uiElement = (UIElement)target;
			if ( GUILayout.Button( "Click" ) )
			{
				InputModule.instance.Submit( uiElement.gameObject );
			}
		}
	}
#endif
}
