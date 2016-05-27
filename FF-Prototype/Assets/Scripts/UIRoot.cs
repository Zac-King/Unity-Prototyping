﻿using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace gui
{
    public enum UIEvents
    {
        HOVER,
        CLICK,
    }
    public class UIRoot : MonoBehaviour, ISubscriber
    {
        [SerializeField]
        private GameObject _combatPanel;

        [SerializeField]
        private GameObject _infoPanel;

        [SerializeField]
        private GameObject _resolvePanel;

        [SerializeField]
        private GameObject _beginPanel;

        [SerializeField]
        private GameObject _phasePanel;

        [SerializeField]
        private UnityEngine.UI.Text _abilityInfo;

        [SerializeField]
        private UnityEngine.UI.Text _partyInfo;
		bool game = false;

        /// <summary>
        /// subscribe to combat and gui events then turn everything off
        /// </summary>
        void Awake()
        {
            Subscribe<string>(MessageLayer.COMBAT, "enter state", OnCombatChange);
            Subscribe<Unit.CombatUnit>(MessageLayer.UNIT, "Unit Change", OnUnitChange);
			if (game)
			{
				_beginPanel.SetActive (false);
				_combatPanel.SetActive (false);
				_resolvePanel.SetActive (false);
			}
            CombatSystem.OnCombatStart += OnCombatStart;
        }

        void OnCombatStart()
        {
            _phasePanel.SetActive(true);
            _combatPanel.SetActive(true);
        }
        /// <summary>
        /// when the user hovers over a different ability 
        /// </summary>
        void OnAbilityChange(string arg)
        {
            _abilityInfo.text = arg;
        }


        /// <summary>
        /// when the unit changes update the info text
        /// </summary>
        /// <param name="arg"></param>
        void OnUnitChange(Unit.CombatUnit arg)
        {
            //Debug.Log("ui unit change");
            _partyInfo.text =
                "Name: " + arg.name +
                "\nHP: " + arg.health +
                "\nAttack: " + arg.attack +
                "\nDefense: " + arg.defense;
        }
        /// <summary>
        /// event listener for combat
        /// </summary>
        /// <param name="state"></param>
        void OnCombatChange(string state)
        {
            // Debug.Log("ui change from combat with " + state);
            _beginPanel.SetActive(false);
            //_combatPanel.SetActive(false);
            _resolvePanel.SetActive(false);
            //  _phasePanel.SetActive(false);

            state = state.ToLower();
            switch (state)
            {
                case "init":
                    break;//setup gui
                case "start":

                    break;
                case "active":
                    break;
                case "endturn":
                    break;
                case "resolve":
                    _resolvePanel.SetActive(true);
                    break;

            }
        } 

        public void Subscribe(MessageLayer t, string e, Callback c)
        {
            EventSystem.Subscribe(t, e, c, this);
        }

        public void Subscribe<T>(MessageLayer t, string e, Callback<T> c)
        {
            EventSystem.Subscribe(t, e, c, this);
        }

    }
}
