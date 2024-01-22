using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_clickCnt", "_foodNameTxt", "_foodImg", "_levelUpBtn", "_starImgs")]
	public class ES3UserType_KitchenLevelPanel : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_KitchenLevelPanel() : base(typeof(KitchenLevelPanel)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (KitchenLevelPanel)obj;
			
			writer.WritePrivateField("_clickCnt", instance);
			writer.WritePrivateFieldByRef("_foodNameTxt", instance);
			writer.WritePrivateFieldByRef("_foodImg", instance);
			writer.WritePrivateFieldByRef("_levelUpBtn", instance);
			writer.WritePrivateField("_starImgs", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (KitchenLevelPanel)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_clickCnt":
					instance = (KitchenLevelPanel)reader.SetPrivateField("_clickCnt", reader.Read<System.Int32>(), instance);
					break;
					case "_foodNameTxt":
					instance = (KitchenLevelPanel)reader.SetPrivateField("_foodNameTxt", reader.Read<TMPro.TMP_Text>(), instance);
					break;
					case "_foodImg":
					instance = (KitchenLevelPanel)reader.SetPrivateField("_foodImg", reader.Read<UnityEngine.UI.Image>(), instance);
					break;
					case "_levelUpBtn":
					instance = (KitchenLevelPanel)reader.SetPrivateField("_levelUpBtn", reader.Read<UnityEngine.UI.Button>(), instance);
					break;
					case "_starImgs":
					instance = (KitchenLevelPanel)reader.SetPrivateField("_starImgs", reader.Read<UnityEngine.UI.Image[]>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_KitchenLevelPanelArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_KitchenLevelPanelArray() : base(typeof(KitchenLevelPanel[]), ES3UserType_KitchenLevelPanel.Instance)
		{
			Instance = this;
		}
	}
}