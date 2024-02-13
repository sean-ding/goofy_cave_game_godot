using Godot;
using System;

public partial class AttackData : Node
{
	public enum DamageTypes
	{
		Damage,
		Slice,
		Pierce,
		Bludgeon,
		Force,
		Heat,
		Chill,
		Shock,
		Toxic,
		Acid,
		Mental,
		Entropic,
		Axiomatic,
		Null
	}
	public double Damage { get; set; }
	public DamageTypes DamageType { get; set; }
	public float AttackTime { get; set; }
	public Vector2 Displacement { get; set; }
	public Vector2 Knockback { get; set; }
	public string AttackAnim { get; set; }
	public int Combo { get; set; }
	public float ComboPostTime { get; set; }
	public float ComboPreTime { get; set; }
	public AttackData(double damage, DamageTypes damageType, float attackTime, Vector2 displacement, Vector2 knockback, string attackAnim, int combo, float comboPostTime, float comboPreTime = -1)
	{
		Damage = damage;
		DamageType = damageType;
		AttackTime = attackTime;
		Displacement = displacement;
		Knockback = knockback;
		AttackAnim = attackAnim;
		Combo = combo;
		ComboPostTime = comboPostTime;
		ComboPreTime = comboPreTime < 0 ? comboPostTime : comboPreTime;
	}
}
