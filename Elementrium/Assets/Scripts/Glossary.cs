﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using StateHandling;
using TriumObject;
using BackpackObject;
using Initialization;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;


namespace GlossaryObject
{

    public class Glossary: MonoBehaviour
    {

		//private Backpack bp;            // The user's backpack

		//public Glossary(Backpack bp)
		//{
		//	this.bp = bp;
		//}
		public static bool displayOpen = false;
		public Animator GlossaryAnim;
		//[SerializeField]
		public Text names;
		public Text info;
		public Text fact1;
		public Text fact2;
		public Text fact3;


		public void setnames(string name) {
			this.names.text = name.ToString ();
		}
		public void setinfo(string id, string mass, string formula) {
			this.info.text = "Symbol: " + formula + "\n Atomic Number: " + id + "\n Atomic Mass: " + mass;
		}
		public void setfact1(string newfact1) {
			this.fact1.text = newfact1.ToString ();
		}
		public void setfact2(string newfact2) {
			this.fact2.text = newfact2.ToString ();
		}
		public void setfact3(string newfact3) {
			this.fact3.text = newfact3.ToString ();
		}


		public SortedDictionary<int, int> getSortedAtomicGlossary()
		{

            Backpack backpack = Initialize.player;


			SortedDictionary<int, int> sd = new SortedDictionary<int, int>();

			Hashtable table = backpack.getBP();


			foreach (int key in table)
			{
				Trium t = (Trium)table[key];

				if (t.getAtomicNumber() == -1)
				{
					continue;
				}

				sd.Add(t.getAtomicNumber(), key);

			}

			return sd;
		}

		public SortedDictionary<string, int> getSortedCompoundGlossary()
		{


            Backpack backpack = Initialize.player;

			SortedDictionary<string, int> sd = new SortedDictionary<string, int>();

			Hashtable table = backpack.getBP();


			foreach (int key in table)
			{
				Trium t = (Trium)table[key];

				if (t.getAtomicNumber() != -1)
				{
					continue;
				}

				sd.Add(t.getName(), key);

			}

			return sd;

		}

        public ButtonListControl buttonListControl;
        public int onTab;

        public void ExitGlossary()
        {
            SceneManager.LoadScene("MainGameScene");
            Initialize.sh.setCurrentState("MainGameScene", true, true);
        }
        void Start()
        {
            //onTab = 1;
            //buttonListControl.PopulateGlossaryList("Atom");
            //print(Initialize.buddyList.Count);
			PopulateGlossaryAtoms();
        }

        public void GlossaryTabs()
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Tab_Atom")
            {
                if (onTab == 2)
                {
                    buttonListControl.GlossaryPopulateAtomList(getSortedAtomicGlossary());
                    onTab = 1;
                }

            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Tab_Molecule")
            {
                if (onTab == 1)
                {
                    buttonListControl.GlossaryPopulateCompoundList(getSortedCompoundGlossary());
                    onTab = 2;
                }
            }
        }
		public void CloseTab()
		{
			GlossaryAnim.Play("GlossaryInfoRetract");
			displayOpen = false;
		}
		public void PopulateGlossaryAtoms() {
			
			string connectionPath = "URI=file:" + Application.dataPath + "/Elementrium.db";
		
			IDbConnection dbconn;
			dbconn = (IDbConnection)new SqliteConnection(connectionPath);

			dbconn.Open();

			IDbCommand dbcmd = dbconn.CreateCommand();
			//string sqlQuery = "SELECT Trium.ID, Trium.Name, Trium.ElementID, Element.AtomicNumber, Trium.Formula, " +
				//"Trium.Mass, Trium.FactOne, Trium.FactTwo, Trium.FactThree FROM Trium"
				//+ "INNER JOIN Element ON Trium.ElementID=Element.ID";                 
			string sqlQuery = "SELECT ID, Name, Formula, Mass, FactOne, FactTwo, FactThree " + 
				"FROM Trium";
			dbcmd.CommandText = sqlQuery;

			IDataReader reader = dbcmd.ExecuteReader();

			// reader.Read() will return True or False. If true, we will execute what is in the while() loop
			while (reader.Read())
			{
				int id = reader.GetInt32(0);            /* the ID column of the Trium */
				string element = reader.GetString(1);      /* the Name column of the Trium */
				string formula = reader.GetString(2);    /* the Formula column of the Trium */
				decimal mass = reader.GetDecimal (3);
			//	string first = reader.GetString (4);
				//string second = reader.GetString (5);
				//string third = reader.GetString (6);
				buttonListControl.PopulateList (element, id, mass, formula); //,first, second, third
				/*this.names.text = element.ToString();
				this.info.text = "Atomic Number: " +id;*/
			}


			reader.Close();
			reader = null;
			dbcmd.Dispose();
			dbcmd = null;
			dbconn.Close();
			dbconn = null;
		}
		//public static void popupInfo(string objname) {
		//	Debug.Log (objname);
			//this.names.text = element.ToString();
		    //this.info.text = "Atomic Number: " +id;
		//}
    }

}
