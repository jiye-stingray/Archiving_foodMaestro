using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("direction", "_furnitureData", "x", "y", "width", "length", "origin")]
	public class ES3UserType_Furniture : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Furniture() : base(typeof(Model.Furniture)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Model.Furniture)obj;
			
			writer.WritePrivateField("direction", instance);
			writer.WritePropertyByRef("_furnitureData", instance._furnitureData);
			writer.WriteProperty("x", instance.x, ES3Type_int.Instance);
			writer.WriteProperty("y", instance.y, ES3Type_int.Instance);
			writer.WriteProperty("width", instance.width, ES3Type_int.Instance);
			writer.WriteProperty("length", instance.length, ES3Type_int.Instance);
			writer.WritePrivatePropertyByRef("origin", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Model.Furniture)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "direction":
					instance = (Model.Furniture)reader.SetPrivateField("direction", reader.Read<Model.Direction>(), instance);
					break;
					case "_furnitureData":
						instance._furnitureData = reader.Read<FurnitureData>();
						break;
					case "x":
						instance.x = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "y":
						instance.y = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "width":
						instance.width = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "length":
						instance.length = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "origin":
					instance = (Model.Furniture)reader.SetPrivateProperty("origin", reader.Read<Model.Tile>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_FurnitureArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_FurnitureArray() : base(typeof(Model.Furniture[]), ES3UserType_Furniture.Instance)
		{
			Instance = this;
		}
	}
}