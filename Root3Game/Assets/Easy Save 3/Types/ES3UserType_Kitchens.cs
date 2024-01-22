using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_savepath", "_kitchenTrans", "_foodData", "_level", "_panel", "_levelUpGold", "_kitchenLevelUpSound")]
	public class ES3UserType_Kitchens : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Kitchens() : base(typeof(Kitchens)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Kitchens)obj;
			
			writer.WritePrivateField("_savepath", instance);
			writer.WritePrivateField("_kitchenTrans", instance);
			writer.WritePropertyByRef("_foodData", instance._foodData);
			writer.WritePrivateField("_level", instance);
			writer.WritePropertyByRef("_panel", instance._panel);
			writer.WriteProperty("_levelUpGold", instance._levelUpGold, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Single[,])));
			writer.WritePrivateFieldByRef("_kitchenLevelUpSound", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Kitchens)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_savepath":
					instance = (Kitchens)reader.SetPrivateField("_savepath", reader.Read<System.String>(), instance);
					break;
					case "_kitchenTrans":
					instance = (Kitchens)reader.SetPrivateField("_kitchenTrans", reader.Read<UnityEngine.Transform[]>(), instance);
					break;
					case "_foodData":
						instance._foodData = reader.Read<FoodData>();
						break;
					case "_level":
					instance = (Kitchens)reader.SetPrivateField("_level", reader.Read<System.Int32>(), instance);
					break;
					case "_panel":
						instance._panel = reader.Read<KitchenLevelPanel>(ES3UserType_KitchenLevelPanel.Instance);
						break;
					case "_levelUpGold":
						instance._levelUpGold = reader.Read<System.Single[,]>();
						break;
					case "_kitchenLevelUpSound":
					instance = (Kitchens)reader.SetPrivateField("_kitchenLevelUpSound", reader.Read<UnityEngine.AudioClip>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_KitchensArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_KitchensArray() : base(typeof(Kitchens[]), ES3UserType_Kitchens.Instance)
		{
			Instance = this;
		}
	}
}