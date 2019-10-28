using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConfigCollectionItem : ScriptableObject
{
	public Sprite icon;

	public abstract int GetId();
}

[CreateAssetMenu(fileName = "ConfigItem", menuName = "Config/Item", order = 1)]
public class ConfigItem : ConfigCollectionItem
{
	public EItemId id;
	public UsableItem usable;

	public override int GetId()
	{
		return (int)id;
	}
}

[CreateAssetMenu(fileName = "ConfigItem", menuName = "Config/Power Up", order = 2)]
public class ConfigPowerUp : ConfigCollectionItem
{
	public EPowerUpId id;

	public override int GetId()
	{
		return (int)id;
	}
}
