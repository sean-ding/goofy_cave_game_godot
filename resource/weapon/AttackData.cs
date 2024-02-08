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
	public string AttackAnim { get; set; }
	public int Combo { get; set; }
	public AttackData(double damage, DamageTypes damageType, float attackTime, Vector2 displacement, string attackAnim, int combo)
	{
		Damage = damage;
		DamageType = damageType;
		AttackTime = attackTime;
		Displacement = displacement;
		AttackAnim = attackAnim;
		Combo = combo;
	}
}
