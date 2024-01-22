using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute()]
	public class ES3UserType_Tile : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Tile() : base(typeof(Model.Tile)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Model.Tile)obj;
			
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Model.Tile)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_TileArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_TileArray() : base(typeof(Model.Tile[]), ES3UserType_Tile.Instance)
		{
			Instance = this;
		}
	}
}