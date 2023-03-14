using System;

[AttributeUsage(AttributeTargets.Parameter)]
public class AssetPathField : Attribute
{
	public AssetType AssetType = AssetType.Invalid;

	public AssetPathField(AssetType assetType)
	{
		AssetType = assetType;
	}
}
